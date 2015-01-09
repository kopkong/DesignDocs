using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class EntityInfoFactory
    {
        static public SoldierInfo GetSoldierInfoFromConfig(int configID, int level, int count)
        {
            SoldierInfo s = new SoldierInfo();
            if (DBConfigMgr.Instance.MapSoldier.ContainsKey(configID))
            {
                s.SoldierConfig = DBConfigMgr.Instance.MapSoldier[configID];
            }
            s.Level = level;
            s.Rank = 0;
            s.SoldierCount = count;

            return s;
        }

        static public SoldierInfo GetSoldierInfoFromPlayerSlot(int slotIndex)
        {
            SoldierInfo s = new SoldierInfo();
            SlotSoldier s1 = (SlotSoldier)PlayerDataMgr.Instance.GetPlayerBag(SlotType.SlotType_Soldier)[slotIndex];
            s.SoldierConfig = DBConfigMgr.Instance.MapSoldier[s1.ConfigID];
            s.Level = s1.Lv;
            s.Rank = s1.Rank;
            s.SoldierCount = s1.Count;

            return s;
        }

        static public GeneralInfo GetGeneralInfoFromConfig(int configID, int level, int soldierCount)
        {
            GeneralInfo g = new GeneralInfo();
            g.GeneralConfig = DBConfigMgr.Instance.MapGeneral[configID];
            g.Level = level;
            g.Rank = 0;
            g.SoldierCount = soldierCount;

            return g;
        }

        static public GeneralInfo GetGeneralInfoFromPlayerSlot(int slotIndex)
        {
            GeneralInfo g = new GeneralInfo();
            SlotGeneral s1 = (SlotGeneral)PlayerDataMgr.Instance.GetPlayerBag(SlotType.SlotType_General)[slotIndex];
            SlotSoldier s2 = (SlotSoldier)PlayerDataMgr.Instance.GetPlayerBag(SlotType.SlotType_Soldier)[s1.SoldierIndex];

            g.GeneralConfig = s1.GeneralConfig;
            g.Level = s1.Lv;
            g.Rank = s1.Rank;
            g.SoldierConfigID = s2.ConfigID;
            g.SoldierCount = s2.Count;
            g.SlotIndex = slotIndex;
            g.Exp = s1.ExtraData2;

            return g;
        }

        /// <summary>
        /// 获取一个指定星级的标准武将
        /// </summary>
        /// <param name="star"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        static public GeneralInfo GetBasicStarGeneralInfo(int star, int level)
        {
            GeneralInfo g = new GeneralInfo();
            int[] generalTemplate = { 0, 2000, 123, 1, 2, 3 };
            g.GeneralConfig = DBConfigMgr.Instance.MapGeneral[generalTemplate[star]];
            g.Level = level;
            g.Rank = 0;
            g.Skill1 = level;

            g.Skill2 = star > 2 ? level : 0;

            return g;
        }

        /// <summary>
        /// 获取一个指定星级的标准士兵
        /// </summary>
        /// <param name="star"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        static public SoldierInfo GetBasicStarSoldierInfo(int star, int level)
        {
            SoldierInfo s = new SoldierInfo();
            int[] soldierTemplate = {0,1001,1,8,14,23};
            s.SoldierConfig = DBConfigMgr.Instance.MapSoldier[soldierTemplate[star]];
            s.Level = level;
            s.SoldierCount = Math.Max(20, 10 + level / 8);

            return s;
        }
    }

    public class EntityFactory
    {
        static string[] BuffAttributeName = 
        { 
            "Default", "HP", "MaxHP", "Attack", "Defense",
            "MoveSpeed", "AttackSpeed", "AttackRange", "CriticalRate", "DodgeRate",
            "HitRate", "SuckBlood", "Stun", "God", "Sheep","Shield"
        };

        static string[] ModelPatchList = { "", "rifleman21.plist", "rifleman23.plist", "rifleman22.plist" };

        static private string ComplementBuffValue(float initV, float growthV,int lv)
        {
            float v = initV + (lv - 1) * growthV;
            string buffV = String.Empty;

            if (Math.Abs(initV) < 1) // 百分比的
            {
                buffV = String.Format("{0}%", Math.Round(v * 100));
            }
            else
            {
                buffV = String.Format("{0}", Math.Round(v));
            }

            return buffV;
         }

        static public int GenerateSkillID(int templateID, int lv)
        {
            return (templateID - 1) * 1000 + lv;
        }

        static public Skill GenerateASkill(int lv, SkillTemplate template)
        {
            Skill s = new Skill();
            s.ID = GenerateSkillID(template.ID,lv);
            s.IconID = template.IconID;
            s.MaxHit = template.MaxHit;
            s.Name = template.Name;
            s.EffectID = template.EffectID;
            s.EffectRange = template.EffectRange;
            s.ConditionParam = template.ConditionParam;
            s.ConditionType = template.ConditionType;
            s.EffectBullet = 0;
            s.CoolDown = template.CoolDown;
            s.Possiblity = template.Possibility;
            s.SkillCategory = template.Category;
            s.Description = template.Description;
            s.SkillParam = String.Empty;
            s.Buff = String.Empty;
            s.SkillType = template.SkillType.Trim();
            return s;
        }

        static public Buff GenerateBuff(int lv, SkillTemplate template,int buffIndex,out string value)
        {
            Buff b = new Buff();
            b.ID = 1;
            value = "0";

            if (DBConfigMgr.Instance.MapBuff != null && DBConfigMgr.Instance.MapBuff.Count > 0)
            {
                b.ID = DBConfigMgr.Instance.MapBuff.Keys.Max() + 1;
            }

            b.Time = template.Time;
            b.Target = template.Target;
            b.ModelPath = "";
            b.ModelOffset = 0;

            if (buffIndex == 1 && template.Buff1 > 0)
            {
                b.BuffType = template.Buff1Type;
                b.Name = String.Format("{0}Lv{1}Buff1", template.Name, lv);
                b.Positive = template.Buff1InitialValue > 0 ? 1 : 0;

                value = ComplementBuffValue(template.Buff1InitialValue, template.Buff1Growth, lv);
                b.BuffKV = String.Format("{0},{1}", template.Buff1, value);

                // 负的效果转成正的
                value = value.Replace("-", "");
            }

            if (buffIndex == 2 && template.Buff2 > 0)
            {
                b.BuffType = template.Buff2Type;
                b.Name = String.Format("{0}Lv{1}Buff2", template.Name, lv);
                b.Positive = template.Buff2InitialValue > 0 ? 1 : 0;

                value = ComplementBuffValue(template.Buff2InitialValue, template.Buff2Growth, lv);
                b.BuffKV = String.Format("{0},{1}", template.Buff2, value);

                // 负的效果转成正的
                value = value.Replace("-", "");
            }

            return b;
        }

        static public General GenerateGeneral(GeneralTemplate template)
        {
            General config = new General();

            config.ID = template.ID;
            config.Title = template.Title;
            config.Name = template.Name;
            config.Star = template.Star;
            config.SoldierType = template.SoldierType;
            config.Nationality = template.Nationality;
            config.InitialLv = 1;
            config.IconID = "g1.png";
            config.ModelPatch = ModelPatchList[template.SoldierType];
            config.AttackType = template.SoldierType == 3 ? 8003 : 1;
            config.CriticalRate = 200;
            config.DodgeRate = 200;
            config.HitRate = 100;
            config.Souls = 0;
            config.Description = "";
            config.Rank1LeastLevel = 10;
            config.Rank2LeastLevel = 20;
            config.Rank3LeastLevel = 30;
            config.Rank4LeastLevel = 40;
            config.Rank5LeastLevel = 50;
            config.HPRankRate = 1.2f;
            config.ATKRankRate = 1.2f;
            config.DEFRankRate = 1.2f;

            config.HP = Formula.HPFromSta(template.Stamina * 5 );
            config.AttackPower = Formula.ATKFromStr(template.Strength * 5 );
            config.DefensePower = Formula.DEFFromAgi(template.Agility * 5);
            config.HPGrowth = Formula.HPFromSta(template.Stamina);
            config.ATKGrowth = Formula.ATKFromStr(template.Strength);
            config.DEFGrowth = Formula.DEFFromAgi(template.Agility);

            return config;
        }

        static public General GenerateGeneral(int id, string name,int soldierType, MainAttribute attribute)
        {
            General config = new General();
            config.ID = id;

            config.Name = name;
            config.Title = "";
            config.Star = 1;
            config.SoldierType = soldierType;
            config.Nationality = 0;
            config.InitialLv = 1;
            config.IconID = "g0.png";
            config.ModelPatch = ModelPatchList[soldierType];
            config.AttackType = soldierType == 3 ? 8003 : 1;
            config.CriticalRate = 200;
            config.DodgeRate = 200;
            config.HitRate = 100;
            config.Souls = 0;
            config.RankUpLevel1Costs = "2,1003,100;";
            config.RankUpLevel2Costs = "2,1003,200;";
            config.RankUpLevel3Costs = "2,1003,300;";
            config.RankUpLevel4Costs = "2,1003,400;";
            config.RankUpLevel5Costs = "2,1003,500;";
            config.Rank1LeastLevel = 10;
            config.Rank2LeastLevel = 20;
            config.Rank3LeastLevel = 30;
            config.Rank4LeastLevel = 40;
            config.Rank5LeastLevel = 50;

            config.HP = attribute.HP;
            config.AttackPower = attribute.ATK;
            config.DefensePower = attribute.DEF;
            config.HPGrowth = attribute.HP_GROWTH;
            config.ATKGrowth = attribute.ATK_GROWTH;
            config.DEFGrowth = attribute.DEF_GROWTH;

            config.MoveSpeed = attribute.MOVE_SPEED;
            config.AttackRange = attribute.ATTACK_RANGE;
            config.AttackSpeed = attribute.ATTACK_SPEED;

            return config;
        }
    }
}
