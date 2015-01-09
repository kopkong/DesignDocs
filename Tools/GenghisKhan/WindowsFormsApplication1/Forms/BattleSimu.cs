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
using System.Threading;

namespace WindowsFormsApplication1.Forms
{
    public partial class BattleSimu : Form
    {
        public BattleSimu()
        {
            InitializeComponent();
        }

        // 每一对测试对象进行测试的的次数（也就是调用战斗模块多少次）
        const int SAMPLE_TEST_COUNT = 100;

        GKBattleWrapper.Engine engine = new GKBattleWrapper.Engine(@"D:\2design\配置导出");

        Thread uiThread = null;

        private void beginNotificationThread()
        {
            textBox1.Text = "";
        }

        private void updateNotificationThread(string textBoxMessage)
        {
            textBox1.Text = textBoxMessage ;
        }

        private void endNotificationThread()
        {
            textBox1.Text = "测试完毕！";
            uiThread.Abort();
        }

        private void simulateBattle(List<int> testGroup)
        {
            List<int> compareLevels = new List<int>();

            if (CB_SimLevel1.Checked) compareLevels.Add(1);
            if (CB_SimLevel20.Checked) compareLevels.Add(20);
            if (CB_SimLevel40.Checked) compareLevels.Add(40);
            if (CB_SimLevel60.Checked) compareLevels.Add(60);

            foreach (int lv in compareLevels)
            {
                doLevelSimulate(testGroup,lv);

                this.Invoke(new Action<string>(this.updateNotificationThread), "");
            }
        }

        private void doGroupSimulate(List<int> testGroup, int player, GKBattleWrapper.Camp camp1,int lv,
            out int win,out int lose,out float time)
        {
            win = 0; lose = 0; time = 0.0f;

            int testCount = (int)NUD_TestCount.Value;
            bool needDetailInfo = CB_DetailInfo.Checked;

            foreach (int opponent in testGroup)
            {
                if (player != opponent)
                {
                    //Console.WriteLine(String.Format("Opponent:{0}", opponent));
                    GKBattleWrapper.Camp camp2 = new GKBattleWrapper.Camp();
                    constructCamp(ref camp2, opponent, DBConfigMgr.Instance.MapGeneral[opponent].InitialSoldier, lv, lv);

                    for (int j = 1; j <= testCount; j++)
                    {
                        GKBattleWrapper.BattleResult result = engine.calc(camp1, camp2);
                        //GKBattleWrapper.BattleResult result2 = engine.calc(camp2, camp1);

                        int ret = result.whowin == 0 ? win++ : lose++;
                        time += result.time;
                        //ret = result2.whowin == 0 ? lose++ : win++;
                    }

                    if (needDetailInfo)
                    {
                        string resultText = String.Format("{0} 对 {1} 的战绩是 {2}胜 {3}负 ({4}%)",
                            DBConfigMgr.Instance.MapGeneral[player].Name,
                            DBConfigMgr.Instance.MapGeneral[opponent].Name,
                            win, lose, Math.Round(win * 1.0 / (win + lose), 2) * 100);

                        this.Invoke(new Action<string>(this.updateNotificationThread), resultText);
                    }
                }
            }
        }

        private void doLevelSimulate(List<int> testGroup,int lv)
        {
            this.Invoke(new Action<string>(this.updateNotificationThread), String.Format("等级{0}测试", lv));
            StringBuilder sb = new StringBuilder();

            // header
            sb.AppendLine("General,Soldier,赢,输,胜率,GeneralHP,GeneralATK,GEneralDEF,SoldierHP,SoldierATK,SoldierDEF,平均耗时");

            // 进度
            int progress = 0;

            foreach (int player in testGroup)
            {
                int win, lose;
                float time;
                GKBattleWrapper.Camp camp1 = new GKBattleWrapper.Camp();
                General playerGeneral = DBConfigMgr.Instance.MapGeneral[player];
                constructCamp(ref camp1, player, playerGeneral.InitialSoldier, lv, lv);

                string notifyMessage = String.Format("{0} 正在和对手搏斗.. 时间：{1} 当前是{2}/{3}", playerGeneral.Name,System.DateTime.Now.ToLongTimeString(),progress,testGroup.Count());

                this.Invoke(new Action<string>(this.updateNotificationThread), notifyMessage);

                doGroupSimulate(testGroup, player, camp1, lv, out win, out lose,out time);

                Soldier playerSoldier = DBConfigMgr.Instance.MapSoldier[playerGeneral.InitialSoldier];

                sb.AppendFormat("{0},{1},{2},{3},{4}%,{5},{6},{7},{8},{9},{10},{11}" + Environment.NewLine,
                    playerGeneral.Name, playerSoldier.Name,
                    win, lose, Math.Round(win * 1.0 / (win + lose), 2) * 100,
                    playerGeneral.HP + playerGeneral.HPGrowth * (lv - 1), 
                    playerGeneral.AttackPower + playerGeneral.ATKGrowth * (lv - 1),
                    playerGeneral.DefensePower + playerGeneral.DEFGrowth * (lv - 1),
                    playerSoldier.HP + playerSoldier.HPGrowth * (lv -1),
                    playerSoldier.AttackPower + playerSoldier.ATKGrowth * (lv - 1),
                    playerSoldier.DefensePower + playerSoldier.DEFGrowth * (lv - 1),
                    time / ((testGroup.Count() - 1) * (float)NUD_TestCount.Value)
                    );

                progress++;
            }
            
            DateTime now = System.DateTime.Now;
            string fileName = String.Format("LV{0}_{1}.csv", lv, now.ToLongDateString() + "-" + now.ToLongTimeString());
            Utility.WriteText(sb, fileName);
        }

        /// <summary>
        /// 配置一只Camp
        /// </summary>
        /// <param name="camp"></param>
        private void constructCamp(ref GKBattleWrapper.Camp camp,int generalID,int soldierID,int generalLv,int soldierLv)
        {
            GKBattleWrapper.CampSlot slot1 = new GKBattleWrapper.CampSlot();

            slot1.GeneralID = generalID;
            slot1.GeneralLevel = generalLv;
            slot1.Position = 3;
            slot1.SoldierID = soldierID;
            slot1.SoldierLevel = soldierLv;
            slot1.SoldierCount = 10;
            camp.AddSlot(slot1);
        }

        private void basicTest()
        {
            this.Invoke(new Action(this.beginNotificationThread));
            this.Invoke(new Action<string>(this.updateNotificationThread), "属性基础测试" + Environment.NewLine);

            simulateBattle(BattleTest.getTestGroupID());

            this.Invoke(new Action(this.endNotificationThread));
        }

        private void basicSoldierTest()
        {
            this.Invoke(new Action(this.beginNotificationThread));
            this.Invoke(new Action<string>(this.updateNotificationThread), "兵种测试" + Environment.NewLine);

            simulateBattle(BattleTest.getTestGroupID());

            this.Invoke(new Action(this.endNotificationThread));
        }

        private void realGeneralTest()
        {
            this.Invoke(new Action(this.beginNotificationThread));
            this.Invoke(new Action<string>(this.updateNotificationThread), "真实3星武将测试" + Environment.NewLine);

            simulateBattle(BattleTest.getActualGeneralTestGroupID(4));

            this.Invoke(new Action(this.endNotificationThread));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            uiThread = new Thread(new ThreadStart(this.basicTest));
            uiThread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            uiThread = new Thread(new ThreadStart(this.realGeneralTest));
            uiThread.Start();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            uiThread = new Thread(new ThreadStart(this.basicSoldierTest));
            uiThread.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BattleSimulate battleSimu = new BattleSimulate();
            
            battleSimu.Show();
        }
    }
}
