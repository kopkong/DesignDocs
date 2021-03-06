﻿using System;
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
        public string IconID { get; set; }

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

        [DataMember]
        public string RankUpLevel1Costs { get; set; }

        [DataMember]
        public string RankUpLevel2Costs { get; set; }

        [DataMember]
        public string RankUpLevel3Costs { get; set; }

        [DataMember]
        public string RankUpLevel4Costs { get; set; }

        [DataMember]
        public string RankUpLevel5Costs { get; set; }

        [DataMember]
        public int Rank1LeastLevel { get; set; }

        [DataMember]
        public int Rank2LeastLevel { get; set; }

        [DataMember]
        public int Rank3LeastLevel { get; set; }

        [DataMember]
        public int Rank4LeastLevel { get; set; }

        [DataMember]
        public int Rank5LeastLevel { get; set; }
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
        public string IconID { get; set; }

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

        [DataMember]
        public string RankUpLevel1Costs { get; set; }

        [DataMember]
        public string RankUpLevel2Costs { get; set; }

        [DataMember]
        public string RankUpLevel3Costs { get; set; }

        [DataMember]
        public string RankUpLevel4Costs { get; set; }

        [DataMember]
        public string RankUpLevel5Costs { get; set; }

        [DataMember]
        public string AddCount1Costs { get; set; }

        [DataMember]
        public string AddCount2Costs { get; set; }

        [DataMember]
        public string AddCount3Costs { get; set; }

        [DataMember]
        public string AddCount4Costs { get; set; }

        [DataMember]
        public string AddCount5Costs { get; set; }

        [DataMember]
        public string AddCount6Costs { get; set; }

        [DataMember]
        public string AddCount7Costs { get; set; }

        [DataMember]
        public string AddCount8Costs { get; set; }

        [DataMember]
        public string AddCount9Costs { get; set; }

        [DataMember]
        public string AddCount10Costs { get; set; }
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
        public string RuleID {get;set;}

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
        public string LevelReward { get; set; }

        [DataMember]
        public string EliteLevelReward { get; set; }

        [DataMember]
        public int MoneyReward { get; set; }

        [DataMember]
        public int EliteMoneyReward { get; set; }

        [DataMember]
        public int GeneralExpReward { get; set; }

        [DataMember]
        public int EliteGeneralExpReward { get; set; }

        [DataMember]
        public string ClientShowRewards { get; set; }

        [DataMember]
        public string EliteClientShowRewards { get; set; }

        [DataMember]
        public int ConsumeEnergy { get; set; }

        [DataMember]
        public int EliteConsumeEnergy { get; set; }

        [DataMember]
        public int DefaultLocked { get; set; }

        [DataMember]
        public int UnlockNextLevelID { get; set; }

        [DataMember]
        public int UnlockChapter { get; set; }

        [DataMember]
        public int DailyMostTimes { get; set; }

        [DataMember]
        public int EliteDailyMostTimes { get; set; }

        [DataMember]
        public int Star1Time { get; set; }

        [DataMember]
        public int Star2Time { get; set; }

        [DataMember]
        public int Star3Time { get; set; }
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

    [DataContract]
    public class Task
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Type { get; set; }

        [DataMember]
        public int TaskLevel { get; set; }

        [DataMember]
        public int TaskTarget { get; set; }

        [DataMember]
        public int TaskParameter1 { get; set; }

        [DataMember]
        public int TaskParameter2 { get; set; }

        [DataMember]
        public int PlayerExpReward { get; set; }

        [DataMember]
        public int DiamondReward { get; set; }

        [DataMember]
        public int MoneyReward { get; set; }

        [DataMember]
        public string ItemReward { get; set; }

        [DataMember]
        public string ClientReward { get; set; }

        //[DataMember]
        //public int Cycle { get; set; }

        [DataMember]
        public string StartTime { get; set; }

        [DataMember]
        public string EndTime { get; set; }

        [DataMember]
        public int PreviousTask { get; set; }

    }

    [DataContract]
    public class Skill
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int SkillType { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int SkillCategory { get; set; }

        [DataMember]
        public int Range { get; set; }

        [DataMember]
        public int Target { get; set; }

        [DataMember]
        public int MaxHit { get; set; }

        [DataMember]
        public int Possiblity { get; set; }

        [DataMember]
        public int CoolDown { get; set; }

        [DataMember]
        public int EffectRange { get; set; }

        [DataMember]
        public int SkillParameter { get; set; }

        [DataMember]
        public int EffectWeapon { get; set; }

        [DataMember]
        public int FixedDamage { get; set; }

        [DataMember]
        public int EffectID { get; set; }

        [DataMember]
        public int Buff1 { get; set; }

        [DataMember]
        public int Buff1Possibility { get; set; }

        [DataMember]
        public int Buff1Para1 { get; set; }

        [DataMember]
        public int Buff1Para2 { get; set; }

        [DataMember]
        public int Buff2 { get; set; }

        [DataMember]
        public int Buff2Possibility { get; set; }

        [DataMember]
        public int Buff2Para1 { get; set; }

        [DataMember]
        public int Buff2Para2 { get; set; }

    }
}
