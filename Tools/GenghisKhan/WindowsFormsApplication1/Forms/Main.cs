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
            ConfigDataMgr.Instance.Init();
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
                case 1:
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
                case 4:
                    {
                        break;
                    }

                default:
                    break;
            }
        }
    }
}
