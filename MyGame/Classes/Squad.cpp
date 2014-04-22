//
//  Squad.cpp
//  MyGame
//
//  Created by 孔 令锴 on 14-4-22.
//
//

#include "Squad.h"

Squad::Squad(std::string name, Point posistion){
    _name = name;
    _pos = posistion;
    _speed = 1.0f;
    
    _heroHealth = 100;
    _soldierHealth = 25;
    _currentHeroHealth = 100;
    _soldierCount = 4;
    _currentSoldierHealth = _soldierCount * _soldierHealth;
    _state = SquadState::BattleBegin;
    
    _heroDead = false;
    _soldierDead = false;
    
}

std::string Squad::getName(){
    return _name;
}

void Squad::setName(std::string name){
    _name = name;
}

void Squad::setTargetPosition(Point target){
    _targetPoint = target;
}

void Squad::setPosition(Point pos){
    _pos = pos;
}

Point Squad::getPosition(){
    return _pos;
}

unsigned int Squad::getSoldiersCount(){
    return _soldierCount;
}

void Squad::move(float dt){
    Point direction = _targetPoint - _pos;
    float xDirection = direction.x > 0 ? 1 : -1;
    float yDirection = direction.y > 0 ? 1 : -1;
    
    float tan = 0;
    if(direction.x != 0){
        tan = (_targetPoint.y - _pos.y) / ( _targetPoint.x - _pos.x);
    }
    
    _pos = Point(_pos.x + xDirection * dt * _speed, _pos.y + yDirection * dt * _speed);
}

int Squad::attack(){
    return _soldierCount * _attackPoint * 0.5 + _attackPoint;
}

void Squad::beAttacked(int attackPoint){
    // random attack to hero or soldier
    if(random()%100 > 50) {// attack hero
        if(_currentHeroHealth > 0){
            _currentHeroHealth -= attackPoint * (1 - _defensePoint/1000);
        }
        else
        {
            _currentSoldierHealth -= attackPoint * (1 -_defensePoint/2000);
        }
    }else{ // attack soldiers
        if(_currentSoldierHealth > 0){
            _currentSoldierHealth -= attackPoint * (1 -_defensePoint/2000);
        }else{
            _currentHeroHealth -= attackPoint * (1- _defensePoint/1000);
        }
    }
}

bool Squad::die(){
    return _heroDead && _soldierDead;
}
