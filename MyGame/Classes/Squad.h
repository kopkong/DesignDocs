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

enum SquadState{
    BattleBegin,
    BattleEnd,
    FightBegin,
    Fighting,
    FightEnd,
    Moving,
    Wait
};

enum SquadType{
    Footman,
    Knight,
    Archer
};

enum SquadFaceTo
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
private:
    bool _soldierDead;
    
public:
    std::vector<Point> _soldiersPos;
    std::vector<int> _soldiersHealth;
    
    // Use this property to index all squads in the battle field
    CC_SYNTHESIZE(int,_index,Index);
    CC_SYNTHESIZE(int,_target,TargetIndex);
    CC_SYNTHESIZE(SquadState,_state,State);
    CC_SYNTHESIZE(std::string,_name,Name);
    CC_SYNTHESIZE(Point,_pos,Position);
    CC_SYNTHESIZE(Point,_target,TargetPosition);
    CC_SYNTHESIZE(float,_speed,Speed);
    CC_SYNTHESIZE(SquadFaceTo,_faceTo,FaceTo);
    CC_SYNTHESIZE(unsigned int,_soldierCount,SoldierCount);
    CC_SYNTHESIZE(SquadType, _type, SoldierType);
    CC_SYNTHESIZE(SquadSide, _side, SquadSide);
    
    // Squad's attack range, if hero is alive, ues hero's position
    // otherwise using the first's soldier's position
    CC_SYNTHESIZE(float,_attackRange,AttackRange);
    
    // Soldier's attack point, hero = atk * 2
    CC_SYNTHESIZE(float,_attackPoint,AttackPoint);
    
    // Soldier's defense point, hero = def * 2
    CC_SYNTHESIZE(float,_defensePoint,DefensePoint);
    
    // Hero's max HP
    CC_SYNTHESIZE(int, _heroHealth, HeroHealth);
    
    // Every soldier's max HP
    CC_SYNTHESIZE(int, _soldierHealth, SoldierHealth);
    
    CC_SYNTHESIZE(bool,_heroDead,HeroDead);
    
    Squad(std::string,Point,int);
    
    bool faceToRight();
    bool die();
    void draw();
};

#endif /* defined(__MyGame__Squad__) */
