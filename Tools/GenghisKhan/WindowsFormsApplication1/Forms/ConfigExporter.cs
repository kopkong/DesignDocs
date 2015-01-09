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
using System.Runtime.Serialization;

namespace WindowsFormsApplication1
{
    public partial class ConfigExporter : Form
    {
        public ConfigExporter()
        {
            InitializeComponent();
        }

        //是否导出标准格式的Json
        private static bool UseStandardFormatJson = true;

        private const string LUASCRIPT_NAME_GUIDE = "GKGuideStep.lua";
        private const string LUASCRIPT_NAME_ARMORUPGRADE = "ArmorUpgradeMoney.lua";
        private const string LUASCRIPT_NAME_SKILLUPGRADE = "GeneralSkillUpgradeMoney.lua";
        private const string LUASCRIPT_NAME_SOLDIERUPGRADE = "SoldierUpgradeMoney.lua";
        private const string LUASCRIPT_NAME_ANGELUPGRADE = "AngelUpgradeHeroSoul.lua";
        private const string LUASCRIPT_NAME_ANGELSKILLUPGRADE = "AngelSkillUpgradeMoney.lua";
        private const string LUASCRIPT_NAME_ARENAREWARD = "ArenaRewardTable.lua";

        /// <summary>
        /// 导出lua配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            List<int> armorCosts = new List<int>();
            List<int> generalSkillCosts = new List<int>();
            List<int> soldierCosts = new List<int>();
            List<int> angelCosts = new List<int>();
            Dictionary<string, List<int>> angelSkillCosts = new Dictionary<string, List<int>>();

            for (int lv = 1; lv <= 100; lv++)
            {
                armorCosts.Add(Formula.UpgradeCostMoney(1, lv));

                generalSkillCosts.Add(Formula.UpgradeCostMoney(2, lv));

                soldierCosts.Add(Formula.UpgradeCostMoney(3, lv));

                if(lv <= 50)
                {
                    angelCosts.Add(Formula.UpgradeAngelCostHeroSoul(lv));
                }
            }

            string[] angelSkillString = { "", "AngelSkill1Money", "AngelSkill2Money", "AngelSkill3Money", "AngelSKill4Money" };
            int[] angelSkillLimit = {0,0,10,20,30};

            for (int i = 1; i <= 4; i++)
            {
                List<int> list = new List<int>();
                for (int lv = 1; lv <= 50 - angelSkillLimit[i]; lv++)
                {
                    list.Add(Formula.UpgradeAngelSkillCostMoney(i,lv));
                }
                angelSkillCosts.Add(angelSkillString[i], list);
            }

            exportLuaConfig(armorCosts, LUASCRIPT_NAME_ARMORUPGRADE, "GetUpgradeArmorMoneyCost");
            exportLuaConfig(generalSkillCosts, LUASCRIPT_NAME_SKILLUPGRADE,"GetUpgradeGeneralSkillMoneyCost");
            exportLuaConfig(soldierCosts, LUASCRIPT_NAME_SOLDIERUPGRADE,"GetUpgradeSoldierMoneyCost");
            exportLuaConfig(angelCosts, LUASCRIPT_NAME_ANGELUPGRADE, "GetUpgradeAngelHeroSoulCost");
            exportLuaConfig(angelSkillCosts, LUASCRIPT_NAME_ANGELSKILLUPGRADE, "GetUpgradeAngelSkillMoneyCost");
            exportArenaRewardConfig();
            exportGuideConfig();
            
        }

        /// <summary>
        /// 重命名json配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 批量copy Json配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(System.Environment.CurrentDirectory, "*.json");
            string path1 = @"D:\DesignDocs\RealFightSimu\Resources\Config";
            string path2 = @"D:\0code\client32\Resources\Jsonconfig";
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

        

        /// <summary>
        /// 导出所有json配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            exportJsonConfig<AllConfig>(DBConfigMgr.Instance.MapAllConfig, "allconfig.json");
            exportJsonConfig<General>(DBConfigMgr.Instance.MapGeneral, "general.json");
            exportJsonConfig<Soldier>(DBConfigMgr.Instance.MapSoldier, "soldier.json");
            exportJsonConfig<Item>(DBConfigMgr.Instance.MapItem, "item.json");
            exportJsonConfig<Armor>(DBConfigMgr.Instance.MapArmor, "armor.json");
            exportJsonConfig<ArmorMaterial>(DBConfigMgr.Instance.MapArmorMaterial, "armorMaterial.json");
            exportJsonConfig<SoldierMaterial>(DBConfigMgr.Instance.MapSoldierMaterial, "soldierMaterial.json");
            exportJsonConfig<Experience>(DBConfigMgr.Instance.MapExperience, "experience.json");
            exportJsonConfig<Chapter>(DBConfigMgr.Instance.MapChapter, "chapter.json");
            exportJsonConfig<Level>(DBConfigMgr.Instance.MapLevel, "level.json");
            exportJsonConfig<Nobility>(DBConfigMgr.Instance.MapNobility, "nobility.json");
            exportJsonConfig<Task>(DBConfigMgr.Instance.MapTask, "task.json");
            exportJsonConfig<Skill>(DBConfigMgr.Instance.MapSkill, "skill.json");
            exportJsonConfig<Effect>(DBConfigMgr.Instance.MapEffect, "effect.json");
            exportJsonConfig<Bullet>(DBConfigMgr.Instance.MapBullet, "bullet.json");
            exportJsonConfig<Angel>(DBConfigMgr.Instance.MapAngel, "angel.json");
            exportJsonConfig<MaxSquad>(DBConfigMgr.Instance.MapMaxSquads, "maxSquads.json");
            exportJsonConfig<CheckIn>(DBConfigMgr.Instance.MapCheckIn, "checkin.json");
            exportJsonConfig<Shop>(DBConfigMgr.Instance.MapShop, "shop.json");
            exportJsonConfig<Goods>(DBConfigMgr.Instance.MapGoods,"goods.json");
            exportJsonConfig<Plot>(DBConfigMgr.Instance.MapPlot, "plot.json");
            exportJsonConfig<GeneralMaterial>(DBConfigMgr.Instance.MapGeneralMaterial, "generalMaterial.json");
            exportJsonConfig<Buff>(DBConfigMgr.Instance.MapBuff, "buff.json");
            exportJsonConfig<Scene>(DBConfigMgr.Instance.MapScene, "scene.json");
            exportJsonConfig<VIP>(DBConfigMgr.Instance.MapVIP, "vip.json");
            exportJsonConfig<Recharge>(DBConfigMgr.Instance.MapRecharge, "recharge.json");
        }

        /// <summary>
        /// 导出配置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <param name="fileName"></param>
        private void exportJsonConfig<T>(IDictionary<int,T> dict, string fileName)
        {
            var encoding = new UTF8Encoding(false);

            string str = Utility.parseDataToJsonString<T>(dict.Values, UseStandardFormatJson);
            string path = Path.Combine(System.Environment.CurrentDirectory, fileName);
            File.WriteAllText(path, str, encoding);
            textBox1.Text += fileName + " 生成成功" + Environment.NewLine;
        }

        /// <summary>
        /// 导出lua配置
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fileName"></param>
        /// <param name="functionName"></param>
        private void exportLuaConfig(List<int> list, string fileName, string functionName)
        {
            var encoding = new UTF8Encoding(false);
            StringBuilder sb = new StringBuilder();
            sb.Append("local upgrademoney=" + Environment.NewLine);
            sb.Append("{");

            for (int i = 0; i < list.Count(); i++)
            {
                sb.AppendFormat("[{0}] = {{money={1}}},", i + 1, list[i]);
            }

            sb.Append("}" + Environment.NewLine);
            sb.AppendFormat(@"
local totalmoney = 0;
for i=1,#upgrademoney do
	totalmoney = totalmoney + upgrademoney[i].money;
	upgrademoney[i].totalmoney = totalmoney;
end

function {0}(curlevel, destlevel)
	return upgrademoney[destlevel].totalmoney - upgrademoney[curlevel].totalmoney;
end", functionName);

            string path = Path.Combine(System.Environment.CurrentDirectory, fileName);
            File.WriteAllText(path, sb.ToString(), encoding);
            textBox1.Text += fileName + " 生成成功" + Environment.NewLine;

        }

        /// <summary>
        /// 导出lua配置,参数为Dictionary
        /// </summary>
        /// <param name="list"></param>
        /// <param name="fileName"></param>
        /// <param name="functionName"></param>
        private void exportLuaConfig(Dictionary<string,List<int>> dict, string fileName, string functionName)
        {
            var encoding = new UTF8Encoding(false);
            StringBuilder sb = new StringBuilder();

            foreach (string key in dict.Keys)
            {
                sb.AppendFormat("local {0}=" + Environment.NewLine, key);
                sb.Append("{");

                for (int i = 0; i < dict[key].Count(); i++)
                {
                    sb.AppendFormat("[{0}] = {{money={1}}},", i + 1, dict[key][i]);
                }

                sb.Append("}" + Environment.NewLine);
                sb.AppendFormat(@"
local totalmoney = 0;
for i=1,#{0} do
	totalmoney = totalmoney + {0}[i].money;
	{0}[i].totalmoney = totalmoney;
end" + Environment.NewLine, key);
            };

            sb.Append(@"local upgrademoney = {" + Environment.NewLine);
            foreach (string key in dict.Keys)
            {
                sb.AppendFormat("{0},",key);
            };
            sb.Append("}" + Environment.NewLine);
            
            sb.AppendFormat(@"
-- index 是技能次序，从1开始。分别是1、2、3、4
function {0}(index,curlevel, destlevel)
	return upgrademoney[index][destlevel].totalmoney - upgrademoney[index][curlevel].totalmoney;
end" + Environment.NewLine, functionName);

            string path = Path.Combine(System.Environment.CurrentDirectory, fileName);
            File.WriteAllText(path, sb.ToString(), encoding);
            textBox1.Text += fileName + " 生成成功" + Environment.NewLine;
        }

        /// <summary>
        /// 导出竞技场奖励配置
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="fileName"></param>
        /// <param name="functionName"></param>
        private void exportArenaRewardConfig()
        {
            var encoding = new UTF8Encoding(false);
            StringBuilder sb = new StringBuilder();
            sb.Append("local tb=" + Environment.NewLine);
            sb.AppendLine("{");

            Dictionary<int, string> dict = Formula.ArenaHonerReward();

            for (int i = 0; i < dict.Count(); i++)
            {
                sb.AppendFormat("{{ rank = {0}, rewards= {{ {1} }} }}," + Environment.NewLine, dict.ElementAt(i).Key, dict.ElementAt(i).Value) ;
            };

            sb.Append("};" + Environment.NewLine);
            sb.AppendFormat(@"
GetArenaReward = function ( rank )
	for i=1, #tb do
        local v=tb[i];
		if v.rank >= rank then
			return v.rank,v.rewards;
		end
	end

	return;
end");
            string fileName = LUASCRIPT_NAME_ARENAREWARD;
            string path = Path.Combine(System.Environment.CurrentDirectory, fileName);
            File.WriteAllText(path, sb.ToString(), encoding);
            textBox1.Text += fileName + " 生成成功" + Environment.NewLine;
        }

        private void exportGuideConfig()
        {
            var encoding = new UTF8Encoding(false);
            GuideConfigExporter exporter = new GuideConfigExporter();
            string fileName = LUASCRIPT_NAME_GUIDE;
            string path = Path.Combine(System.Environment.CurrentDirectory, fileName);
            StringBuilder sb = exporter.exportLuaConfig();
            File.WriteAllText(path, sb.ToString(), encoding);
            textBox1.Text += fileName + " 生成成功" + Environment.NewLine;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(System.Environment.CurrentDirectory);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string codePath = @"D:\0code\";
            System.Diagnostics.Process.Start(codePath);
        }

        /// <summary>
        /// 拷贝lua配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            string[] files = Directory.GetFiles(System.Environment.CurrentDirectory, "*.lua");
            string clientLuaPath = @"D:\0code\client32\Resources\uiScript\netLuaData";
            string gameServerPath = @"D:\0code\server\Resource\Script\GameServer";
            string centerServerPath = @"D:\0code\server\Resource\Script\CenterServer";
            string guidePath = @"D:\0code\client32\Resources\uiScript\commonGuide\step";

            if (!Directory.Exists(clientLuaPath))
            {
                textBox1.Text += "找不到路径" + clientLuaPath;
            }
            else
            {
                File.Copy(Path.Combine(System.Environment.CurrentDirectory, LUASCRIPT_NAME_ARMORUPGRADE), 
                    Path.Combine(clientLuaPath,LUASCRIPT_NAME_ARMORUPGRADE),true);
                File.Copy(Path.Combine(System.Environment.CurrentDirectory, LUASCRIPT_NAME_SKILLUPGRADE),
                    Path.Combine(clientLuaPath,LUASCRIPT_NAME_SKILLUPGRADE),true);
                File.Copy(Path.Combine(System.Environment.CurrentDirectory, LUASCRIPT_NAME_ANGELUPGRADE),
                    Path.Combine(clientLuaPath,LUASCRIPT_NAME_ANGELUPGRADE),true);
                File.Copy(Path.Combine(System.Environment.CurrentDirectory, LUASCRIPT_NAME_ANGELSKILLUPGRADE),
                    Path.Combine(clientLuaPath,LUASCRIPT_NAME_ANGELSKILLUPGRADE),true);
                File.Copy(Path.Combine(System.Environment.CurrentDirectory, LUASCRIPT_NAME_ARENAREWARD),
                    Path.Combine(clientLuaPath, LUASCRIPT_NAME_ARENAREWARD), true);
            }

            if (!Directory.Exists(gameServerPath))
            {
                textBox1.Text += "找不到路径" + gameServerPath;
            }
            else
            {
                File.Copy(Path.Combine(System.Environment.CurrentDirectory, LUASCRIPT_NAME_ARMORUPGRADE),
                    Path.Combine(gameServerPath,LUASCRIPT_NAME_ARMORUPGRADE),true);
                File.Copy(Path.Combine(System.Environment.CurrentDirectory, LUASCRIPT_NAME_SKILLUPGRADE),
                    Path.Combine(gameServerPath,LUASCRIPT_NAME_SKILLUPGRADE),true);
                File.Copy(Path.Combine(System.Environment.CurrentDirectory, LUASCRIPT_NAME_ANGELUPGRADE), 
                    Path.Combine(gameServerPath,LUASCRIPT_NAME_ANGELUPGRADE),true);
                File.Copy(Path.Combine(System.Environment.CurrentDirectory, LUASCRIPT_NAME_ANGELSKILLUPGRADE),
                    Path.Combine(gameServerPath, LUASCRIPT_NAME_ANGELSKILLUPGRADE), true);
            }

            if (!Directory.Exists(centerServerPath))
            {
                textBox1.Text += "找不到路径" + centerServerPath;
            }
            else
            {
                File.Copy(Path.Combine(System.Environment.CurrentDirectory, LUASCRIPT_NAME_ARENAREWARD),
                    Path.Combine(centerServerPath,LUASCRIPT_NAME_ARENAREWARD),true);
            }

            if (!Directory.Exists(guidePath))
            {
                textBox1.Text += "找不到路径" + guidePath;
            }
            else
            {
                File.Copy(Path.Combine(System.Environment.CurrentDirectory, LUASCRIPT_NAME_GUIDE),
                    Path.Combine(guidePath, LUASCRIPT_NAME_GUIDE), true);
            }

            textBox1.Text = "lua拷贝完成";
        }

    }
}
