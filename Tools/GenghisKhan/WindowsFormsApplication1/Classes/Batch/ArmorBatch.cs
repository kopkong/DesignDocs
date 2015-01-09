using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public static class ArmorBatch
    {
        /// <summary>
        /// 刷新装备的属性
        /// </summary>
        public static void RefreshArmorMainAttribute()
        {
            double armorEqualGeneralPercent = 0.2;

            Dictionary<int, int[]> armorAttributeTypes = new Dictionary<int, int[]>()
            {
                {1, new int[]{0,0,150,80}},
                {2, new int[]{0,80,0,150}},
                {3, new int[]{0,160,70,0}}
            };

            foreach (Armor config in DBConfigMgr.Instance.MapArmor.Values)
            {
                double starRate = Formula.CONST_STAR_GAP_PARAMS[config.Star];

                double hpPercent = 1.0 * armorAttributeTypes[config.Type][(int)ATTRIBUTEINDEX.HP] / 100.0;
                config.HP = (int)(Batch.BASIC_ATTRIBUTE.HP * starRate * armorEqualGeneralPercent * hpPercent);
                config.HPGrowth = (int)(Batch.BASIC_ATTRIBUTE.HP_GROWTH * starRate * armorEqualGeneralPercent * hpPercent);

                double atkPercent = 1.0 * armorAttributeTypes[config.Type][(int)ATTRIBUTEINDEX.ATK] / 100.0;
                config.ATK = (int)(Batch.BASIC_ATTRIBUTE.ATK * starRate * armorEqualGeneralPercent * atkPercent);
                config.ATKGrowth = (int)(Batch.BASIC_ATTRIBUTE.ATK_GROWTH * starRate * armorEqualGeneralPercent * atkPercent);

                double defPercent = 1.0 * armorAttributeTypes[config.Type][(int)ATTRIBUTEINDEX.DEF] / 100.0;
                config.DEF = (int)(Batch.BASIC_ATTRIBUTE.DEF * starRate * armorEqualGeneralPercent * defPercent);
                config.DEFGrowth = (int)(Batch.BASIC_ATTRIBUTE.DEF_GROWTH * starRate * armorEqualGeneralPercent * defPercent);
            }
        }

        /// <summary>
        /// 生成装备碎片
        /// </summary>
        public static void GenerateArmorMaterial()
        {
            Dictionary<int, int> starConsumes = new Dictionary<int, int> {
                {3,10},
                {4,20},
                {5,30}
                };

            IEnumerable<Armor> starArmors =
            from armor in DBConfigMgr.Instance.MapArmor.Values
            where armor.Star > 2
            select armor;

            starArmors = starArmors.OrderByDescending(armor => armor.Star);

            foreach (Armor armor in starArmors)
            {
                ArmorMaterial armorMat = new ArmorMaterial();
                armorMat.ID = armor.ID;
                armorMat.Name = armor.Name + "碎片";
                armorMat.IconID = armor.IconID;
                armorMat.ArmorID = armor.ID;
                armorMat.Star = armor.Star;
                armorMat.ConsumeMaterials = starConsumes[armorMat.Star];
                armorMat.IconID = armor.IconID;
                armorMat.Description = String.Format("集齐{0}个碎片，就可以合成{1}", armorMat.ConsumeMaterials, armorMat.Name);
                DBConfigMgr.Instance.MapArmorMaterial.Add(armorMat.ID, armorMat);
            }
        }
    }
}
