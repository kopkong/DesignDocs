//
//  Squad.cpp
//  MyGame
//
//  Created by 孔 令锴 on 14-4-22.
//
//

#include "Squad.h"

Squad::Squad(std::string name, Point posistion,int index){
    _name = name;
    _pos = posistion;
    _speed = 10.0f;
    _index = index;
    
    // initialize some properties
    _heroHealth = 100;
    _soldierHealth = 20;
    _attackPoint = 6;
    _defensePoint = 2;
    
    _soldierCount = 20;
    _state = SquadState::BattleBegin;
    
    _heroDead = false;
    _soldierDead = false;
    
    if(_pos.x < 1024/2){
        _faceTo = SquadFaceTo::Right;
    }
    else{
        _faceTo = SquadFaceTo::Left;
    }
}

bool Squad::faceToRight(){
    if( _faceTo == SquadFaceTo::Right)
        return true;
    
    return false;
}

bool Squad::die(){
    return _heroDead && _soldierDead;
}
