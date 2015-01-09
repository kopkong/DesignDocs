using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public static class CampaignBatch
    {
        /// <summary>
        /// 返回章节中的关卡ID
        /// </summary>
        /// <param name="chapter">章节ID号</param>
        /// <param name="index">关卡在本章的次序</param>
        /// <returns></returns>
        public static int GetLevelIDFromChapter(int chapter, int index)
        {
            if (chapter == 1) return index;
            else
                return 6 + 10 * (chapter - 2) + index;
        }

        private static int GetChapterReferLevel(int chapterID)
        {
            if (chapterID <= 20)
                return chapterID * 3;
            else
                return 60 + (chapterID - 20) * 4;

        }

        private static int GetChapterMoneyReward(int chapterID, bool isElite)
        {
            double money = GetChapterReferLevel(chapterID) * 100 * 0.3;

            if (isElite)
                money = money * 1.5;

            return (int)money;
        }

        private static int GetChapterGeneralExpReward(int chapterID, bool isElite)
        {
            if (isElite)
                return chapterID * 75;

            return chapterID * 50;
        }

        /// <summary>
        /// 重新刷新计算所有关卡奖励的武将经验
        /// </summary>
        /// <returns></returns>
        public static int RefreshLevelGeneralEXPReward()
        {
            foreach (Chapter chapter in DBConfigMgr.Instance.MapChapter.Values.Where(x => x.ID <= 30))
            {
                // 所有的关卡
                IEnumerable<KeyValuePair<int, Level>> chapterLevelIDs = DBConfigMgr.Instance.MapLevel.Where(x => x.Value.ChapterID == chapter.ID);

                // 每个章节有多少关卡
                int levelCount = chapterLevelIDs.Count();
                int basicEXPReward = GetChapterGeneralExpReward(chapter.ID, false);
                int basicEliteExpReward = GetChapterGeneralExpReward(chapter.ID, true);

                for (int i = 0; i < levelCount; i++)
                {
                    Level level = chapterLevelIDs.ElementAt(i).Value;
                    int levelID = level.ID;

                    level.GeneralExpReward = basicEXPReward + i * 5;
                    level.EliteGeneralExpReward = basicEliteExpReward + i * 5;
                }
            }

            return 0;
        }

        /// <summary>
        /// 刷新所有关卡的金币奖励
        /// </summary>
        /// <returns></returns>
        public static int RefreshLevelMoneyReward()
        {
            foreach (Chapter chapter in DBConfigMgr.Instance.MapChapter.Values.Where(x => x.ID <= 30))
            {
                // 所有的关卡
                IEnumerable<KeyValuePair<int, Level>> chapterLevelIDs = DBConfigMgr.Instance.MapLevel.Where(x => x.Value.ChapterID == chapter.ID);

                // 每个章节有多少关卡
                int levelCount = chapterLevelIDs.Count();
                int basicMoneyReward = GetChapterMoneyReward(chapter.ID, false);
                int basicEliteMoneyReward = GetChapterMoneyReward(chapter.ID, true);

                for (int i = 0; i < levelCount; i++)
                {
                    Level level = chapterLevelIDs.ElementAt(i).Value;
                    int levelID = level.ID;

                    level.MoneyReward = basicMoneyReward + i * 10;
                    level.EliteMoneyReward = basicEliteMoneyReward + i * 10;
                }
            }
            return 0;
        }

        /// <summary>
        /// 刷新所有关卡的宝箱关奖励
        /// </summary>
        /// <returns></returns>
        public static void RefreshLevelTreasureBoxReward()
        {
            // 1009 - 1011 钥匙
            // 1012 - 1014 宝箱

            for (int chapterID = 1; chapterID <= 20; chapterID++)
            {
                int level = GetLevelIDFromChapter(chapterID, 10);

                if (chapterID == 1)
                    level = GetLevelIDFromChapter(1, 6);

                if (chapterID <= 3)
                {
                    DBConfigMgr.Instance.MapLevel[level].ClientShowRewards = "2,1012,1;";
                    DBConfigMgr.Instance.MapLevel[level].EliteClientShowRewards = "2,1012,1;2,1009,1;";
                }
                else if (chapterID <= 7)
                {
                    DBConfigMgr.Instance.MapLevel[level].ClientShowRewards = "2,1013,1;";
                    DBConfigMgr.Instance.MapLevel[level].EliteClientShowRewards = "2,1013,1;2,1010,1;";
                }
                else if (chapterID <= 10)
                {
                    DBConfigMgr.Instance.MapLevel[level].ClientShowRewards = "2,1014,1;";
                    DBConfigMgr.Instance.MapLevel[level].EliteClientShowRewards = "2,1014,1;2,1011,1;";
                }
                else
                {
                    DBConfigMgr.Instance.MapLevel[level].ClientShowRewards = "2,1014,1;";
                    DBConfigMgr.Instance.MapLevel[level].EliteClientShowRewards = "2,1014,2;2,1011,2;";
                }

                DBConfigMgr.Instance.MapLevel[level].DailyMostTimes = 6;
                DBConfigMgr.Instance.MapLevel[level].EliteDailyMostTimes = 6;
                DBConfigMgr.Instance.MapLevel[level].LevelReward = "TreasureBoxReward";
                DBConfigMgr.Instance.MapLevel[level].EliteLevelReward = "TreasureBoxReward";
            }

        }

        /// <summary>
        /// 刷新关卡的武将碎片奖励
        /// </summary>
        public static void RefreshLevelGeneralMaterialReward()
        {
            IEnumerable<GeneralMaterial> originalList =
                from genMat in DBConfigMgr.Instance.MapGeneralMaterial.Values
                where genMat.Star == 3
                select genMat;

            int top = 54;
            List<GeneralMaterial> mat3List = new List<GeneralMaterial>();
            Utility.GetTopRandomArrayList<GeneralMaterial>(originalList, top, ref mat3List);

            List<int> rewardLevels = new List<int>();
            for (int chapterID = 2; chapterID <= 20; chapterID++)
            {
                int level1 = GetLevelIDFromChapter(chapterID, 2);
                int level2 = GetLevelIDFromChapter(chapterID, 5);
                int level3 = GetLevelIDFromChapter(chapterID, 8);

                rewardLevels.Add(level1);
                rewardLevels.Add(level2);
                rewardLevels.Add(level3);
            }

            for (int i = 0; i < top; i++)
            {
                string s = string.Format("21,{0},1;", mat3List[i].ID);
                DBConfigMgr.Instance.MapLevel[rewardLevels[i]].EliteClientShowRewards = s;
                DBConfigMgr.Instance.MapLevel[rewardLevels[i]].EliteDailyMostTimes = 5;

                DBConfigMgr.Instance.MapLevel[rewardLevels[i]].EliteLevelReward = "LowerGeneralMaterialReward";

            }
        }

        /// <summary>
        /// 刷新关卡的部队扩充道具奖励
        /// </summary>
        public static void RefreshLevelSoldierExpansionItemReward()
        {
            //从第3章开始
            for (int chapterID = 3; chapterID <= 30; chapterID++)
            {
                int level1 = GetLevelIDFromChapter(chapterID, 1);
                int level2 = GetLevelIDFromChapter(chapterID, 4);
                int level3 = GetLevelIDFromChapter(chapterID, 7);
                int level4 = GetLevelIDFromChapter(chapterID, 8);

                if (chapterID < 10)
                {
                    // 初级
                    DBConfigMgr.Instance.MapLevel[level1].EliteClientShowRewards = "2,1015,1;";
                    DBConfigMgr.Instance.MapLevel[level2].EliteClientShowRewards = "2,1016,1;";
                    DBConfigMgr.Instance.MapLevel[level3].EliteClientShowRewards = "2,1017,1;";

                    DBConfigMgr.Instance.MapLevel[level1].EliteDailyMostTimes = 8;
                    DBConfigMgr.Instance.MapLevel[level2].EliteDailyMostTimes = 8;
                    DBConfigMgr.Instance.MapLevel[level3].EliteDailyMostTimes = 8;

                    DBConfigMgr.Instance.MapLevel[level1].EliteLevelReward = "LowerSoldierExpansionReward";
                    DBConfigMgr.Instance.MapLevel[level2].EliteLevelReward = "LowerSoldierExpansionReward";
                    DBConfigMgr.Instance.MapLevel[level3].EliteLevelReward = "LowerSoldierExpansionReward";

                }
                else if (chapterID < 20)
                {
                    // 中级
                    if (chapterID % 2 == 0)
                    {
                        DBConfigMgr.Instance.MapLevel[level1].EliteClientShowRewards = "2,1018,1;";
                        DBConfigMgr.Instance.MapLevel[level2].EliteClientShowRewards = "2,1019,1;";
                        DBConfigMgr.Instance.MapLevel[level3].EliteClientShowRewards = "2,1020,1;";
                    }
                    else
                    {
                        DBConfigMgr.Instance.MapLevel[level1].EliteClientShowRewards = "2,1021,1;";
                        DBConfigMgr.Instance.MapLevel[level2].EliteClientShowRewards = "2,1022,1;";
                        DBConfigMgr.Instance.MapLevel[level3].EliteClientShowRewards = "2,1023,1;";
                        DBConfigMgr.Instance.MapLevel[level4].EliteClientShowRewards = "2,1024,1;";

                        DBConfigMgr.Instance.MapLevel[level4].EliteDailyMostTimes = 6;
                        DBConfigMgr.Instance.MapLevel[level4].EliteLevelReward = "MidSoldierExpansionReward";
                    }

                    DBConfigMgr.Instance.MapLevel[level1].EliteLevelReward = "MidSoldierExpansionReward";
                    DBConfigMgr.Instance.MapLevel[level1].EliteLevelReward = "MidSoldierExpansionReward";
                    DBConfigMgr.Instance.MapLevel[level1].EliteLevelReward = "MidSoldierExpansionReward";
                }
                else
                {
                    // 高级
                    if (chapterID % 2 == 0)
                    {
                        DBConfigMgr.Instance.MapLevel[level1].EliteClientShowRewards = "2,1025,1;";
                        DBConfigMgr.Instance.MapLevel[level2].EliteClientShowRewards = "2,1026,1;";
                        DBConfigMgr.Instance.MapLevel[level3].EliteClientShowRewards = "2,1027,1;";
                    }
                    else
                    {
                        DBConfigMgr.Instance.MapLevel[level1].EliteClientShowRewards = "2,1028,1;";
                        DBConfigMgr.Instance.MapLevel[level2].EliteClientShowRewards = "2,1029,1;";
                        DBConfigMgr.Instance.MapLevel[level3].EliteClientShowRewards = "2,1030,1;";
                        DBConfigMgr.Instance.MapLevel[level4].EliteClientShowRewards = "2,1031,1;";

                        DBConfigMgr.Instance.MapLevel[level4].EliteDailyMostTimes = 6;
                        DBConfigMgr.Instance.MapLevel[level4].EliteLevelReward = "HigherSoldierExpansionReward";
                    }

                    DBConfigMgr.Instance.MapLevel[level1].EliteDailyMostTimes = 6;
                    DBConfigMgr.Instance.MapLevel[level2].EliteDailyMostTimes = 6;
                    DBConfigMgr.Instance.MapLevel[level3].EliteDailyMostTimes = 6;

                    DBConfigMgr.Instance.MapLevel[level1].EliteLevelReward = "HigherSoldierExpansionReward";
                    DBConfigMgr.Instance.MapLevel[level1].EliteLevelReward = "HigherSoldierExpansionReward";
                    DBConfigMgr.Instance.MapLevel[level1].EliteLevelReward = "HigherSoldierExpansionReward";
                }
            }

        }

        public static void RefreshLevelEnemy()
        {
            foreach (int levelID in DBConfigMgr.Instance.MapLevel.Keys)
            {
                Formation f1 = new Formation();
                f1.InitNPCFormation(levelID, false);
                foreach (NPCEnemy enemy in f1.NPCFormation.Values)
                {
                    enemy.SoldierConfigID = DBConfigMgr.Instance.MapGeneral[enemy.GeneralConfigID].InitialSoldier;
                    enemy.GeneralLevel = DBConfigMgr.Instance.MapLevel[levelID].RefLevel;
                    enemy.SoldierLevel = DBConfigMgr.Instance.MapLevel[levelID].RefLevel;
                }
                RefreshOneLevelEnemy(levelID, f1.NPCFormation, false);

                Formation f2 = new Formation();
                f2.InitNPCFormation(levelID, true);
                foreach (NPCEnemy enemy in f2.NPCFormation.Values)
                {
                    enemy.SoldierConfigID = DBConfigMgr.Instance.MapGeneral[enemy.GeneralConfigID].InitialSoldier;
                    enemy.GeneralLevel = DBConfigMgr.Instance.MapLevel[levelID].EliteRefLevel;
                    enemy.SoldierLevel = DBConfigMgr.Instance.MapLevel[levelID].EliteRefLevel;
                }
                RefreshOneLevelEnemy(levelID, f2.NPCFormation, true);
            }
        }

        /// <summary>
        /// 刷新关卡的怪物
        /// </summary>
        /// <param name="levelID"></param>
        /// <param name="enemyList"></param>
        /// <param name="isElite"></param>
        public static void RefreshOneLevelEnemy(int levelID, Dictionary<int, NPCEnemy> enemyList, bool isElite)
        {
            string enemyStr = String.Empty;
            foreach (NPCEnemy enemy in enemyList.Values)
            {
                string fString = String.Format("{0},{1},{2},{3},{4},{5};", enemy.Position, enemy.GeneralConfigID,
                    enemy.GeneralLevel, enemy.SoldierConfigID, enemy.SoldierLevel, enemy.SoldierCount);

                enemyStr += fString;
            }

            if (!isElite)
            {
                DBConfigMgr.Instance.MapLevel[levelID].Enemy = enemyStr;
            }
            else
            {
                DBConfigMgr.Instance.MapLevel[levelID].EliteEnemy = enemyStr;
            }
        }

        /// <summary>
        /// 刷新关卡的装备奖励
        /// </summary>
        public static void RefreshLevelArmorReward()
        {
            //从第1章开始
            for (int chapterID = 1; chapterID <= 20; chapterID++)
            {
                if (chapterID < 2)
                {
                    // 1星
                    DBConfigMgr.Instance.MapLevel[3].ClientShowRewards = "3,1,1;";
                    DBConfigMgr.Instance.MapLevel[5].ClientShowRewards = "3,2,1;";

                    DBConfigMgr.Instance.MapLevel[3].LevelReward = "LowerArmorReward";
                    DBConfigMgr.Instance.MapLevel[5].LevelReward = "LowerArmorReward";
                }
                else if (chapterID < 10)
                {
                    // 1星 、 2星装备
                    DBConfigMgr.Instance.MapLevel[GetLevelIDFromChapter(chapterID, 1)].ClientShowRewards = String.Format("3,{0},1;", (chapterID - 2) * 3 + 1);
                    DBConfigMgr.Instance.MapLevel[GetLevelIDFromChapter(chapterID, 4)].ClientShowRewards = String.Format("3,{0},1;", (chapterID - 2) * 3 + 2);
                    DBConfigMgr.Instance.MapLevel[GetLevelIDFromChapter(chapterID, 7)].ClientShowRewards = String.Format("3,{0},1;", (chapterID - 2) * 3 + 3);

                    DBConfigMgr.Instance.MapLevel[GetLevelIDFromChapter(chapterID, 1)].DailyMostTimes = 10;
                    DBConfigMgr.Instance.MapLevel[GetLevelIDFromChapter(chapterID, 4)].DailyMostTimes = 10;
                    DBConfigMgr.Instance.MapLevel[GetLevelIDFromChapter(chapterID, 7)].DailyMostTimes = 10;

                    DBConfigMgr.Instance.MapLevel[GetLevelIDFromChapter(chapterID, 1)].LevelReward = "LowerArmorReward";
                    DBConfigMgr.Instance.MapLevel[GetLevelIDFromChapter(chapterID, 4)].LevelReward = "LowerArmorReward";
                    DBConfigMgr.Instance.MapLevel[GetLevelIDFromChapter(chapterID, 7)].LevelReward = "LowerArmorReward";
                }
                else if (chapterID < 20)
                {
                    // 3星碎片
                    for (int i = 1; i <= 9; i++)
                    {
                        DBConfigMgr.Instance.MapLevel[GetLevelIDFromChapter(chapterID, i)].ClientShowRewards = String.Format("4,{0},1;", (chapterID - 10) * 9 + i);
                        DBConfigMgr.Instance.MapLevel[GetLevelIDFromChapter(chapterID, i)].DailyMostTimes = 8;
                        DBConfigMgr.Instance.MapLevel[GetLevelIDFromChapter(chapterID, i)].LevelReward = "LowerArmorMaterialReward";
                    }
                }
                else if (chapterID == 20)
                {

                }
            }
        }

    }
}
