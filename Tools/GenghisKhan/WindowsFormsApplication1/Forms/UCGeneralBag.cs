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
        Random rnd = new Random();
        public UCGeneralBag()
        {
            InitializeComponent();

            NUD_3Star.Value = 10;
            NUD_4Star.Value = 30;
            NUD_5Star.Value = 50;
        }

        public int CurrentGeneralID { get; set; }

        public void InitList()
        {
            RefreshBag();
        }

        private void RefreshBag()
        {
            dataGridView1.Rows.Clear();

            int soldierTypeIndex = comboBox1.SelectedIndex;
            int star = comboBox4.SelectedIndex;
            int usage = comboBox5.SelectedIndex;

            if (star != -1)
                star = star + 1;

            IEnumerable<General> genlist = DBConfigMgr.Instance.MapGeneral.Values;

            if (soldierTypeIndex > 0)
            {
                genlist = genlist.Where(x => x.SoldierType == soldierTypeIndex);
            }

            if (star > 0)
            {
                genlist = genlist.Where(x => x.Star == star);
            }

            if (usage >= 0)
            {
                genlist = genlist.Where(x => x.Usage == usage);
            }

            IEnumerable<object[]> dataList =
                from general in genlist
                select new object[] { general.ID,general.Name,general.Star,general.InitialSoldier};

            Utility.BindDataGridView(ref dataGridView1, dataList);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                RefreshBag();
            }
        }

        private void comboBox4_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox4.SelectedIndex >= 0)
                RefreshBag();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshBag();
        }

        private void comboBox5_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox5.SelectedIndex >= 0)
                RefreshBag();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            GeneralBatch.RefreshGeneralApperance();

            Console.WriteLine("刷新成功");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GeneralBatch.RefreshSpeedAndRange();
            Console.WriteLine("调整速度完毕");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int generalRows = DBConfigMgr.Instance.UpdateAllGeneralBasicInfo();
            int soldierRows = DBConfigMgr.Instance.UpdateAllSoldier();

            MessageBox.Show(String.Format("成功保存了{0}条武将数据,{1}条部队数据", generalRows,soldierRows));
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            
            int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);

            TB_Name.Text = DBConfigMgr.Instance.MapGeneral[id].Name;
            TB_Star.Text = DBConfigMgr.Instance.MapGeneral[id].Star.ToString();
            TB_SoldierType.Text = DBConfigMgr.Instance.MapGeneral[id].SoldierType.ToString();
            TB_InitialSoldier.Text = DBConfigMgr.Instance.MapGeneral[id].InitialSoldier.ToString();
            CB_Nationality.SelectedIndex = DBConfigMgr.Instance.MapGeneral[id].Nationality;
            CB_Usage.SelectedIndex = DBConfigMgr.Instance.MapGeneral[id].Usage;

            string[] specialSoldier = DBConfigMgr.Instance.MapGeneral[id].SpecialSoldier.TrimEnd(';').Split(',');
            string[] specialArmors = DBConfigMgr.Instance.MapGeneral[id].SpecialArmor.Split(';');

            if (specialSoldier.Length > 1)
            {
                TB_SpecialSoldier.Text = specialSoldier[0];
                TB_SpecialSoldierSkill.Text = specialSoldier[1];

                updateSoldierLabel(TB_SpecialSoldier, ref LB_SoldierName);
                updateSkillLabel(TB_SpecialSoldierSkill, ref LB_SkillName1);
            }
            else
            {
                TB_SpecialSoldier.Text = "";
                TB_SpecialSoldierSkill.Text = "";
            }

            if (specialArmors.Length > 1)
            {
                string[] specialWeapon = specialArmors[0].Split(',');
                string[] specialCloth = specialArmors[1].Split(',');
                string[] specialHelmet = specialArmors[2].Split(',');

                TB_SpecialWeapon.Text = specialWeapon[0];
                TB_SpecialWeaponSkill.Text = specialWeapon[1];
                updateArmorLabel(TB_SpecialWeapon, ref LB_WeaponName);
                updateSkillLabel(TB_SpecialWeaponSkill, ref LB_SkillName2);

                TB_SpecialCloth.Text = specialCloth[0];
                TB_SpecialClothSkill.Text = specialCloth[1];
                updateArmorLabel(TB_SpecialCloth, ref LB_ClothName);
                updateSkillLabel(TB_SpecialClothSkill, ref LB_SkillName3);

                TB_SpecialHelmet.Text = specialHelmet[0];
                TB_SpecialHelmetSkill.Text = specialHelmet[1];
                updateArmorLabel(TB_SpecialHelmet, ref LB_HelmetName);
                updateSkillLabel(TB_SpecialHelmetSkill, ref LB_SkillName4);
            }
            else
            {
                TB_SpecialWeapon.Text = "";
                TB_SpecialWeaponSkill.Text = "";

                TB_SpecialCloth.Text = "";
                TB_SpecialClothSkill.Text = "";

                TB_SpecialHelmet.Text = "";
                TB_SpecialHelmetSkill.Text = "";
            }
            
            CurrentGeneralID = id;
        }

        /// <summary>
        /// 保存单个武将信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            string name = TB_Name.Text;
            string star = TB_Star.Text;
            string type = TB_SoldierType.Text;
            string initialSoldier = TB_InitialSoldier.Text;

            if (name != "")
                DBConfigMgr.Instance.MapGeneral[CurrentGeneralID].Name = name;

            if (star != "")
                DBConfigMgr.Instance.MapGeneral[CurrentGeneralID].Star = Convert.ToInt32(star);

            if (type != "")
                DBConfigMgr.Instance.MapGeneral[CurrentGeneralID].SoldierType = Convert.ToInt32(type);

            if (initialSoldier != "")
                DBConfigMgr.Instance.MapGeneral[CurrentGeneralID].InitialSoldier = Convert.ToInt32(initialSoldier);

            DBConfigMgr.Instance.MapGeneral[CurrentGeneralID].Nationality = CB_Nationality.SelectedIndex;
            DBConfigMgr.Instance.MapGeneral[CurrentGeneralID].Usage = CB_Usage.SelectedIndex;

            DBConfigMgr.Instance.UpdateGeneralBasicInfo(CurrentGeneralID);
        }

        /// <summary>
        /// 生成武将碎片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            // 清空 数据
            DBConfigMgr.Instance.MapGeneralMaterial.Clear();

            GeneralBatch.GenerateGeneralMaterial();

            MessageBox.Show(String.Format("成功插入武将碎片{0}个",DBConfigMgr.Instance.SaveNewGeneralMaterials()));
        }

        private void displayGeneralMaterialList(int star)
        {            
            IEnumerable<object[]> list =
                from genMat in DBConfigMgr.Instance.MapGeneralMaterial.Values
                where genMat.Star == star
                select new object[]{genMat.ID,genMat.Name,genMat.ConsumeMaterials};


            Utility.BindDataGridView(ref dataGridView2, list);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            displayGeneralMaterialList(4);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            displayGeneralMaterialList(5);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            displayGeneralMaterialList(3);
        }

        /// <summary>
        /// 随机分配一个初始部队
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            GeneralBatch.RefreshGeneralInitialSoldier();

            MessageBox.Show("刷新完毕");
        }

        /// <summary>
        /// 刷新钟爱部队和装备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button10_Click_1(object sender, EventArgs e)
        {
            GeneralBatch.RefreshGeneralSpecialArmorAndSoldier();

            DBConfigMgr.Instance.UpdateAllGeneralBasicInfo();

            MessageBox.Show("刷新完毕");
        }

        private void updateSoldierLabel(TextBox tb, ref Label lb)
        {
            if (tb.TextLength > 0)
            {
                int soldierID = 0;
                Int32.TryParse(tb.Text, out soldierID);

                if (DBConfigMgr.Instance.MapSoldier.ContainsKey(soldierID))
                {
                    lb.Text = DBConfigMgr.Instance.MapSoldier[soldierID].Name;
                }
            }
        }

        private void updateSkillLabel(TextBox tb, ref Label lb)
        {
            if (tb.TextLength > 0)
            {
                int skillID = 0;
                Int32.TryParse(tb.Text, out skillID);

                if (DBConfigMgr.Instance.MapSkill.ContainsKey(skillID))
                {
                    lb.Text = DBConfigMgr.Instance.MapSkill[skillID].Name;
                }
            }
        }

        private void updateArmorLabel(TextBox tb, ref Label lb)
        {
            if (tb.TextLength > 0)
            {
                int armorID = 0;
                Int32.TryParse(tb.Text, out armorID);

                if (DBConfigMgr.Instance.MapArmor.ContainsKey(armorID))
                {
                    lb.Text = DBConfigMgr.Instance.MapArmor[armorID].Name;
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            GeneralBatch.RefreshGeneranUpgradeCost();

            MessageBox.Show("刷新成功！");
        }
    }
}
