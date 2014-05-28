//
//  Particle.h
//  Game2
//
//  Created by 孔 令锴 on 14-5-26.
//
//

#ifndef __Game2__Particle__
#define __Game2__Particle__

#include <iostream>
#include "cocos2d.h"

USING_NS_CC;

class Particle : public cocos2d::Node
{
protected:
    ParticleSystemQuad* _emitter;

public:
    ~Particle(void);
    virtual void onEnter(void);
    
    void onTouchesBegan(const std::vector<Touch*>& touches, Event  *event);
    void onTouchesMoved(const std::vector<Touch*>& touches, Event  *event);
    void onTouchesEnded(const std::vector<Touch*>& touches, Event  *event);
    
    virtual void update(float dt);
    void setEmitterPosition(Point pos);
    
};

class FireBall: public Particle{
    
public:
    virtual void onEnter(void) override;
};

class IceBall:public Particle{
public:
    virtual void onEnter(void) override;
};

#endif /* defined(__Game2__Particle__) */
