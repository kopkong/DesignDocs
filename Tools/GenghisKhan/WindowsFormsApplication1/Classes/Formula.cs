using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public static class Formula
    {
        // 计算武将战斗力
        public static int ComputeBattlePowerPoint(int slotIndex)
        {
            double point = 0;
            
            SlotGeneral g = (SlotGeneral)PlayerDataMgr.Instance.GetPlayerBag(SlotType.SlotType_General)[slotIndex];

            SlotSoldier s = (SlotSoldier)PlayerDataMgr.Instance.GetPlayerBag(SlotType.SlotType_Soldier)[g.SoldierIndex];

            point = g.HP * 0.038 + g.ATK * 0.06 + g.DEF * 0.17 + g.CriticalRate * 1.9 + g.DodgeRate * 1.3 + g.HitRate * 1.4;

            // 加上部队
            point += (s.HP * 0.024 + s.ATK * 0.09 + s.DEF * 0.13) * s.Count;

            // 加上装备

            return (int)point;
        }
    }
}
