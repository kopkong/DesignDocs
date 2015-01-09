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
        OFF = -1,
        A1 = 0, A2, A3, A4,
        B1, B2, B3, B4, 
        C1, C2, C3, C4,
        D1, D2, D3, D4, 
        E1, E2, E3, E4
    }

    public class Slot
    {
        public Slot() { }

        public SlotType Type { get; set; }

        public int Index { get; set; }

        public int ConfigID { get; set; }

        public int Lv { get; set; }

        public int Rank { get; set; }

        // 额外附加数据
        public int ExtraData { get; set; }


        // 额外附加数据
        public int ExtraData2 { get; set; }
    }

    public class SlotGeneral : Slot
    {
        public General GeneralConfig { get; set; }

        public SlotGeneral(SlotType type, int index, int id)
        {
            Type = type;
            Index = index;
            ConfigID = id;

            Lv = 1;
            Rank = 0;

            // 是否上阵
            ExtraData = 0;

            // 经验值
            ExtraData2 = 0;

            //GeneralConfig = DBConfigMgr.Instance.MapGeneral[ConfigID];
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
        public Soldier SoldierConfig { get; set; }
        
        public SlotSoldier(SlotType type, int index, int id)
        {
            Type = type;
            Index = index;
            ConfigID = id;

            Lv = 1;
            Rank = 0;
            AddedCount = 0;

            // 绑定的武将
            ExtraData = 0;

            SoldierConfig = DBConfigMgr.Instance.MapSoldier[ConfigID];
        }

        // 部队额外升级过的数量
        public int AddedCount { get; set; }

        public int Count
        {
            get
            {
                return SoldierConfig.InitialCount + AddedCount;
            }
        }
    }
}
