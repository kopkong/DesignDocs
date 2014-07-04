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

        public void InitChapter()
        {
            // Init
            dataGridView1.Rows.Clear();

            foreach (int i in ConfigDataMgr.Instance._MapChapter.Keys)
            {
                AddChapterRow(i);
            }

            groupBox1.Visible = false;
        }

        private void AddChapterRow(int index)
        {
            Chapter chapter = ConfigDataMgr.Instance._MapChapter[index];

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
            Level levelConfig = ConfigDataMgr.Instance._MapLevel[index];
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
            IEnumerable<KeyValuePair<int,Level>> chapterLevelIDs = ConfigDataMgr.Instance._MapLevel.Where(x => x.Value.ChapterID == selectedID);

            dataGridView2.Rows.Clear();
            foreach (KeyValuePair<int, Level> i in chapterLevelIDs)
            {
                AddLevelRow(i.Key);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int selectedID = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            UCFormation ucFormation = new UCFormation();
            ucFormation.InitPlayerFormation();
            groupBox1.Controls.Add(ucFormation);
            groupBox1.Visible = true;
        }
    }
}
