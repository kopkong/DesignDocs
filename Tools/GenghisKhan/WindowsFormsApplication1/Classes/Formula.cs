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

        /// <summary>
        /// 获取对应关卡的参考等级
        /// </summary>
        /// <param name="levelID"></param>
        /// <returns></returns>
        public static int GetReferenceLevel(int levelID)
        {
            int chapter = DBConfigMgr.Instance.MapLevel[levelID].ChapterID;
            bool isElite = DBConfigMgr.Instance.MapLevel[levelID].IsEliteLevel == 1;

            int referenceLevel = 1;
            if(chapter> 1)
                referenceLevel = (chapter - 1) * 5;

            if (isElite)
                referenceLevel += 10;

            return referenceLevel;
            
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
            point += 0.1 * (sInfo.HP * 0.25 + sInfo.ATK * 0.25 + sInfo.DEF * 0.25) * sInfo.SoldierCount;

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
                MainAttribute basicAttribute = GetGeneralMainAttributeGrowth(1);
                MainAttribute basicAttribute2 = GetGeneralMainAttributeGrowth(2);
                double rate = 1 + gap * pair.Value.Star / 100;

                // 降低1星怪物的属性
                if (pair.Value.Star == 1)
                    rate = 0.8;

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
        public static int GetLevelDifficulty(int levelID)
        {
            int referLevel = GetReferenceLevel(levelID);
            int basicAttributes = GetGeneralMainAttributeGrowth(referLevel).SUM;

            int referBattlePoint = (int)(basicAttributes * 0.25 * 2 ) * GetOnBattleSquads(referLevel);

            return referBattlePoint;
        }

        /// <summary>
        /// 重新刷新计算所有关卡奖励的武将经验
        /// </summary>
        /// <returns></returns>
        public static int ComputeLevelGeneralEXPReward()
        {
            foreach (Chapter chapter in DBConfigMgr.Instance.MapChapter.Values)
            {
                // 所有的关卡
                IEnumerable<KeyValuePair<int, Level>> chapterLevelIDs = DBConfigMgr.Instance.MapLevel.Where(x => x.Value.ChapterID == chapter.ID);
                
                // 每个章节有多少关卡
                int levelCount = chapterLevelIDs.Count();

                for (int i = 0; i < levelCount; i++ )
                {
                    Level level = chapterLevelIDs.ElementAt(i).Value;
                    int levelID = level.ID;

                    int referLevel = Formula.GetReferenceLevel(levelID);
                    level.GeneralExpReward = referLevel * 80 - 20 + i* 50;
                }
            }

            return DBConfigMgr.Instance.SaveLevelGeneralEXPReward();
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
    }
}
