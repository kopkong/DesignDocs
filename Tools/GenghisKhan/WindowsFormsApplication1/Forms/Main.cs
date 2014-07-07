using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1;
using WindowsFormsApplication1.Forms;

namespace WindowsFormsApplication1
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            InitAll();
        }

        private void InitAll()
        {
            // 按照顺序初始化
            //ConfigMgr.Instance.Init();
            SQLiteHelper.Instance.Init(@"D:\DesignDocs\Tools\GenghisKhan\test.db");
            DBConfigMgr.Instance.Init();
            PlayerDataMgr.Instance.Init();
            updatePlayerInfo();
        }

        private void updatePlayerInfo()
        {
            CKPlayer p = PlayerDataMgr.Instance.GetPlayer();

            TB_Name.Text = p.Name;
            TB_NobleRanks.Text = p.NobleRanks.ToString();
            TB_Level.Text = p.Lv.ToString();
            TB_VIPLevel.Text = p.VIPLevele.ToString();
            TB_Energy.Text = p.Energy.ToString();
            TB_Coin.Text = p.Coin.ToString();
            TB_Diamond.Text = p.Diamonds.ToString();
            TB_SoulJade.Text = p.SoulJade.ToString();
            TB_Fame.Text = p.Fame.ToString();
            TB_Honor.Text = p.Honor.ToString();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Tab Index {0}", tabControl1.SelectedIndex);
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    {
                        break;
                    }
                case 1: // 仓库， 装备、部队、道具背包
                    {
                        break;
                    }
                case 2: // 合成， 碎片背包
                    {
                        break;
                    }
                case 3: // 武将背包
                    {
                        UCGeneralBag ucGeneralBag = new UCGeneralBag();
                        tabControl1.SelectedTab.Controls.Add(ucGeneralBag);
                        ucGeneralBag.Dock = DockStyle.Fill;
                        ucGeneralBag.InitList();
                        break;
                    }
                case 4: // 阵容
                    {
                        UCFormation ucFormation = new UCFormation();
                        tabControl1.SelectedTab.Controls.Add(ucFormation);
                        ucFormation.InitPlayerFormation();
                        break;
                    }
                case 5: // 任务
                    {
                        break;
                    }
                case 6: //  战役
                    {
                        UCChapters ucChapter = new UCChapters();
                        tabControl1.SelectedTab.Controls.Add(ucChapter);
                        ucChapter.Dock = DockStyle.Fill;
                        ucChapter.InitChapter();

                        break;
                    }
                case 7: // 探索
                    {
                        break;
                    }

                default:
                    break;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();

            form.Show();
        }
    }
}
