using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public static class SoldierBatch
    {
        /// <summary>
        /// 刷新士兵三维
        /// </summary>
        public static void RefreshRandomSoldierMainAttribute()
        {
            foreach (Soldier g in DBConfigMgr.Instance.MapSoldier.Values)
            {
                Dictionary<int, int> subSoldierTypeToRandomType = new Dictionary<int, int>()
                {
                    {1,5},{2,2},{3,3},{4,2},{5,3},{6,4},{7,2}
                };

                int rIndex = subSoldierTypeToRandomType[g.SubSoldierType];

                double starRate = Formula.CONST_STAR_GAP_PARAMS[g.Star];
                double soldierEqualGeneralPercent = 0.4;

                double hpPercent = 1.0 * BattleTest.BasicRandomTypeTable.ElementAt(rIndex).Value[(int)ATTRIBUTEINDEX.HP] / 100.0;
                g.HP = (int)(Batch.BASIC_ATTRIBUTE.HP * starRate * soldierEqualGeneralPercent * hpPercent);
                g.HPGrowth = (int)(Batch.BASIC_ATTRIBUTE.HP_GROWTH * starRate * soldierEqualGeneralPercent * hpPercent);

                double atkPercent = 1.0 * BattleTest.BasicRandomTypeTable.ElementAt(rIndex).Value[(int)ATTRIBUTEINDEX.ATK] / 100.0;
                g.AttackPower = (int)(Batch.BASIC_ATTRIBUTE.ATK * starRate * soldierEqualGeneralPercent * atkPercent);
                g.ATKGrowth = (int)(Batch.BASIC_ATTRIBUTE.ATK_GROWTH * starRate * soldierEqualGeneralPercent * atkPercent);

                double defPercent = 1.0 * BattleTest.BasicRandomTypeTable.ElementAt(rIndex).Value[(int)ATTRIBUTEINDEX.DEF] / 100.0;
                g.DefensePower = (int)(Batch.BASIC_ATTRIBUTE.DEF * starRate * soldierEqualGeneralPercent * defPercent);
                g.DEFGrowth = (int)(Batch.BASIC_ATTRIBUTE.DEF_GROWTH * starRate * soldierEqualGeneralPercent * defPercent);

                g.MoveSpeed = Batch.SOLDIER_BASIC_ATTRIBUTE[g.SoldierType].MOVE_SPEED;
                g.AttackSpeed = Batch.SOLDIER_BASIC_ATTRIBUTE[g.SoldierType].ATTACK_SPEED;
                g.AttackRange = Batch.SOLDIER_BASIC_ATTRIBUTE[g.SoldierType].ATTACK_RANGE;
            }
        }


        /// <summary>
        /// 刷士兵升级所需的道具
        /// </summary>
        public static void RefreshSoldierCountItems()
        {
            int[] item13 = { 0, 1016, 1016, 1016, 1017, 1017, 1016, 1016 };
            int[] item46 = { 0, 1020, 1022, 1021, 1023, 1024, 1018, 1019 };
            int[] item710 = { 0, 1025, 1027, 1026, 1030, 1031, 1028, 1029 };

            foreach (Soldier s in DBConfigMgr.Instance.MapSoldier.Values)
            {
                s.AddCount1Costs = String.Format("2,{0},1;2,3,{1}"
                    , item13[s.SubSoldierType], Formula.UpgradeSoldierCountCostMoney(1));

                s.AddCount2Costs = String.Format("2,{0},5;2,3,{1}"
                    , item13[s.SubSoldierType], Formula.UpgradeSoldierCountCostMoney(2));

                s.AddCount3Costs = String.Format("2,{0},10;2,3,{1}"
                    , item13[s.SubSoldierType], Formula.UpgradeSoldierCountCostMoney(3));

                s.AddCount4Costs = String.Format("2,{0},1;2,3,{1}"
                    , item46[s.SubSoldierType], Formula.UpgradeSoldierCountCostMoney(4));

                s.AddCount5Costs = String.Format("2,{0},5;2,3,{1}"
                    , item46[s.SubSoldierType], Formula.UpgradeSoldierCountCostMoney(5));

                s.AddCount6Costs = String.Format("2,{0},10;2,3,{1}"
                    , item46[s.SubSoldierType], Formula.UpgradeSoldierCountCostMoney(6));

                s.AddCount7Costs = String.Format("2,{0},1;2,3,{1}"
                    , item710[s.SubSoldierType], Formula.UpgradeSoldierCountCostMoney(7));

                s.AddCount8Costs = String.Format("2,{0},5;2,3,{1}"
                    , item710[s.SubSoldierType], Formula.UpgradeSoldierCountCostMoney(8));

                s.AddCount9Costs = String.Format("2,{0},10;2,3,{1}"
                    , item710[s.SubSoldierType], Formula.UpgradeSoldierCountCostMoney(9));

                s.AddCount10Costs = String.Format("2,{0},20;2,3,{1}"
                    , item710[s.SubSoldierType], Formula.UpgradeSoldierCountCostMoney(10));

            }
        }

        /// <summary>
        /// 刷士兵的武器特效
        /// </summary>
        public static void RefreshSoldierWeapon()
        {
            foreach (Soldier s in DBConfigMgr.Instance.MapSoldier.Values)
            {
                if (s.SubSoldierType == 4) s.AttackType = 8003;
                else if (s.SubSoldierType == 5) s.AttackType = 8004;
                else s.AttackType = 1;
            }
        }
    }
}
