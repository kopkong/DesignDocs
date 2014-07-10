using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public static class Formula
    {

        const int BASICHEROGROWTH = 100;
        const int BASICARMORGROWTH = 50;
        const int BASICSOLDIER = 50;
        const int BASICTALENTGROWTH = 15;
        const int BASICNOBLERANKGROWTH = 15;

        const int HIGHHEROGROWTH = 120;
        const int HIGHARMORGROWTH = 75;
        const int HIGHSOLDIERGROWTH = 75;

        
        struct MainAttribute
        {
            public int HP;
            public int ATK;
            public int DEF;
            public int SUM
            {
                get
                {
                    return HP + ATK + DEF;
                }
            }
        }

        public static int GetOnBattleSquads(int playerLevel)
        {
            if (playerLevel < 5)
                return 6;
            else if (playerLevel < 10)
                return 7;
            else if (playerLevel < 15)
                return 8;
            else if (playerLevel < 20)
                return 9;
            else if (playerLevel < 25)
                return 10;
            else if (playerLevel < 30)
                return 11;
            else if (playerLevel < 35)
                return 12;
            else if (playerLevel < 40)
                return 13;
            else if (playerLevel < 45)
                return 14;
            else if (playerLevel < 50)
                return 15;
            else if (playerLevel < 55)
                return 16;
            else if (playerLevel < 60)
                return 17;
            else if (playerLevel < 70)
                return 18;
            else if (playerLevel < 90)
                return 19;
            else
                return 20;
            
        }

        private static MainAttribute GetGeneralMainAttributeGrowth(int level)
        {
            MainAttribute main;

            int initialHP = 250;
            int initialATK = 35;
            int initialDef = 15;

            int growthHP = 48;
            int growthATK = 12;
            int growthDEF = 3;

            main.HP = initialHP + growthHP * (level - 1);
            main.ATK = initialATK + growthATK * (level - 1);
            main.DEF = initialDef + growthDEF * (level - 1);

            return main;
        }

        // 计算武将战斗力
        public static int ComputeBattlePowerPoint(GeneralInfo gInfo, SoldierInfo sInfo)
        {
            double point = 0;

            //point = gInfo.HP * 0.038 + gInfo.ATK * 0.06 + gInfo.DEF * 0.17 + gInfo.CriticalRate * 1.9 + gInfo.DodgeRate * 1.3 + gInfo.HitRate * 1.4;
            point = gInfo.HP * 0.25 + gInfo.ATK * 0.25 + gInfo.DEF * 0.25;

            // 加上部队
            point += (sInfo.HP * 0.25 + sInfo.ATK * 0.25 + sInfo.DEF * 0.25) * sInfo.SoldierCount;

            // 加上装备
            return (int)point;
        }

        /// <summary>
        /// 模拟队伍1是否取胜
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns></returns>
        public static bool ComputeTeamOneWin(Formation f1, Formation f2)
        {
            if (f1.TeamBattlePowerPoint / 2 > f2.TeamBattlePowerPoint)
                return true;
            else if (f1.TeamBattlePowerPoint * 2 < f2.TeamBattlePowerPoint)
                return false;

            double winPossiblity = 0.9;

            int battlePointGap = f1.TeamBattlePowerPoint - f2.TeamBattlePowerPoint;
            if (battlePointGap > 0) // f1 > f2
            {
                winPossiblity += battlePointGap / (10 * f2.TeamBattlePowerPoint); // = 0.9 + (A-B)/10B
            }
            else // f1 < f2
            {
                winPossiblity += battlePointGap / (0.9 * f2.TeamBattlePowerPoint);
            }

            Random r = new Random();
            return r.NextDouble() <= winPossiblity;
        }

        /// <summary>
        /// 重新刷新并计算武将的属性和成长性
        /// </summary>
        public static void ComputeGeneralBasicAttribute(int gap)
        {
            foreach(KeyValuePair<int,General> pair in DBConfigMgr.Instance.MapGeneral)
            {
                MainAttribute basicAttribute = GeneralMainAttributeGrowth(1);
                MainAttribute basicAttribute2 = GeneralMainAttributeGrowth(2);
                double rate = 1 + gap * pair.Value.Star / 100;


                pair.Value.HP = (int)(basicAttribute.HP * rate);
                pair.Value.AttackPower = (int)(basicAttribute.ATK * rate);
                pair.Value.DefensePower = (int)(basicAttribute.DEF * rate);

                pair.Value.HPGrowth = (int)((basicAttribute2.HP-basicAttribute.HP) * rate);
                pair.Value.ATKGrowth = (int)((basicAttribute2.ATK- basicAttribute.ATK) * rate);
                pair.Value.DEFGrowth = (int)((basicAttribute2.DEF - basicAttribute.DEF) * rate);
            }
        }

        /// <summary>
        /// 调整关卡难度系数
        /// </summary>
        public static void ComputeLevelDifficulty(int levelID,int referLevel,int difficulty)
        {
            int basicAttributes = GetGeneralMainAttributeGrowth(referLevel).SUM;
            double rate = 1 + difficulty / 100;

            int referBattlePoint = (int)(basicAttributes * 0.25 * 2 * rate) * GetOnBattleSquads(referLevel);

            int points = 0;
            List<int> allEnemies = new List<int>();

            int[,] enemyIDPool = new int[,]
            {
                {2000,2005,2010},
                {2001,2006,2011},
                {2002,2007,2012},
                {2003,2008,2013},
                {2004,2009,2014}
            };

            while (points < referBattlePoint)
            {
                // 随机添加一个队伍
                
            }
        }
    }
}
