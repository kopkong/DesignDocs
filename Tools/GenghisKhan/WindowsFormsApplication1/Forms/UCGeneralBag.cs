using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace WindowsFormsApplication1
{
    public partial class UCGeneralBag : UserControl
    {
        public UCGeneralBag()
        {
            InitializeComponent();
        }

        public void InitList()
        {
            RefreshBag(SoldierType.ALL);
        }

        private void RefreshBag(SoldierType type)
        {
            dataGridView1.Rows.Clear();

            foreach (int i in PlayerDataMgr.Instance.GetPlayerBag(SlotType.SlotType_General).Keys)
            {
                Console.WriteLine("Add general index {0}", i);

                if (type != SoldierType.ALL)
                    if ((int)type != DBConfigMgr.Instance.MapGeneral[i].SoldierType)
                        continue;

                AddRow(i);
            }
        }

        private void AddRow(int index)
        {
            Slot slotData = PlayerDataMgr.Instance.GetPlayerBag(SlotType.SlotType_General)[index];

            DataGridViewRow row = new DataGridViewRow();
            DataGridViewTextBoxCell textboxName = new DataGridViewTextBoxCell();
            textboxName.Value = DBConfigMgr.Instance.MapGeneral[slotData.ConfigID].Name;
            row.Cells.Add(textboxName);

            DataGridViewTextBoxCell textboxLv = new DataGridViewTextBoxCell();
            textboxLv.Value = slotData.Lv.ToString();
            row.Cells.Add(textboxLv);

            DataGridViewTextBoxCell textboxRank = new DataGridViewTextBoxCell();
            textboxRank.Value = slotData.Rank.ToString();
            row.Cells.Add(textboxRank);

            DataGridViewTextBoxCell textboxBattlePoint = new DataGridViewTextBoxCell();
            textboxBattlePoint.Value = Formula.ComputeBattlePowerPoint(index);
            row.Cells.Add(textboxBattlePoint);

            DataGridViewTextBoxCell textboxStar = new DataGridViewTextBoxCell();
            textboxStar.Value = DBConfigMgr.Instance.MapGeneral[slotData.ConfigID].Star;
            row.Cells.Add(textboxStar);

            dataGridView1.Rows.Add(row);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                RefreshBag((SoldierType)comboBox1.SelectedIndex);
            }
        }
    }
}
