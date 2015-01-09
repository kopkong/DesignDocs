using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1.Forms
{
    public partial class BattleSimulate : Form
    {
        public BattleSimulate()
        {
            InitializeComponent();
            //initData();
        }

        int[] GENERAL_IDS = { 1, 43, 15, 9 };
        int[] SOLDIER_IDS = { 1, 9, 30, 10 };

        private void button1_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < 20; i++)
            {
                string cbBoxAName = String.Format("CB_A{0}", i);
                string cbBoxBName = string.Format("CB_B{0}", i);

                ComboBox cbA = this.Controls.Find(cbBoxAName, true)[0] as ComboBox;
                ComboBox cbB = this.Controls.Find(cbBoxBName, true)[0] as ComboBox;

                cbA.SelectedIndex = 0;
                cbB.SelectedIndex = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("{");
            writeCamp(ref sb, "mycamp");
            writeCamp(ref sb, "enemycamp");
            sb.AppendLine("}");

            Utility.WriteText(sb, "battlesimulate.json");

            // Copy
            File.Copy(Environment.CurrentDirectory + "\\battlesimulate.json",
                "D:\\0code\\server\\Resource\\GameConfig\\battlesimulate.json",true);

            MessageBox.Show("生成完毕");
            
        }

        private void writeCamp(ref StringBuilder sb,string camp)
        {
            sb.AppendFormat("\t\"{0}\":" + Environment.NewLine,camp);
            sb.AppendLine("\t[");

            for (int j = 0; j < 20; j++)
            {
                string cbBoxName = camp == "mycamp" ? string.Format("CB_A{0}", j) : string.Format("CB_B{0}", j);
                ComboBox cb = this.Controls.Find(cbBoxName, true)[0] as ComboBox;

                if (cb.SelectedIndex > 0)
                {
                    sb.AppendLine("\t\t{");
                    sb.AppendFormat("\t\t\t\"battlepos\":{0}," + Environment.NewLine, j);
                    sb.AppendFormat("\t\t\t\"generalconfigid\":{0}," + Environment.NewLine, GENERAL_IDS[cb.SelectedIndex]);
                    sb.AppendFormat("\t\t\t\"generalclass\":{0}," + Environment.NewLine, 0);
                    sb.AppendFormat("\t\t\t\"skilllevel0\":{0}," + Environment.NewLine, 1);
                    sb.AppendFormat("\t\t\t\"skilllevel1\":{0}," + Environment.NewLine, 1);
                    sb.AppendFormat("\t\t\t\"soldierconfigid\":{0}," + Environment.NewLine, SOLDIER_IDS[cb.SelectedIndex]);
                    sb.AppendFormat("\t\t\t\"soldierclass\":{0}," + Environment.NewLine, 0);
                    sb.AppendFormat("\t\t\t\"soldierlevel\":{0}," + Environment.NewLine, 1);
                    sb.AppendFormat("\t\t\t\"soldiercount\":{0}," + Environment.NewLine, 10);
                    sb.AppendLine("\t\t}");
                    sb.AppendLine("\t\t,");
                }
            }

            sb.AppendLine("\t],");
        }

        private void setArmy(string team, int row, int type)
        {
            for (int i = 0; i <= 4; i++)
            {
                string cbBoxName = String.Format("CB_{0}{1}", team, row + 4 * i);
                ComboBox cb = this.Controls.Find(cbBoxName, true)[0] as ComboBox;

                cb.SelectedIndex = type;
            }
        }

        #region 鼠标点击事件
        private void button3_Click(object sender, EventArgs e)
        {
            setArmy("B", 0, 1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            setArmy("B", 0, 2);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            setArmy("B", 0, 3);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            setArmy("B", 1, 1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            setArmy("B", 1, 2);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            setArmy("B", 1, 3);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            setArmy("B", 2, 1);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            setArmy("B", 2, 2);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            setArmy("B", 2, 3);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            setArmy("B", 3, 1);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            setArmy("B", 3, 2);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            setArmy("B", 3, 3);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            setArmy("A", 0, 1);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            setArmy("A", 0, 2);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            setArmy("A", 0, 3);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            setArmy("A", 1, 1);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            setArmy("A", 1, 2);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            setArmy("A", 1, 3);
        }

        private void button23_Click(object sender, EventArgs e)
        {
            setArmy("A", 2, 1);
        }

        private void button22_Click(object sender, EventArgs e)
        {
            setArmy("A", 2, 2);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            setArmy("A", 2, 3);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            setArmy("A", 3, 1);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            setArmy("A", 3, 2);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            setArmy("A", 3, 3);
        }
        #endregion
    }
}
