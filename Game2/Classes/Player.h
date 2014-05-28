//
//  Player.h
//  Game2
//
//  Created by 孔 令锴 on 14-5-20.
//
//

#ifndef __Game2__Player__
#define __Game2__Player__

#include <iostream>
#include "cocos2d.h"

USING_NS_CC;

class Fighter : public cocos2d::Node
{
protected:
    Sprite* _icon;
    Sprite* _hpTop;
    Label* _coolDownMsg;
    
public:
    CC_SYNTHESIZE(std::string,_fighterName,FighterName);
    CC_SYNTHESIZE(int, _hp,HP);
    CC_SYNTHESIZE(unsigned int, _maxHP,MAXHP);
	CC_SYNTHESIZE(std::string,_image,Image);
    CC_SYNTHESIZE(std::string,_imageGrayScale,ImageGrayScale);
    CC_SYNTHESIZE(unsigned int,_attackPoint,AttackPoint);
    CC_SYNTHESIZE(int,_attackInterval,AttackInterval);
    CC_SYNTHESIZE(bool,_isDead,Dead);
    CC_SYNTHESIZE(int,_coolDown,CoolDown);
    
    virtual void onEnter() override;
    
    virtual void updateHPLabel();
    
    virtual void updateCoolDown();
    
    virtual void refreshCoolDown();
    
    virtual void beAttacked(const Fighter*, unsigned int);
    
    virtual void die();
};

class Player:public Fighter
{
public:
    Player(const Player& p);
    Player(std::string s,unsigned int h){_fighterName = s; _hp = h; _maxHP = h;_attackPoint = 1;_isDead = false;}
};

class Monster: public Fighter
{
public:
    Monster(const Monster& m);
    Monster(std::string s,unsigned int h){_fighterName = s; _hp = h; _maxHP = h;_attackPoint = 1;_isDead= false;}
};
#endif /* defined(__Game2__Player__) */
