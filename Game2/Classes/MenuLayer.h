//
//  MenuLayer.h
//  Game2
//
//  Created by 孔 令锴 on 14-5-17.
//
//

#ifndef __Game2__MenuLayer__
#define __Game2__MenuLayer__

#include <iostream>
#include "cocos2d.h"

USING_NS_CC;

class MenuLayer : public cocos2d::Layer
{
public:
    // there's no 'id' in cpp, so we recommend returning the class instance pointer
    static cocos2d::Scene* createScene();
    
    // Here's a difference. Method 'init' in cocos2d-x returns bool, instead of returning 'id' in cocos2d-iphone
    virtual bool init();
    
    // implement the "static create()" method manually
    CREATE_FUNC(MenuLayer);
    
    void update(float dt);
    void showLevels();
    void showHelp();
    void showRecords();
    
    
};

#endif /* defined(__Game2__MenuLayer__) */
