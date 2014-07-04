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
        }

        public int LevelID{get;set;}

        public bool Passed { get; set; }

        public int Times{get;set;}

        public int HighestStar { get; set; }
    }

    public class PlayerDataMgr
    {
        private static PlayerDataMgr instance;
        private static object syncRoot = new Object();
        private static CKPlayer player = new CKPlayer();
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

        public CKPlayer GetPlayer()
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
            foreach(int i in ConfigDataMgr.Instance._MapLevel.Keys)
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
            AddGeneral(2);
            AddGeneral(3);
            AddGeneral(4);
            AddGeneral(5);
            AddGeneral(6);

            // 一开始都要上阵
            MakeGeneralOnBattle(1, FormationPosition.A2);
            MakeGeneralOnBattle(2, FormationPosition.A3);
            MakeGeneralOnBattle(3, FormationPosition.A4);
            MakeGeneralOnBattle(4, FormationPosition.B2);
            MakeGeneralOnBattle(5, FormationPosition.B3);
            MakeGeneralOnBattle(6, FormationPosition.B4);
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
            AddSoldier(ConfigDataMgr.Instance._MapGeneral[id].InitialSoldier, s.Index);
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
        #endregion
    }
}
