using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public static class Formula
    {
        // 计算武将战斗力
        public static int ComputeBattlePowerPoint(GeneralInfo gInfo, SoldierInfo sInfo)
        {
            double point = 0;

            point = gInfo.HP * 0.038 + gInfo.ATK * 0.06 + gInfo.DEF * 0.17 + gInfo.CriticalRate * 1.9 + gInfo.DodgeRate * 1.3 + gInfo.HitRate * 1.4;

            // 加上部队
            point += (sInfo.HP * 0.024 + sInfo.ATK * 0.09 + sInfo.DEF * 0.13) * sInfo.SoldierCount;

            // 加上装备

            return (int)point;
        }

        /// <summary>
        /// 模拟队伍1是否取胜
        /// </summary>
        /// <param name="f1"></param>
        /// <param name="f2"></param>
        /// <returns></returns>
        public static bool ComputeTeamOneWin(Formation f1, Formation f2)
        {
            if (f1.TeamBattlePowerPoint / 2 > f2.TeamBattlePowerPoint)
                return true;
            else if (f1.TeamBattlePowerPoint * 2 < f2.TeamBattlePowerPoint)
                return false;

            double winPossiblity = 0.9;

            int battlePointGap = f1.TeamBattlePowerPoint - f2.TeamBattlePowerPoint;
            if (battlePointGap > 0) // f1 > f2
            {
                winPossiblity += battlePointGap / (10 * f2.TeamBattlePowerPoint); // = 0.9 + (A-B)/10B
            }
            else // f1 < f2
            {
                winPossiblity += battlePointGap / (0.9 * f2.TeamBattlePowerPoint);
            }

            Random r = new Random();
            return r.NextDouble() <= winPossiblity;
        }
    }
}
