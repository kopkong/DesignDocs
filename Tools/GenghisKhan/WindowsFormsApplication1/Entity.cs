using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace WindowsFormsApplication1
{
    [DataContract]
    public class AllConfig
    {
        [DataMember]
        public int Type { get; set; }

        [DataMember]
        public string File { get; set; }

    }

    [DataContract]
    public class General
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int InitialLv { get; set; }

        [DataMember]
        public string SmallIcon { get; set; }

        [DataMember]
        public string ModelPatch { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Star { get; set; }

        [DataMember]
        public int SoldierType { get; set; }

        [DataMember]
        public int AttackType { get; set; }

        [DataMember]
        public int HP { get; set; }

        [DataMember]
        public int AttackPower { get; set; }

        [DataMember]
        public int DefensePower { get; set; }

        [DataMember]
        public int MoveSpeed { get; set; }

        [DataMember]
        public int AttackSpeed { get; set; }

        [DataMember]
        public int AttackRange { get; set; }

        [DataMember]
        public int CriticalRate { get; set; }

        [DataMember]
        public int DodgeRate { get; set; }

        [DataMember]
        public int HitRate { get; set; }

        [DataMember]
        public string SkillID { get; set; }

        [DataMember]
        public string TalentID { get; set; }

        [DataMember]
        public int HPGorwth { get; set; }

        [DataMember]
        public int ATKGrowth { get; set; }

        [DataMember]
        public int DEFGrowth { get; set; }

        [DataMember]
        public float HPRankRate { get; set; }

        [DataMember]
        public float ATKRankRate { get; set; }

        [DataMember]
        public float DEFRankRate { get; set; }

        [DataMember]
        public string SpecialArmor { get; set; }

        [DataMember]
        public int Souls { get; set; }

        [DataMember]
        public int InitialSoldier { get; set; }
    }

    [DataContract]
    public class Soldier
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int InitialLv { get; set; }

        [DataMember]
        public string SmallIcon { get; set; }

        [DataMember]
        public string ModelPatch { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Star { get; set; }

        [DataMember]
        public int SoldierType { get; set; }

        [DataMember]
        public int AttackType { get; set; }

        [DataMember]
        public int HP { get; set; }

        [DataMember]
        public int AttackPower { get; set; }

        [DataMember]
        public int DefensePower { get; set; }

        [DataMember]
        public int MoveSpeed { get; set; }

        [DataMember]
        public int AttackSpeed { get; set; }

        [DataMember]
        public int AttackRange { get; set; }

        [DataMember]
        public int CriticalRate { get; set; }

        [DataMember]
        public int DodgeRate { get; set; }

        [DataMember]
        public int HitRate { get; set; }

        [DataMember]
        public string SkillID { get; set; }

        [DataMember]
        public string AIID { get; set; }

        [DataMember]
        public int HPGorwth { get; set; }

        [DataMember]
        public int ATKGrowth { get; set; }

        [DataMember]
        public int DEFGrowth { get; set; }

        [DataMember]
        public float HPRankRate { get; set; }

        [DataMember]
        public float ATKRankRate { get; set; }

        [DataMember]
        public float DEFRankRate { get; set; }
    }

    [DataContract]
    public class Armor
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string IconID { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Star { get; set; }

        [DataMember]
        public int Type { get; set; }

        [DataMember]
        public int HP { get; set; }

        [DataMember]
        public int HPGrowth { get; set; }

        [DataMember]
        public int ATK { get; set; }

        [DataMember]
        public int ATKGrowth { get; set; }

        [DataMember]
        public int DEF { get; set; }

        [DataMember]
        public int DEFGrowth { get; set; }

        [DataMember]
        public string ExtraSkills { get; set; }

        [DataMember]
        public int SoldCoin { get; set; }

    }

    [DataContract]
    public class Item
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string IconID { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public string Star { get; set; }

        [DataMember]
        public int Type { get; set; }

        [DataMember]
        public int RuleID {get;set;}

    }

    [DataContract]
    public class Angel
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int InitialLv { get; set; }

        [DataMember]
        public string SkillID { get; set; }
    }
}
