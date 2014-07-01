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

    public class Slot
    {
        public Slot(SlotType type,int index,int id)
        {
            Type = type;
            Index = index;
            ConfigID = id;

            Lv = 1;
            Rank = 1;
        }

        public SlotType Type { get; set; }

        public int Index { get; set; }

        public int ConfigID { get; set; }

        public int Lv { get; set; }

        public int Rank { get; set; }

        // 装备或者部队绑定在哪个英雄身上,其他不需要这个数据
        public int BindGeneralSlot { get; set; }
    }
}
