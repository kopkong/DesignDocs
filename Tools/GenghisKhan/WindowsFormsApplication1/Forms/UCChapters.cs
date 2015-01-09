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

        public int LastSelectedLevelRow { get; set; }

        //UCFormation playerFormationControl;
        UCFormation npcFormationControl = new UCFormation();
        UCFormation npcFormationControl2 = new UCFormation();

        private Formation teamOneFormation;
        private Formation teamTwoFormation;
        private int currentSelectedLevelID = 1;
        private int currentSelectedChapterID = 1;

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

            dataGridView1.Rows.Add(row);
        }

        private void AddLevelRow(int index)
        {
            Level levelConfig = DBConfigMgr.Instance.MapLevel[index];
            LevelRecord record = PlayerDataMgr.Instance.GetPlayerLevelRecords()[index];

            // 先计算好战斗力BattlePoint
            int normalRefPower = Formula.GetRefBattlePoint(levelConfig.RefLevel);
            int eliteRefPower = Formula.GetRefBattlePoint(levelConfig.EliteRefLevel);

            // 参考人数和实际人数
            int normalRefSquads = Formula.GetOnBattleSquads(levelConfig.RefLevel);
            int eliteRefSquads = Formula.GetOnBattleSquads(levelConfig.EliteRefLevel);

            Formation normalFormation = new Formation();
            normalFormation.InitNPCFormation(levelConfig.ID, false);
            int normalRealPower = normalFormation.TeamBattlePowerPoint;

            Formation eliteFormation = new Formation();
            eliteFormation.InitNPCFormation(levelConfig.ID, true);
            int eliteRealPower = eliteFormation.TeamBattlePowerPoint;

            int normalRealSquads = normalFormation.NPCFormation.Count();
            int eliteRealSquads = eliteFormation.NPCFormation.Count();
            DataGridViewRow row = new DataGridViewRow();

            DataGridViewTextBoxCell textBoxID = new DataGridViewTextBoxCell();
            textBoxID.Value = levelConfig.ID;
            row.Cells.Add(textBoxID);

            DataGridViewTextBoxCell textBoxName = new DataGridViewTextBoxCell();
            textBoxName.Value = levelConfig.Name;
            row.Cells.Add(textBoxName);

            DataGridViewTextBoxCell textBoxNormalBattleCompare = new DataGridViewTextBoxCell();
            textBoxNormalBattleCompare.Value = String.Format("{0}/{1}",normalRealPower,normalRefPower);
            row.Cells.Add(textBoxNormalBattleCompare);

            DataGridViewTextBoxCell textBoxNormalSquadsCompare = new DataGridViewTextBoxCell();
            textBoxNormalSquadsCompare.Value = String.Format("{0}/{1}", normalRealSquads, normalRefSquads);
            if (normalRefSquads != normalRealSquads)
                textBoxNormalSquadsCompare.Style.BackColor = Color.Red;
            row.Cells.Add(textBoxNormalSquadsCompare);

            DataGridViewTextBoxCell textBoxEliteBattleCompare = new DataGridViewTextBoxCell();
            textBoxEliteBattleCompare.Value = String.Format("{0}/{1}", eliteRealPower, eliteRefPower);
            row.Cells.Add(textBoxEliteBattleCompare);

            DataGridViewTextBoxCell textBoxEliteSquadsCompare = new DataGridViewTextBoxCell();
            textBoxEliteSquadsCompare.Value = String.Format("{0}/{1}", eliteRealSquads, eliteRefSquads);
            if( eliteRealSquads != eliteRefSquads)
                textBoxEliteSquadsCompare.Style.BackColor = Color.Red;
            row.Cells.Add(textBoxEliteSquadsCompare);

            DataGridViewTextBoxCell textBoxHasPlot = new DataGridViewTextBoxCell();
            string hasPlotBefore = "无";
            string hasPlotIn ="无";
            string hasPlotAfter ="无";
            if (DBConfigMgr.Instance.MapPlot.ContainsKey(index))
            {
                Plot p = DBConfigMgr.Instance.MapPlot[index];
                if (p.BeforeBattle.Length > 0)
                    hasPlotBefore = "有";

                if (p.InBattle.Length > 0)
                    hasPlotIn = "有";

                if (p.AfterBattle.Length > 0)
                    hasPlotAfter = "有";

                textBoxHasPlot.Style.ForeColor = Color.Green;
            }

            textBoxHasPlot.Value = String.Format("{0}/{1}/{2}", hasPlotBefore, hasPlotIn, hasPlotAfter);
            row.Cells.Add(textBoxHasPlot);

            DataGridViewTextBoxCell textBoxNormalRewards = new DataGridViewTextBoxCell();
            string hasRewards = "无";
            if (levelConfig.ClientShowRewards.Length > 0)
            {
                hasRewards = "有";
                textBoxNormalRewards.Style.ForeColor = Color.Green;
            }
            textBoxNormalRewards.Value = hasRewards;
            row.Cells.Add(textBoxNormalRewards);

            DataGridViewTextBoxCell textBoxEliteRewards = new DataGridViewTextBoxCell();
            hasRewards = "无";
            if (levelConfig.EliteClientShowRewards.Length > 0)
            {
                hasRewards = "有";
                textBoxEliteRewards.Style.ForeColor = Color.Green;
            }
            textBoxEliteRewards.Value = hasRewards;
            row.Cells.Add(textBoxEliteRewards);

            dataGridView2.Rows.Add(row);
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index < 0)
                return;

            int selectedID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            currentSelectedChapterID = selectedID;

            RefreshLevelData();

            currentSelectedLevelID = Convert.ToInt32(dataGridView2.Rows[0].Cells[0].Value);
            selectLevel();
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
            
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectedID = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);
            currentSelectedLevelID = selectedID;

            LastSelectedLevelRow = e.RowIndex;
            selectLevel();
            Console.WriteLine("选择了关卡ID {0}", selectedID);

            //if (e.ColumnIndex == 6)
            //{
            //    if (PlayerDataMgr.Instance.GetPlayerLevelRecords()[currentSelectedLevelID].Locked)
            //    {
            //        MessageBox.Show("本关还没有解锁，不能挑战");
            //        return;
            //    }

            //    bool win = Formula.ComputeTeamOneWin(teamOneFormation, teamTwoFormation);

            //    string message = win ? "挑战成功" : "挑战失败";
            //    MessageBox.Show(message);

            //    // 应该给奖励以及扣除体力点数
            //    if (win)
            //    {
            //        PlayerInfo pInfo = PlayerDataMgr.Instance.GetPlayer();
            //        pInfo.Coin += DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].MoneyReward;
            //        pInfo.AddExp(10);
            //        pInfo.Energy -= 6;

            //        PlayerDataMgr.Instance.PlayerFinishedLevel(currentSelectedLevelID, 1);
            //        RefreshLevelData();

            //        foreach (GeneralInfo gInfo in teamOneFormation.FormationMap.Keys)
            //        {
            //            gInfo.AddExp(DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].GeneralExpReward);
            //        }
            //        playerFormationControl.RefreshBattlePowerPoint();

            //        MainForm main = (MainForm)FindForm();
            //        main.UpdatePlayerInfo();
            //    }
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = TB_LevelName.Text;
            string reflv = TB_RefLevel.Text;
            string refElitelv = TB_RefEliteLevel.Text;
            string enemy = TB_Enemy.Text;
            string eliteEmeny = TB_EliteEnemy.Text;
            int strongEnemy = CB_StrongEnemy.Checked ? 1 : 0;

            if (name != "")
                DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].Name = name;

            if (reflv != "")
                DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].RefLevel = Convert.ToInt32(reflv);

            if (refElitelv != "")
                DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].EliteRefLevel = Convert.ToInt32(refElitelv);

            if (enemy != "")
                DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].Enemy = enemy;

            if (eliteEmeny != "")
                DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].EliteEnemy = eliteEmeny;

            DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].StrongEnemy = strongEnemy;
            DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].EnemyAngel = TB_EnemyAngel.Text;
            DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].EliteEnemyAngel = TB_EliteEnemyAngel.Text;

            DBConfigMgr.Instance.UpdateLevelBasicAttributes(currentSelectedLevelID);
            RefreshLevelData();

            dataGridView2.Rows[LastSelectedLevelRow].Selected = true;
            dataGridView2.CurrentCell = dataGridView2.Rows[LastSelectedLevelRow].Cells[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].EliteEnemy = DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].Enemy;
            DBConfigMgr.Instance.UpdateLevelEnemy(currentSelectedLevelID);
        }

        private void selectLevel()
        {
            // 显示关卡的怪物
            panel1.Controls.Clear();
            panel2.Controls.Clear();

            npcFormationControl = new UCFormation();
            npcFormationControl.InitNPCFormation(currentSelectedLevelID, false);
            panel1.Controls.Add(npcFormationControl);
            teamOneFormation = npcFormationControl.CurrentFormation;

            npcFormationControl2 = new UCFormation();
            npcFormationControl2.InitNPCFormation(currentSelectedLevelID, true);
            panel2.Controls.Add(npcFormationControl2);
            teamTwoFormation = npcFormationControl2.CurrentFormation;

            groupBox1.Visible = true;

            int reflv1 = DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].RefLevel ;
            int reflv2 = DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].EliteRefLevel;

            TB_LevelName.Text = DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].Name;
            TB_RefLevel.Text = reflv1.ToString();
            TB_RefEliteLevel.Text = reflv2.ToString();
            TB_Enemy.Text = DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].Enemy;
            TB_EliteEnemy.Text = DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].EliteEnemy;
            TB_EnemyAngel.Text = DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].EnemyAngel;
            TB_EliteEnemyAngel.Text = DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].EliteEnemyAngel;

            LB_RefSquads1.Text = Formula.GetOnBattleSquads(reflv1).ToString();
            LB_RefSquads2.Text = Formula.GetOnBattleSquads(reflv2).ToString();

            // 强敌
            CB_StrongEnemy.Checked = DBConfigMgr.Instance.MapLevel[currentSelectedLevelID].StrongEnemy == 1;
        }

        private void TB_Enemy_MouseDown(object sender, MouseEventArgs e)
        {
            TB_Enemy.SelectAll();
        }

        private void TB_EliteEnemy_MouseDown(object sender, MouseEventArgs e)
        {
            TB_EliteEnemy.SelectAll();
        }

        private void 配置怪物ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LevelEnemy form = new LevelEnemy();
            form.InitLevelEnemy(currentSelectedLevelID, false);
            form.Show();
        }

        private void 配置精英关ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LevelEnemy form = new LevelEnemy();
            form.InitLevelEnemy(currentSelectedLevelID, true);
            form.Show();
        }

        private void 配置剧情对话ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LevelPlot form = new LevelPlot();
            form.LevelID = currentSelectedLevelID;
            form.Init();
            form.Show();
        }

        private void 配置奖励ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LevelReward form = new LevelReward();
            form.Init(currentSelectedLevelID, false);
            form.Show();
        }

        private void 配置精英关奖励ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LevelReward form = new LevelReward();
            form.Init(currentSelectedLevelID, true);
            form.Show();
        }


    }
}
