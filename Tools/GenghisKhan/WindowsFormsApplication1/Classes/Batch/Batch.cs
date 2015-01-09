using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    enum ATTRIBUTEINDEX
    {
        SOLDIERTYPE    = 0,
        HP,
        ATK,
        DEF
    };

    public static class Batch
    {
        public static MainAttribute BASIC_ATTRIBUTE = Formula.BasicMainAttribute();
        public static MainAttribute FOORMAN_BASIC_ATTRIBUTE = Formula.BasicMainAttributeWithSoldierType(1);
        public static MainAttribute CHIVALRY_BASIC_ATTRIBUTE = Formula.BasicMainAttributeWithSoldierType(2);
        public static MainAttribute ARCHER_BASIC_ATTRIBUTE = Formula.BasicMainAttributeWithSoldierType(3);
        public static MainAttribute[] SOLDIER_BASIC_ATTRIBUTE = { BASIC_ATTRIBUTE, FOORMAN_BASIC_ATTRIBUTE, CHIVALRY_BASIC_ATTRIBUTE, ARCHER_BASIC_ATTRIBUTE };

        public static int NATIONALITY_NONE = 0;
        public static int NATIONALITY_MONGOLIA = 1;
        public static int NATIONALITY_CHINA = 2;
        public static int NATIONALITY_ARABIC = 3;
        public static int NATIONALITY_EUROPE = 4;
    }
}
