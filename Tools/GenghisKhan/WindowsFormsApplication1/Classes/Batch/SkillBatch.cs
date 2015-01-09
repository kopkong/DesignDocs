using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public static class SkillBatch
    {
        /// <summary>
        /// 从模板生成技能
        /// </summary>
        public static void GenerateSkills()
        {
            DBConfigMgr.Instance.MapSkill.Clear();
            DBConfigMgr.Instance.MapBuff.Clear();

            foreach (SkillTemplate template in DBConfigMgr.Instance.MapSkillTemplate.Values)
            {
                if (template.Buff1 < 0 && template.Buff2 < 0 &&
                    template.SkillType == "GKBSkill_ForeverPassive") continue;

                if (template.Buff1 < 0 && template.Buff2 < 0 &&
                    template.SkillType == "GKBSkill_ForeverPassive") continue;

                for (int lv = 1; lv <= template.MaxLevel; lv++)
                {
                    switch (template.SkillType.Trim())
                    {
                        case "GKBSkill_ForeverPassive":
                            {
                                GeneratePassiveSkill(template, lv);
                                break;
                            }
                        case "GKBSkill_AngelForeverPassive":
                            {
                                GenerateAnglePassiveSkill(template, lv);
                                break;
                            }
                        case "GKBSkill_AngelInitiative":
                            {
                                GenerateAngelInitiativeSkill(template, lv);
                                break;
                            }
                        case "GKBSkill_AngelSummon":
                            {
                                GenerateAngleSummonSkill(template, lv);
                                break;
                            }
                        case "GKBSkill_Initiative":
                            {
                                GenerateInitiativeSKill(template, lv);
                                break;
                            }

                        default:
                            break;
                    }
                }
            }
        }

        #region 生成不同类型的技能
        private static void GeneratePassiveSkill(SkillTemplate template, int lv)
        {
            Skill s = EntityFactory.GenerateASkill(lv, template);
            string buff1Value = String.Empty;
            string buff2Value = String.Empty;
            s.SkillParam = template.SkillParam;

            if (template.Buff1 > 0)
            {
                Buff b1 = EntityFactory.GenerateBuff(lv, template, 1, out buff1Value);
                s.Buff = b1.ID.ToString();
                s.Description = s.Description.Replace("{B1}", buff1Value.ToString());
                s.SkillParam += "," + b1.ID.ToString();
                DBConfigMgr.Instance.MapBuff.Add(b1.ID, b1);
            }

            if (template.Buff2 > 0)
            {
                Buff b2 = EntityFactory.GenerateBuff(lv, template, 2, out buff2Value);
                s.Buff += "," + b2.ID.ToString();
                s.Description = s.Description.Replace("{B2}", buff2Value.ToString());
                s.SkillParam += "," + b2.ID.ToString();
                DBConfigMgr.Instance.MapBuff.Add(b2.ID, b2);
            }

            DBConfigMgr.Instance.MapSkill.Add(s.ID, s);
        }

        private static void GenerateAngleSummonSkill(SkillTemplate template, int lv)
        {
            Skill s = EntityFactory.GenerateASkill(lv, template);
            s.SkillParam = template.SkillParam;

            int level = (int)(1 + 2.5 * (lv - 1));
            int count = (int)(10 + 0.25 * (lv - 1));

            s.SkillParam += string.Format(",{0},{1}", count, level);
            s.Description = s.Description.Replace("{B1}", count.ToString()).Replace("{B2}", level.ToString());

            DBConfigMgr.Instance.MapSkill.Add(s.ID, s);
        }

        private static void GenerateAnglePassiveSkill(SkillTemplate template, int lv)
        {
            Skill s = EntityFactory.GenerateASkill(lv, template);
            string buff1Value = String.Empty;
            string buff2Value = String.Empty;
            //s.SkillParam = template.SkillParam;

            if (template.Buff1 > 0)
            {
                Buff b1 = EntityFactory.GenerateBuff(lv, template, 1, out buff1Value);
                s.Buff = b1.ID.ToString();
                s.Description = s.Description.Replace("{B1}", buff1Value.ToString());
                s.SkillParam += b1.ID.ToString();
                DBConfigMgr.Instance.MapBuff.Add(b1.ID, b1);
            }

            if (template.Buff2 > 0)
            {
                Buff b2 = EntityFactory.GenerateBuff(lv, template, 2, out buff2Value);
                s.Buff += "," + b2.ID.ToString();
                s.Description = s.Description.Replace("{B2}", buff2Value.ToString());
                s.SkillParam += "," + b2.ID.ToString();
                DBConfigMgr.Instance.MapBuff.Add(b2.ID, b2);
            }

            DBConfigMgr.Instance.MapSkill.Add(s.ID, s);
        }

        private static void GenerateAngelInitiativeSkill(SkillTemplate template, int lv)
        {
            Skill s = EntityFactory.GenerateASkill(lv, template);
            string buff1Value = String.Empty;
            string buff2Value = String.Empty;
            s.SkillParam = template.SkillParam;

            if (template.Buff1 > 0)
            {
                Buff b1 = EntityFactory.GenerateBuff(lv, template, 1, out buff1Value);
                s.Buff = b1.ID.ToString();
                s.Description = s.Description.Replace("{B1}", buff1Value.ToString());
                s.SkillParam += "," + b1.ID.ToString();
                DBConfigMgr.Instance.MapBuff.Add(b1.ID, b1);
            }

            if (template.Buff2 > 0)
            {
                Buff b2 = EntityFactory.GenerateBuff(lv, template, 2, out buff2Value);
                s.Buff += "," + b2.ID.ToString();
                s.Description = s.Description.Replace("{B2}", buff2Value.ToString());
                s.SkillParam += "," + b2.ID.ToString();
                DBConfigMgr.Instance.MapBuff.Add(b2.ID, b2);
            }

            DBConfigMgr.Instance.MapSkill.Add(s.ID, s);
        }

        private static void GenerateInitiativeSKill(SkillTemplate template, int lv)
        {
            Skill s = EntityFactory.GenerateASkill(lv, template);
            string buff1Value = String.Empty;
            string buff2Value = String.Empty;
            s.SkillParam = string.Format("{0},{1},{2},{3}", template.ConditionType, template.Possibility,
                template.MaxHit, template.EffectRange);

            if (template.Buff1 > 0)
            {
                Buff b1 = EntityFactory.GenerateBuff(lv, template, 1, out buff1Value);
                s.Buff = b1.ID.ToString();
                s.Description = s.Description.Replace("{B1}", buff1Value.ToString());
                s.SkillParam += "," + b1.ID.ToString();
                DBConfigMgr.Instance.MapBuff.Add(b1.ID, b1);
            }

            if (template.Buff2 > 0)
            {
                Buff b2 = EntityFactory.GenerateBuff(lv, template, 2, out buff2Value);
                s.Buff += "," + b2.ID.ToString();
                s.Description = s.Description.Replace("{B2}", buff2Value.ToString());
                s.SkillParam += "," + b2.ID.ToString();
                DBConfigMgr.Instance.MapBuff.Add(b2.ID, b2);
            }

            DBConfigMgr.Instance.MapSkill.Add(s.ID, s);
        }

        #endregion
    }
}
