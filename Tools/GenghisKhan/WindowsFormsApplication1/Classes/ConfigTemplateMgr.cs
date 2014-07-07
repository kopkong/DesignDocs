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
            string sql = "select * from General";
            using (SQLiteDataReader reader = SQLiteHelper.Instance.ExecuteReader(sql, null))
            {
                while (reader.Read())
                {
                    General g = ParseFromDataReader<General>(reader);
                    MapGeneral.Add(g.ID, g);
                }
            }

        }

        


    }
}
