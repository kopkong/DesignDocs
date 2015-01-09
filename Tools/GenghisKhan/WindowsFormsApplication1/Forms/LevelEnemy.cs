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
    public partial class LevelEnemy : Form
    {
        public LevelEnemy()
        {
            InitializeComponent();

            CanReComputeBattlePoint = false;
        }

        int currentEditButtonID = -1;

        public int LevelID { get; set; }

        public bool IsEliteLevel { get; set; }

        public bool CanReComputeBattlePoint { get; set; }

        public Formation EnemyFormation { get; set; }

        private Dictionary<int,NPCEnemy> EnemyList = new Dictionary<int,NPCEnemy>();

        public void InitLevelEnemy(int levelID,bool isElite)
        {
            IsEliteLevel = isElite;

            string name = DBConfigMgr.Instance.MapLevel[levelID].Name;
            if (isElite)
                name += "精英";
            this.Text = name;

            EnemyFormation = new Formation();
            EnemyFormation.InitNPCFormation(levelID,isElite);

            LevelID = levelID;
            EnemyList = EnemyFormation.NPCFormation;
            EnemyList.OrderBy(x => x.Key);

            RefreashEnemyData();
            ReComputeBattlePoint();
            GetReferBattlePoint();

            CanReComputeBattlePoint = true;
        }

        private void RefreashEnemyData()
        {
            foreach (KeyValuePair<int, NPCEnemy> pair in EnemyList)
            {
                string buttonName = "BTN_" + pair.Key.ToString();

                Button b = (Button)this.Controls.Find(buttonName,false)[0];
                SetButtonString(b, pair.Value);
            }
        }

        private void SetButtonString(Button b, NPCEnemy npc)
        {
            string generalName = DBConfigMgr.Instance.MapGeneral[npc.GeneralConfigID].Name;

            string generalLv = npc.GeneralLevel.ToString();

            b.Text = string.Format("｛{0}｝-{1}级", generalName, generalLv); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CampaignBatch.RefreshOneLevelEnemy(LevelID, EnemyList, IsEliteLevel);
            DBConfigMgr.Instance.UpdateLevelEnemy(LevelID);

            //MessageBox.Show("保存完毕");
            this.Close();
        }

        private void ReComputeBattlePoint()
        {
            EnemyFormation.NPCFormation = EnemyList;
            EnemyFormation.ReComputeNPCTeamBattlePowerPoint();
            LB_RealBattlePoint.Text = EnemyFormation.TeamBattlePowerPoint.ToString();
        }

        /// <summary>
        /// 关卡难度的参考值
        /// </summary>
        private void GetReferBattlePoint()
        {
            int refLevel = IsEliteLevel ? DBConfigMgr.Instance.MapLevel[LevelID].EliteRefLevel :
                DBConfigMgr.Instance.MapLevel[LevelID].RefLevel;

            int refBattlePoint = Formula.GetRefBattlePoint(refLevel);

            LB_ReferLevel.Text = refLevel.ToString();
            LB_RefSquads.Text = Formula.GetOnBattleSquads(refLevel).ToString() ;
            LB_RefBattlePoint.Text = refBattlePoint.ToString();
        }

        private void SetTextBoxString()
        {
            if (currentEditButtonID < 0 || currentEditButtonID >= 20)
                return;

            if (EnemyList.ContainsKey(currentEditButtonID))
            {
                textBox1.Text = EnemyList[currentEditButtonID].BuildString();
            }
            else
                textBox1.Text = "";

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string buttonName = "BTN_" + currentEditButtonID.ToString();
            Button b = (Button)this.Controls.Find(buttonName, false)[0];

            if (textBox1.Text == "")
            {
                EnemyList.Remove(currentEditButtonID);
                b.Text = "";
            }
            else
            {
                string completStr = currentEditButtonID.ToString() + "," + textBox1.Text;
                if (EnemyList.ContainsKey(currentEditButtonID))
                    EnemyList[currentEditButtonID].SetValueByString(completStr);
                else
                    EnemyList.Add(currentEditButtonID, new NPCEnemy(completStr));

                SetButtonString(b, EnemyList[currentEditButtonID]);
            }

            ReComputeBattlePoint();
        }

        #region BTN_CLICK
        private void BTN_0_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 0;
            SetTextBoxString();
        }

        private void BTN_4_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 4;
            SetTextBoxString();
        }

        private void BTN_1_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 1;
            SetTextBoxString();
        }

        private void BTN_2_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 2;
            SetTextBoxString();
        }

        private void BTN_3_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 3;
            SetTextBoxString();
        }

        private void BTN_5_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 5;
            SetTextBoxString();
        }

        private void BTN_6_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 6;
            SetTextBoxString();
        }

        private void BTN_7_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 7;
            SetTextBoxString();
        }

        private void BTN_8_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 8;
            SetTextBoxString();
        }

        private void BTN_9_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 9;
            SetTextBoxString();
        }

        private void BTN_10_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 10;
            SetTextBoxString();
        }

        private void BTN_11_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 11;
            SetTextBoxString();
        }

        private void BTN_12_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 12;
            SetTextBoxString();
        }

        private void BTN_13_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 13;
            SetTextBoxString();
        }

        private void BTN_14_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 14;
            SetTextBoxString();
        }

        private void BTN_15_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 15;
            SetTextBoxString();
        }

        private void BTN_16_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 16;
            SetTextBoxString();
        }

        private void BTN_17_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 17;
            SetTextBoxString();
        }

        private void BTN_18_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 18;
            SetTextBoxString();
        }

        private void BTN_19_Click(object sender, EventArgs e)
        {
            currentEditButtonID = 19;
            SetTextBoxString();
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            ResetLevel(true);

            ReComputeBattlePoint();
        }

        public void ResetLevel(bool updateControl)
        {
            int refLevel = IsEliteLevel ? DBConfigMgr.Instance.MapLevel[LevelID].EliteRefLevel :
                DBConfigMgr.Instance.MapLevel[LevelID].RefLevel;

            foreach (KeyValuePair<int, NPCEnemy> pair in EnemyList)
            {
                pair.Value.GeneralLevel = refLevel;
                pair.Value.SoldierLevel = refLevel;

                if (updateControl)
                {
                    string buttonName = "BTN_" + pair.Key.ToString();
                    Button b = (Button)this.Controls.Find(buttonName, false)[0];
                    SetButtonString(b, pair.Value);
                }
            }
        }
    }
}
