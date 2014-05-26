//
//  Monster.h
//  Game2
//
//  Created by 孔 令锴 on 14-5-20.
//
//

#ifndef __Game2__Monster__
#define __Game2__Monster__

#include <iostream>
#include "cocos2d.h"

USING_NS_CC;

class Monster
{
public:
    CC_SYNTHESIZE(std::string,_name,Name);
    CC_SYNTHESIZE(int, _hp,HP);
    CC_SYNTHESIZE(unsigned int, _maxHP,MAXHP);
	CC_SYNTHESIZE(std::string,image,Image);
    
    Monster(std::string s,unsigned int h){_name = s; _hp = h; _maxHP = h;}
    bool beAttacked(unsigned int);
};
#endif /* defined(__Game2__Monster__) */
