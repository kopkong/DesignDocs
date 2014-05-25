//
//  Monster.cpp
//  Game2
//
//  Created by 孔 令锴 on 14-5-20.
//
//

#include "Monster.h"

bool Monster::beAttacked(unsigned int d)
{
    _hp -= d;
    
    return _hp >= 0;
}