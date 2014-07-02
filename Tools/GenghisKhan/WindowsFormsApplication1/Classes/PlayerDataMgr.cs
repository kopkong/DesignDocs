using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class PlayerDataMgr
    {
        private static PlayerDataMgr instance;
        private static object syncRoot = new Object();
        private static CKPlayer player = new CKPlayer();
        private Dictionary<SlotType,Dictionary<int, Slot>> playerBags = new Dictionary<SlotType,Dictionary<int, Slot>>();

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
        }

        public CKPlayer GetPlayer()
        {
            return player;
        }

        public Dictionary<int, Slot> GetPlayerBag(SlotType type)
        {
            return playerBags[type];
        }

        private void AddInitialGeneralForPlayer()
        {
            AddGeneral(1);
            AddGeneral(2);
            AddGeneral(3);
            AddGeneral(4);
            AddGeneral(5);
            AddGeneral(6);
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
            s.BindGeneralSlot = generalIndex;

            playerBags[SlotType.SlotType_Soldier].Add(s.Index, s);
        }
    }
}
