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

namespace WindowsFormsApplication1.Forms
{
    public partial class RenameModel : Form
    {
        public RenameModel()
        {
            InitializeComponent();

            init();
        }

        Dictionary<string, string> ConvertNameSearchTable = new Dictionary<string, string>();

        private void button1_Click(object sender, EventArgs e)
        {
            string sourcePath = TB_SourcePath.Text.Replace("\"","");
            string targetPath = TB_TargetPath.Text;
            string destPath = TB_DestPath.Text;

            string type = RB_General.Checked? "general":"soldier";
            string strID = TB_ID.Text;

            bool isEnemy = false;
            if (sourcePath.Contains("- E"))
            {
                isEnemy = true;
            }

            string finalTargetPath = renameTargetFiles(sourcePath, targetPath, type, strID, isEnemy);

            generateFinalFiles(finalTargetPath, destPath, type, strID, isEnemy);

            MessageBox.Show("处理完毕！");
        }

        /// <summary>
        /// 后缀名
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private string getSufixName(string file)
        {
            string name = "";

            foreach (string key in ConvertNameSearchTable.Keys)
            {
                if (file.Contains(key))
                {
                    name = ConvertNameSearchTable[key];
                    break;
                }
            }

            return name;
        }

        /// <summary>
        /// 建立target 文件的保存目录
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="type"></param>
        /// <param name="strID"></param>
        /// <param name="name"></param>
        /// <param name="isEnemy"></param>
        /// <returns></returns>
        private string createTargetDirectory(string targetPath,string type,string strID,string name,bool isEnemy)
        {
            string modelDirName = string.Format(@"{0}{1}-{2}", type, strID, name);
            if (isEnemy)
            {
                modelDirName += @"_e\";
            }
            else
            {
                modelDirName += "\\";
            }

            modelDirName = Path.Combine(targetPath, modelDirName);
            if (!Directory.Exists(modelDirName))
            {
                Directory.CreateDirectory(modelDirName);
            }

            return modelDirName;
        }

        /// <summary>
        /// 生成最终文件，也就是plist和pvr.ccz
        /// </summary>
        /// <param name="targetPath"></param>
        /// <param name="destPath"></param>
        /// <param name="type"></param>
        /// <param name="strID"></param>
        /// <param name="isEnemy"></param>
        private void generateFinalFiles(string targetPath, string destPath, string type, string strID, bool isEnemy)
        {
            string texturePackerPath = @"C:\Program Files\CodeAndWeb\TexturePacker\bin\TexturePacker.exe";
            string enemyPreString = isEnemy ? "_1" : "";
            string fileName = String.Format("{0}{1}{2}", type, strID, enemyPreString);
            string plistPath = Path.Combine(destPath,fileName + ".plist");
            string texturePath = Path.Combine(destPath,fileName + ".pvr.ccz");

            string cmd = String.Format(" \"{0}\" \"{1}\" --format cocos2d --allow-free-size --sheet \"{2}\" --data \"{3}\" ",
                texturePackerPath, targetPath.TrimEnd('\\'), texturePath,plistPath);

            string output = string.Empty;
            Console.WriteLine(cmd);
            Utility.RunCmd(cmd, out output);

        }

        /// <summary>
        /// 重命名并且拷贝文件到Target 目录中
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="targetPath"></param>
        /// <param name="type"></param>
        /// <param name="strID"></param>
        /// <param name="isEnemy"></param>
        private string renameTargetFiles(string sourcePath,string targetPath,string type,string strID,bool isEnemy)
        {
            string name = string.Empty;
            if (type == "general")
            {
                name = DBConfigMgr.Instance.MapGeneral[Convert.ToInt32(strID)].Name;
            }
            else
            {
                name = DBConfigMgr.Instance.MapSoldier[Convert.ToInt32(strID)].Name;
            }

            // 处理target文件存放的路径
            string finalTargetPath = createTargetDirectory(targetPath, type, strID, name, isEnemy);

            if (Directory.Exists(sourcePath))
            {
                String[] files = Directory.GetFiles(sourcePath);
                foreach (string f in files)
                {
                    string sufix = getSufixName(f);
                    if (sufix != "")
                    {
                        string enemyPreString = isEnemy ? "_1" : "";
                        string newFileName = String.Format("{0}{1}{3}{2}.png", type, strID, sufix, enemyPreString);
                        FileInfo file = new FileInfo(f);

                        string newPath = Path.Combine(finalTargetPath, newFileName);
                        file.CopyTo(newPath, true);
                    }
                }
            }

            return finalTargetPath;
        }

        private void RB_General_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_General.Checked)
            {
                RB_Soldier.Checked = false;
            }

        }

        private void RB_Soldier_CheckedChanged(object sender, EventArgs e)
        {
            if (RB_Soldier.Checked)
            {
                RB_General.Checked = false;
            }
        }

        /// <summary>
        /// 初始化一些东西
        /// </summary>
        private void init()
        {
            TB_TargetPath.Text = @"D:\1art\model\";
            TB_DestPath.Text = @"D:\0code\client32\Resources\ModelResource\Actor";

            TB_BatchGeneralSource.Text = @"D:\1art\美术开发内容\00 角色(s)\动画输出-小版本\武将";
            TB_BatchSoldierSource.Text = @"D:\1art\美术开发内容\00 角色(s)\动画输出-小版本\部队";
            TB_BatchTargetPath.Text = @"D:\1art\model\";
            TB_BatchDestPath.Text = @"D:\0code\client32\Resources\ModelResource\Actor";

            RB_Soldier.Checked = true;

            //1-4    待机
            //5-8    移动
            //9-12   冲锋
            //13-16  攻击
            //17     击飞
            //18-21  死亡
            //22-29  施法

            ConvertNameSearchTable.Add("001.png", "_idle_0000");
            ConvertNameSearchTable.Add("002.png", "_idle_0001");
            ConvertNameSearchTable.Add("003.png", "_idle_0002");
            ConvertNameSearchTable.Add("004.png", "_idle_0003");
            ConvertNameSearchTable.Add("005.png", "_move_0000");
            ConvertNameSearchTable.Add("006.png", "_move_0001");
            ConvertNameSearchTable.Add("007.png", "_move_0002");
            ConvertNameSearchTable.Add("008.png", "_move_0003");
            ConvertNameSearchTable.Add("009.png", "_sprint_0000");
            ConvertNameSearchTable.Add("010.png", "_sprint_0001");
            ConvertNameSearchTable.Add("011.png", "_sprint_0002");
            ConvertNameSearchTable.Add("012.png", "_sprint_0003");
            ConvertNameSearchTable.Add("013.png", "_attack_0000");
            ConvertNameSearchTable.Add("014.png", "_attack_0001");
            ConvertNameSearchTable.Add("015.png", "_attack_0002");
            ConvertNameSearchTable.Add("016.png", "_attack_0003");
            ConvertNameSearchTable.Add("018.png", "_death_0000");
            ConvertNameSearchTable.Add("019.png", "_death_0001");
            ConvertNameSearchTable.Add("020.png", "_death_0002");
            ConvertNameSearchTable.Add("021.png", "_death_0003");
            ConvertNameSearchTable.Add("022.png", "_skill1_0000");
            ConvertNameSearchTable.Add("023.png", "_skill1_0001");
            ConvertNameSearchTable.Add("024.png", "_skill1_0002");
            ConvertNameSearchTable.Add("025.png", "_skill1_0003");
            ConvertNameSearchTable.Add("026.png", "_skill1_0004");
            ConvertNameSearchTable.Add("027.png", "_skill1_0005");
            ConvertNameSearchTable.Add("028.png", "_skill1_0006");
            ConvertNameSearchTable.Add("029.png", "_skill1_0007");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 武将路径
            string generalModelPath = TB_BatchGeneralSource.Text;

            // 士兵路径
            string soldierModelPath = TB_BatchSoldierSource.Text;

            string targetPath = TB_BatchTargetPath.Text;
            string destPath = TB_BatchDestPath.Text;

            if (Directory.Exists(generalModelPath))
            {
                foreach (string dir in Directory.GetDirectories(generalModelPath))
                {
                    int idx1 = dir.IndexOf("[");
                    int idx2 = dir.IndexOf("]");

                    string strID = dir.Substring(idx1 + 1, idx2 - idx1 - 1);
                    string type = "general";

                    bool isEnemy = false;
                    if (dir.Contains("- E"))
                    {
                        isEnemy = true;
                    }

                    string finalTargetPath = renameTargetFiles(dir, targetPath, type, strID, isEnemy);
                    generateFinalFiles(finalTargetPath, destPath, type, strID, isEnemy);
                }
            }

            if (Directory.Exists(soldierModelPath))
            {
                foreach (string dir in Directory.GetDirectories(soldierModelPath))
                {
                    int idx1 = dir.IndexOf("[");
                    int idx2 = dir.IndexOf("]");

                    string strID = dir.Substring(idx1 + 1, idx2 - idx1 - 1);
                    string type = "soldier";

                    bool isEnemy = false;
                    if (dir.Contains("- E"))
                    {
                        isEnemy = true;
                    }

                    string finalTargetPath = renameTargetFiles(dir, targetPath, type, strID, isEnemy);
                    generateFinalFiles(finalTargetPath, destPath, type, strID, isEnemy);
                }
            }
            

            MessageBox.Show("处理完毕!");
        }
    }
}
