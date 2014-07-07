using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class GeneralInfo
    {
        public GeneralInfo() { }

        public General GeneralConfig{get;set;}

        public int Level { get; set; }

        public int Rank { get; set; }

        public int SoldierConfigID { get; set; }

        public int SoldierCount { get; set; }

        public int HP
        {
            get
            {
                return (int)((GeneralConfig.HP + GeneralConfig.HPGorwth * Level) * (Rank + 1) * GeneralConfig.HPRankRate);
            }
        }

        public int ATK
        {
            get
            {
                return (int)((GeneralConfig.AttackPower + GeneralConfig.ATKGrowth * Level) * (Rank + 1) * GeneralConfig.ATKRankRate);
            }

        }

        public int DEF
        {
            get
            {
                return (int)((GeneralConfig.DefensePower + GeneralConfig.DEFGrowth * Level) * (Rank +1) * GeneralConfig.ATKRankRate);
            }
        }

        public double DodgeRate
        {
            get
            {
                return GeneralConfig.DodgeRate / 100.0;
            }
        }

        public double HitRate
        {
            get
            {
                return GeneralConfig.HitRate / 100.0;
            }
        }

        public double CriticalRate
        {
            get
            {
                return GeneralConfig.CriticalRate / 100.0;
            }
        }

    }
}
