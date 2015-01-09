using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class LevelRecord
    {
        public LevelRecord(int id)
        {
            LevelID = id;
            Passed = false;
            Times = 0;
            HighestStar = 0;

            if (id == 1)
                Locked = false;
            else
                Locked = true;
        }

        public int LevelID{get;set;}

        public bool Passed { get; set; }

        public int Times{get;set;}

        public int HighestStar { get; set; }

        public bool Locked { get; set; }
    }

    public class PlayerDataMgr
    {
        private static PlayerDataMgr instance;
        private static object syncRoot = new Object();
        private static PlayerInfo player = new PlayerInfo();
        private Dictionary<SlotType,Dictionary<int, Slot>> playerBags = new Dictionary<SlotType,Dictionary<int, Slot>>();
        private List<int> playerFinishedChapters = new List<int>();
        private Dictionary<int, LevelRecord> playerLevelRecords = new Dictionary<int, LevelRecord>();

        private PlayerDataMgr() { }

        public static PlayerDataMgr Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if(instance == null)
                            instance = new PlayerDataMgr();
                    }
                }

                return instance;
            }
        }

        public void Init()
        {
            InitPlayerBag();
            InitPlayerLevels();
        }

        public PlayerInfo GetPlayer()
        {
            return player;
        }

        public Dictionary<int, LevelRecord> GetPlayerLevelRecords()
        {
            return playerLevelRecords;
        }

        public Dictionary<int, Slot> GetPlayerBag(SlotType type)
        {
            return playerBags[type];
        }

        #region Chapter and Level

        private void InitPlayerLevels()
        {
            foreach(int i in DBConfigMgr.Instance.MapLevel.Keys)
            {
                playerLevelRecords.Add(i, new LevelRecord(i));
            }
        }

        public bool IsThisChapterPlayerFinished(int chapterID)
        {
            return playerFinishedChapters.Exists(x => x == chapterID);
        }


        public void PlayerFinishedChapter(int chapterID)
        {
            if(!IsThisChapterPlayerFinished(chapterID))
                playerFinishedChapters.Add(chapterID);
        }

        public void PlayerFinishedLevel(int levelID, int star)
        {
            if (playerLevelRecords.ContainsKey(levelID))
            {
                playerLevelRecords[levelID].Passed = true;
                playerLevelRecords[levelID].Times += 1;
                playerLevelRecords[DBConfigMgr.Instance.MapLevel[levelID].UnlockNextLevelID].Locked = false;

                if (playerLevelRecords[levelID].HighestStar < star)
                    playerLevelRecords[levelID].HighestStar = star;
            }
            else
            {
                LevelRecord record = new LevelRecord(levelID);
                record.LevelID = levelID;
                record.Passed = true;
                record.Times = 1;
                record.HighestStar = star;
            }
        }

        public int GetPlayerLevelTodayTimes(int id)
        {
            if (playerLevelRecords.ContainsKey(id))
                return playerLevelRecords[id].Times;
            else
                return 0;
        }
        #endregion

        #region 背包和仓库
        private void AddInitialGeneralForPlayer()
        {
            AddGeneral(1);
            AddGeneral(44);
            AddGeneral(45);
            //AddGeneral(46);
            //AddGeneral(47);
            //AddGeneral(65);

            // 一开始都要上阵
            MakeGeneralOnBattle(1, FormationPosition.C1);
            MakeGeneralOnBattle(2, FormationPosition.B1);
            MakeGeneralOnBattle(3, FormationPosition.D1);
            //MakeGeneralOnBattle(4, FormationPosition.C1);
            //MakeGeneralOnBattle(5, FormationPosition.C2);
            //MakeGeneralOnBattle(6, FormationPosition.C3);
        }

        private void InitPlayerBag()
        {
            playerBags.Add(SlotType.SlotType_General, new Dictionary<int, Slot>());
            playerBags.Add(SlotType.SlotType_Soldier, new Dictionary<int, Slot>());
            playerBags.Add(SlotType.SlotType_Armor, new Dictionary<int, Slot>());
            playerBags.Add(SlotType.SlotType_Item, new Dictionary<int, Slot>());
            playerBags.Add(SlotType.SlotType_SoldierMaterial, new Dictionary<int, Slot>());
            playerBags.Add(SlotType.SlotType_ArmorMaterial, new Dictionary<int, Slot>());

            AddInitialGeneralForPlayer();
        }

        public void AddGeneral(int id)
        {
            // Find index
            int index = 1;
            if (playerBags[SlotType.SlotType_General].Count > 0)
            {
                index = playerBags[SlotType.SlotType_General].Keys.Max() + 1;
            }

            SlotGeneral s = new SlotGeneral(SlotType.SlotType_General, index, id);
            s.FormationPosition = 0;
            playerBags[SlotType.SlotType_General].Add(s.Index, s);
            
            // Add default soldier for general
            //AddSoldier(DBConfigMgr.Instance.MapGeneral[id].InitialSoldier, s.Index);
        }

        public void AddSoldier(int id,int generalIndex)
        {
            int index = 1;
            if (playerBags[SlotType.SlotType_Soldier].Count > 0)
            {
                index = playerBags[SlotType.SlotType_Soldier].Keys.Max() + 1;
            }

            SlotSoldier s = new SlotSoldier(SlotType.SlotType_Soldier, index, id);
            s.ExtraData = generalIndex;

            playerBags[SlotType.SlotType_Soldier].Add(s.Index, s);
        }

        #endregion

        #region 阵型
        private void MakeGeneralOnBattle(int slotIndex, FormationPosition formationPosition)
        {
            SlotGeneral slot = (SlotGeneral)playerBags[SlotType.SlotType_General][slotIndex];
            slot.FormationPosition = formationPosition;
            slot.ExtraData = (int)formationPosition;
        }

        private void MakeGeneralOffBattle(int slotIndex)
        {
            SlotGeneral slot = (SlotGeneral)playerBags[SlotType.SlotType_General][slotIndex];
            slot.FormationPosition = FormationPosition.OFF;
            slot.ExtraData = 0;
        }

        private bool IsFormationPositionOccupied(FormationPosition formationPosition)
        {
            foreach (int i in playerBags[SlotType.SlotType_General].Keys)
            {
                if (playerBags[SlotType.SlotType_General][i].ExtraData != 0)
                    return true;
            }

            return false;
        }

        public void SavePlayerFormation(Dictionary<FormationPosition,int> formationDict)
        {
            foreach (FormationPosition position in formationDict.Keys)
            {
                MakeGeneralOnBattle(formationDict[position],position);
            }
        }

        public Dictionary<int,SlotGeneral> GetOnBattleGenerals()
        {
            Dictionary<int, SlotGeneral> onBattleGenerals = new Dictionary<int,SlotGeneral>();

            foreach (KeyValuePair<int, Slot> kv in playerBags[SlotType.SlotType_General])
            {
                if (kv.Value.ExtraData >= 0)
                {
                    onBattleGenerals.Add(kv.Key, (SlotGeneral)kv.Value);
                }
            }

            return onBattleGenerals;
        }

        #endregion
    }
}
