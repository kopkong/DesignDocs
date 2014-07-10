using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Forms
{
    public partial class UCChapters : UserControl
    {
        public UCChapters()
        {
            InitializeComponent();
        }

        UCFormation playerFormationControl;
        UCFormation npcFormationControl;

        private Formation teamOneFormation;
        private Formation teamTwoFormation;
        private int currentSelectedLevelID;
        private int currentSelectedChapterID;

        public void InitChapter()
        {
            // Init
            dataGridView1.Rows.Clear();

            foreach (int i in DBConfigMgr.Instance.MapChapter.Keys)
            {
                AddChapterRow(i);
            }

            groupBox1.Visible = false;
        }

        private void AddChapterRow(int index)
        {
            Chapter chapter = DBConfigMgr.Instance.MapChapter[index];

            DataGridViewRow row = new DataGridViewRow();
            DataGridViewTextBoxCell textBoxID = new DataGridViewTextBoxCell();
            textBoxID.Value = chapter.ID;
            row.Cells.Add(textBoxID);

            DataGridViewTextBoxCell textboxName = new DataGridViewTextBoxCell();
            textboxName.Value = chapter.Name;
            row.Cells.Add(textboxName);

            DataGridViewTextBoxCell textboxIsPassed = new DataGridViewTextBoxCell();
            textboxIsPassed.Value = PlayerDataMgr.Instance.IsThisChapterPlayerFinished(index);
            row.Cells.Add(textboxIsPassed);

            dataGridView1.Rows.Add(row);
        }

        private void AddLevelRow(int index)
        {
            Level levelConfig = DBConfigMgr.Instance.MapLevel[index];
            LevelRecord record = PlayerDataMgr.Instance.GetPlayerLevelRecords()[index];

            DataGridViewRow row = new DataGridViewRow();

            DataGridViewTextBoxCell textBoxID = new DataGridViewTextBoxCell();
            textBoxID.Value = levelConfig.ID;
            row.Cells.Add(textBoxID);

            DataGridViewTextBoxCell textBoxName = new DataGridViewTextBoxCell();
            textBoxName.Value = levelConfig.Name;
            row.Cells.Add(textBoxName);

            DataGridViewTextBoxCell textTodayTimes = new DataGridViewTextBoxCell();
            textTodayTimes.Value = record.Times;
            row.Cells.Add(textTodayTimes);

            DataGridViewTextBoxCell textBoxPassed = new DataGridViewTextBoxCell();
            textBoxPassed.Value = record.Passed;
            row.Cells.Add(textBoxPassed);

            DataGridViewTextBoxCell textBoxHighestStar = new DataGridViewTextBoxCell();
            textBoxHighestStar.Value = record.HighestStar;
            row.Cells.Add(textBoxHighestStar);

            dataGridView2.Rows.Add(row);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index < 0)
                return;

            int selectedID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            currentSelectedChapterID = selectedID;

            RefreshLevelData();
        }

        private void RefreshLevelData()
        {
            IEnumerable<KeyValuePair<int, Level>> chapterLevelIDs = DBConfigMgr.Instance.MapLevel.Where(x => x.Value.ChapterID == currentSelectedChapterID);

            dataGridView2.Rows.Clear();
            foreach (KeyValuePair<int, Level> i in chapterLevelIDs)
            {
                AddLevelRow(i.Key);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Console.WriteLine("当前选择了{0}行",dataGridView2.SelectedRows.Count);
            int selectedID = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);
            currentSelectedLevelID = selectedID;
            Console.WriteLine("选择了关卡ID {0}", selectedID);

            if (PlayerDataMgr.Instance.GetPlayerLevelRecords()[currentSelectedLevelID].Locked)
            {
                MessageBox.Show("本关还没有解锁，不能挑战");
                return;
            }

            panel1.Controls.Clear();
            panel2.Controls.Clear();

            playerFormationControl = new UCFormation();
            playerFormationControl.InitPlayerFormation();
            panel1.Controls.Add(playerFormationControl);
            teamOneFormation = playerFormationControl.CurrentFormation;

            npcFormationControl = new UCFormation();
            npcFormationControl.InitNPCFormation(selectedID);
            panel2.Controls.Add(npcFormationControl);
            teamTwoFormation = npcFormationControl.CurrentFormation;
            
            groupBox1.Visible = true;
        }

        private void BTN_StartBattle_Click(object sender, EventArgs e)
        {
            bool win = Formula.ComputeTeamOneWin(teamOneFormation,teamTwoFormation);

            string message = win?"挑战成功":"挑战失败";
            MessageBox.Show(message);

            // 应该给奖励以及扣除体力点数
            if (win)
            {
                PlayerInfo pInfo = PlayerDataMgr.Instance.GetPlayer();
                pInfo.Coin += DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].MoneyReward;
                pInfo.AddExp(10);
                pInfo.Energy -= 6;

                PlayerDataMgr.Instance.PlayerFinishedLevel(currentSelectedLevelID, 1);
                RefreshLevelData();

                foreach (GeneralInfo gInfo in teamOneFormation.FormationMap.Keys)
                {
                    gInfo.AddExp(DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].GeneralExpReward);
                }
                playerFormationControl.RefreshBattlePowerPoint();

                MainForm main = (MainForm)FindForm();
                main.UpdatePlayerInfo();
            }
        }
    }
}
