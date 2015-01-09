using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Forms
{
    public partial class MoneySimu : Form
    {
        public MoneySimu()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            CB_AngelSKill.Checked = true;
            CB_ArmorUp.Checked = true;
            CB_SkillUp.Checked = true;
            CB_SoldierUp.Checked = true;
            CB_SoldierCount.Checked = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("等级,升级士兵,升级装备,升级技能,升级天使技能,升级士兵数量,总花费,总收入");

            int destLevel = (int)NUD_Level.Value;
            for(int level = 1; level <= destLevel ; level ++ )
            {
                calculateMoney(level, ref sb);
            }

            Utility.WriteText(sb, "money.csv");
        }

        private int dailyMoney()
        {
            IEnumerable<int> dailyTaskMoneyList = from config in DBConfigMgr.Instance.MapTask.Values
                                    where config.Type == 2
                                    select config.MoneyReward;
            int dailyMoney = dailyTaskMoneyList.Sum();

            return dailyMoney;
        }

        private int levelMoney(int level)
        {
            int chapterid = (int) (level / 3) + 1;
            int lastLevelIndex = chapterid == 1?6:10;

            int levelID = CampaignBatch.GetLevelIDFromChapter(chapterid,lastLevelIndex);
            int battleCount = (DBConfigMgr.Instance.MapExperience[level].PlayerEnd -
                DBConfigMgr.Instance.MapExperience[level].PlayerStart) / 6;

            return DBConfigMgr.Instance.MapLevel[levelID].MoneyReward * battleCount;
        }

        private int gainMoney(int destLevel)
        {
            int money = 0;
            for (int i = 1; i <= destLevel; i++)
            {
                money += levelMoney(destLevel);
            }

            return money;
        }

        private int calculateSoldierCountMoney(int destLevel)
        {
            return 0;
        }

        private int calculateAllAngelSkillMoney(int destLevel)
        {
            int money = 0;

            for (int level = 2; level <= destLevel; level= level +2)
            {
                int angelSkill1Level = level / 2;
                int angelSkill2Level = (level - 20) / 2 + 1;
                int angelSkill3Level = (level - 40) / 2 + 1;
                int angelSkill4Level = (level - 60) / 2 + 1;

                money += Formula.UpgradeAngelSkillCostMoney(1, angelSkill1Level);
                money += angelSkill2Level >= 1 ? Formula.UpgradeAngelSkillCostMoney(2, angelSkill2Level) : 0;
                money += angelSkill3Level >= 1 ? Formula.UpgradeAngelSkillCostMoney(3, angelSkill3Level) : 0;
                money += angelSkill4Level >= 1 ? Formula.UpgradeAngelSkillCostMoney(4, angelSkill4Level) : 0;
            }

            return money;
        }

        private int calculateSoldierUpMoney(int destLevel)
        {
            int money = 0;
            for (int level = 1; level <= destLevel; level++)
            {
                money += Formula.UpgradeCostMoney(3, level);
            }

            return money;
        }

        private int calculateArmorUpMoney(int destLevel)
        {
            int money = 0;
            for (int level = 1; level <= destLevel; level++)
            {
                money += Formula.UpgradeCostMoney(1, level);
            }

            return money;
        }

        private int calculateSkillUpMoney(int destLevel)
        {
            int money = 0;
            for (int level = 1; level <= destLevel; level++)
            {
                money += Formula.UpgradeCostMoney(2, level);
            }

            return money;
        }

        private void calculateMoney(int destLevel,ref StringBuilder sb)
        {
            int angelMoney = 0, soldierMoney = 0, armorMoney = 0, skillMoney = 0, soldierCountMoney = 0;
            int generalCount = Formula.GetOnBattleSquads(destLevel);
            TB_Output.Text = String.Format("升到{0}级需要练{1}个武将" + Environment.NewLine, destLevel, generalCount);

            if (CB_AngelSKill.Checked) angelMoney = calculateAllAngelSkillMoney(destLevel);

            if(CB_SoldierUp.Checked) soldierMoney = calculateSoldierUpMoney(destLevel) * generalCount;

            if (CB_ArmorUp.Checked) armorMoney = calculateArmorUpMoney(destLevel) * generalCount * 3;

            if (CB_SkillUp.Checked) skillMoney = calculateSkillUpMoney(destLevel) * generalCount * 2;

            if (CB_SoldierCount.Checked) soldierCountMoney = calculateSoldierCountMoney(destLevel) * generalCount;

            int allMoney = angelMoney + soldierMoney + armorMoney + skillMoney + soldierCountMoney;
            TB_Output.Text += String.Format("升级到{0}级，所需消耗的金币为{1}",
                destLevel, allMoney) + Environment.NewLine;

            sb.AppendLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7}", destLevel, soldierMoney, armorMoney, skillMoney, angelMoney, soldierCountMoney, allMoney, gainMoney(destLevel)));

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dictionary<int, int> dayLevelRefTable = new Dictionary<int, int>()
            {
                {1, 15},{2, 20},{3, 22},{4, 24},{5, 26},{6, 28},{7, 30},{14, 40},
                {21, 46},{28, 50},{60, 58},{90, 65},{120, 71},{150, 76},{180, 80}
            };

            
        }
    }
}
