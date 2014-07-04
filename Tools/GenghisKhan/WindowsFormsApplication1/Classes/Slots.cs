using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public enum SlotType
    {
        SlotType_General,
        SlotType_Soldier,
        SlotType_Armor,
        SlotType_Item,
        SlotType_SoldierMaterial,
        SlotType_ArmorMaterial,
        SlotType_ALL
    }

    public enum SoldierType
    {
        ALL,
        Footman,
        Chivalry,
        Archer,
        None
    }

    public enum FormationPosition
    {
        OFF = 0,
        A1 = 1, A2, A3, A4, A5,
        B1, B2, B3, B4, B5,
        C1, C2, C3, C4, C5,
        D1, D2, D3, D4, D5,
    }

    public class Slot
    {
        public Slot() { }
        //public Slot(SlotType type,int index,int id)
        //{
        //    Type = type;
        //    Index = index;
        //    ConfigID = id;

        //    Lv = 1;
        //    Rank = 1;
        //}

        public SlotType Type { get; set; }

        public int Index { get; set; }

        public int ConfigID { get; set; }

        public int Lv { get; set; }

        public int Rank { get; set; }

        // 额外附加数据
        public int ExtraData { get; set; }
    }

    public class SlotGeneral : Slot
    {
        private General generalConfig;

        public SlotGeneral(SlotType type, int index, int id)
        {
            Type = type;
            Index = index;
            ConfigID = id;

            Lv = 1;
            Rank = 1;

            // 是否上阵
            ExtraData = 0;

            generalConfig = ConfigDataMgr.Instance._MapGeneral[ConfigID];
        }

        public int HP
        {
            get
            {
                return (int)((generalConfig.HP + generalConfig.HPGorwth * Lv) * Rank * generalConfig.HPRankRate);
            }
        }

        public int ATK { 
            get
            {
                return (int)((generalConfig.AttackPower + generalConfig.ATKGrowth * Lv) * Rank * generalConfig.ATKRankRate);
            }
            
        }

        public int DEF { 
            get {
                return (int)((generalConfig.DefensePower + generalConfig.DEFGrowth * Lv) * Rank * generalConfig.ATKRankRate);
            } 
        }

        public double DodgeRate
        {
            get
            {
                return generalConfig.DodgeRate/100.0;
            }
        }

        public double HitRate
        {
            get
            {
                return generalConfig.HitRate / 100.0;
            }
        }

        public double CriticalRate
        {
            get
            {
                return generalConfig.CriticalRate / 100.0;
            }
        }

        public int SoldierIndex
        {
            get
            {
                foreach (int i in PlayerDataMgr.Instance.GetPlayerBag(SlotType.SlotType_Soldier).Keys)
                {
                    if (PlayerDataMgr.Instance.GetPlayerBag(SlotType.SlotType_Soldier)[i].ExtraData == Index)
                    {
                        return i;
                    }
                }

                return 0;
            }
        }

        public FormationPosition FormationPosition { get; set; }
    }

    public class SlotSoldier : Slot
    {
        private Soldier soldierConfig;
        
        public SlotSoldier(SlotType type, int index, int id)
        {
            Type = type;
            Index = index;
            ConfigID = id;

            Lv = 1;
            Rank = 1;
            AddedCount = 0;

            // 绑定的武将
            ExtraData = 0;

            soldierConfig = ConfigDataMgr.Instance._MapSoldier[ConfigID];
        }

        public int HP
        {
            get
            {
                return (int)((soldierConfig.HP + soldierConfig.HPGorwth * Lv) * Rank * soldierConfig.HPRankRate);
            }
            
        }

        public int ATK { 
            get
            {
                return (int)((soldierConfig.AttackPower + soldierConfig.ATKGrowth * Lv) * Rank * soldierConfig.ATKRankRate);
            }
            
        }

        public int DEF { 
            get {
                return (int)((soldierConfig.DefensePower + soldierConfig.DEFGrowth * Lv) * Rank * soldierConfig.ATKRankRate);
            } 
        }

        // 部队额外升级过的数量
        public int AddedCount { get; set; }

        public int Count
        {
            get
            {
                return soldierConfig.InitialCount + AddedCount;
            }
        }
    }
}
