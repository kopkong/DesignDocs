//
//  Animation.cpp
//  Game2
//
//  Created by 孔 令锴 on 14-5-27.
//
//

#include "Animation.h"

void CKAnimation::getFlipNumberAnimation(float duration,const std::function<void(Node*)> &func,Action* &a)
{
    OrbitCamera *orbit = OrbitCamera::create(duration,0.5,0,0,180,0,0);
    DelayTime *delay = DelayTime::create(duration/2.0);
    FiniteTimeAction *sequence = Sequence::create(delay,
                                                  CallFuncN::create(func)
                                                  ,NULL);
    //filpNumberAnimation = Spawn::create(flip,sequence,NULL);
   
    a = Spawn::create(orbit,sequence,NULL);
}

void CKAnimation::getParticleMoveAnimation(float duration, cocos2d::Point pos, const std::function<void (Node *)> &func, cocos2d::Action *&a)
{
    Size screenSize = Director::getInstance()->getWinSize();
    Point newPos = Point(pos.x - screenSize.width/2, pos.y - screenSize.height/2);
    MoveTo *mt = MoveTo::create(duration, newPos);
    
    a = Sequence::create(mt,Hide::create(),CallFuncN::create(func),NULL);
}