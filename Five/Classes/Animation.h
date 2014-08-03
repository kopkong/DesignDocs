//
//  Animation.h
//  Game2
//
//  Created by 孔 令锴 on 14-5-27.
//
//

#ifndef __Game2__Animation__
#define __Game2__Animation__

#include <iostream>
#include "cocos2d.h"
USING_NS_CC;

class CKAnimation
{
protected:
    CKAnimation();
    ~CKAnimation();
    
public:
    static void getFlipNumberAnimation(float duration,const std::function<void(Node*)> &func,Action* &a);
    
    static void getParticleMoveAnimation(float duration,Point pos,const std::function<void(Node*)> &func,Action* &a);
};

#endif /* defined(__Game2__Animation__) */
