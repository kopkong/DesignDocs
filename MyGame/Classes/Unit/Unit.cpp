//
//  Unit.cpp
//  MyGame
//
//  Created by 孔 令锴 on 14-4-28.
//
//

#include "Unit.h"

Unit::Unit()
{
    
}

Unit::~Unit()
{
    
}

Unit::Unit(int index, int hp, float attackInterval)
{
    _index = index;
    _healthPoint = hp;
    _attackInterval = attackInterval;
    _timeSinceLastAttack = attackInterval;
    _targetIndex = -1;
    _doAttackTimes = 0;
    _tryAttackTimes = 0;
}