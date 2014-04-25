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
    _index = index;
    
    // initialize some properties
    _heroHealth = 800;
    _soldierHealth = 250;
    _attackPoint = 6;
    _defensePoint = 2;
    
    _soldierCount = 20;
    _state = SquadState::BattleBegin;
    
    if(_pos.x < 1024/2){
        _faceTo = Orientation::Right;
    }
    else{
        _faceTo = Orientation::Left;
    }
}

bool Squad::faceToRight(){
    if( _faceTo == Orientation::Right)
        return true;
    
    return false;
}
