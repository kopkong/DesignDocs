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
    public class Experience
    {
        [DataMember]
        public int ID { get; set; }

        public int Lv { get; set; }

        public int PlayerStart { get; set; }

        public int PlayerEnd { get; set; }

        public int GeneralStart { get; set; }

        public int GeneralEnd { get; set; }
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
        public string Description { get; set; }

        [DataMember]
        public int Star { get; set; }

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
        public int HPGrowth { get; set; }

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
        public string Description { get; set; }

        [DataMember]
        public int Star{ get; set; }

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
        public int HPGrowth { get; set; }

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
        public int InitialCount { get; set; }
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
        public string Description { get; set; }

        [DataMember]
        public int Star{ get; set; }

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
        public int Star { get; set; }

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

    [DataContract]
    public class Chapter
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string IconID { get; set; }

        [DataMember]
        public int NextChapterID { get; set; }

        [DataMember]
        public string RewardRule { get; set; }

        [DataMember]
        public int X { get; set; }

        [DataMember]
        public int Y { get; set; }
    }

    [DataContract]
    public class Level
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int ChapterID { get; set; }

        [DataMember]
        public string IconID { get; set; }

        [DataMember]
        public string BackGround { get; set; }

        [DataMember]
        public string Enemy { get; set; }

        [DataMember]
        public int IsEliteLevel { get; set; }

        [DataMember]
        public string LevelReward { get; set; }

        [DataMember]
        public int MoneyReward { get; set; }

        [DataMember]
        public int GeneralExpReward { get; set; }

        [DataMember]
        public string ClientShowRewards { get; set; }

        [DataMember]
        public int ConsumeEnergy { get; set; }

        [DataMember]
        public int DefaultLocked { get; set; }

        [DataMember]
        public int UnlockNextLevelID { get; set; }

        [DataMember]
        public int UnlockChapter { get; set; }

        [DataMember]
        public int DailyMostTimes { get; set; }

        [DataMember]
        public int Star1Time { get; set; }

        [DataMember]
        public int Star2Time { get; set; }

        [DataMember]
        public int Star3Time { get; set; }

        [DataMember]
        public int FailTime { get; set; }
    }

    [DataContract]
    public class ArmorMaterial
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string IconID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int  Star{ get; set; }

        [DataMember]
        public int Type{get;set;}

        [DataMember]
        public int ArmorID{get;set;}

        [DataMember]
        public int ConsumeMaterials{get;set;}
    }

    [DataContract]
    public class SoldierMaterial
    {
        [DataMember]
        public int ID{get;set;}

        [DataMember]
        public string Name{get;set;}

        [DataMember]
        public string IconID{get;set;}

        [DataMember]
        public string Description{get;set;}

        [DataMember]
        public int Star{get;set;}

        [DataMember]
        public int Type{get;set;}

        [DataMember]
        public int ArmyID{get;set;}

        [DataMember]
        public int ConsumeMaterials{get;set;}
    }

    [DataContract]
    public class Nobility
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int StarCosts { get; set; }

        [DataMember]
        public int HP { get; set; }

        [DataMember]
        public int AttackPower { get; set; }

        [DataMember]
        public int DefensePower { get; set; }

        [DataMember]
        public string ExtraSkills { get; set; }

    }
}
