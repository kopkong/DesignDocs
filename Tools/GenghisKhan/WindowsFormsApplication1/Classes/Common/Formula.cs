using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public struct MainAttribute
    {
        public int HP;
        public int ATK;
        public int DEF;
        public int HP_GROWTH;
        public int ATK_GROWTH;
        public int DEF_GROWTH;
        public int ATTACK_SPEED;
        public int ATTACK_RANGE;
        public int MOVE_SPEED;
        
        public int SUM
        {
            get
            {
                return (int)HP / 2 + ATK + DEF;
            }
        }
    }

    public static class Formula
    {
        public static double[] CONST_STAR_GAP_PARAMS = { 0, 0.57, 0.69, 0.83, 1, 1.2 };
        public static double[] CONST_CLASS_GAP_PARAMS = { 1, 1.15, 1.32, 1.52, 1.75, 2.01 };
        public static double[] CONST_SKILL_STAR_GAP_PARAMS = { 0, 0, 0.83, 0.83, 1, 1 };
        const int CONST_BASE_GENERAL_VALUE = 30;
        const int CONST_BASE_SOLDIER_VALUE = 5;
        const int CONST_BASE_SKILL_VALUE = 8;
        const int CONST_BASE_ARMOR_VALUE = 6;

        public static int GetConfigID(int type, int id)
        {
            return type * 16777216 + id;
        }

        /// <summary>
        /// 上阵人数公式
        /// </summary>
        /// <param name="playerLevel"></param>
        /// <returns></returns>
        public static int GetOnBattleSquads(int playerLevel)
        {
            for (int i = 1; i < DBConfigMgr.Instance.MapMaxSquads.Keys.Max(); i++)
            {
                if (playerLevel >= DBConfigMgr.Instance.MapMaxSquads[i].Level &&
                    playerLevel < DBConfigMgr.Instance.MapMaxSquads[i + 1].Level)
                {
                    return DBConfigMgr.Instance.MapMaxSquads[i].MaxSquads;
                }
            }

            return 20;
        }

        /// <summary>
        /// 获取对应关卡的参考等级
        /// </summary>
        /// <param name="levelID"></param>
        /// <returns></returns>
        public static int GetReferenceLevel(int levelID)
        {
            int chapter = DBConfigMgr.Instance.MapLevel[levelID].ChapterID;
            //bool isElite = DBConfigMgr.Instance.MapLevel[levelID].IsEliteLevel == 1;

            int referenceLevel = 1;
            if(chapter> 1)
                referenceLevel = (chapter - 1) * 5;

            //if (isElite)
            //    referenceLevel += 10;

            return referenceLevel;
            
        }

        /// <summary>
        /// 基础属性参照
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static MainAttribute BasicMainAttribute()
        {
            MainAttribute attr;

            attr.HP = 1000;
            attr.ATK = 100;
            attr.DEF = 50;

            attr.HP_GROWTH = 120;
            attr.ATK_GROWTH = 20;
            attr.DEF_GROWTH = 10;

            attr.ATTACK_SPEED = 0;
            attr.ATTACK_RANGE = 0;
            attr.MOVE_SPEED = 0;

            return attr;
        }

        public static MainAttribute BasicMainAttributeWithSoldierType(int type)
        {
            int[] moveSpeed = { 0, 40, 60, 40 };
            int[] attackSpeed = { 0, 45, 40, 35 };
            int[] attackRange = { 0, 30, 40, 260 };

            MainAttribute main = BasicMainAttribute();

            main.ATTACK_RANGE = attackRange[type];
            main.ATTACK_SPEED = attackSpeed[type];
            main.MOVE_SPEED = moveSpeed[type];

            return main;
        }

        /// <summary>
        /// 耐力转换HP
        /// </summary>
        /// <param name="sta"></param>
        /// <returns></returns>
        public static int HPFromSta(float sta)
        {
            return (int)(sta * 60);
        }

        /// <summary>
        /// 力量转换攻击力
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ATKFromStr(float str)
        {
            return (int)(str * 8);
        }

        /// <summary>
        /// 敏捷转换防御力
        /// </summary>
        /// <param name="agi"></param>
        /// <returns></returns>
        public static int DEFFromAgi(float agi)
        {
            return (int)(agi * 8);
        }

        /// <summary>
        /// 战斗计算公式
        /// </summary>
        /// <param name="gInfo"></param>
        /// <param name="sInfo"></param>
        /// <returns></returns>
        public static int BattlePowerPoint(GeneralInfo gInfo, SoldierInfo sInfo)
        {
            double point = 0;

            // 武将自身的
            point += CONST_STAR_GAP_PARAMS[gInfo.GeneralConfig.Star] * CONST_CLASS_GAP_PARAMS[gInfo.Rank] *
                (gInfo.Level + 5) * CONST_BASE_GENERAL_VALUE;

            // 加上部队
            point += CONST_STAR_GAP_PARAMS[sInfo.SoldierConfig.Star] * (sInfo.Level + 5) *
                sInfo.SoldierCount * CONST_BASE_SOLDIER_VALUE;

            // 加上技能1
            point += gInfo.Skill1  * CONST_SKILL_STAR_GAP_PARAMS[gInfo.GeneralConfig.Star] * CONST_BASE_SKILL_VALUE;
            
            // 加上技能2
            if (gInfo.Skill2 > 0) point += gInfo.Skill2 * CONST_SKILL_STAR_GAP_PARAMS[gInfo.GeneralConfig.Star] * CONST_BASE_SKILL_VALUE;

            // 加上装备
            return (int)point;
        }

        /// <summary>
        /// 获取战斗力参考
        /// </summary>
        public static int GetRefBattlePoint(int referLevel)
        {
            int star = 1;
            if (referLevel >= 18) star = 2;
            if (referLevel >= 30) star = 3;
            if (referLevel >= 40) star = 4;
            if (referLevel >= 55) star = 5;

            GeneralInfo generalInfo = EntityInfoFactory.GetBasicStarGeneralInfo(star, referLevel);
            SoldierInfo soldierInfo = EntityInfoFactory.GetBasicStarSoldierInfo(star, referLevel);

            int referBattlePoint = GetOnBattleSquads(referLevel) * BattlePowerPoint(generalInfo, soldierInfo);

            return referBattlePoint;
        }

        /// <summary>
        /// 计算每个关卡结束之后武将可以升到多少级
        /// </summary>
        public static void ComputeGeneralLevelAterEveryLevel()
        {
            int levelCount = DBConfigMgr.Instance.MapLevel.Count();
            Dictionary<int, int> dict = new Dictionary<int, int>();
            Dictionary<int, int> dict2 = new Dictionary<int, int>();

            int expSum = 0;
            int playerEXPSum = 0;
            int curLevel = 1;
            int curPlayerLevel = 1;

            for (int i = 1; i < levelCount; i++)
            {
                expSum += DBConfigMgr.Instance.MapLevel[i].GeneralExpReward;
                playerEXPSum += 10;
                for (int j = curLevel; j < 100; j++)
                {
                    if (expSum >= DBConfigMgr.Instance.MapExperience[j].GeneralStart &&
                        expSum <= DBConfigMgr.Instance.MapExperience[j].GeneralEnd)
                    {
                        curLevel = j;
                        dict.Add(i, j);
                        break;
                    }
                }

                for (int k = curPlayerLevel; k < 100; k++)
                {
                    if (playerEXPSum >= DBConfigMgr.Instance.MapExperience[k].PlayerStart &&
                        playerEXPSum <= DBConfigMgr.Instance.MapExperience[k].PlayerEnd)
                    {
                        curPlayerLevel = k;
                        dict2.Add(i, k);
                        break;
                    }
                }
            }

            foreach (int id in dict.Keys)
            {
                Console.WriteLine(String.Format("关卡{0},武将等级{1},玩家等级{2}",id,dict[id],dict2[id]));
            }
        }

        /// <summary>
        /// 基础金币消耗公式, 凡是消耗金钱皆通过基础消耗乘以系数可以算出
        /// </summary>
        /// <param name="type">类型 1：装备， 2：技能，3：部队,4 天使技能</param>
        /// <param name="level"></param>
        /// <returns></returns>
        private static int BasicUpgradeCostMoney(int type, int level)
        {
            double basic = Math.Pow(level - 1, 1.5) ;
            double[] parameter = { 0, 5.0, 10.0, 15.0, 150 };

            return (int)Math.Round(basic * parameter[type]) + 5 ;
        }

        /// <summary>
        /// 装备、技能、部队升级金币消耗公式
        /// </summary>
        /// <param name="type"></param>
        /// <param name="level"></param>
        public static int UpgradeCostMoney(int type, int level)
        {
            int money = BasicUpgradeCostMoney(type,level);
            return money;
        }

        /// <summary>
        /// 计算天使技能升级消耗金钱公式
        /// </summary>
        /// <param name="index"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int UpgradeAngelSkillCostMoney(int index, int level)
        {
            //天使技能开放等级 0,10,20,30
            int[] levelOffset = {0,0,10,20,30};

            int money = BasicUpgradeCostMoney(4, level + levelOffset[index]);

            return money;
        }

        /// <summary>
        /// 部队扩充人数金币消耗公式
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int UpgradeSoldierCountCostMoney(int count)
        {
            return count * 1000;
        }

        /// <summary>
        /// 获取守护神升级的英魂消耗
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static int UpgradeAngelCostHeroSoul(int level)
        {
            if (level <= 2)
            {
                return 0;
            }
            if (level <= 10)
            {
                //return (level - 1) * 5;
                return 5;
            }
            else if (level <= 20)
            {
                //return UpgradeAngelCostHeroSoul(10) + (level - 10) * 10;
                return 10;
            }
            else if (level <= 30)
            {
                //return UpgradeAngelCostHeroSoul(20) + (level - 20) * 15;
                return 15;
            }
            else if (level <= 40)
            {
                //return UpgradeAngelCostHeroSoul(30) + (level - 30) * 20;
                return 20;
            }
            else
            {
                //return UpgradeAngelCostHeroSoul(40) + (level - 40) * 25;
                return 25;
            }
        }

        /// <summary>
        /// 竞技场奖励
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> ArenaHonerReward()
        {
            int moneyConfigID = GetConfigID(2,3);
            int honorConfigID = GetConfigID(2,5);
            int diamondConfigID = GetConfigID(2,2);
            
            Dictionary<int, string> dict = new Dictionary<int, string>()
            {
                // Money, Honor, Diamond
                { 1, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,85000,honorConfigID,1200,diamondConfigID,650) },
                { 2, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,78000,honorConfigID,1100,diamondConfigID,600) },
                { 3, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,73000,honorConfigID,1050,diamondConfigID,550) },
                { 4, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,68000,honorConfigID,1000,diamondConfigID,500) },
                { 5, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,63000,honorConfigID,950,diamondConfigID,450) },
                { 6, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,58000,honorConfigID,900,diamondConfigID,420) },
                { 7, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,54000,honorConfigID,850,diamondConfigID,390) },
                { 8, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,50000,honorConfigID,800,diamondConfigID,360) },
                { 9, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,46000,honorConfigID,750,diamondConfigID,330) },
                { 10, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,42000,honorConfigID,700,diamondConfigID,300) },
                { 20, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,38000,honorConfigID,660,diamondConfigID,270) },
                { 30, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,35000,honorConfigID,620,diamondConfigID,250) },
                { 40, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,32000,honorConfigID,580,diamondConfigID,230) },
                { 50, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,29000,honorConfigID,540,diamondConfigID,210) },
                { 60, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,26000,honorConfigID,500,diamondConfigID,190) },
                { 70, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,23000,honorConfigID,475,diamondConfigID,170) },
                { 80, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,20000,honorConfigID,450,diamondConfigID,150) },
                { 100, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,17000,honorConfigID,425,diamondConfigID,130) },
                { 200, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,16000,honorConfigID,400,diamondConfigID,120) },
                { 300, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,15000,honorConfigID,375,diamondConfigID,110) },
                { 400, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,14000,honorConfigID,350,diamondConfigID,100) },
                { 500, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,13000,honorConfigID,325,diamondConfigID,90) },
                { 600, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,12000,honorConfigID,300,diamondConfigID,80) },
                { 800, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,11000,honorConfigID,275,diamondConfigID,70) },
                { 1000, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,10000,honorConfigID,250,diamondConfigID,60) },
                { 1500, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,9000,honorConfigID,225,diamondConfigID,50) },
                { 2000, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,8000,honorConfigID,200,diamondConfigID,50) },
                { 3000, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,7000,honorConfigID,175,diamondConfigID,40) },
                { 4000, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,6000,honorConfigID,150,diamondConfigID,40) },
                { 5000, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,5000,honorConfigID,125,diamondConfigID,30) },
                { 6000, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,4000,honorConfigID,100,diamondConfigID,30) },
                { 8000, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,3000,honorConfigID,75,diamondConfigID,20) },
                { 10000, String.Format("{{ configid = {0}, number ={1} }}, {{ configid = {2}, number ={3} }} , {{ configid = {4}, number ={5} }} ",moneyConfigID,2000,honorConfigID,50,diamondConfigID,20) }
            };

            return dict;
        }
    }
}
