//
//  Squad.h
//  MyGame
//
//  Created by 孔 令锴 on 14-4-22.
//
//

#ifndef __MyGame__Squad__
#define __MyGame__Squad__

#include <iostream>
#include "cocos2d.h"
USING_NS_CC;

struct CharacterRes
{
    std::vector<std::string> Idle;
    std::vector<std::string> Move;
};

enum SquadState{
    BattleBegin,
    BattleEnd,
    FightBegin,
    Fighting,
    FightEnd,
    Moving,
    Wait,
	Eliminated
};

enum SquadType{
    None,
    Footman,
    Knight,
    Archer
};

enum Orientation
{
    Right,
    Left
};

enum SquadSide
{
    TeamA,
    TeamB
};

class Squad{
    
public:    
    // Use this property to index all squads in the battle field
    CC_SYNTHESIZE(int,_index,Index);
    CC_SYNTHESIZE(int,_targetSquadIndex,TargetSquadIndex);
    CC_SYNTHESIZE(SquadState,_state,State);
    CC_SYNTHESIZE(std::string,_name,Name);
    CC_SYNTHESIZE(Point,_pos,Position);
    CC_SYNTHESIZE(Point,_target,TargetPosition);
    CC_SYNTHESIZE(float,_speed,Speed);
    CC_SYNTHESIZE(Orientation,_faceTo,FaceTo);
    CC_SYNTHESIZE(unsigned int,_soldierCount,SoldierCount);
    CC_SYNTHESIZE(SquadType, _type, SoldierType);
    CC_SYNTHESIZE(SquadSide, _side, SquadSide);
    CC_SYNTHESIZE(unsigned int,_meleeSquads,MeleeSquads);

    // Resources
    CC_SYNTHESIZE(CharacterRes,_SoldierRes,SoldierRes);
    CC_SYNTHESIZE(std::string,_spriteTexture,SpriteTexture);
    CC_SYNTHESIZE(Orientation, _spriteOrientation, SpriteOrientation);
    
	// Squad is eliminated or not
	CC_SYNTHESIZE(bool, _eliminated, Eliminated);
    
    // Squad's attack range, if hero is alive, ues hero's position
    // otherwise using the first's soldier's position
    CC_SYNTHESIZE(float,_attackRange,AttackRange);
    
    // Soldier's attack point, hero = atk * 2
    CC_SYNTHESIZE(float,_attackPoint,AttackPoint);
    
    // Soldier's defense point, hero = def * 2
    CC_SYNTHESIZE(float,_defensePoint,DefensePoint);
    
    // Attack Interval, attack speed is slower with the bigger interval
    CC_SYNTHESIZE(float,_attackInterval,AttackInterval);
    
    // Hero's max HP
    CC_SYNTHESIZE(int, _heroHealth, HeroHealth);
    
    // Every soldier's max HP
    CC_SYNTHESIZE(int, _soldierHealth, SoldierHealth);
    
    CC_SYNTHESIZE(bool,_heroDead,HeroDead);
    
    Squad(std::string,Point,int);
    
    bool faceToRight();
};

#endif /* defined(__MyGame__Squad__) */
