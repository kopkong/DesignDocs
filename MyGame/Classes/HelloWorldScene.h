#ifndef __HELLOWORLD_SCENE_H__
#define __HELLOWORLD_SCENE_H__

#include "cocos2d.h"
#include "Squad.h"
#include "Battle.h"

class HelloWorld : public cocos2d::Layer
{
private:
    Size _screenSize;
    int _testTimes;
    int _allTestTimes;
    
    bool _inLeveltSelect;
    bool _inDisplayResult;
    int _level;
    
    int _leftSideSquads;
    int _rightSideSquads;
    SquadSide _whichSideWin;
    
	void initBattle();
    
public:
    // there's no 'id' in cpp, so we recommend returning the class instance pointer
    static cocos2d::Scene* createScene();

    // Here's a difference. Method 'init' in cocos2d-x returns bool, instead of returning 'id' in cocos2d-iphone
    virtual bool init();
    
    
    void menuLevel1Callback();
    void menuLevel2Callback();
    void menuLevel3Callback();
    
    void menuBackToLevelSelectCallback();
        
    void update(float);
    
    void resetTest();
    
    void showLevelSelect();
    
    void showResults();
    
    // implement the "static create()" method manually
    CREATE_FUNC(HelloWorld);
};

#endif // __HELLOWORLD_SCENE_H__
