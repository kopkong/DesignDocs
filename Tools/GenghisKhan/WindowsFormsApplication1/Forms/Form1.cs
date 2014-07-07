using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using WindowsFormsApplication1;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private delegate void BeginInvokeDelegate();

        Thread uiThread = null;

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += ConfigMgr.Instance.ErrorMessage;
            //uiThread = new Thread(new ThreadStart(this.updateLabel));
            //uiThread.Start();

            //Thread.Sleep(1000);
            //uiThread.Abort();
            
        }

        private void updateLabel()
        {
            for (int i = 10; i < 100; i++)
            {
                Thread.Sleep(100);
                this.Invoke(new Action<int>(this.testA), i);
            }

            this.Invoke(new Action(this.testB));
            
        }

        private void testA(int v)
        {
            this.label1.Text = v.ToString();
        }

        private void testB()
        {
            // Finish thread
            uiThread.Abort();

            textBox1.Text += "TestA done" + Environment.NewLine;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(System.Environment.CurrentDirectory, "*.json");

            foreach (string f in files)
            {
                string cocosGeneratePre = "数据表_";
                if (f.Contains(cocosGeneratePre))
                {
                    File.Copy(f, f.Replace(cocosGeneratePre, ""),true);
                    File.Delete(f);
                }
            }

            textBox1.Text = "批量改名成功";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(System.Environment.CurrentDirectory, "*.json");
            string path1 = @"D:\DesignDocs\RealFightSimu\Resources\Config";
            string path2 = @"D:\GenghisKhan\client\Resources\Jsonconfig";
            string path3 = @"D:\GenghisKhan\server\Resource\GameConfig";

            bool path1Exists = true;
            bool path2Exists = true;
            bool path3Exists = true;

            if(!Directory.Exists(path1))
            {
                textBox1.Text = "找不到路径" + path1;
                path1Exists = false;
            }

            if(!Directory.Exists(path2))
            {
                textBox1.Text = "找不到路径" + path2;
                path2Exists = false;
            }

            if (!Directory.Exists(path3))
            {
                textBox1.Text = "找不到路径" + path3;
                path3Exists = false;
            }

            foreach (string f in files)
            {
                FileInfo file = new FileInfo(f);
                if(path1Exists)
                    File.Copy(f, Path.Combine(path1,file.Name) , true);

                if(path2Exists)
                    File.Copy(f, Path.Combine(path2, file.Name), true);

                if (path3Exists)
                    File.Copy(f, Path.Combine(path3, file.Name), true);
            }

            textBox1.Text = "拷贝完成";
        }
    }
}
