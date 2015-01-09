using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public static class GeneralBatch
    {
        static Random rnd = new Random();

        /// <summary>
        /// 重新计算攻击速度和范围
        /// </summary>
        public static void RefreshSpeedAndRange()
        {
            foreach (KeyValuePair<int, General> pair in DBConfigMgr.Instance.MapGeneral)
            {
                pair.Value.MoveSpeed = Batch.SOLDIER_BASIC_ATTRIBUTE[pair.Value.SoldierType].MOVE_SPEED;
                pair.Value.AttackSpeed = Batch.SOLDIER_BASIC_ATTRIBUTE[pair.Value.SoldierType].ATTACK_SPEED;
                pair.Value.AttackRange = Batch.SOLDIER_BASIC_ATTRIBUTE[pair.Value.SoldierType].ATTACK_RANGE;
            }

            foreach (KeyValuePair<int, Soldier> pair in DBConfigMgr.Instance.MapSoldier)
            {
                pair.Value.MoveSpeed = Batch.SOLDIER_BASIC_ATTRIBUTE[pair.Value.SoldierType].MOVE_SPEED;
                pair.Value.AttackSpeed = Batch.SOLDIER_BASIC_ATTRIBUTE[pair.Value.SoldierType].ATTACK_SPEED;
                pair.Value.AttackRange = Batch.SOLDIER_BASIC_ATTRIBUTE[pair.Value.SoldierType].ATTACK_RANGE;
            }
        }


        /// <summary>
        /// 从模版生成武将
        /// </summary>
        public static void GenerateGenerals()
        {
            DBConfigMgr.Instance.MapGeneral.Clear();

            foreach (GeneralTemplate template in DBConfigMgr.Instance.MapGeneralTemplate.Values)
            {
                General g = EntityFactory.GenerateGeneral(template);

                DBConfigMgr.Instance.MapGeneral.Add(g.ID, g);
            }
        }

        /// <summary>
        /// 生成测试用的武将
        /// </summary>
        public static void GenerateTestGenerals()
        {
            // 生成基础测试组的武将

            List<General> list = BattleTest.getBasicSoldierTestGroupUnit();

            foreach (General g in list)
            {
                DBConfigMgr.Instance.MapGeneral.Add(g.ID, g);
            }
        }

        /// <summary>
        /// 删除测试武将
        /// </summary>
        public static void ClearTestGenerals()
        {
            int[] keys = DBConfigMgr.Instance.MapGeneral.Keys.ToArray();
            foreach (int k in keys)
            {
                if (k > 10000)
                {
                    DBConfigMgr.Instance.MapGeneral.Remove(k);
                }
            }

        }

        /// <summary>
        /// 随机分配武将的技能
        /// </summary>
        public static void DispatchRandomSkillForGeneral()
        {
            // 主动技能
            //55 - 64


            // 被动技能
            // 25 - 31

            // 天赋技能
            // 37 - 54

            foreach (General g in DBConfigMgr.Instance.MapGeneral.Values)
            {
                if (g.Star < 3)
                {
                    int skillID = rnd.Next(25, 32);
                    g.SkillID = String.Format("{0}", skillID * 1000 + 1);
                }
                else
                {
                    int skillID1 = 1, skillID2 = 1;
                    if (g.SoldierType == 1)
                    {
                        skillID1 = rnd.Next(55, 57);
                    }
                    else if (g.SoldierType == 2)
                    {
                        skillID1 = rnd.Next(57, 59);
                    }
                    else
                    {
                        skillID1 = rnd.Next(59, 61);
                    }

                    skillID2 = rnd.Next(25, 32);
                    g.SkillID = string.Format("{0};{1}", skillID1 * 1000 + 1, skillID2 * 1000 + 1);
                }
            }
        }

        /// <summary>
        /// 给武将分配天赋
        /// </summary>
        public static void DispatchTalentForGeneral()
        {
            // 天赋 
            //37	生命修为	武将的生命最大值增加{B1}%
            //38	攻击修为	武将的攻击力增加{B1}%
            //39	防御修为	武将的防御力增加{B1}%
            //40	步兵克星	武将对步兵所造成的伤害增加{B1}%
            //41	骑兵克星	武将对骑兵所造成的伤害增加{B1}%
            //42	射手克星	武将对射手所造成的伤害增加{B1}%
            //43	步兵领悟	武将受到步兵所造成的伤害减少{B1}%
            //44	骑兵领悟	武将受到骑兵所造成的伤害减少{B1}%
            //45	射手领悟	武将受到射手所造成的伤害减少{B1}%
            //46	步兵先锋	如果所带部队为步兵，则部队的攻击力增加{B1}%
            //47	骑兵先锋	如果所带部队为骑兵，则部队的攻击力增加{B1}%
            //48	射手先锋	如果所带部队为射手，则部队的攻击力增加{B1}%
            //49	步兵统领	如果所带部队为步兵，则部队的最大生命值增加{B1}%
            //50	骑兵统领	如果所带部队为骑兵，则部队的最大生命值增加{B1}%
            //51	射手统领	如果所带部队为射手，则部队的最大生命值增加{B1}%
            //52	步兵之盾	如果所带部队为步兵，则部队的防御力增加{B1}%
            //53	骑兵之盾	如果所带部队为骑兵，则部队的防御力增加{B1}%
            //54	射手之盾	如果所带部队为射手，则部队的防御力增加{B1}%

            string talentID = String.Format("{0},{1},{2},{3},{4}",
                36 * 1000 + 1, 37 * 1000 + 1, 38 * 1000 + 1, 36 * 1000 + 2, 36 * 1000 + 2);

            foreach (General g in DBConfigMgr.Instance.MapGeneral.Values)
            {
                g.TalentID = talentID;
            }
        }

        /// <summary>
        /// 刷新武将的钟爱装备和部队
        /// </summary>
        public static void RefreshGeneralSpecialArmorAndSoldier()
        {
            //Dictionary<int,IEnumerable<Armor>> starArmors = new Dictionary<int,IEnumerable<Armor>>();
            //Dictionary<int,IEnumerable<Soldier>> starSoldiers = new Dictionary<int,IEnumerable<Soldier>>();

            //starArmors.Add(3,null);
            //starArmors.Add(4,null);
            //starArmors.Add(5,null);
            //starSoldiers.Add(3,null);
            //starSoldiers.Add(4,null);
            //starSoldiers.Add(5,null);

            //for(int star = 3; star <=5 ;star ++)
            //{
            //    starArmors[star] =
            //    from config in DBConfigMgr.Instance.MapArmor.Values
            //    where config.Star == star
            //    select config;

            //    starSoldiers[star] = 
            //    from config in DBConfigMgr.Instance.MapSoldier.Values
            //    where config.Star == star
            //    select config;
            //}

            // star2 soldiers
            int[,] star3Soldiers = { 
                {1,1,1,1},
                { 0,8, 12, 10 }, 
                { 0,8, 12, 10 }, 
                { 0,9, 13, 11 }, 
                { 0,9, 13, 11 } 
            };

            // 25 -36, 37 - 48

            foreach (General g in DBConfigMgr.Instance.MapGeneral.Values)
            {
                if (g.Nationality == 0) // 主角不需要
                {
                    continue;
                }

                if (g.Star <= 3) // 2、3星武将
                {
                    g.SpecialSoldier = string.Format("{0},{1}", star3Soldiers[g.Nationality, g.SoldierType], EntityFactory.GenerateSkillID(33, 10 * g.Star));

                    int weapon = 1;
                    int cloth = 2;
                    int helmet = 3;

                    if (g.Nationality < 3)
                    {
                        if (g.SoldierType == 3)
                        {
                            weapon = rnd.Next(29, 31);
                        }
                        else
                        {
                            weapon = rnd.Next(25, 29);
                        }

                        cloth = rnd.Next(31, 34);
                        helmet = rnd.Next(34, 37);
                    }
                    else
                    {
                        if (g.SoldierType == 3)
                        {
                            weapon = rnd.Next(41, 43);
                        }
                        else
                        {
                            weapon = rnd.Next(37, 41);
                        }

                        cloth = rnd.Next(43, 46);
                        helmet = rnd.Next(46, 49);
                    }

                    g.SpecialArmor = string.Format("{0},{3};{1},{4};{2},{5}",
                        weapon, cloth, helmet, EntityFactory.GenerateSkillID(38, 1), EntityFactory.GenerateSkillID(39, 1), EntityFactory.GenerateSkillID(37, 1));
                }
            }
        }

        /// <summary>
        /// 随机给武将分配初始部队
        /// </summary>
        public static void RefreshGeneralInitialSoldier()
        {
            foreach (General gen in DBConfigMgr.Instance.MapGeneral.Values)
            {
                string[] generalModelPathList = { "", "general2019.plist", "general2002.plist", "general2004.plist" };
                gen.ModelPatch = generalModelPathList[gen.SoldierType];

                if (gen.ID > 1000)
                {
                    if (gen.SoldierType == 1)
                    {
                        //gen.InitialSoldier = 3;
                        gen.InitialSoldier = rnd.Next(1001, 1004);
                    }

                    if (gen.SoldierType == 2)
                    {
                        //gen.InitialSoldier = 7;
                        gen.InitialSoldier = rnd.Next(1006, 1008);
                    }

                    if (gen.SoldierType == 3)
                    {
                        //gen.InitialSoldier = 4;
                        gen.InitialSoldier = rnd.Next(1004, 1006);
                    }

                }
                else
                {
                    if (gen.SoldierType == 1)
                    {
                        //gen.InitialSoldier = 3;
                        gen.InitialSoldier = rnd.Next(1, 4);
                    }

                    if (gen.SoldierType == 2)
                    {
                        //gen.InitialSoldier = 7;
                        gen.InitialSoldier = rnd.Next(6, 8);
                    }

                    if (gen.SoldierType == 3)
                    {
                        //gen.InitialSoldier = 4;
                        gen.InitialSoldier = rnd.Next(4, 6);
                    }
                }

            }
        }

        /// <summary>
        /// 更新武将形象
        /// </summary>
        public static void RefreshGeneralApperance()
        {
            Dictionary<int,string[]> existGeneralModels = new Dictionary<int,string[]>()
            {
                {1,new string[]{"general1.plist","g1.png","g1_b.png"}},
                {2,new string[]{"general1.plist","g1.png","g1_b.png"}},
                {3,new string[]{"general1.plist","g1.png","g1_b.png"}},
                {4,new string[]{"general4.plist","g4.png","g4_b.png"}},
                {5,new string[]{"general4.plist","g4.png","g4_b.png"}},
                {6,new string[]{"general4.plist","g4.png","g4_b.png"}},
                {43,new string[]{"general43.plist","g43.png","g43_b.png"}},
                {9,new string[]{"general9.plist","g9.png","g9_b.png"}},
                {15,new string[]{"general15.plist","g15.png","g15_b.png"}}
            };

            string[] generalModelPathList = { "", "general2019.plist", "general2002.plist", "general2004.plist" };
            string[] generalIconList = { "", "g2019.png", "g2002.png", "g2004.png" };
            string[] generalBigIconList = { "", "g2019_b.png", "g2002_b.png", "g2004_b.png" };
            int[] generalAttackTypes = { 1, 1, 1, 8003 };

            foreach (General gen in DBConfigMgr.Instance.MapGeneral.Values)
            {
                gen.ModelPatch = generalModelPathList[gen.SoldierType];
                gen.IconID = generalIconList[gen.SoldierType];
                gen.BigIcon = generalBigIconList[gen.SoldierType];
                gen.AttackType = generalAttackTypes[gen.SoldierType];

                if (existGeneralModels.ContainsKey(gen.ID))
                {
                    gen.ModelPatch = existGeneralModels[gen.ID][0];
                    gen.IconID = existGeneralModels[gen.ID][1];
                    gen.BigIcon = existGeneralModels[gen.ID][2];
                }
                else
                {
                    if (gen.Nationality == Batch.NATIONALITY_MONGOLIA )
                    {
                        if (gen.SoldierType == (int)SoldierType.Archer)
                        {
                            gen.ModelPatch = "general2005.plist";
                        }

                        if (gen.SoldierType == (int)SoldierType.Footman)
                        {
                            gen.ModelPatch = "general2000.plist";
                            gen.IconID = "g2000.png";
                            gen.BigIcon = "g2000_b.png";
                        }
                    }

                    if (gen.Nationality == Batch.NATIONALITY_ARABIC)
                    {
                        if (gen.SoldierType == (int)SoldierType.Footman)
                        {
                            gen.ModelPatch = "general2025.plist";
                            gen.IconID = "g2025.png";
                            gen.BigIcon = "g2025_b.png";
                        }

                        if (gen.SoldierType == (int)SoldierType.Archer)
                        {
                            gen.ModelPatch = "general2029.plist";
                            gen.IconID = "g2029.png";
                            gen.BigIcon = "g2029_b.png";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 刷新武将三维
        /// </summary>
        public static void RefreshRandomGeneralMainAttribute()
        {
            foreach (General g in DBConfigMgr.Instance.MapGeneral.Values.Where(x => x.ID < 10000))
            {
                //int rIndex = rnd.Next(0, BattleTest.BasicRandomTypeTable.Count());

                //int[] archerTypes = { 0, 2, 3 };
                //if (g.SoldierType == 3)
                //{
                //    int r = rnd.Next(3);
                //    rIndex = archerTypes[r];
                //}

                //if (g.ID <= 6) // 主角
                //    rIndex = 0;

                //double luckyPoint = 0;//Math.Round(rnd.NextDouble() * 100,0);

                //double starRate = Formula.CONST_STAR_GAP_PARAMS[g.Star];

                //double hpPercent = 1.0 * BattleTest.BasicRandomTypeTable.ElementAt(rIndex).Value[(int)ATTRIBUTEINDEX.HP] / 100.0;
                //g.HP = (int)(Batch.BASIC_ATTRIBUTE.HP * starRate * hpPercent) + (int)luckyPoint;
                //g.HPGrowth = (int)(Batch.BASIC_ATTRIBUTE.HP_GROWTH * starRate * hpPercent);

                //double atkPercent = 1.0 * BattleTest.BasicRandomTypeTable.ElementAt(rIndex).Value[(int)ATTRIBUTEINDEX.ATK] / 100.0;
                //g.AttackPower = (int)(Batch.BASIC_ATTRIBUTE.ATK * starRate * atkPercent);
                //g.ATKGrowth = (int)(Batch.BASIC_ATTRIBUTE.ATK_GROWTH * starRate * atkPercent);

                //double defPercent = 1.0 * BattleTest.BasicRandomTypeTable.ElementAt(rIndex).Value[(int)ATTRIBUTEINDEX.DEF] / 100.0;
                //g.DefensePower = (int)(Batch.BASIC_ATTRIBUTE.DEF * starRate * defPercent);
                //g.DEFGrowth = (int)(Batch.BASIC_ATTRIBUTE.DEF_GROWTH * starRate * defPercent);

                g.MoveSpeed = Batch.SOLDIER_BASIC_ATTRIBUTE[g.SoldierType].MOVE_SPEED;
                g.AttackSpeed = Batch.SOLDIER_BASIC_ATTRIBUTE[g.SoldierType].ATTACK_SPEED;
                g.AttackRange = Batch.SOLDIER_BASIC_ATTRIBUTE[g.SoldierType].ATTACK_RANGE;

                //if(g.Title == "") g.Title = BattleTest.BasicRandomTypeTable.ElementAt(rIndex).Key;
            }
        }

        /// <summary>
        /// 生成武将碎片
        /// </summary>
        public static void GenerateGeneralMaterial()
        {
            Dictionary<int, int> starConsumes = new Dictionary<int, int> {
                {3,10},
                {4,20},
                {5,30}
                };

                    IEnumerable<General> starGenerals =
            from general in DBConfigMgr.Instance.MapGeneral.Values
            where general.ID < 1000 && general.ID > 6 && general.Star > 2
            select general;

            starGenerals = starGenerals.OrderByDescending(general => general.Star);

            foreach (General general in starGenerals)
            {
                GeneralMaterial genMat = new GeneralMaterial();
                genMat.ID = general.ID;
                genMat.Name = general.Name + "武魂";
                genMat.IconID = general.IconID;
                genMat.GeneralID = general.ID;
                genMat.Star = general.Star;
                genMat.ConsumeMaterials = starConsumes[genMat.Star];
                genMat.IconID = general.IconID;
                genMat.Description = String.Format("集齐{0}个武魂，就可以召唤武将{1}",genMat.ConsumeMaterials,genMat.Name);
                DBConfigMgr.Instance.MapGeneralMaterial.Add(genMat.ID, genMat);
            }
        }

        /// <summary>
        /// 刷新武将进阶材料
        /// </summary>
        public static void RefreshGeneranUpgradeCost()
        {
            foreach (General general in DBConfigMgr.Instance.MapGeneral.Values)
            {
                general.RankUpLevel1Costs = "2,1003,100;";
                general.RankUpLevel2Costs = "2,1003,200;";
                general.RankUpLevel3Costs = "2,1003,300;";

                if (general.Star >= 3)
                {
                    general.RankUpLevel4Costs = String.Format("2,1003,400;21,{0},10;", general.ID);
                    general.RankUpLevel5Costs = String.Format("2,1003,500;21,{0},20;", general.ID); 
                }
            }

        }
    }
}
