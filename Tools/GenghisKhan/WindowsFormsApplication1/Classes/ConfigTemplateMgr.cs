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

        public void Init()
        {
            LoadGeneralData();
            LoadSoldierData();
            LoadArmorData();
            LoadItemData();
            LoadChapterData();
            LoadLevelData();
            LoadExperienceData();
            LoadArmorMaterialData();
            LoadSoldierMaterialData();
            LoadNobilityData();
            LoadTaskData();
            LoadSkillData();
        }

        private T ParseFromDataReader<T>(SQLiteDataReader reader)
        {
            T obj = Activator.CreateInstance<T>();

            foreach (PropertyInfo pI in obj.GetType().GetProperties())
            {
                Type type = pI.PropertyType;

                int o = reader.GetOrdinal(pI.Name);
                if (o >= 0 )
                {
                    Object rawValue = reader[pI.Name];
                    Object v = Convert.ChangeType(rawValue, type);
                    pI.SetValue(obj, v, null);
                }
            }

            return (T)obj;
        }

        private void LoadGeneralData()
        {
            MapGeneral = new Dictionary<int, General>();
            string sql = "select * from GeneralConfig";
            using (SQLiteDataReader reader = SQLiteHelper.Instance.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    General g = ParseFromDataReader<General>(reader);
                    MapGeneral.Add(g.ID, g);
                }
            }

        }

        private void LoadSoldierData()
        {
            MapSoldier = new Dictionary<int, Soldier>();
            string sql = "select * from SoldierConfig";
            using (SQLiteDataReader reader = SQLiteHelper.Instance.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    Soldier s = ParseFromDataReader<Soldier>(reader);
                    MapSoldier.Add(s.ID, s);
                }
            }
        }

        private void LoadChapterData()
        {
            MapChapter = new Dictionary<int, Chapter>();
            string sql = "select * from ChapterConfig";
            using (SQLiteDataReader reader = SQLiteHelper.Instance.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    Chapter c = ParseFromDataReader<Chapter>(reader);
                    MapChapter.Add(c.ID, c);
                }
            }
        }

        private void LoadLevelData()
        {
            MapLevel = new Dictionary<int, Level>();
            string sql = "select * from LevelConfig";
            using (SQLiteDataReader reader = SQLiteHelper.Instance.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    Level l = ParseFromDataReader<Level>(reader);
                    MapLevel.Add(l.ID, l);
                }
            }
        }

        private void LoadExperienceData()
        {
            MapExperience = new Dictionary<int, Experience>();
            string sql = "select * from ExperienceConfig";
            using (SQLiteDataReader reader = SQLiteHelper.Instance.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    Experience e = ParseFromDataReader<Experience>(reader);
                    MapExperience.Add(e.ID, e);
                }
            }
        }

        private void LoadArmorData()
        {
            MapArmor = new Dictionary<int, Armor>();
            string sql = "select * from ArmorConfig";
            using (SQLiteDataReader reader = SQLiteHelper.Instance.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    Armor a = ParseFromDataReader<Armor>(reader);
                    MapArmor.Add(a.ID, a);
                }
            }
        }

        private void LoadItemData()
        {
            MapItem = new Dictionary<int, Item>();
            string sql = "select * from ItemConfig";
            using (SQLiteDataReader reader = SQLiteHelper.Instance.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    Item i = ParseFromDataReader<Item>(reader);
                    MapItem.Add(i.ID, i);
                }
            }
        }

        private void LoadArmorMaterialData()
        {
            MapArmorMaterial = new Dictionary<int, ArmorMaterial>();
            string sql = "select * from ArmorMaterialConfig";

            using (SQLiteDataReader reader = SQLiteHelper.Instance.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    ArmorMaterial a = ParseFromDataReader<ArmorMaterial>(reader);
                    MapArmorMaterial.Add(a.ID, a);
                }
            }
        }

        private void LoadSoldierMaterialData()
        {
            MapSoldierMaterial = new Dictionary<int, SoldierMaterial>();
            string sql = "select * from SoldierMaterialConfig";
            using (SQLiteDataReader reader = SQLiteHelper.Instance.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    SoldierMaterial s = ParseFromDataReader<SoldierMaterial>(reader);
                    MapSoldierMaterial.Add(s.ID, s);
                }
            }
        }

        private void LoadNobilityData()
        {
            MapNobility = new Dictionary<int, Nobility>();
            string sql = "select * from NobilityConfig";
            using (SQLiteDataReader reader = SQLiteHelper.Instance.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    Nobility s = ParseFromDataReader<Nobility>(reader);
                    MapNobility.Add(s.ID, s);
                }
            }
        }

        private void LoadTaskData()
        {
            MapTask = new Dictionary<int, Task>();
            string sql = "select * from TaskConfig";
            using (SQLiteDataReader reader = SQLiteHelper.Instance.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    Task s = ParseFromDataReader<Task>(reader);
                    MapTask.Add(s.ID, s);
                }
            }
        }

        private void LoadSkillData()
        {
            MapSkill = new Dictionary<int, Skill>();
            string sql = "select * from SkillConfig";
            using (SQLiteDataReader reader = SQLiteHelper.Instance.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    Skill s = ParseFromDataReader<Skill>(reader);
                    MapSkill.Add(s.ID, s);
                }
            }
        }

        /// <summary>
        /// 保存修改过的武将数据
        /// </summary>
        public int SaveGeneralData()
        {
            string cmd = String.Empty;
            foreach (KeyValuePair<int, General> pair in MapGeneral)
            {
                string sql = String.Format(@"update GeneralConfig set HP={1},AttackPower={2},DefensePower={3},
                    HPGrowth={4},ATKGrowth={5},DEFGrowth={6} where ID = {0};",
                    pair.Key,pair.Value.HP, pair.Value.AttackPower, pair.Value.DefensePower,
                    pair.Value.HPGrowth, pair.Value.ATKGrowth, pair.Value.DEFGrowth);

                cmd += sql;
            }

            return SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);
        }

        public int SaveLevelEnemyData(int levelID,Dictionary<int,NPCEnemy> enemyList)
        {
            string enemyStr = String.Empty;
            foreach (NPCEnemy enemy in enemyList.Values)
            {
                string fString = String.Format("{0},{1},{2},{3},{4},{5};",enemy.Position,enemy.GeneralConfigID,
                    enemy.GeneralLevel,enemy.SoldierConfigID,enemy.SoldierLevel,enemy.SoldierCount);

                enemyStr += fString;
            }

            string cmd = String.Format(@"update LevelConfig set Enemy = '{1}' where ID = {0};",
                levelID, enemyStr);
            MapLevel[levelID].Enemy = enemyStr;

            return SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);
        }

        public int SaveLevelGeneralEXPReward()
        {
            string cmd = String.Empty;
            foreach (KeyValuePair<int, Level> pair in MapLevel)
            {
                string sql = String.Format(@"update LevelConfig set GeneralExpReward ={1} where ID = {0};",
                    pair.Key,pair.Value.GeneralExpReward);

                cmd += sql;
            }

            return SQLiteHelper.Instance.ExecuteNonQuery(cmd, null);

        }
    }
}
