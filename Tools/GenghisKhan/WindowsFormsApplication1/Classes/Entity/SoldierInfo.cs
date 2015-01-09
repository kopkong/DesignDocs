using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class SoldierInfo
    {
        public SoldierInfo() { }

        public Soldier SoldierConfig{get;set;}

        public int Level { get; set; }

        public int Rank { get; set; }

        public int SoldierCount { get; set; }

        public int HP
        {
            get
            {
                return (int)((SoldierConfig.HP + SoldierConfig.HPGrowth * Level) * (Rank +1)* SoldierConfig.HPRankRate);
            }
        }

        public int ATK
        {
            get
            {
                return (int)((SoldierConfig.AttackPower + SoldierConfig.ATKGrowth * Level) * (Rank + 1) * SoldierConfig.ATKRankRate);
            }

        }

        public int DEF
        {
            get
            {
                return (int)((SoldierConfig.DefensePower + SoldierConfig.DEFGrowth * Level) * (Rank + 1) * SoldierConfig.ATKRankRate);
            }
        }
    }
}
