using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Reflection;

namespace WindowsFormsApplication1
{
    public class DBConfigMgr
    {
        private static DBConfigMgr instance;
        private static object syncRoot = new Object();

        public IDictionary<int, AllConfig> MapAllConfig;
        public IDictionary<int, AllConfig> MapAllRewardTypes;
        public IDictionary<int, General> MapGeneral;
        public IDictionary<int, Soldier> MapSoldier;
        public IDictionary<int, Armor> MapArmor;
        public IDictionary<int, Item> MapItem;
        public IDictionary<int, ArmorMaterial> MapArmorMaterial;
        public IDictionary<int, SoldierMaterial> MapSoldierMaterial;
        public IDictionary<int, Chapter> MapChapter;
        public IDictionary<int, Level> MapLevel;
        public IDictionary<int, Experience> MapExperience;
        public IDictionary<int, Nobility> MapNobility;
        public IDictionary<int, Task> MapTask;
        public IDictionary<int, Skill> MapSkill;
        public IDictionary<int, MaxSquad> MapMaxSquads;
        public IDictionary<int, Angel> MapAngel;
        public IDictionary<int, Effect> MapEffect;
        public IDictionary<int, Bullet> MapBullet;
        public IDictionary<int, CheckIn> MapCheckIn;
        public IDictionary<int, Shop> MapShop;
        public IDictionary<int, Goods> MapGoods;
        public IDictionary<int, Plot> MapPlot;
        public IDictionary<int, GeneralMaterial> MapGeneralMaterial;
        public IDictionary<int, Buff> MapBuff;
        public IDictionary<int, SkillTemplate> MapSkillTemplate;
        public IDictionary<int, GeneralTemplate> MapGeneralTemplate;
        public IDictionary<int, Scene> MapScene;
        public IDictionary<int, VIP> MapVIP;
        public IDictionary<int, Recharge> MapRecharge;
        public IDictionary<int, Guide> MapGuide;

        public static DBConfigMgr Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DBConfigMgr();
                    }
                }
                return instance;
            }
        }

        public string GetNameByTypeConfigID(int type, int id)
        {
            switch (type)
            {
                case 1:
                    {
                        if(MapGeneral.ContainsKey(id))
                            return MapGeneral[id].Name;
                        break;
                    }
                case 2:
                    {
                        if(MapItem.ContainsKey(id))
                            return MapItem[id].Name;
                        break;
                    }
                case 3:
                    {
                        if(MapArmor.ContainsKey(id))
                            return MapArmor[id].Name;
                        break;
                    }
                case 4:
                    {
                        if(MapArmorMaterial.ContainsKey(id))
                            return MapSoldier[id].Name;
                        break;
                    }
                case 5:
                    {
                        if (MapSoldier.ContainsKey(id))
                            return MapSoldier[id].Name;
                        break;
                    }
                case 6:
                    {
                        if (MapSoldierMaterial.ContainsKey(id))
                            return MapSoldierMaterial[id].Name;
                        break;
                    }
                case 21:
                    {
                        if (MapGeneralMaterial.ContainsKey(id))
                            return MapGeneralMaterial[id].Name;

                        break;
                    }
                default:
                    break;
            }

            return "没有找到该ID";
        }

        public void Init()
        {
            LoadConfigData<AllConfig>(ref MapAllConfig, "AllConfig");
            LoadConfigData<General>(ref MapGeneral, "GeneralConfig");
            LoadConfigData<Soldier>(ref MapSoldier, "SoldierConfig");
            LoadConfigData<Armor>(ref MapArmor, "ArmorConfig");
            LoadConfigData<Item>(ref MapItem, "ItemConfig");
            LoadConfigData<Chapter>(ref MapChapter, "ChapterConfig");
            LoadConfigData<Level>(ref MapLevel, "LevelConfig");
            LoadConfigData<Experience>(ref MapExperience, "ExperienceConfig");
            LoadConfigData<ArmorMaterial>(ref MapArmorMaterial, "ArmorMaterialConfig");
            LoadConfigData<SoldierMaterial>(ref MapSoldierMaterial, "SoldierMaterialConfig");
            LoadConfigData<Nobility>(ref MapNobility, "NobilityConfig");
            LoadConfigData<Task>(ref MapTask, "TaskConfig");
            LoadConfigData<Skill>(ref MapSkill, "SkillConfig");
            LoadConfigData<MaxSquad>(ref MapMaxSquads, "MaxSquads");
            LoadConfigData<Angel>(ref MapAngel, "AngelConfig");
            LoadConfigData<Effect>(ref MapEffect, "EffectConfig");
            LoadConfigData<Bullet>(ref MapBullet, "BulletConfig");
            LoadConfigData<CheckIn>(ref MapCheckIn, "CheckInConfig");
            LoadConfigData<Shop>(ref MapShop, "ShopConfig");
            LoadConfigData<Goods>(ref MapGoods, "GoodsConfig");
            LoadConfigData<Plot>(ref MapPlot, "PlotConfig");
            LoadConfigData<GeneralMaterial>(ref MapGeneralMaterial, "GeneralMaterialConfig");
            LoadConfigData<Buff>(ref MapBuff, "BuffConfig");
            LoadConfigData<SkillTemplate>(ref MapSkillTemplate, "SkillTemplateConfig");
            LoadConfigData<GeneralTemplate>(ref MapGeneralTemplate, "GeneralTemplateConfig");
            LoadConfigData<Scene>(ref MapScene, "SceneConfig");
            LoadConfigData<VIP>(ref MapVIP, "VIPConfig");
            LoadConfigData<Recharge>(ref MapRecharge, "RechargeConfig");
            LoadConfigData<Guide>(ref MapGuide, "GuideConfig");

            MapAllRewardTypes = new Dictionary<int, AllConfig>();
            foreach (AllConfig config in MapAllConfig.Values)
            {
                if (config.CanBeReward)
                    MapAllRewardTypes.Add(config.ID, config);
            }
        }

        private T ParseFromDataReader<T>(ref int ID,SQLiteDataReader reader)
        {
            T obj = Activator.CreateInstance<T>();

            foreach (PropertyInfo pI in obj.GetType().GetProperties())
            {
                Type type = pI.PropertyType;

                int o = reader.GetOrdinal(pI.Name);
                if (o >= 0 )
                {
                    Object rawValue = reader[pI.Name];

                    if (type.Name == "Int32" && rawValue is System.DBNull) rawValue = 0;

                    Object v = Convert.ChangeType(rawValue, type);

                    if (pI.Name == "ID")
                        ID = Convert.ToInt32(rawValue);

                    pI.SetValue(obj, v, null);
                }
            }

            return (T)obj;
        }

        private void LoadConfigData<T>(ref IDictionary<int, T> dict, string tableName)
        {
            dict = new Dictionary<int, T>();
            string sql = "select * from " + tableName;
            using (SQLiteDataReader reader = SQLiteHelper.Instance.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    int id = 0;
                    T obj = ParseFromDataReader<T>(ref id,reader);
                    dict.Add(id, obj);
                }
            }
        }

        /// <summary>
        /// 更新所有的武将基本数据
        /// </summary>
        public int UpdateAllGeneralBasicInfo()
        {
            string cmd = String.Empty;
            foreach (KeyValuePair<int, General> pair in MapGeneral)
            {
                General config = pair.Value;
                string sql = String.Format(@"update GeneralConfig set HP={1},AttackPower={2},DefensePower={3},
                    HPGrowth={4},ATKGrowth={5},DEFGrowth={6},AttackSpeed = {7},AttackRange = {8},MoveSpeed = {9},
                    InitialSoldier = {10},SpecialArmor= '{11}', SpecialSoldier = '{12}',ModelPatch = '{13}',
                    IconID = '{14}',BigIcon = '{15}',Title = '{16}',RankUpLevel1Costs = '{17}',RankUpLevel2Costs='{18}',
                    RankUpLevel3Costs='{19}',RankUpLevel4Costs='{20}',RankUpLevel5Costs='{21}',AttackType = {22} 
                    where ID = {0};",
                    pair.Key, config.HP, config.AttackPower, config.DefensePower,
                    config.HPGrowth, config.ATKGrowth, config.DEFGrowth, config.AttackSpeed,
                    config.AttackRange, config.MoveSpeed, config.InitialSoldier,
                    config.SpecialArmor,config.SpecialSoldier,config.ModelPatch,config.IconID,
                    config.BigIcon,config.Title,config.RankUpLevel1Costs,config.RankUpLevel2Costs,
                    config.RankUpLevel3Costs,config.RankUpLevel4Costs,config.RankUpLevel5Costs,config.AttackType
                );

                cmd += sql;
            }

            return SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);
        }

        /// <summary>
        /// 更新所有的武将技能
        /// </summary>
        public void UpdateAllGeneralSkillAndTalent()
        {
            string cmd = String.Empty;
            foreach (KeyValuePair<int, General> pair in MapGeneral)
            {
                string sql = String.Format(@"update GeneralConfig set SkillID = '{1}',TalentID = '{2}' where ID = {0};",
                    pair.Key, pair.Value.SkillID,pair.Value.TalentID);

                cmd += sql;
            }

            SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);
        }

        /// <summary>
        /// 保存部队数据
        /// </summary>
        /// <returns></returns>
        public int UpdateAllSoldier()
        {
            string cmd = String.Empty;
            foreach (KeyValuePair<int, Soldier> pair in MapSoldier)
            {
                Soldier config = pair.Value;
                string sql = String.Format(@"update SoldierConfig set HP={1},AttackPower={2},DefensePower={3},
                    HPGrowth={4},ATKGrowth={5},DEFGrowth={6},AttackSpeed = {7},AttackRange = {8},MoveSpeed = {9},
                    AddCount1Costs='{10}', AddCount2Costs = '{11}', AddCount3Costs = '{12}', AddCount4Costs = '{13}',
                    AddCount5Costs = '{14}', AddCount6Costs ='{15}', AddCount7Costs = '{16}', AddCount8Costs = '{17}',
                    AddCount9Costs = '{18}', AddCount10Costs = '{19}',AttackType = {20} where ID = {0};",
                    pair.Key, pair.Value.HP, pair.Value.AttackPower, pair.Value.DefensePower,
                    pair.Value.HPGrowth, pair.Value.ATKGrowth, pair.Value.DEFGrowth, pair.Value.AttackSpeed,
                    pair.Value.AttackRange,pair.Value.MoveSpeed,config.AddCount1Costs,config.AddCount2Costs,
                    config.AddCount3Costs,config.AddCount4Costs,config.AddCount5Costs,config.AddCount6Costs,
                    config.AddCount7Costs,config.AddCount8Costs,config.AddCount9Costs,config.AddCount10Costs,
                    config.AttackType);

                cmd += sql;
            }

            return SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);
        }

        /// <summary>
        /// 更新所有的装备信息
        /// </summary>
        /// <returns></returns>
        public int UpdateAllArmor()
        {
            string cmd = String.Empty;
            foreach (Armor armor in MapArmor.Values)
            {
                string sql = String.Format(@"update ArmorConfig set HP={1},ATK={2},DEF={3},
                    HPGrowth={4},ATKGrowth={5},DEFGrowth={6} where ID = {0};",
                    armor.ID,armor.HP,armor.ATK,armor.DEF,armor.HPGrowth,armor.ATKGrowth,armor.DEFGrowth);

                cmd += sql;
            }

            return SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);
        }

        public int UpdateLevelEnemy(int levelID)
        {
            string cmd = String.Format(@"update LevelConfig set Enemy = '{1}', EliteEnemy = '{2}' where ID = {0};",
                levelID, MapLevel[levelID].Enemy,MapLevel[levelID].EliteEnemy);

            return SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);
        }
        
        /// <summary>
        /// 更新所有的关卡怪物
        /// </summary>
        public void UpdateAllLevelEnemy()
        {
            string cmd = String.Empty;
            foreach (Level level in MapLevel.Values)
            {
                cmd += String.Format(@"update LevelConfig set Enemy = '{1}', EliteEnemy ='{2}' where ID = {0};",
                    level.ID, level.Enemy, level.EliteEnemy);
            }

            SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);
        }

        public int UpdateAllLevelGeneralEXPReward()
        {
            string cmd = String.Empty;
            foreach (KeyValuePair<int, Level> pair in MapLevel)
            {
                string sql = String.Format(@"update LevelConfig set GeneralExpReward ={1}, EliteGeneralExpReward = {2}
                        where ID = {0};",
                    pair.Key,pair.Value.GeneralExpReward,pair.Value.EliteGeneralExpReward);

                cmd += sql;
            }

            return SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);
        }

        public int UpdateAllLevelMoneyReward()
        {
            string cmd = String.Empty;
            foreach (KeyValuePair<int, Level> pair in MapLevel)
            {
                string sql = String.Format(@"update LevelConfig set MoneyReward ={1},EliteMoneyReward = {2} where ID = {0};",
                    pair.Key, pair.Value.MoneyReward,pair.Value.EliteMoneyReward);

                cmd += sql;
            }

            return SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);
        }

        public void UpdateLevelBasicAttributes(int id)
        {
            Level config = MapLevel[id];
            string sql = String.Format(@"update LevelConfig set Name ='{1}', RefLevel = {2},EliteRefLevel = {3},
                        Enemy = '{4}', EliteEnemy = '{5}',StrongEnemy = {6},EnemyAngel = '{7}', EliteEnemyAngel =                               '{8}' where ID = {0};",
                    id,MapLevel[id].Name,MapLevel[id].RefLevel,MapLevel[id].EliteRefLevel,
                    config.Enemy, config.EliteEnemy, config.StrongEnemy,config.EnemyAngel,config.EliteEnemyAngel);

            SQLiteHelper.Instance.ExecuteNonQuery(sql, null);
        }

        /// <summary>
        /// 更新武将基本信息
        /// </summary>
        /// <param name="id"></param>
        public void UpdateGeneralBasicInfo(int id)
        {
            General g = MapGeneral[id];
            string sql = String.Format(@"update GeneralConfig set Name = '{1}',Star = {2},SoldierType = {3}, InitialSoldier = {4},
                       Usage = {5}, Nationality = {6} where ID = {0}",
                g.ID,g.Name,g.Star,g.SoldierType,g.InitialSoldier,g.Usage,g.Nationality);

            SQLiteHelper.Instance.ExecuteNonQuery(sql, null);
        }

        /// <summary>
        /// 更新剧情内容
        /// </summary>
        /// <param name="id"></param>
        /// <param name="beforePlot"></param>
        /// <param name="inPlot"></param>
        /// <param name="afterPlot"></param>
        public void UpdatePlot(int id,string beforePlot, string inPlot,string afterPlot)
        {
            string cmd = String.Empty;
            if (MapPlot.ContainsKey(id))
            {
                cmd = String.Format("Update PlotConfig set BeforeBattle = '{1}',InBattle='{2}',AfterBattle='{3}' where ID = {0}",
                    id, beforePlot, inPlot, afterPlot);

                MapPlot[id].BeforeBattle = beforePlot;
                MapPlot[id].InBattle = inPlot;
                MapPlot[id].AfterBattle = afterPlot;

            }
            else
            {
                cmd = String.Format("Insert into PlotConfig values({0},'{1}','{2}','{3}')",
                    id, beforePlot, inPlot, afterPlot);

                Plot p = new Plot();
                p.ID = id;
                p.BeforeBattle = beforePlot;
                p.InBattle = inPlot;
                p.AfterBattle = afterPlot;

                MapPlot.Add(p.ID,p);
            }

            SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);
        }

        /// <summary>
        /// 更新关卡参考等级
        /// </summary>
        /// <returns></returns>
        public int UpdateLevelRefLevel()
        {
            string cmd = String.Empty;
            foreach (KeyValuePair<int, Level> pair in MapLevel)
            {
                string sql = String.Format(@"update LevelConfig set RefLevel={1},EliteRefLevel={2} where ID = {0};",
                    pair.Key, pair.Value.RefLevel,pair.Value.EliteRefLevel);

                cmd += sql;
            }

            return SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);
        }

        /// <summary>
        /// 更新关卡奖励数据
        /// </summary>
        /// <returns></returns>
        public int UpdateAllLevelReward()
        {
            string cmd = String.Empty;
            foreach (KeyValuePair<int, Level> pair in MapLevel)
            {
                string sql = String.Format(@"update LevelConfig set MoneyReward={1},EliteMoneyReward={2},
                        GeneralExpReward = {3},EliteGeneralExpReward = {4},ClientShowRewards = '{5}',
                        EliteClientShowRewards = '{6}',LevelReward = '{7}', DailyMostTimes = {8},
                        EliteDailyMostTimes = {9},EliteLevelReward = '{10}' where ID = {0};",
                    pair.Key, pair.Value.MoneyReward,pair.Value.EliteMoneyReward,pair.Value.GeneralExpReward, 
                    pair.Value.EliteGeneralExpReward, pair.Value.ClientShowRewards, pair.Value.EliteClientShowRewards,
                    pair.Value.LevelReward,pair.Value.DailyMostTimes,pair.Value.EliteDailyMostTimes,
                    pair.Value.EliteLevelReward);

                cmd += sql;
            }

            return SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);
        }

        /// <summary>
        /// 更新一条关卡奖励数据
        /// </summary>
        /// <param name="id"></param>
        public void UpdateLevelReward(int id)
        {
            Level config = MapLevel[id];
            string sql = String.Format(@"update LevelConfig set MoneyReward={1},EliteMoneyReward={2},
                        GeneralExpReward = {3},EliteGeneralExpReward = {4},ClientShowRewards = '{5}',
                        EliteClientShowRewards = '{6}' where ID = {0};",
                    id, config.MoneyReward, config.EliteMoneyReward,config.GeneralExpReward,
                    config.EliteGeneralExpReward,config.ClientShowRewards,config.EliteClientShowRewards);
            SQLiteHelper.Instance.ExecuteNonQuery(sql, null);
        }

        /// <summary>
        /// 清空道具奖励先
        /// </summary>
        public void ClearAllLevelItemReward()
        {
            foreach (Level l in MapLevel.Values)
            {
                l.ClientShowRewards = "";
                l.EliteClientShowRewards = "";
            }

        }

        /// <summary>
        /// 更新任务奖励
        /// </summary>
        public void UpdateTaskReward(int taskID)
        {
            Task t = MapTask[taskID];
            string sql = String.Format(@"update TaskConfig set Name = '{1}',TaskLevel ={2},PlayerExpReward = {3},
                DiamondReward = {4}, MoneyReward = {5}, ItemReward = '{6}',Description = '{7}' where ID = {0}",
                t.ID, t.Name, t.TaskLevel, t.PlayerExpReward, t.DiamondReward, t.MoneyReward, t.ItemReward,
                t.Description);

            SQLiteHelper.Instance.ExecuteNonQuery(sql, null);

        }

        /// <summary>
        /// 保存新的武将碎片
        /// </summary>
        /// <returns></returns>
        public int SaveNewGeneralMaterials()
        {
            // Clear old data first
            string cmd = "Delete from GeneralMaterialConfig";
            SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);

            cmd = String.Empty;
            foreach (GeneralMaterial genMat in MapGeneralMaterial.Values)
            {
                string sql = String.Format("Insert into GeneralMaterialConfig values({0},'{1}','{2}','{3}',{4},{5},{6});",
                    genMat.ID,genMat.Name,genMat.IconID,genMat.Description,genMat.Star,genMat.GeneralID,genMat.ConsumeMaterials);

                cmd += sql;
            }

            return SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);
        }

        /// <summary>
        /// 保存新的装备碎片
        /// </summary>
        /// <returns></returns>
        public int SaveNewArmorMaterials()
        {
            // Clear old data first
            string cmd = "Delete from ArmorMaterialConfig";
            SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);

            cmd = String.Empty;
            foreach (ArmorMaterial armorMat in MapArmorMaterial.Values)
            {
                string sql = String.Format("Insert into ArmorMaterialConfig values({0},'{1}','{2}','{3}',{4},{5},{6},{7});",
                    armorMat.ID, armorMat.Name, armorMat.IconID, armorMat.Description, armorMat.Star, armorMat.Type,armorMat.ArmorID, armorMat.ConsumeMaterials);

                cmd += sql;
            }

            return SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);
        }

        /// <summary>
        /// 更新关卡奖励脚本名
        /// </summary>
        public void UpdateAllLevelRewardScriptName()
        {
            string cmd = String.Empty;
            foreach (KeyValuePair<int, Level> pair in MapLevel)
            {
                string sql = String.Format(@"update LevelConfig set LevelReward='{1}' where ID = {0};",
                    pair.Key, pair.Value.LevelReward);

                cmd += sql;
            }

            SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);
        }

        /// <summary>
        /// 重新保存所有的技能和buff
        /// </summary>
        public void ClearAndSaveSkillAndBuff()
        {
            string cmd = "delete from SkillConfig; delete from BuffConfig;";

            foreach (Skill config in MapSkill.Values)
            {
                string sql = String.Format(@"insert into SkillConfig values({0},'{1}','{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},'{11}',{12},'{13}','{14}','{15}');",
                    config.ID,config.SkillType,config.Name,config.Description,config.IconID,config.SkillCategory,
                    config.MaxHit,config.Possiblity,config.CoolDown,config.EffectRange,config.ConditionType,
                    config.ConditionParam,config.EffectBullet,config.EffectID,config.Buff,config.SkillParam);

                cmd += sql;
            }

            foreach (Buff config in MapBuff.Values)
            {
                string sql = String.Format(@"insert into BuffConfig values({0},'{1}',{2},{3},{4},'{5}',{6},{7},'{8}');",
                   config.ID,config.Name,config.BuffType,config.Positive,config.Time,config.ModelPath,config.ModelOffset,
                   config.Target,config.BuffKV);

                cmd += sql;
            }

            SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);
        }

        /// <summary>
        /// 重新保存所有的武将
        /// </summary>
        public void ClearAndSaveGeneral()
        {
            string cmd = "delete from GeneralConfig where id < 10000;";

            foreach (General config in MapGeneral.Values)
            {
                string sql = String.Format(@"insert into GeneralConfig values({0},'{1}','{2}',{3},'{4}','{5}','{6}',{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},
                       '{20}','{21}',{22},{23},{24},{25},{26},{27},'{28}',{29},'{30}','{31}','{32}','{33}','{34}',{35},{36},{37},{38},{39},{40},{41},'{42}','{43}');", 
                    config.ID, config.Title, config.Name, config.InitialLv, config.IconID, config.ModelPatch, config.Description,
                    config.Star, config.SoldierType, config.AttackType, config.InitialSoldier, config.HP, config.AttackPower, config.DefensePower, config.MoveSpeed, config.AttackSpeed, config.AttackRange,
                    config.CriticalRate, config.DodgeRate, config.HitRate, config.SkillID, config.TalentID, config.HPGrowth, config.ATKGrowth, config.DEFGrowth, config.HPRankRate, config.ATKRankRate, config.DEFRankRate,
                    config.SpecialArmor, config.Souls, config.RankUpLevel1Costs, config.RankUpLevel2Costs, config.RankUpLevel3Costs, config.RankUpLevel4Costs, config.RankUpLevel5Costs,
                    config.Rank1LeastLevel, config.Rank2LeastLevel, config.Rank3LeastLevel, config.Rank4LeastLevel, config.Rank5LeastLevel, config.Nationality, config.Usage,config.SpecialSoldier,config.BigIcon);

                cmd += sql;
            }

            SQLiteHelper.Instance.ExecuteDataTable(cmd, null);
        }

        /// <summary>
        /// 重新保存测试武将
        /// </summary>
        public void SaveTestGeneral()
        {
            string cmd = String.Empty;
            foreach (General config in MapGeneral.Values.Where(x=> x.ID > 10000))
            {
                string sql = String.Format(@"insert into GeneralConfig values({0},'{1}','{2}',{3},'{4}','{5}','{6}',{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},
                       '{20}','{21}',{22},{23},{24},{25},{26},{27},'{28}',{29},'{30}','{31}','{32}','{33}','{34}',{35},{36},{37},{38},{39},{40},{41},'{42}','{43}');",
                    config.ID, config.Title, config.Name, config.InitialLv, config.IconID, config.ModelPatch, config.Description,
                    config.Star, config.SoldierType, config.AttackType, config.InitialSoldier, config.HP, config.AttackPower, config.DefensePower, config.MoveSpeed, config.AttackSpeed, config.AttackRange,
                    config.CriticalRate, config.DodgeRate, config.HitRate, config.SkillID, config.TalentID, config.HPGrowth, config.ATKGrowth, config.DEFGrowth, config.HPRankRate, config.ATKRankRate, config.DEFRankRate,
                    config.SpecialArmor, config.Souls, config.RankUpLevel1Costs, config.RankUpLevel2Costs, config.RankUpLevel3Costs, config.RankUpLevel4Costs, config.RankUpLevel5Costs,
                    config.Rank1LeastLevel, config.Rank2LeastLevel, config.Rank3LeastLevel, config.Rank4LeastLevel, config.Rank5LeastLevel, config.Nationality, config.Usage, config.SpecialSoldier,config.BigIcon);

                cmd += sql;
            }

            SQLiteHelper.Instance.ExecuteDataTable(cmd, null);
        }

        public void ClearTestGeneral()
        {
            string cmd = "delete from GeneralConfig where id > 10000;";
            SQLiteHelper.Instance.ExecuteDataTable(cmd, null);
        }
    }
}
