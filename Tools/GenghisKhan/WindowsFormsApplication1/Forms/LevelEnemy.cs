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

        public int LevelID { get; set; }

        public bool CanReComputeBattlePoint { get; set; }

        public Formation EnemyFormation { get; set; }

        private Dictionary<int,NPCEnemy> EnemyList = new Dictionary<int,NPCEnemy>();

        public void InitLevelEnemy(int levelID)
        {
            EnemyFormation = new Formation();
            EnemyFormation.InitNPCFormation(levelID);

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
            if(EnemyList!= null && EnemyList.Count > 0)
            {
                dataGridView1.Rows.Clear();
                foreach(KeyValuePair<int,NPCEnemy> kv in EnemyList)
                {
                    DataGridViewRow row = new DataGridViewRow();

                    DataGridViewTextBoxCell cell0 = new DataGridViewTextBoxCell();
                    cell0.Value = (FormationPosition)kv.Key;
                    row.Cells.Add(cell0);

                    DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                    cell1.Value = kv.Value.GeneralConfigID;
                    row.Cells.Add(cell1);

                    DataGridViewTextBoxCell cell2 = new DataGridViewTextBoxCell();
                    cell2.Value = kv.Value.GeneralLevel;
                    row.Cells.Add(cell2);

                    DataGridViewTextBoxCell cell3 = new DataGridViewTextBoxCell();
                    cell3.Value = kv.Value.SoldierConfigID;
                    row.Cells.Add(cell3);

                    DataGridViewTextBoxCell cell4 = new DataGridViewTextBoxCell();
                    cell4.Value = kv.Value.SoldierLevel;
                    row.Cells.Add(cell4);

                    DataGridViewTextBoxCell cell5 = new DataGridViewTextBoxCell();
                    cell5.Value = kv.Value.SoldierCount;
                    row.Cells.Add(cell5);

                    dataGridView1.Rows.Add(row);

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReComputeBattlePoint();
            DBConfigMgr.Instance.SaveLevelEnemyData(LevelID, EnemyList);
            MessageBox.Show("保存完毕");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReComputeBattlePoint();
        }

        private void ReComputeBattlePoint()
        {
            EnemyList.Clear();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    string v0 = row.Cells[0].Value.ToString();
                    FormationPosition p = (FormationPosition)Enum.Parse(typeof(FormationPosition), v0);

                    int pos = (int)p;
                    int generalConfig = Convert.ToInt32(row.Cells[1].Value);
                    int generalLevel = Convert.ToInt32(row.Cells[2].Value);
                    int soldierConfig = Convert.ToInt32(row.Cells[3].Value);
                    int soldierLevel = Convert.ToInt32(row.Cells[4].Value);
                    int soldierCount = Convert.ToInt32(row.Cells[5].Value);

                    NPCEnemy n = new NPCEnemy(pos, generalConfig, generalLevel, soldierConfig, soldierLevel, soldierCount);
                    EnemyList.Add(n.Position, n);
                }
            }

            EnemyFormation.NPCFormation = EnemyList;
            EnemyFormation.ReComputeNPCTeamBattlePowerPoint();
            LB_RealBattlePoint.Text = EnemyFormation.TeamBattlePowerPoint.ToString();

        }

        /// <summary>
        /// 关卡难度的参考值
        /// </summary>
        private void GetReferBattlePoint()
        {
            int refLevel = Formula.GetReferenceLevel(LevelID);
            int refBattlePoint = Formula.GetLevelDifficulty(LevelID);

            LB_ReferLevel.Text = refLevel.ToString();
            LB_RefBattlePoint.Text = refBattlePoint.ToString();
        }

        private void ResetTheFormation(FormationPosition[] formation)
        {
            int rowIndex = 0;
            CanReComputeBattlePoint = false;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    row.Cells[0].Value = formation[rowIndex].ToString();
                }

                rowIndex++;
            }

            CanReComputeBattlePoint = true;
        }

        /// <summary>
        /// 方块阵型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            FormationPosition[] formation = 
            { 
                FormationPosition.B2,
                FormationPosition.C2,
                FormationPosition.B3,
                FormationPosition.C3,
                FormationPosition.D2,
                FormationPosition.D3,
                FormationPosition.B1,
                FormationPosition.C1,
                FormationPosition.D1,
                FormationPosition.B4,
                FormationPosition.C4,
                FormationPosition.D4,
                FormationPosition.A1,
                FormationPosition.E1,
                FormationPosition.A2,
                FormationPosition.E2,
                FormationPosition.A3,
                FormationPosition.E3,
                FormationPosition.A4,
                FormationPosition.E4
            };

            ResetTheFormation(formation);
            
        }

        /// <summary>
        /// 锥形阵
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            FormationPosition[] formation = 
            { 
                FormationPosition.C1,
                FormationPosition.B2,
                FormationPosition.D2,
                FormationPosition.C2,
                FormationPosition.A2,
                FormationPosition.E3,
                FormationPosition.B3,
                FormationPosition.C3,
                FormationPosition.D3,
                FormationPosition.C4,
                FormationPosition.A4,
                FormationPosition.E4,
                FormationPosition.B4,
                FormationPosition.D4,
                FormationPosition.A2,
                FormationPosition.E2,
                FormationPosition.B1,
                FormationPosition.D1,
                FormationPosition.A1,
                FormationPosition.E1
            };

            ResetTheFormation(formation);
        }

        /// <summary>
        /// 长条阵型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            FormationPosition[] formation = 
            { 
                FormationPosition.B1,
                FormationPosition.D1,
                FormationPosition.B2,
                FormationPosition.D2,
                FormationPosition.B3,
                FormationPosition.D3,
                FormationPosition.B4,
                FormationPosition.D4,
                FormationPosition.C1,
                FormationPosition.C2,
                FormationPosition.C3,
                FormationPosition.C4,
                FormationPosition.A1,
                FormationPosition.E1,
                FormationPosition.A2,
                FormationPosition.E2,
                FormationPosition.A3,
                FormationPosition.E3,
                FormationPosition.A4,
                FormationPosition.E4
            };

            ResetTheFormation(formation);
        }

        /// <summary>
        /// U形
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            FormationPosition[] formation = 
            { 
                FormationPosition.A1,
                FormationPosition.A2,
                FormationPosition.A3,
                FormationPosition.E1,
                FormationPosition.E2,
                FormationPosition.E3,
                FormationPosition.B3,
                FormationPosition.D3,
                FormationPosition.B4,
                FormationPosition.C4,
                FormationPosition.D4,
                FormationPosition.A4,
                FormationPosition.E4,
                FormationPosition.C3,
                FormationPosition.B2,
                FormationPosition.C2,
                FormationPosition.D2,
                FormationPosition.B1,
                FormationPosition.C1,
                FormationPosition.D1
            };

            ResetTheFormation(formation);
        }


        /// <summary>
        /// 竖条阵型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            FormationPosition[] formation = 
            { 
                FormationPosition.A1,
                FormationPosition.B1,
                FormationPosition.C1,
                FormationPosition.D1,
                FormationPosition.E1,
                FormationPosition.A3,
                FormationPosition.B3,
                FormationPosition.C3,
                FormationPosition.D3,
                FormationPosition.E3,
                FormationPosition.B4,
                FormationPosition.C4,
                FormationPosition.D4,
                FormationPosition.A4,
                FormationPosition.E4,
                FormationPosition.B2,
                FormationPosition.C2,
                FormationPosition.D2,
                FormationPosition.A2,
                FormationPosition.E2
            };

            ResetTheFormation(formation);
        }

        /// <summary>
        /// 圆形
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            FormationPosition[] formation = 
            { 
                FormationPosition.B1,
                FormationPosition.C1,
                FormationPosition.D1,
                FormationPosition.A2,
                FormationPosition.A3,
                FormationPosition.E2,
                FormationPosition.E3,
                FormationPosition.B4,
                FormationPosition.D4,
                FormationPosition.C4,
                FormationPosition.A1,
                FormationPosition.E1,
                FormationPosition.A4,
                FormationPosition.E4,
                FormationPosition.B2,
                FormationPosition.C2,
                FormationPosition.D2,
                FormationPosition.B3,
                FormationPosition.C3,
                FormationPosition.D3
            };

            ResetTheFormation(formation);
        }

        /// <summary>
        /// 倒三角
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            FormationPosition[] formation = 
            { 
                FormationPosition.C4,
                FormationPosition.B3,
                FormationPosition.D3,
                FormationPosition.C3,
                FormationPosition.C2,
                FormationPosition.B2,
                FormationPosition.D2,
                FormationPosition.C1,
                FormationPosition.B1,
                FormationPosition.D1,
                FormationPosition.A1,
                FormationPosition.E1,
                FormationPosition.B4,
                FormationPosition.D4,
                FormationPosition.A2,
                FormationPosition.E2,
                FormationPosition.A3,
                FormationPosition.E3,
                FormationPosition.A4,
                FormationPosition.E4
            };

            ResetTheFormation(formation);
        }

        /// <summary>
        /// 倒U形
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click(object sender, EventArgs e)
        {
            FormationPosition[] formation = 
            { 
                FormationPosition.C1,
                FormationPosition.B1,
                FormationPosition.D1,
                FormationPosition.A2,
                FormationPosition.E2,
                FormationPosition.A3,
                FormationPosition.E3,
                FormationPosition.A4,
                FormationPosition.E4,
                FormationPosition.B2,
                FormationPosition.D2,
                FormationPosition.C2,
                FormationPosition.A1,
                FormationPosition.E1,
                FormationPosition.B3,
                FormationPosition.D3,
                FormationPosition.C3,
                FormationPosition.B4,
                FormationPosition.D4,
                FormationPosition.C4
            };

            ResetTheFormation(formation);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (CanReComputeBattlePoint)
                ReComputeBattlePoint();
        }
    }
}
