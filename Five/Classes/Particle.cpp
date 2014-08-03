//
//  Particle.cpp
//  Game2
//
//  Created by 孔 令锴 on 14-5-26.
//
//

#include "Particle.h"
#include "Resources.h"

Particle::~Particle()
{
    CC_SAFE_RELEASE(_emitter);
}

void Particle::onEnter()
{
    cocos2d::Node::onEnter();
    
    _emitter = NULL;
    
    auto listener = EventListenerTouchAllAtOnce::create();
    listener->onTouchesBegan = CC_CALLBACK_2(Particle::onTouchesBegan,this);
    listener->onTouchesMoved = CC_CALLBACK_2(Particle::onTouchesMoved,this);
    listener->onTouchesEnded = CC_CALLBACK_2(Particle::onTouchesEnded,this);
    
    _eventDispatcher->addEventListenerWithSceneGraphPriority(listener,this);
    
    scheduleUpdate();
}

void Particle::onTouchesBegan(const std::vector<Touch*>& touches, Event *event)
{
    onTouchesEnded(touches, event);
}

void Particle::onTouchesMoved(const std::vector<Touch*>& touches, Event *event)
{
    return onTouchesEnded(touches, event);
}

void Particle::onTouchesEnded(const std::vector<Touch*>& touches, Event* event)
{
    auto touch = touches[0];
    
    auto location = touch->getLocation();
    
    auto pos = Point::ZERO;
    
    if(_emitter != NULL)
    {
        _emitter->setPosition(location - pos);
    }
}

void Particle::update(float dt)
{
    
}

void Particle::setEmitterPosition(Point pos)
{
    if(_emitter != NULL)
        _emitter->setPosition(pos);
}

void FireBall::onEnter()
{
    Particle::onEnter();
    
    _emitter = ParticleSun::create();
    _emitter->retain();
    this->addChild(_emitter);
    
    _emitter->setTexture(Director::getInstance()->getTextureCache()->addImage(Resources::getInstance()->getFireImage()));
    
}

void IceBall::onEnter()
{
    Particle::onEnter();
    
    _emitter = ParticleMeteor::create();
    _emitter->retain();
    this->addChild(_emitter);
    
    _emitter->setTexture(Director::getInstance()->getTextureCache()->addImage(Resources::getInstance()->getFireImage()));
    
}

