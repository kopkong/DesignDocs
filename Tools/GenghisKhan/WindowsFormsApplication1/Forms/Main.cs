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
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class MainForm : Form
    {
        Random rnd = new Random();
        public MainForm()
        {
            InitializeComponent();

            InitAll();
        }

        private void InitAll()
        {
            // 按照顺序初始化
            //ConfigMgr.Instance.Init();
            File.CreateText("test.txt");
            string dbpath = File.ReadAllText("tool.config");

            SQLiteHelper.Instance.Init(dbpath);
            DBConfigMgr.Instance.Init();
            PlayerDataMgr.Instance.Init();
            UpdatePlayerInfo();

        }

        public void UpdatePlayerInfo()
        {
            PlayerInfo p = PlayerDataMgr.Instance.GetPlayer();

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
                case 1: // 部队
                    {
                        UCSoldier ucSoldier = new UCSoldier();
                        tabControl1.SelectedTab.Controls.Add(ucSoldier);
                        ucSoldier.Dock = DockStyle.Fill;
                        break;
                    }
                case 2: // 装备
                    {
                        UCArmor ucArmor = new UCArmor();
                        tabControl1.SelectedTab.Controls.Add(ucArmor);
                        ucArmor.Dock = DockStyle.Fill;
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
                        InitMaxSquadList();
                        //UCFormation ucFormation = new UCFormation();
                        //tabControl1.SelectedTab.Controls.Add(ucFormation);
                        //ucFormation.InitPlayerFormation();
                        break;
                    }
                case 5: // 任务
                    {
                        UCTask ucTask = new UCTask();
                        tabControl1.SelectedTab.Controls.Add(ucTask);
                        ucTask.Dock = DockStyle.Fill;
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
                case 10: // 竞技场
                    {
                        UCArena ucArena = new UCArena();
                        tabControl1.SelectedTab.Controls.Add(ucArena);
                        ucArena.Dock = DockStyle.Fill;

                        break;
                    }
                case 11: //爵位
                    {
                        break;
                    }
                case 12:// 守护神
                    {
                        break;
                    }
                case 13: // 抽宝箱
                    {
                        UCLottery ucLottery = new UCLottery();
                        tabControl1.SelectedTab.Controls.Add(ucLottery);
                        ucLottery.Dock = DockStyle.Fill;

                        break;
                    }

                default:
                    break;
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            ConfigExporter form = new ConfigExporter();

            form.Show();
        }

        /// <summary>
        /// 刷新关卡怪物和等级难度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            CampaignBatch.RefreshLevelEnemy();

            DBConfigMgr.Instance.UpdateAllLevelEnemy();

            MessageBox.Show("全部关卡怪物等级刷新完毕！");
        }

        public void InitMaxSquadList()
        {
            dataGridView1.Rows.Clear();

            foreach (MaxSquad m in DBConfigMgr.Instance.MapMaxSquads.Values)
            {
                DataGridViewRow row = new DataGridViewRow();

                DataGridViewTextBoxCell textbox1 = new DataGridViewTextBoxCell();
                textbox1.Value = m.Level;
                row.Cells.Add(textbox1);

                DataGridViewTextBoxCell textbox2 = new DataGridViewTextBoxCell();
                textbox2.Value = m.MaxSquads;
                row.Cells.Add(textbox2);

                dataGridView1.Rows.Add(row);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form form2 = new BattleSimu();
            form2.Show();
        }

        /// <summary>
        /// 重置关卡金钱奖励
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            CampaignBatch.RefreshLevelMoneyReward();
            int affectRows = DBConfigMgr.Instance.UpdateAllLevelMoneyReward();
            MessageBox.Show(String.Format("刷新了{0}条关卡奖励", affectRows));
        }

        /// <summary>
        /// 重置关卡经验奖励
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            CampaignBatch.RefreshLevelGeneralEXPReward();
            int affectRows =DBConfigMgr.Instance.UpdateAllLevelGeneralEXPReward();
            MessageBox.Show(String.Format("刷新了{0}条关卡奖励", affectRows));
        }

        /// <summary>
        /// 随机刷新出来关卡的额外奖励
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button14_Click(object sender, EventArgs e)
        {
            if(CB_ClearAllRewards.Checked)
                DBConfigMgr.Instance.ClearAllLevelItemReward();

            if(CB_GeneralMaterial.Checked)
                CampaignBatch.RefreshLevelGeneralMaterialReward();

            CampaignBatch.RefreshLevelSoldierExpansionItemReward();
            CampaignBatch.RefreshLevelArmorReward();
            CampaignBatch.RefreshLevelTreasureBoxReward();

            DBConfigMgr.Instance.UpdateAllLevelReward();
            MessageBox.Show("刷新完毕！");
        }

        /// <summary>
        /// 重置所有关卡的脚本奖励名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button15_Click(object sender, EventArgs e)
        {
            foreach (Level level in DBConfigMgr.Instance.MapLevel.Values)
            {
                if (level.ChapterID <= 30)
                {
                    level.LevelReward = String.Format("CampaignReward");
                    level.EliteLevelReward = String.Format("EliteCampaignReward");
                }

                if (level.ChapterID == 900)
                    level.LevelReward = "ExploreReward";

                if (level.ChapterID == 999)
                    level.LevelReward = "RegionReward";

                if (level.ChapterID == 1000)
                    level.LevelReward = "LongMarchReward";
            }

            DBConfigMgr.Instance.UpdateAllLevelRewardScriptName();

            MessageBox.Show("刷新成功！");
        }

        /// <summary>
        /// 生成技能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button16_Click(object sender, EventArgs e)
        {
            SkillBatch.GenerateSkills();

            DBConfigMgr.Instance.ClearAndSaveSkillAndBuff();

            MessageBox.Show("生成完毕！");
        }

        /// <summary>
        /// 随机给武将配技能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button18_Click(object sender, EventArgs e)
        {
            GeneralBatch.DispatchRandomSkillForGeneral();
            GeneralBatch.DispatchTalentForGeneral();

            DBConfigMgr.Instance.UpdateAllGeneralSkillAndTalent();

            MessageBox.Show("分配完毕！");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            GeneralBatch.GenerateGenerals();

            DBConfigMgr.Instance.ClearAndSaveGeneral();

            MessageBox.Show("保存完毕");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Form form = new RenameModel();
            form.Show();
        }

        /// <summary>
        /// 生成测试用的武将
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button21_Click(object sender, EventArgs e)
        {
            GeneralBatch.GenerateTestGenerals();

             // 保存到数据库
            DBConfigMgr.Instance.SaveTestGeneral();

            MessageBox.Show("生成完毕！");
        }

        /// <summary>
        /// 删除测试用武将
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button22_Click(object sender, EventArgs e)
        {
            GeneralBatch.ClearTestGenerals();

            DBConfigMgr.Instance.ClearTestGeneral();

            MessageBox.Show("清理完毕！");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            GeneralBatch.RefreshRandomGeneralMainAttribute();

            DBConfigMgr.Instance.UpdateAllGeneralBasicInfo();

            MessageBox.Show("刷新完毕！");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            SoldierBatch.RefreshRandomSoldierMainAttribute();

            DBConfigMgr.Instance.UpdateAllSoldier();

            MessageBox.Show("刷新完毕！");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            ArmorBatch.RefreshArmorMainAttribute();

            DBConfigMgr.Instance.UpdateAllArmor();

            MessageBox.Show("刷新完毕");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            MoneySimu form = new MoneySimu();
            form.Show();
        }

    }
}
