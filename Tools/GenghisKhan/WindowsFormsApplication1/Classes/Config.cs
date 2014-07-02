using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApplication1
{
    public class ConfigDataMgr
    {
        private static ConfigDataMgr instance;
        private static object syncRoot = new Object();

        public static ConfigDataMgr Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ConfigDataMgr();
                    }
                }

                return instance;
            }
        }

        const string MESSAGE_DUPLICATEID = "有重复的ID";
        const string MESSAGE_PASS = "通过了检查";
        const string MESSAGE_NOCONTENT = "配置没有内容!";

        StringBuilder message = new StringBuilder();

        // 不能使用这个字符串作为数据列名
        const string INVALID_DATAMEMBER_NAME = "ConfigType";

        const string ALL_CONFIG = "allconfig.json";
        const string WUJIANG_CONFIG = "general.json";
        const string SOLDIER_CONFIG = "soldier.json";
        const string ITEM_CONFIG = "item.json";
        const string ARMOR_CONFIG = "armor.json";
        const string SOLDIERLMATERIAL_CONFIG = "soldierMaterial.json";
        const string ARMORMATERIAL_CONFIG = "armorMaterial.json";
        const string ANGEL_CONFIG = "angel.json";
        const string EXP_CONFIG = "experience.json";

        List<AllConfig> _ListAllConfig;
        List<General> _ListGeneral;
        List<Soldier> _ListSoldier;
        List<Armor> _ListArmor;
        List<Item> _ListItem;
        List<Angel> _ListAngel;
        List<Experience> _ListExperience;

        public IDictionary<int, AllConfig> _MapAllConfig;
        public IDictionary<int, General> _MapGeneral;
        public IDictionary<int, Soldier> _MapSoldier;
        public IDictionary<int, Armor> _MapArmor;
        public IDictionary<int, Item> _MapItem;
        public IDictionary<int, Angel> _MapAngel;
        public IDictionary<int, Experience> _MapExperience;

        public void Init()
        {
            message.Append(checkFileExists());

            message.Append(checkAllConfigJson());
            message.Append(checkWujiangJson());
            message.Append(checkSoldierJson());
            message.Append(checkAngelJson());

            Console.Write(message.ToString());
        }

        private string checkFileExists()
        {
            StringBuilder sb = new StringBuilder();
            bool allSuccess = true;
            if (!File.Exists(ALL_CONFIG))
            {
                allSuccess = false;
                sb.AppendLine("没有找到AllConfig!");
            }
            else
            {
                _ListAllConfig = new List<AllConfig>();
                _MapAllConfig = new Dictionary<int, AllConfig>();
                JsonHelper.GetJsonStringArray<AllConfig>(ALL_CONFIG, ref _ListAllConfig);
                _ListAllConfig.ForEach(delegate(AllConfig config)
                {
                    if (_MapAllConfig.ContainsKey(config.Type))
                        sb.AppendLine(ALL_CONFIG + MESSAGE_DUPLICATEID);
                    else
                        _MapAllConfig.Add(config.Type, config);
                });
            }

            if (!File.Exists(WUJIANG_CONFIG))
            {
                allSuccess = false;
                sb.AppendLine("没有发现武将配置文件！");
            }
            else
            {
                _ListGeneral = new List<General>();
                _MapGeneral = new Dictionary<int, General>();
                JsonHelper.GetJsonStringArray<General>(WUJIANG_CONFIG, ref _ListGeneral);
                _ListGeneral.ForEach(delegate(General g)
                {
                    if (_MapGeneral.ContainsKey(g.ID))
                        sb.AppendLine(WUJIANG_CONFIG + MESSAGE_DUPLICATEID);
                    else
                        _MapGeneral.Add(g.ID, g);
                });
            }

            if (!File.Exists(ITEM_CONFIG))
            {
                allSuccess = false;
                sb.AppendLine("没有发现道具配置文件！");
            }
            else
            {
                _ListItem = new List<Item>();
                _MapItem = new Dictionary<int, Item>();
                JsonHelper.GetJsonStringArray<Item>(ITEM_CONFIG, ref _ListItem);
                _ListItem.ForEach(delegate(Item g)
                {
                    if (_MapItem.ContainsKey(g.ID))
                        sb.AppendLine(ITEM_CONFIG + MESSAGE_DUPLICATEID);
                    else
                        _MapItem.Add(g.ID, g);
                });
            }

            if (!File.Exists(ARMOR_CONFIG))
            {
                allSuccess = false;
                sb.AppendLine("没有发现装备配置文件！");
            }
            else
            {
                _ListArmor = new List<Armor>();
                _MapArmor = new Dictionary<int, Armor>();
                JsonHelper.GetJsonStringArray<Armor>(ARMOR_CONFIG, ref _ListArmor);
                _ListArmor.ForEach(delegate(Armor g)
                {
                    if (_MapArmor.ContainsKey(g.ID))
                        sb.AppendLine(ARMOR_CONFIG + MESSAGE_DUPLICATEID);
                    else
                        _MapArmor.Add(g.ID, g);
                });
            }

            if (!File.Exists(SOLDIER_CONFIG))
            {
                allSuccess = false;
                sb.AppendLine("没有发现部队配置文件！");
            }
            else
            {
                _ListSoldier = new List<Soldier>();
                _MapSoldier = new Dictionary<int, Soldier>();
                JsonHelper.GetJsonStringArray<Soldier>(SOLDIER_CONFIG, ref _ListSoldier);
                _ListSoldier.ForEach(delegate(Soldier g)
                {
                    if (_MapSoldier.ContainsKey(g.ID))
                        sb.AppendLine(SOLDIER_CONFIG + MESSAGE_DUPLICATEID);
                    else
                        _MapSoldier.Add(g.ID, g);
                });
            }

            if (!File.Exists(ARMORMATERIAL_CONFIG))
            {
                allSuccess = false;
                sb.AppendLine("没有发现装备碎片配置文件！");
            }

            if (!File.Exists(SOLDIERLMATERIAL_CONFIG))
            {
                allSuccess = false;
                sb.AppendLine("没有发现部队碎片配置文件！");
            }

            if (!File.Exists(ANGEL_CONFIG))
            {
                allSuccess = false;
                sb.AppendLine("没有发现守护神配置文件！");
            }
            else
            {
                _ListAngel = new List<Angel>();
                _MapAngel = new Dictionary<int, Angel>();
                JsonHelper.GetJsonStringArray<Angel>(ANGEL_CONFIG, ref _ListAngel);
                _ListAngel.ForEach(delegate(Angel g)
                {
                    if (_MapAngel.ContainsKey(g.ID))
                        sb.AppendLine(ANGEL_CONFIG + MESSAGE_DUPLICATEID);
                    else
                        _MapAngel.Add(g.ID, g);
                });
            }

            if (!File.Exists(EXP_CONFIG))
            {
                allSuccess = false;
                sb.AppendLine("没有经验配置");
            }
            else
            {
                _MapExperience = new Dictionary<int, Experience>();
                JsonHelper.GetJsonStringArray<Experience>(EXP_CONFIG, ref _ListExperience);
                _ListExperience.ForEach(delegate(Experience e)
                {
                    if (_MapExperience.ContainsKey(e.ID))
                        sb.AppendLine(EXP_CONFIG + MESSAGE_DUPLICATEID);
                    else
                        _MapExperience.Add(e.ID, e);
                });
            }

            if (allSuccess)
            {
                sb.AppendLine("没有缺少任何配置文件！");
            }

            return sb.ToString();
        }

        private string checkAllConfigJson()
        {
            StringBuilder sb = new StringBuilder();
            if (_ListAllConfig.Count == 0)
            {
                sb.AppendLine("AllConfig 文件是空的！");
            }
            else
            {
                bool allaboveZero = _ListAllConfig.All(config =>
                    config.Type > 0);

                if (!allaboveZero)
                {
                    sb.AppendLine("TypeID不能小于等于0");
                }

                _ListAllConfig.ForEach(delegate(AllConfig config)
                {
                    if (!config.File.Contains(".json"))
                    {
                        sb.AppendFormat("AllConfig 文件中Type【%d】的File格式不正确", config.Type);
                    }
                });

                if (sb.Length == 0)
                    sb.AppendLine(ALL_CONFIG + MESSAGE_PASS);
            }
            return sb.ToString();
        }

        private string checkWujiangJson()
        {
            StringBuilder sb = new StringBuilder();
            if (_ListGeneral.Count == 0)
            {
                sb.AppendLine(WUJIANG_CONFIG + MESSAGE_NOCONTENT);
            }
            else
            {
                // 具体对每项的检查在这里
                _ListGeneral.ForEach(delegate(General g)
                {
                    if (g.ID <= 0)
                    {
                        sb.AppendFormat("general配置中ID %d 不合法，不能小于0" + Environment.NewLine, g.ID);
                    }

                    if (g.SoldierType < 1 || g.SoldierType > 3)
                    {
                        sb.AppendFormat("general配置中ID为%d 的项， SoldierType %d不合法" + Environment.NewLine, g.ID, g.SoldierType);
                    }

                    if (!_MapSoldier.ContainsKey(g.InitialSoldier))
                    {
                        sb.AppendFormat("general配置中ID为%d 的项， InitialSoldier错误，在soldier配置中找不到这项" + Environment.NewLine, g.ID);
                    }
                });

            }
            if (sb.Length == 0)
                sb.AppendLine(WUJIANG_CONFIG + MESSAGE_PASS);

            return sb.ToString();
        }

        private string checkSoldierJson()
        {
            StringBuilder sb = new StringBuilder();
            if (_ListSoldier.Count == 0)
            {
                sb.AppendLine(SOLDIER_CONFIG + MESSAGE_NOCONTENT);
            }
            else
            {
                // 具体对每项的检查在这里
                _ListSoldier.ForEach(delegate(Soldier config)
                {
                    if (config.ID <= 0)
                    {
                        sb.AppendFormat("solider配置中ID %d 不合法，不能小于0" + Environment.NewLine, config.ID);
                    }

                    if (config.SoldierType < 1 || config.SoldierType > 3)
                    {
                        sb.AppendFormat("solider配置中ID为%d 的项， SoldierType %d不合法" + Environment.NewLine, config.ID, config.SoldierType);
                    }
                });
            }

            if (sb.Length == 0)
                sb.AppendLine(SOLDIER_CONFIG + MESSAGE_PASS);

            return sb.ToString();
        }

        private string checkItemjson()
        {
            StringBuilder sb = new StringBuilder();

            return sb.ToString();
        }

        private string checkArmorjson()
        {
            StringBuilder sb = new StringBuilder();

            return sb.ToString();
        }

        private string checkArmorMaterialjson()
        {
            StringBuilder sb = new StringBuilder();

            return sb.ToString();
        }

        private string checkSoldierMaterialjson()
        {
            StringBuilder sb = new StringBuilder();

            return sb.ToString();
        }

        private string checkAngelJson()
        {
            StringBuilder sb = new StringBuilder();
            if (_ListAngel.Count == 0)
            {
                sb.AppendLine(ANGEL_CONFIG + MESSAGE_NOCONTENT);
            }
            else
            {
                // 具体对每项的检查在这里
                _ListAngel.ForEach(delegate(Angel config)
                {
                    if (config.ID <= 0)
                    {
                        sb.AppendFormat("angel配置中ID %d 不合法，不能小于0" + Environment.NewLine, config.ID);
                    }

                });
            }

            if (sb.Length == 0)
                sb.AppendLine(ANGEL_CONFIG + MESSAGE_PASS);

            return sb.ToString();
        }
    }
}
