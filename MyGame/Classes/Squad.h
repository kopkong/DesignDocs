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

class Squad{
private:
    std::string _name;
    Point _pos;
    Point _targetPoint;
    SquadState _state;
    float _speed;
    bool _heroDead;
    bool _soldierDead;
    
    unsigned int _heroHealth;
    unsigned int _soldierHealth;
    unsigned int _soldierCount;
    unsigned int _currentSoldierHealth;
    unsigned int _currentHeroHealth;
    
    unsigned int _attackPoint;
    unsigned int _defensePoint;
    
    Sprite* _spriteHero;
    Sprite* _spriteSoldier;
    
public:
    Squad(std::string,Point);
    std::string getName();
    void setPosition(Point);
    Point getPosition();
    void setTargetPosition(Point);
    void setName(std::string);
    unsigned int getSoldiersCount();
    
    int attack();
    void beAttacked(int);
    bool die();
    void move(float dt);
    void draw();
};

#endif /* defined(__MyGame__Squad__) */
