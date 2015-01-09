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
    public partial class UCTask : UserControl
    {
        public UCTask()
        {
            InitializeComponent();
        }

        const int DIALY_TASK = 2;
        const int ACHIEVEMENT_TASK = 1;

        public int CurrentTaskID { get; set; }

        private void displayList(int type)
        {
            IEnumerable<KeyValuePair<int, Task>> list = DBConfigMgr.Instance.MapTask.Where(x => x.Value.Type == type);

            dataGridView1.Rows.Clear();
            foreach (KeyValuePair<int, Task> pair in list)
            {
                addRow(pair.Value);
            }
        }

        private void addRow(Task t)
        {
            DataGridViewRow row = new DataGridViewRow();
            DataGridViewTextBoxCell textBox1 = new DataGridViewTextBoxCell();
            textBox1.Value = t.ID;
            row.Cells.Add(textBox1);

            DataGridViewTextBoxCell textBox2 = new DataGridViewTextBoxCell();
            textBox2.Value = t.Name;
            row.Cells.Add(textBox2);

            DataGridViewTextBoxCell textBox3 = new DataGridViewTextBoxCell();
            textBox3.Value = t.TaskLevel;
            row.Cells.Add(textBox3);

            dataGridView1.Rows.Add(row);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            displayList(ACHIEVEMENT_TASK);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            displayList(DIALY_TASK);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CurrentTaskID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

            TB_Name.Text = DBConfigMgr.Instance.MapTask[CurrentTaskID].Name;
            TB_EXP.Text = DBConfigMgr.Instance.MapTask[CurrentTaskID].PlayerExpReward.ToString();
            TB_Money.Text = DBConfigMgr.Instance.MapTask[CurrentTaskID].MoneyReward.ToString();
            TB_Diamond.Text = DBConfigMgr.Instance.MapTask[CurrentTaskID].DiamondReward.ToString();
            TB_Level.Text = DBConfigMgr.Instance.MapTask[CurrentTaskID].TaskLevel.ToString();
            TB_Desc.Text = DBConfigMgr.Instance.MapTask[CurrentTaskID].Description;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DBConfigMgr.Instance.MapTask[CurrentTaskID].Name = TB_Name.Text;
            DBConfigMgr.Instance.MapTask[CurrentTaskID].PlayerExpReward = Convert.ToInt32(TB_EXP.Text);
            DBConfigMgr.Instance.MapTask[CurrentTaskID].MoneyReward = Convert.ToInt32(TB_Money.Text);
            DBConfigMgr.Instance.MapTask[CurrentTaskID].DiamondReward = Convert.ToInt32(TB_Diamond.Text);
            DBConfigMgr.Instance.MapTask[CurrentTaskID].TaskLevel = Convert.ToInt32(TB_Level.Text);
            DBConfigMgr.Instance.MapTask[CurrentTaskID].Description = TB_Desc.Text;

            DBConfigMgr.Instance.UpdateTaskReward(CurrentTaskID);
        }




    }
}
