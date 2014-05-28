//
//  Player.cpp
//  Game2
//
//  Created by 孔 令锴 on 14-5-20.
//
//

#include "Player.h"
#include "Resources.h"
#include "LayerConfig.h"
#include "GameCore.h"


void Fighter::beAttacked(const Fighter* attacker,unsigned int d)
{
    _hp -= attacker->getAttackPoint() * d;
    
    log("%s's hp = %d",_fighterName.c_str(),_hp);
    updateHPLabel();
    
    if(_hp <=0 )
        die();
}

void Fighter::die()
{
    _isDead = true;
    
    // gray icon
    _icon->setTexture(_imageGrayScale);
    
    // Notify core game to stop
    if(_fighterName == "Player")
        CoreGame::getInstance()->setMonsterWin();
    else
        CoreGame::getInstance()->setPlayerWin();
}

void Fighter::onEnter()
{
	_icon = Sprite::create(_image);
	_icon->setPosition(0,0);
	this->addChild(_icon,GAMELAYERUIBOTTOMTAG);
    
    auto bottom = Sprite::create(Resources::getInstance()->getHPBar2());
	Point pos(_icon->getPositionX() - _icon->getContentSize().width/2 ,_icon->getPositionY() - _icon->getContentSize().height/2);
    
	bottom->setPosition(pos);
    bottom->setAnchorPoint(Point::ZERO);
	this->addChild(bottom,GAMELAYERUIMIDTAG);
    
    _hpTop = Sprite::create(Resources::getInstance()->getHPBar1());
	_hpTop->setPosition(pos);
    _hpTop->setAnchorPoint(Point::ZERO);
	this->addChild(_hpTop,GAMELAYERUITOPTAG);
    
    _coolDown = _attackInterval;
    
    _coolDownMsg = Label::createWithBMFont(Resources::getInstance()->getNumberFont(),"9");
    _coolDownMsg->setPosition(pos.x + 50,pos.y - 50);
    
    this->addChild(_coolDownMsg,GAMELAYERUITOPTAG);
}

void Fighter::updateHPLabel()
{
    float percentage = _hp * 1.0f / _maxHP;

    Rect rect = _hpTop->getTextureRect();
    
    Rect rect2 = Rect(0, 0, rect.size.width * percentage, rect.size.height);
    
    _hpTop->setTextureRect(rect2);
    
}

void Fighter::updateCoolDown()
{
    _coolDown -- ;
    __String* cooldownStr = __String::createWithFormat("%d",_coolDown);
    _coolDownMsg->setString(cooldownStr->getCString());
}

void Fighter::refreshCoolDown()
{
    _coolDown = _attackInterval;
}


////////////////////////////////////
/// Player
///////////////////////////////////
Player::Player(const Player& p)
{
    _fighterName = p.getFighterName();
    _hp = p.getHP();
    _maxHP = p.getMAXHP();
    _image = p.getImage();
    _attackPoint = p.getAttackPoint();
}


////////////////////////////////////
/// Monster
///////////////////////////////////
Monster::Monster(const Monster& m)
{
    _fighterName = m.getFighterName();
    _hp = m.getHP();
    _maxHP = m.getMAXHP();
    _image = m.getImage();
    _attackPoint = m.getAttackPoint();
}