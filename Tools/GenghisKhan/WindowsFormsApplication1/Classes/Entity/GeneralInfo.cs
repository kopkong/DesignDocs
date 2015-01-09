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

        public int Exp { get; set; }

        public int SlotIndex { get; set; }

        public int SoldierConfigID { get; set; }

        public int SoldierCount { get; set; }

        public int Skill1 { get; set; }

        public int Skill2 { get; set; }

        public int HP
        {
            get
            {
                return (int)((GeneralConfig.HP + GeneralConfig.HPGrowth * Level) * (Rank + 1) * GeneralConfig.HPRankRate);
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

        public void AddExp(int exp)
        {
            if (exp > 0)
            {
                this.Exp += exp;

                int maxEXPAvailable = DBConfigMgr.Instance.MapExperience[PlayerDataMgr.Instance.GetPlayer().Lv].GeneralEnd - 1;

                if(this.Exp >= maxEXPAvailable)
                {
                    this.Exp = maxEXPAvailable;
                    Console.WriteLine("武将等级不能超过玩家等级!因此该武将无法继续获得经验");
                }

                // 同步数据
                PlayerDataMgr.Instance.GetPlayerBag(SlotType.SlotType_General)[SlotIndex].ExtraData2 = this.Exp;

                if (Exp >= DBConfigMgr.Instance.MapExperience[Level].GeneralEnd)
                {
                    Level++;

                    // 同步slot数据
                    PlayerDataMgr.Instance.GetPlayerBag(SlotType.SlotType_General)[SlotIndex].Lv = Level;
                }
            }

        }

    }
}
