using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication1
{
    public class JsonHelper
    {
        public static string GetJson<T>(T obj)
        {
            DataContractJsonSerializer json = new DataContractJsonSerializer(obj.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, obj);
                string szJson = Encoding.UTF8.GetString(stream.ToArray());
                return szJson;
            }
        }

        public static T ParseFromJson<T>(string szJson)
        {
            T obj = Activator.CreateInstance<T>();
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes((szJson))))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
                return (T)serializer.ReadObject(ms);
            }
        }

        public static void GetJsonStringArray<T>(string fileName, ref List<T> list)
        {
            Regex r = new Regex("(?<={)[^{}]*");

            string s = File.ReadAllText(fileName);
            foreach(Match m in r.Matches(s))
            {
                string jsonString = "{" + m.ToString() + "}";
                list.Add(ParseFromJson<T>(jsonString));
            }
        }
    }
}
