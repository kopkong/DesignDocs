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
    public partial class UCArmor : UserControl
    {
        public UCArmor()
        {
            InitializeComponent();
            initData(0,0);
        }

        private void initData(int type,int star)
        {
            IEnumerable<Armor> armorList = from armor in DBConfigMgr.Instance.MapArmor.Values
                       select armor;

            if (type > 0)
            {
                armorList = armorList.Where(x => x.Type == type); 
            }

            if (star > 0)
            {
                armorList = armorList.Where(x => x.Star == star);
            }


            IEnumerable<object[]> data = from armor in armorList
                    select new object[] { armor.ID, armor.Name, armor.Type };

            Utility.BindDataGridView(ref dataGridView1, data);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 清空 数据
            DBConfigMgr.Instance.MapArmorMaterial.Clear();

            ArmorBatch.GenerateArmorMaterial();

            MessageBox.Show(String.Format("成功插入装备碎片{0}个", DBConfigMgr.Instance.SaveNewArmorMaterials()));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            initData(comboBox1.SelectedIndex, comboBox2.SelectedIndex);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            initData(comboBox1.SelectedIndex, comboBox2.SelectedIndex);
        }


    }
}
