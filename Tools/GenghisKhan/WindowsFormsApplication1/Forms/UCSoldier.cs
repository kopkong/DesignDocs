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
    public partial class UCSoldier : UserControl
    {
        public UCSoldier()
        {
            InitializeComponent();
            refreshList();
        }

        private void refreshList()
        {
            IEnumerable<Soldier> soldierList = DBConfigMgr.Instance.MapSoldier.Values;

            IEnumerable<object[]> dataList =
                from soldier in soldierList
                select new object[] { soldier.ID, soldier.Name, soldier.Star };

            Utility.BindDataGridView(ref dataGridView1, dataList);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SoldierBatch.RefreshRandomSoldierMainAttribute();
            int affectRows = DBConfigMgr.Instance.UpdateAllSoldier();
            MessageBox.Show(String.Format("成功保存了{0}条部队数据", affectRows));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SoldierBatch.RefreshSoldierCountItems();
            DBConfigMgr.Instance.UpdateAllSoldier();
            MessageBox.Show("修改完毕!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SoldierBatch.RefreshSoldierWeapon();
            DBConfigMgr.Instance.UpdateAllSoldier();
            MessageBox.Show("修改完毕!");
        }
    }
}
