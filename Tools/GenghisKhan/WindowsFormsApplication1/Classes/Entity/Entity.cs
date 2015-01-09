using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    [DataContract]
    public class AllConfig
    {
        public int ID { get; set; }

        [DataMember]
        public int Type { get; set; }

        [DataMember]
        public string File { get; set; }

        public string Name { get; set; }

        public bool CanBeReward { get; set; }
    }

    [DataContract]
    public class Experience
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int Lv { get; set; }

        [DataMember]
        public int PlayerStart { get; set; }

        [DataMember]
        public int PlayerEnd { get; set; }

        [DataMember]
        public int GeneralStart { get; set; }

        [DataMember]
        public int GeneralEnd { get; set; }

        [DataMember]
        public int VIPStart { get; set; }

        [DataMember]
        public int VIPEnd { get; set; }

        [DataMember]
        public int LegionStart { get; set; }

        [DataMember]
        public int LegionEnd { get; set; }
    }

    [DataContract]
    public class General
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Title { get; set; }

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

        public int Usage { get; set; }

        public int Nationality { get; set; }

        [DataMember]
        public string SpecialSoldier { get; set; }

        [DataMember]
        public string BigIcon { get; set; }
    }

    [DataContract]
    public class GeneralTemplate
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Name { get; set; }

        public int Star { get; set; }

        public int SoldierType { get; set; }

        public int Nationality { get; set; }

        public float Stamina { get; set; }

        public float Strength { get; set; }

        public float Agility { get; set; }
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

        public int SubSoldierType { get; set; }

        [DataMember]
        public string BigIcon { get; set; }
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
        public string Description { get; set; }

        [DataMember]
        public string IconID { get; set; }

        [DataMember]
        public int Star { get; set; }

        [DataMember]
        public int Type { get; set; }

        [DataMember]
        public string RuleID {get;set;}

        [DataMember]
        public int DiamondPrice { get; set; }

    }

    [DataContract]
    public class Angel
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string IconID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int InitialLv { get; set; }

        [DataMember]
        public string SkillID { get; set; }

        public string NobilityRequire { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string SkillLevelLimit { get; set; }
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

        [DataMember]
        public int OpenLevel { get; set; }
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
        public int BackGround { get; set; }

        [DataMember]
        public string Enemy { get; set; }

        [DataMember]
        public string EliteEnemy { get; set; }

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

        public int RefLevel { get; set; }

        public int EliteRefLevel { get; set; }

        [DataMember]
        public string X { get; set; }

        [DataMember]
        public string Y { get; set; }

        [DataMember]
        public int StrongEnemy { get; set; }

        [DataMember]
        public string EnemyAngel { get; set; } //angelID,angleLv,skill1Lv,skill2Lv,skill3Lv,skill4Lv

        [DataMember]
        public string EliteEnemyAngel { get; set; }
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
        public int ArmyID{get;set;}

        [DataMember]
        public int ConsumeMaterials{get;set;}
    }

    [DataContract]
    public class GeneralMaterial
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
        public int Star { get; set; }

        [DataMember]
        public int GeneralID { get; set; }

        [DataMember]
        public int ConsumeMaterials { get; set; }
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

        [DataMember]
        public string RemarkableLevel { get; set; }

        [DataMember]
        public int RemarkableType { get; set; }
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
        public string SkillType { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string IconID { get; set; }

        public int SkillCategory { get; set; }

        public int MaxHit { get; set; }

        public int Possiblity { get; set; }

        [DataMember]
        public int CoolDown { get; set; }

        public int EffectRange { get; set; }

        public int ConditionType { get; set; }

        public string ConditionParam { get; set; }

        [DataMember]
        public int EffectBullet { get; set; }

        [DataMember]
        public string EffectID { get; set; }

        [DataMember]
        public string Buff { get; set; }

        [DataMember]
        public string SkillParam { get; set; }

    }

    [DataContract]
    public class SkillTemplate
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int MaxLevel { get; set; }

        public string SkillType { get; set; }

        public int Category { get; set; }

        public int Target { get; set; }

        public int MaxHit { get; set; }

        public int Possibility { get; set; }

        public int CoolDown { get; set; }

        public int EffectRange { get; set; }

        public int ConditionType { get; set; }

        public string ConditionParam { get; set; }

        public string EffectID { get; set; }

        public int Time { get; set; }

        public int Buff1 { get; set; }

        public int Buff1Type { get; set; }

        public float Buff1InitialValue { get; set; }

        public float Buff1Growth { get; set; }

        public int Buff2 { get; set; }

        public int Buff2Type { get; set; }

        public float Buff2InitialValue { get; set; }

        public float Buff2Growth { get; set; }

        public string IconID { get; set; }

        public string SkillParam { get; set; }
    }

    [DataContract]
    public class MaxSquad
    {
        [DataMember]
        public int ID{get;set;}

        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public int MaxSquads { get; set; }

    }

    [DataContract]
    public class Effect
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string ModelPatch { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int ModelOffset { get; set; }

        [DataMember]
        public double EffectSpeed { get; set; }

        [DataMember]
        public int FrameNum { get; set; }

        [DataMember]
        public int Layer { get; set; }

        [DataMember]
        public int PosBindType { get; set; }

        [DataMember]
        public string Params { get; set; }

        [DataMember]
        public int FullEffect { get; set; }

    }

    [DataContract]
    public class Bullet
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string ModelPatch { get; set; }

        [DataMember]
        public int AttackType { get; set; }

        [DataMember]
        public int MoveSpeed { get; set; }
    }

    [DataContract]
    public class CheckIn
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int Day { get; set; }

        [DataMember]
        public string Rewards { get; set; }

    }

    [DataContract]
    public class Shop
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string IconID { get; set; }

        [DataMember]
        public int MaxItems { get; set; }

    }

    [DataContract]
    public class Goods
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int ShopID { get; set; }

        [DataMember]
        public string Item { get; set; }

        [DataMember]
        public string Price { get; set; }
    }

    [DataContract]
    public class Plot
    {
        [DataMember]
        public int ID { get; set; } // ID = levelID

        [DataMember]
        public string BeforeBattle { get; set; }

        [DataMember]
        public string InBattle { get; set; }

        [DataMember]
        public string AfterBattle { get; set; }

    }

    [DataContract]
    public class Buff
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int BuffType { get; set; }

        [DataMember]
        public int Positive { get; set; }

        [DataMember]
        public string ModelPath { get; set; }

        [DataMember]
        public int ModelOffset { get; set; }

        [DataMember]
        public int Time { get; set; }

        [DataMember]
        public int Target { get; set; }

        [DataMember]
        public string BuffKV { get; set; }
    }

    [DataContract]
    public class Scene
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Effect { get; set; }

        [DataMember]
        public string Map { get; set; }
    }

    [DataContract]
    public class VIP
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int BuyEnergy { get; set; }

        [DataMember]
        public int BuyGold { get; set; }

        [DataMember]
        public int ExtraArena { get; set; }

        [DataMember]
        public int ExtraExplore { get; set; }

        [DataMember]
        public int ResetLevel { get; set; }

        public string OtherPrivilege { get; set; }

        [DataMember]
        public string PrivilegeText { get; set; }
    }

    [DataContract]
    public class Recharge
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Icon { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Item { get; set; }

        [DataMember]
        public int Price { get; set; }

        [DataMember]
        public int BuyTimes { get; set; }

        [DataMember]
        public string ExtraInfo { get; set; }
    }

    [DataContract]
    public class Guide
    {
        public int ID { get; set; }
        public int GuideID { get; set; }
        public int Step { get; set; }
        public string Trigger { get; set; }
        public string FinishType { get; set; }
        public string OnUI { get; set; }
        public string UIEffect { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int W { get; set; }
        public int H { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int W2 { get; set; }
        public int H2 { get; set; }
        public int FingerX { get; set; }
        public int FingerY { get; set; }
        public int FingerX2 { get; set; }
        public int FingerY2 { get; set; }
        public string HintText { get; set; }
        public int HintX { get; set; }
        public int HintY { get; set; }
    }
}
