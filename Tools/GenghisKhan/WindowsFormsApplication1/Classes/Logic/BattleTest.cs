using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public static class BattleTest
    {
        //基础测试组
        public static Dictionary<string, int[]> BasicRandomTypeTable = new Dictionary<string, int[]>()
        {
            {"普通型", new int[]   {1,100,100,100} }, // 300
            {"血牛型", new int[]   {1,130,95,90}   }, // 315
            {"攻击型", new int[]   {1,90,115,95}  }, // 300
            {"狂暴型", new int[]   {1,85,125,80}   }, // 290
            {"防御型", new int[]   {1,110,90,110} },  // 310
            {"铁盾型", new int[]   {1,100,90,130} },   // 320
        };

        const int BASIC_TYPE_COUNT = 6;
        static string[] SOLIDER_NAME_ARRAY = new string[]{ "", "步兵", "骑兵", "弓兵" };

        private static void adjustAttribute(ref MainAttribute attr,int typeIndex)
        {
            double hpPercent = 1.0 * BasicRandomTypeTable.ElementAt(typeIndex).Value[(int)ATTRIBUTEINDEX.HP] / 100.0;
            double atkPercent = 1.0 * BasicRandomTypeTable.ElementAt(typeIndex).Value[(int)ATTRIBUTEINDEX.ATK] / 100.0;
            double defPercent = 1.0 * BasicRandomTypeTable.ElementAt(typeIndex).Value[(int)ATTRIBUTEINDEX.DEF] / 100.0;

            attr.HP = (int)(attr.HP * hpPercent);
            attr.ATK = (int)(attr.ATK * atkPercent);
            attr.DEF = (int)(attr.DEF * defPercent);

            attr.HP_GROWTH = (int)(attr.HP_GROWTH * hpPercent);
            attr.ATK_GROWTH = (int)(attr.ATK_GROWTH * atkPercent);
            attr.DEF_GROWTH = (int)(attr.DEF_GROWTH * defPercent);
        }

        /// <summary>
        /// 获得基础测试组单位
        /// </summary>
        public static List<General> getBasicTestGroupUnit()
        {
            List<General> testGroup = new List<General>();

            for (int i = 0; i < BASIC_TYPE_COUNT; i++)
            {
                int id = getBasicTestID(i);

                string name = String.Format("{0}", BasicRandomTypeTable.ElementAt(i).Key);
                int soldierType = BasicRandomTypeTable.ElementAt(i).Value[(int)ATTRIBUTEINDEX.SOLDIERTYPE];

                MainAttribute attr = Formula.BasicMainAttributeWithSoldierType(soldierType);
                adjustAttribute(ref attr, i);

                General config = EntityFactory.GenerateGeneral(id, name, soldierType, attr);
                testGroup.Add(config);
            }

            return testGroup;
        }

        public static List<General> getBasicSoldierTestGroupUnit()
        {
            List<General> testGroup = new List<General>();

            for (int i = 0; i < BASIC_TYPE_COUNT; i++)
            {
                for(int soldierType = 1;soldierType<=3; soldierType++)
                {
                    // 去掉防御和血牛类型的弓兵
                    if ((i == 1 || i == 4 || i == 5) && soldierType == 3) continue;

                    int id = getBasicTestID(soldierType * 100 + i);
                    string name = String.Format("{0}{1}", BasicRandomTypeTable.ElementAt(i).Key, SOLIDER_NAME_ARRAY[soldierType]);

                    MainAttribute attr = Formula.BasicMainAttributeWithSoldierType(soldierType);
                    adjustAttribute(ref attr, i);

                    General config = EntityFactory.GenerateGeneral(id, name, soldierType, attr);
                    testGroup.Add(config);
                }
                
            }

            return testGroup;
        }

        /// <summary>
        /// 获得基础测试组的ID
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static List<int> getTestGroupID()
        {
            //List<int> list = new List<int>();

            IEnumerable<int> list = from config in DBConfigMgr.Instance.MapGeneral.Values
                                    where config.ID > 10000
                                    select config.ID;

            return list.ToList();
        }

        private static int getBasicTestID(int offset)
        {
            return 10001 + offset;
        }

        /// <summary>
        /// 获取3星武将测试表
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static List<int> getActualGeneralTestGroupID(int index)
        {
            // 1 步兵组
            // 2 骑兵组
            // 3 弓兵组
            // 4 混合组

            List<int> list = new List<int>();
           
            if (index >= 1 && index <= 3)
            {
                 IEnumerable<int> list2 = from config in DBConfigMgr.Instance.MapGeneral.Values
                       where config.SoldierType == index && config.Star == 3
                       select config.ID;

                 return list2.ToList();
            }

            for (int i = 57; i <= 122; i++)
            {
                list.Add(i);
            }

            return list;
        }
    }
}
