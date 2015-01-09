using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace WindowsFormsApplication1.Forms
{
    public partial class UCArena : UserControl
    {
        public class Player
        {
            public int ID;
            public string Name;
            public int BattlePoint;
            public int Score;
            public int WinCount;
            public int LoseCount;
            public bool ISNPC;

            public Player(int id, string name, int battlePoint,bool isNPC)
            {
                ID = id;
                Name = name;
                BattlePoint = battlePoint;
                Score = isNPC?200:100;
                WinCount = 0;
                LoseCount = 0;
                ISNPC = isNPC;
            }
        }

        public UCArena()
        {
            InitializeComponent();
        }

        Random rnd = new Random();
        private List<Player> _arenaList = new List<Player>();
        Thread uiThread = null;
        FileStream fs; 
        StreamWriter sw; 

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(textBox2.Text);
            int count = value;
            _arenaList.Clear();
            while (count > 0)
            {
                Player p = new Player(count,randomName(),count * 150,false);
                _arenaList.Add(p);

                count--;
            }

            // 加同样多的机器人
            int robotCount = value;
            while (robotCount > 0)
            {
                Player p = new Player(value + robotCount, randomNPCName(), robotCount * 100, true);
                _arenaList.Add(p);
                robotCount--;
            }


            refreshData();
        }

        private void refreshData()
        {
            dataGridView1.Rows.Clear();

            for (int i = 0; i < _arenaList.Count(); i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                DataGridViewTextBoxCell textBox1 = new DataGridViewTextBoxCell();
                textBox1.Value = i;
                row.Cells.Add(textBox1);

                DataGridViewTextBoxCell textBox2 = new DataGridViewTextBoxCell();
                textBox2.Value = _arenaList.ElementAt(i).Name;
                row.Cells.Add(textBox2);

                DataGridViewTextBoxCell textBox3 = new DataGridViewTextBoxCell();
                textBox3.Value = _arenaList.ElementAt(i).BattlePoint;
                row.Cells.Add(textBox3);

                DataGridViewTextBoxCell textBox4 = new DataGridViewTextBoxCell();
                textBox4.Value = _arenaList.ElementAt(i).Score;
                row.Cells.Add(textBox4);

                DataGridViewTextBoxCell textBox5 = new DataGridViewTextBoxCell();
                textBox5.Value = String.Format("{0}/{1}",_arenaList.ElementAt(i).WinCount,
                    _arenaList.ElementAt(i).LoseCount);
                row.Cells.Add(textBox5);

                dataGridView1.Rows.Add(row);
            }
        }

        private string randomName()
        {
            string[] surNameList = 
            {
                "赵","钱","孙","李","周","吴","郑","王","冯","陈","褚","卫",
                "蒋","沈","韩","杨","朱","秦","尤","许","何","吕","施","张",
                "孔","曹","严","华","金","魏","陶","姜","戚","谢","邹","喻",
                "柏","水","窦","章","云","苏","潘","葛","奚","范","彭","郎",
                "鲁","韦","昌","马","苗","凤","花","方","俞","任","袁","柳",
                "酆","鲍","史","唐","费","廉","岑","薛","雷","贺","倪","汤",
                "滕","殷","罗","毕","郝","邬","安","常","乐","于","时","傅",
                "皮","卞","齐","康","伍","余","元","卜","顾","孟","平","黄",
                "和","穆","萧","尹","姚","邵","湛","汪","祁","毛","禹","狄",
                "米","贝","明","臧","计","伏","成","戴","谈","宋","茅","庞",
                "熊","纪","舒","屈","项","祝","董","梁","杜","阮","蓝","闵",
                "席","季","麻","强","贾","路","娄","危","江","童","颜","郭",
                "梅","盛","林","刁","锺","徐","邱","骆","高","夏","蔡","田",
                "樊","胡","凌","霍","虞","万","支","柯","昝","管","卢","莫",
                "经","房","裘","缪","干","解","应","宗","丁","宣","贲","邓",

                "郁","单","杭","洪","包","诸","左","石","崔","吉","钮","龚",
                "程","嵇","邢","滑","裴","陆","荣","翁","荀","羊","於","惠",
                "甄","麴","家","封","芮","羿","储","靳","汲","邴","糜","松",
                "井","段","富","巫","乌","焦","巴","弓","牧","隗","山","谷",
                "车","侯","宓","蓬","全","郗","班","仰","秋","仲","伊","宫",
                "宁","仇","栾","暴","甘","钭","历","戎","祖","武","符","刘",
                "景","詹","束","龙","叶","幸","司","韶","郜","黎","蓟","溥",
                "印","宿","白","怀","蒲","邰","从","鄂","索","咸","籍","赖",
                "卓","蔺","屠","蒙","池","乔","阳","郁","胥","能","苍","双",
                "闻","莘","党","翟","谭","贡","劳","逄","姬","申","扶","堵",
                "冉","宰","郦","雍","却","璩","桑","桂","濮","牛","寿","通",
                "边","扈","燕","冀","僪","浦","尚","农","温","别","庄","晏",
                "柴","瞿","阎","充","慕","连","茹","习","宦","艾","鱼","容",
                "向","古","易","慎","戈","廖","庾","终","暨","居","衡","步",
                "都","耿","满","弘","匡","国","文","寇","广","禄","阙","东",
            };

            string firstNameList ="晨轩清睿宝涛华国亮新凯志明伟嘉东洪建文子云杰兴友才振辰航达鹏宇衡佳强宁丰波森学民永翔鸿海飞义生凡连良乐勇辉龙川宏谦锋双霆玉智增名进德聚军兵忠廷先江昌政君泽超信腾恒礼元磊阳月士洋欣升恩迅科富函业胜震福瀚瑞朔津韵荣为诚斌广庆成峰可健英功冬锦立正禾平旭同全豪源安顺帆向雄材利希风林奇易来咏岩启坤昊朋和纪艺昭映威奎帅星春营章高伦庭蔚益城牧钊刚洲家晗迎罡浩景珂策皓栋起棠登越盛语钧亿基理采备纶献维瑜齐凤毅谊贤逸卫万臻儒钢洁霖隆远聪耀誉继珑哲岚舜钦琛金彰亭泓蒙祥意鑫朗晟晓晔融谋宪励璟骏颜焘垒尚镇济雨蕾韬选议曦奕彦虹宣蓝冠谱泰泊跃韦怡骁俊沣骅歌畅与圣铭溓滔溪巩影锐展笑祖时略敖堂崊绍崇悦邦望尧珺然涵博淼琪群驰照传诗靖会力大山之中方仁世梓竹至充亦丞州言佚序宜";
            int n1 = rnd.Next(surNameList.Count());
            int n2 = rnd.Next(firstNameList.Length);

            Console.WriteLine(string.Format("N1 = {0}, N2 = {1}",n1,n2));

            return surNameList[n1] + firstNameList.Substring(n2, 1); 
        }

        private string randomNPCName()
        {
            return "机器人" + rnd.Next(1000).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            
            uiThread = new Thread(new ThreadStart(this.simuArena));
            uiThread.Start();            
        }

        private bool SimuTeamFight(int p1, int p2)
        {
            if (p1 > p2 && rnd.Next() < 0.95)
                return true;

            return false;
        }

        private void simuArena()
        {
            //float extraScoreRate = 1.1f;
            string logFile = "D:\\arena.log";
            if (File.Exists(logFile)) File.Delete(logFile);
            fs = new FileStream(logFile, FileMode.Append);
            sw = new StreamWriter(fs, Encoding.Default);

            int turn = 10 * 365; // 要打几轮, 1月
            List<Player> rivals = new List<Player>();
            IEnumerable<Player> players = _arenaList.Where(x => x.ISNPC == false);

            while (turn > 0)
            {
                foreach (Player p in players)
                {
                    // 对手列表
                    rivals.Clear();
                    pickRivals(p, ref rivals);

                    // 随机选择一个对手打
                    int chooseNumber = rnd.Next(10);

                    Player rival = rivals[chooseNumber];

                    int vChange = 0;
                    string message;

                    if (SimuTeamFight(p.BattlePoint, rival.BattlePoint))
                    {
                        // 赢了
                        if (p.Score > rival.Score)
                            vChange = 10;
                        else if (p.Score == rival.Score)
                            vChange = 15;
                        else
                            vChange = Math.Max((int)((rival.Score - p.Score) * 0.5), 20);

                        p.Score += vChange;
                        p.WinCount++;
                        rival.LoseCount++;
                        message = String.Format("{0} 击败了 {1} , 积分 {2}", p.Name, rival.Name, vChange) + Environment.NewLine;
                    }
                    else
                    {
                        // 输了
                        if (p.Score < rival.Score)
                            vChange = 0;
                        else
                            vChange = -20;

                        p.Score += vChange;
                        p.LoseCount++;
                        rival.WinCount++;
                        message = String.Format("{0} 输给了 {1} , 积分 {2}", p.Name, rival.Name, vChange) + Environment.NewLine;
                    }

                    //this.Invoke(new Action<string>(this.displayMessage), message);
                }

                // 一轮打完排序
                _arenaList.Sort((left, right) =>
                {
                    if (left.Score > right.Score)
                        return -1;
                    else if (left.Score == right.Score)
                        return 0;
                    else
                        return 1;
                });

                turn--;
            }

            this.Invoke(new Action(this.finishJob));
        }

        private void pickRivals(Player p , ref List<Player> list)
        {
            int rank = GetRank(p.ID);

            if(rank < 5)
            {
                int count = 10; // 10个对手
                int i = 0;
                while(count > 0)
                {
                    if(rank != i)
                    {
                        list.Add(_arenaList.ElementAt(i));
                        count --;
                    }

                    i++;
                }
            }
            else if (rank >= 5 && rank < _arenaList.Count() - 5 )
            {
                list.Add(_arenaList.ElementAt(rank - 1));
                list.Add(_arenaList.ElementAt(rank - 2));
                list.Add(_arenaList.ElementAt(rank - 3));
                list.Add(_arenaList.ElementAt(rank - 4));
                list.Add(_arenaList.ElementAt(rank - 5));

                list.Add(_arenaList.ElementAt(rank + 1));
                list.Add(_arenaList.ElementAt(rank + 2));
                list.Add(_arenaList.ElementAt(rank + 3));
                list.Add(_arenaList.ElementAt(rank + 4));
                list.Add(_arenaList.ElementAt(rank + 5));
            }
            else if(rank >= _arenaList.Count() - 5)
            {
                int count = 10; // 10个对手
                int i = _arenaList.Count() - 1;
                while (count > 0)
                {
                    if (rank != i)
                    {
                        list.Add(_arenaList.ElementAt(i));
                        count--;
                    }

                    i--;
                }
            }
            

        }

        private int GetRank(int ID)
        {
            for(int i = 0 ;i<_arenaList.Count(); i++)
            {
                if (_arenaList.ElementAt(i).ID == ID)
                    return i;
            }
            return 10000;
        }

        private void displayMessage(string v)
        {
            sw.Write(v);
        }

        private void finishJob()
        {
            // Finish thread
            uiThread.Abort();

            textBox1.Text += "模拟完毕" + Environment.NewLine;
            sw.Close();
            fs.Close();
            
            refreshData();
        }
    }
}
