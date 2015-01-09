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

    public partial class LevelReward : Form
    {
        public LevelReward()
        {
            InitializeComponent();
        }

        public int LevelID { get; set; }

        public bool IsElite { get; set; }

        public ItemPack Reward1 { get; set; }

        public ItemPack Reward2 { get; set; }

        public void Init(int levelID,bool isElite)
        {
            this.LevelID = levelID;
            this.IsElite = isElite;
            this.Text = String.Format("{0}({1})的奖励", DBConfigMgr.Instance.MapLevel[levelID].Name,isElite?"精英":"普通");

            CB_Type1.Items.Add("没有");
            CB_Type2.Items.Add("没有");
            CB_Count1.Value = 1;
            CB_Count2.Value = 2;

            foreach (AllConfig config in DBConfigMgr.Instance.MapAllRewardTypes.Values)
            {
                CB_Type1.Items.Add(config.Name);
                CB_Type2.Items.Add(config.Name);
            }

            Level levelConfig = DBConfigMgr.Instance.MapLevel[levelID];
            string[] rewards = levelConfig.ClientShowRewards.Split(';');
            if(isElite)
                rewards = levelConfig.EliteClientShowRewards.Split(';');


            // 赋值
            TB_Money.Text = isElite?levelConfig.EliteMoneyReward.ToString():levelConfig.MoneyReward.ToString();
            TB_EXP.Text = isElite ? levelConfig.EliteGeneralExpReward.ToString() : levelConfig.GeneralExpReward.ToString();

            if (rewards.Length > 0)
            {
                Reward1 = new ItemPack(rewards[0]);

                if (rewards.Length > 1)
                {
                    Reward2 = new ItemPack(rewards[1]);
                }
            }

            if (Reward1 != null && Reward1.Type != 0)
            {
                CB_Type1.SelectedItem = DBConfigMgr.Instance.MapAllRewardTypes[Reward1.Type].Name;
                TB_ID1.Text = Reward1.ID.ToString();
                CB_Count1.Value = Reward1.Count;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TB_Money.Text.Length > 0)
            {
                int money = Convert.ToInt32(TB_Money.Text);
                DBConfigMgr.Instance.MapLevel[LevelID].MoneyReward = money;
            }

            if (TB_EXP.Text.Length > 0)
            {
                int exp = Convert.ToInt32(TB_EXP.Text);
                DBConfigMgr.Instance.MapLevel[LevelID].MoneyReward = exp;
            }

            string rewardString = string.Empty;
            if (CB_Type1.SelectedIndex > 0 && TB_ID1.Text.Length > 0 && CB_Count1.Value > 0)
            {
                Reward1.Type = getTypeIDbyName(CB_Type1.SelectedItem.ToString());
                Reward1.ID = Convert.ToInt32(TB_ID1.Text);
                Reward1.Count = (int)CB_Count1.Value;

                rewardString += String.Format("{0},{1},{2};",Reward1.Type,Reward1.ID,Reward1.Count);
            }

            if (CB_Type2.SelectedIndex > 0 && TB_ID2.Text.Length > 0 && CB_Count2.Value > 0)
            {
                Reward2.Type = getTypeIDbyName(CB_Type2.SelectedItem.ToString());
                Reward2.ID = Convert.ToInt32(TB_ID2.Text);
                Reward2.Count = (int)CB_Count2.Value;
                rewardString += String.Format("{0},{1},{2};");

                rewardString += String.Format("{0},{1},{2};", Reward2.Type, Reward2.ID, Reward2.Count);
            }

            if (IsElite)
                DBConfigMgr.Instance.MapLevel[LevelID].EliteClientShowRewards = rewardString;
            else
                DBConfigMgr.Instance.MapLevel[LevelID].ClientShowRewards = rewardString;

            DBConfigMgr.Instance.UpdateLevelReward(LevelID);

            this.Close();
        }

        private int getTypeIDbyName(string name)
        {
            int ret = 0;
            foreach (AllConfig config in DBConfigMgr.Instance.MapAllRewardTypes.Values)
            {
                if (config.Name == name)
                    return config.ID;
            }

            return ret;
        }

        private void TB_ID1_TextChanged(object sender, EventArgs e)
        {
            int type = getTypeIDbyName(CB_Type1.SelectedItem.ToString());

            if(type > 0)
            {
                int id = Convert.ToInt32(TB_ID1.Text);
                LB_Name1.Text = DBConfigMgr.Instance.GetNameByTypeConfigID(type, id);
            }
        }

        private void TB_ID2_TextChanged(object sender, EventArgs e)
        {
            int type = getTypeIDbyName(CB_Type2.SelectedItem.ToString());

            if (type > 0)
            {
                int id = Convert.ToInt32(TB_ID2.Text);
                LB_Name2.Text = DBConfigMgr.Instance.GetNameByTypeConfigID(type, id);
            }
        }
    }
}
