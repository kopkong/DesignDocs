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
    public partial class UCLottery : UserControl
    {
        Random rnd = new Random();
        Dictionary<int, Dictionary<int, General>> lotteryPool = new Dictionary<int, Dictionary<int, General>>();
        Dictionary<LotteryType, StatisticValue> statistics = new Dictionary<LotteryType, StatisticValue>();

        public int TryTimes { get; set; }

        public enum LotteryType
        {
            FreeMoney,
            FreeDiamond,
            MoneyCost,
            DiamondCost,
            MoneyTen,
            DiamondTen
        };

        public class StatisticValue
        {
            public StatisticValue()
            {
                this.Value1 = 0;
                this.Value2 = 0;
                this.Value3 = 0;
                this.Value4 = 0;
                this.Value5 = 0;
            }

            public void resetValue()
            {
                this.Value1 = 0;
                this.Value2 = 0;
                this.Value3 = 0;
                this.Value4 = 0;
                this.Value5 = 0;
            }

            public int Value1;
            public int Value2;
            public int Value3;
            public int Value4;
            public int Value5;
        };

        public UCLottery()
        {
            InitializeComponent();

            Dictionary<int, General> pool2 = new Dictionary<int, General>();
            Dictionary<int, General> pool3 = new Dictionary<int, General>();
            Dictionary<int, General> pool4 = new Dictionary<int, General>();
            Dictionary<int, General> pool5 = new Dictionary<int, General>();

            lotteryPool.Add(2,pool2);
            lotteryPool.Add(3,pool3);
            lotteryPool.Add(4,pool4);
            lotteryPool.Add(5,pool5);

            foreach (KeyValuePair<int,General> pair in DBConfigMgr.Instance.MapGeneral)
            {
                if (pair.Key > 5 && pair.Key < 2000)
                {
                    lotteryPool[pair.Value.Star].Add(pair.Key,pair.Value);
                }
            }

            statistics.Add(LotteryType.FreeMoney, new StatisticValue());
            statistics.Add(LotteryType.FreeDiamond, new StatisticValue());
            statistics.Add(LotteryType.MoneyCost, new StatisticValue());
            statistics.Add(LotteryType.DiamondCost, new StatisticValue());
            statistics.Add(LotteryType.MoneyTen, new StatisticValue());
            statistics.Add(LotteryType.DiamondTen, new StatisticValue());

            textBox1.Text = "35";
            textBox3.Text = "10";
        }

        private int getRandomGeneralID(int possibility3, int possibility4, int possibility5)
        {
            int randomStar = 1;
            List<int> stars = new List<int>(1000);

            int i = possibility5;
            while ( i > 0 )
            {
                stars.Add(5);
                i--;
            }

            int j = possibility4;
            while (j > 0)
            {
                stars.Add(4);
                j--;
            }

            int k = possibility3;
            while (k > 0)
            {
                stars.Add(3);
                k--;
            }

            int l = 1000 - possibility3 - possibility4 - possibility5;
            while (l > 0)
            {
                stars.Add(2);
                l--;
            }
                

            int N = rnd.Next(1000);

            randomStar = stars[N];

            int length = lotteryPool[randomStar].Count();
            int index = rnd.Next(length);

            return lotteryPool[randomStar].ElementAt(index).Value.ID;
        }

        private void addRow(int index)
        {
            int rowCount = dataGridView1.Rows.Count;

            DataGridViewRow row = new DataGridViewRow();
            DataGridViewTextBoxCell tb1 = new DataGridViewTextBoxCell();
            tb1.Value = rowCount + 1;
            row.Cells.Add(tb1);

            DataGridViewTextBoxCell tb3 = new DataGridViewTextBoxCell();
            tb3.Value = DBConfigMgr.Instance.MapGeneral[index].Name;
            row.Cells.Add(tb3);

            DataGridViewTextBoxCell tb4 = new DataGridViewTextBoxCell();
            tb4.Value = DBConfigMgr.Instance.MapGeneral[index].Star;
            row.Cells.Add(tb4);

            dataGridView1.Rows.Add(row);
        }

        private void prepareLottery(LotteryType type)
        {
            textBox2.Text = "";
            int loop = 1;
            if (textBox3.Text != "")
                loop = Convert.ToInt32(textBox3.Text);

            int count = Convert.ToInt32(textBox1.Text);
            TryTimes = loop;

            statistics[type].resetValue();

            while (loop > 0)
            {
                doLottery(count, type);
                loop--;
            }

            displayLotteryStatistics(type);
        }

        private void doLottery(int count,LotteryType type)
        {
            List<int> lotteryResults = new List<int>();

            switch (type)
            {
                case LotteryType.FreeMoney:
                    {
                        int lotteryTimes = count;
                        while (lotteryTimes > 0)
                        {
                            int id = 0;
                            if (count - lotteryTimes < 35)
                                id = getRandomGeneralID(200, 28, 0);
                            else
                                id = getRandomGeneralID(85, 14, 0);

                            int star = DBConfigMgr.Instance.MapGeneral[id].Star;

                            if ( star == 3)
                                statistics[type].Value1++;

                            if (star == 4)
                                statistics[type].Value2++;

                            lotteryResults.Add(id);
                            lotteryTimes--;
                        }
                        break;
                    }
                case LotteryType.MoneyCost:
                    {
                        int lotteryTimes = count;
                        while (lotteryTimes > 0)
                        {
                            int id = 0;
                            if (count - lotteryTimes < 35)
                                id = getRandomGeneralID(200, 28, 0);
                            else
                                id = getRandomGeneralID(125, 22, 0);

                            int star = DBConfigMgr.Instance.MapGeneral[id].Star;
                            if (star == 3)
                                statistics[type].Value1++;

                            if (star == 4)
                                statistics[type].Value2++;

                            lotteryResults.Add(id);
                            lotteryTimes--;
                        }
                        break;
                    }
                case LotteryType.MoneyTen:
                    {
                        int lotteryTimes = 10 * count;
                        while (lotteryTimes > 0)
                        {
                            int id = 0;
                            if (count - lotteryTimes < 35)
                                id = getRandomGeneralID(200, 28, 0);
                            else
                                id = getRandomGeneralID(125, 22, 0);

                            int star = DBConfigMgr.Instance.MapGeneral[id].Star;
                            if (star == 3)
                                statistics[type].Value1++;

                            if (star == 4)
                                statistics[type].Value2++;

                            lotteryResults.Add(id);
                            lotteryTimes--;
                        }
                        break;
                    }
                case LotteryType.FreeDiamond:
                    {
                        int lotteryTimes = count;
                        while (lotteryTimes > 0)
                        {
                            int id = 0;
                            if (count - lotteryTimes < 10)
                                id = getRandomGeneralID(0, 700, 300);
                            else
                                id = getRandomGeneralID(0, 900, 100);

                            int star = DBConfigMgr.Instance.MapGeneral[id].Star;
                            if (star == 4)
                                statistics[type].Value1++;

                            if (star == 5)
                                statistics[type].Value2++;

                            lotteryResults.Add(id);
                            lotteryTimes--;
                        }
                        break;
                    }

                case LotteryType.DiamondCost:
                    {
                        int lotteryTimes = count;
                        while (lotteryTimes > 0)
                        {
                            int id = 0;
                            if (count - lotteryTimes < 10)
                                id = getRandomGeneralID(0, 700, 300);
                            else
                                id = getRandomGeneralID(0, 850, 150);

                            int star = DBConfigMgr.Instance.MapGeneral[id].Star;
                            if (star == 4)
                                statistics[type].Value1++;

                            if (star == 5)
                                statistics[type].Value2++;

                            lotteryResults.Add(id);
                            lotteryTimes--;
                        }
                        break;
                    }
                case LotteryType.DiamondTen:
                    {
                        int lotteryTimes = 10*count;
                        while (lotteryTimes > 0)
                        {
                            int id = 0;
                            if (count - lotteryTimes < 10)
                                id = getRandomGeneralID(0, 700, 300);
                            else
                                id = getRandomGeneralID(0, 800, 200);

                            int star = DBConfigMgr.Instance.MapGeneral[id].Star;
                            if (star == 4)
                                statistics[type].Value1++;

                            if (star == 5)
                                statistics[type].Value2++;

                            lotteryResults.Add(id);
                            lotteryTimes--;
                        }
                        break;
                    }
                default:
                    break;
            }

            if (checkBox1.Checked)
                displayLotteryResult(type,lotteryResults);
        }

        private void displayLotteryResult(LotteryType type,List<int> results)
        {
            dataGridView1.Rows.Clear();

            Dictionary<int,int> starCount = new Dictionary<int,int>();
            starCount.Add(2,0);
            starCount.Add(3,0);
            starCount.Add(4,0);
            starCount.Add(5,0);

            foreach(int i in results)
            {
                addRow(i);

                starCount[DBConfigMgr.Instance.MapGeneral[i].Star] += 1;
            }

            int totalLotteryCount = results.Count();
            String message = String.Empty;
            switch (type)
            {
                case LotteryType.FreeMoney:
                    {
                        message = String.Format("免费金币抽取{0}次,需要{1}天; 三星武将共{2}个", totalLotteryCount, totalLotteryCount / 5,starCount[3]);

                        break;
                    }
                case LotteryType.FreeDiamond:
                    {
                        message = String.Format("免费钻石抽取{0}次,需要{1}天。三星武将共{2}个,四星武将共{3}个。", totalLotteryCount, totalLotteryCount * 2,starCount[3],starCount[4]);
                        break;
                    }
                case LotteryType.MoneyCost:
                    {
                        message = String.Format("金币抽取{0}次,花费金币共{1}。", totalLotteryCount, totalLotteryCount * 1000);
                        break;
                    }
                case LotteryType.DiamondCost:
                    {
                        message = String.Format("钻石抽取{0}次,花费钻石共{1}。", totalLotteryCount, totalLotteryCount * 150);
                        break;
                    }
                case LotteryType.MoneyTen:
                    {
                        message = String.Format("金币10连抽{0}次,花费金币共{1}。", totalLotteryCount, totalLotteryCount / 10 * 9000);
                        break;
                    }
                case LotteryType.DiamondTen:
                    {
                        message = String.Format("钻石10连抽{0}次,花费钻石共{1}。", totalLotteryCount, totalLotteryCount / 10 * 1350);
                        break;
                    }
                default: break;
            }

            textBox2.Text += message + Environment.NewLine;
        }

        private void displayLotteryStatistics(LotteryType type)
        {
            string message = String.Empty;
            switch (type)
            {
                case LotteryType.FreeMoney:
                    {
                        message = string.Format("平均获得3星武将{0} 个, 4星武将{1}个。",
                            statistics[type].Value1/(TryTimes * 1.0f),
                            statistics[type].Value2 / (TryTimes * 1.0f));
                        break;
                    }
                case LotteryType.MoneyCost:
                    {
                        message = string.Format("平均获得3星武将{0} 个, 4星武将{1}个。",
                            statistics[type].Value1 / (TryTimes * 1.0f),
                            statistics[type].Value2 / (TryTimes * 1.0f));
                        break; 
                    }
                case LotteryType.FreeDiamond:
                    {
                        message = string.Format("平均获得4星武将{0} 个, 5星武将{1}个。",
                            statistics[type].Value1 / (TryTimes * 1.0f),
                            statistics[type].Value2 / (TryTimes * 1.0f));
                        break;
                    }
                case LotteryType.DiamondCost:
                    {
                        message = string.Format("平均获得4星武将{0} 个, 5星武将{1}个。",
                            statistics[type].Value1 / (TryTimes * 1.0f),
                            statistics[type].Value2 / (TryTimes * 1.0f));
                        break;
                    }
                case LotteryType.DiamondTen:
                    {
                        message = string.Format("平均获得4星武将{0} 个, 5星武将{1}个。",
                            statistics[type].Value1 / (TryTimes  * 1.0f),
                            statistics[type].Value2 / (TryTimes  * 1.0f));
                        break;
                    }
                default: break;
            }

            textBox2.Text += message + Environment.NewLine;
        }

        // 免费金币抽
        private void button1_Click(object sender, EventArgs e)
        {
            prepareLottery(LotteryType.FreeMoney);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            prepareLottery(LotteryType.FreeDiamond);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            prepareLottery(LotteryType.MoneyCost);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            prepareLottery(LotteryType.DiamondCost);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            prepareLottery(LotteryType.MoneyTen);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            prepareLottery(LotteryType.DiamondTen);
        }

        private void button7_Click(object sender, EventArgs e)
        {
          //{[2]={},[3]={},[4]={},[5]={}};  
            
            IEnumerable<General> genList =
                from general in DBConfigMgr.Instance.MapGeneral.Values
                where general.ID > 6 && general.ID < 1000
                select general;

            string star2Str = String.Empty;
            string star3Str = String.Empty;
            string star4Str = String.Empty;
            string star5Str = String.Empty;

            IEnumerable<General> star2List = genList.Where(x => x.Star == 2);
            IEnumerable<General> star3List = genList.Where(x => x.Star == 3);
            IEnumerable<General> star4List = genList.Where(x => x.Star == 4);
            IEnumerable<General> star5List = genList.Where(x => x.Star == 5 && x.Usage == 1);

            foreach (General gen in star2List)
            {
                star2Str += gen.ID.ToString() + ",";
            }
            star2Str = star2Str.TrimEnd(',');

            foreach (General gen in star3List)
            {
                star3Str += gen.ID.ToString() + ",";
            }
            star3Str = star3Str.TrimEnd(',');

            foreach (General gen in star4List)
            {
                star4Str += gen.ID.ToString() + ",";
            }
            star4Str= star4Str.TrimEnd(',');

            foreach (General gen in star5List)
            {
                star5Str += gen.ID.ToString() + ",";
            }
            star5Str= star5Str.TrimEnd(',');

            textBox2.Text = String.Format("{{[2]={{ {0} }},[3]={{ {1} }},[4]={{ {2} }},[5]={{ {3} }}}}", star2Str,star3Str,star4Str,star5Str);
        }
    }
}
