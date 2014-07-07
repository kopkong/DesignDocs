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
                    Object v = Convert.ChangeType(reader[pI.Name], type);
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

    }
}
