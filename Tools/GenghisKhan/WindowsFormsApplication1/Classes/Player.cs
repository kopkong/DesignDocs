using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class PlayerInfo
    {
        public PlayerInfo()
        {
            Name = "大孔叔叔测试账号";
            Lv = 1;
            EXP = 0;
            Coin = 0;
            Diamonds = 0;
            TotalConsumeDiamonds = 0;
            HeroJade = 0;
            Honor = 0;
            Fame = 0;
            Energy = 120;
            SoulJade = 0;
        }

        public string Name { get; set; }

        public int Lv { get; set; }

        public int VIPLevele { get; set; }

        public int EXP { get; set; }

        public int Coin { get; set; }

        public int SoulJade { get; set; }

        public int Diamonds { get; set; }

        public int NobleRanks { get; set; }

        public int TotalConsumeDiamonds { get; set; }

        public int HeroJade { get; set; }

        public int Honor { get; set; }

        public int Fame { get; set; }

        public int Energy { get; set; }

        /// <summary>
        /// 增加经验
        /// </summary>
        /// <param name="exp"></param>
        public void AddExp(int exp)
        {
            if (exp > 0)
            {
                this.EXP += exp;

                //  判断是否升级
                if (EXP >= DBConfigMgr.Instance.MapExperience[Lv].PlayerEnd)
                {
                    Lv++;
                }
            }

        }
    }
}
