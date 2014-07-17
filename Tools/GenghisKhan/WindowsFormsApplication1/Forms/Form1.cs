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
using System.Reflection;

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
            string path2 = @"D:\0code\client\Resources\Jsonconfig";
            string path3 = @"D:\0code\server\Resource\GameConfig";

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

        private string parseDataToJsonString<T>(System.Collections.Generic.ICollection<T> list)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            for (int i =0 ; i < list.Count();i++)
            {
                T config = list.ElementAt(i);
                sb.Append("{");
                int count = 0;
                int allCount = config.GetType().GetProperties().Count();

                foreach (PropertyInfo pI in config.GetType().GetProperties())
                {
                    Type type = pI.PropertyType;
                    string name = pI.Name;
                    Object value = pI.GetValue(config);

                    if (type == typeof(String))
                    {
                        sb.AppendFormat("\"{0}\":\"{1}\"",name, value.ToString());
                    }
                    else
                    {
                        sb.AppendFormat("\"{0}\":{1}",name, value.ToString());
                    }
                    
                    count ++;
                    if(count< allCount)
                        sb.Append(",");
                }

                if (i < list.Count()- 1)
                    sb.Append("},");
                else
                    sb.Append("}");
            }

            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// 导出配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            var encoding = new UTF8Encoding(false);
            
            // general
            string str = parseDataToJsonString<General>(DBConfigMgr.Instance.MapGeneral.Values);

            string fileName = Path.Combine(System.Environment.CurrentDirectory, "general.json");
            File.WriteAllText(fileName, str, encoding);
            textBox1.Text+= "general.json 生成成功" + Environment.NewLine;
            

            {
                // soldier
                str = parseDataToJsonString<Soldier>(DBConfigMgr.Instance.MapSoldier.Values);
                fileName = Path.Combine(System.Environment.CurrentDirectory, "soldier.json");
                File.WriteAllText(fileName, str, encoding);
                textBox1.Text += "soldier.json 生成成功\n" + Environment.NewLine;
            }

            {
                // item
                str = parseDataToJsonString<Item>(DBConfigMgr.Instance.MapItem.Values);
                fileName = Path.Combine(System.Environment.CurrentDirectory, "item.json");
                File.WriteAllText(fileName, str, encoding);
                textBox1.Text += "item.json 生成成功\n" + Environment.NewLine;
            }

            {
                // armor
                str = parseDataToJsonString<Armor>(DBConfigMgr.Instance.MapArmor.Values);
                fileName = Path.Combine(System.Environment.CurrentDirectory, "armor.json");
                File.WriteAllText(fileName, str, encoding);
                textBox1.Text += "armor.json 生成成功\n" + Environment.NewLine;
            }

            {
                // soldierMaterial
                str = parseDataToJsonString<SoldierMaterial>(DBConfigMgr.Instance.MapSoldierMaterial.Values);
                fileName = Path.Combine(System.Environment.CurrentDirectory, "soldierMaterial.json");
                File.WriteAllText(fileName, str, encoding);
                textBox1.Text += "soldierMaterial.json 生成成功\n" + Environment.NewLine;
            }

            {
                // armorMaterial
                str = parseDataToJsonString<ArmorMaterial>(DBConfigMgr.Instance.MapArmorMaterial.Values);
                fileName = Path.Combine(System.Environment.CurrentDirectory, "armorMaterial.json");
                File.WriteAllText(fileName, str, encoding);
                textBox1.Text += "armorMaterial.json 生成成功\n" + Environment.NewLine;
            }

            {
                // experience
                str = parseDataToJsonString<Experience>(DBConfigMgr.Instance.MapExperience.Values);
                fileName = Path.Combine(System.Environment.CurrentDirectory, "experience.json");
                File.WriteAllText(fileName, str, encoding);
                textBox1.Text += "experience.json 生成成功\n" + Environment.NewLine;
            }

            {
                // chapter
                str = parseDataToJsonString<Chapter>(DBConfigMgr.Instance.MapChapter.Values);
                fileName = Path.Combine(System.Environment.CurrentDirectory, "chapter.json");
                File.WriteAllText(fileName, str, encoding);
                textBox1.Text += "chapter.json 生成成功\n" + Environment.NewLine;
            }

            {
                // level
                str = parseDataToJsonString<Level>(DBConfigMgr.Instance.MapLevel.Values);
                fileName = Path.Combine(System.Environment.CurrentDirectory, "level.json");
                File.WriteAllText(fileName, str, encoding);
                textBox1.Text += "level.json 生成成功\n" + Environment.NewLine;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(System.Environment.CurrentDirectory);
        }
    }
}
