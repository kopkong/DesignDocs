using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Forms
{
    public partial class LevelPlot : Form
    {
        public LevelPlot()
        {
            InitializeComponent();
        }

        public int LevelID { get; set; }

        private string BeforeBattleWords = String.Empty ;
        private string InBattleWords = String.Empty;
        private string AfterBattleWords = String.Empty ;

        public void Init()
        {
            if (DBConfigMgr.Instance.MapPlot.ContainsKey(LevelID))
            {
                BeforeBattleWords = DBConfigMgr.Instance.MapPlot[LevelID].BeforeBattle;
                InBattleWords = DBConfigMgr.Instance.MapPlot[LevelID].InBattle;
                AfterBattleWords = DBConfigMgr.Instance.MapPlot[LevelID].AfterBattle;

                DisplayWords(BeforeBattleWords, TB_Words1);
                DisplayWords(InBattleWords, TB_Words2);
                DisplayWords(AfterBattleWords, TB_Words3);
            }
        }

        private void DisplayWords(string words,TextBox tb)
        {
            if (words.Length > 0)
            {
                tb.Text = words.Replace("|", Environment.NewLine);
                tb.Text = words.Replace(";", Environment.NewLine);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string talker = TB_ID1.Text;
            string word = TB_W1.Text;

            BeforeBattleWords += string.Format("{0}|{1};", talker, word);

            DisplayWords(BeforeBattleWords, TB_Words1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string talker = TB_ID2.Text;
            string word = TB_W2.Text;

            InBattleWords += string.Format("{0}|{1};", talker, word);

            DisplayWords(InBattleWords, TB_Words2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string talker = TB_ID3.Text;
            string word = TB_W3.Text;

            AfterBattleWords += string.Format("{0}|{1};", talker, word);

            DisplayWords(AfterBattleWords, TB_Words3);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BeforeBattleWords = TB_Words1.Text.Replace(Environment.NewLine, ";");
            InBattleWords = TB_Words2.Text.Replace(Environment.NewLine, ";");
            AfterBattleWords = TB_Words3.Text.Replace(Environment.NewLine, ";");

            DBConfigMgr.Instance.UpdatePlot(this.LevelID, BeforeBattleWords, InBattleWords, AfterBattleWords);

            MessageBox.Show("保存剧情成功！");
        }
    }
}
