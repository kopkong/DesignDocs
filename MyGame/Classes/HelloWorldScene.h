#ifndef __HELLOWORLD_SCENE_H__
#define __HELLOWORLD_SCENE_H__

#include "cocos2d.h"
#include "Squad.h"
#include "Battle.h"

enum MenuTag
{
    Level1,
    Level2
};

class HelloWorld : public cocos2d::Layer
{
private:
    Size _screenSize;
    int _testTimes;
    int _allTestTimes;
    
    bool _inFormationSelect;
    int _currentItemIndex;
    Vector<Label*> _menuLabels;
    Vector<MenuItem*> _menuItems;
    
    bool _inLeveltSelect;
    bool _inDisplayResult;
    int _level;
    
    Formation _leftFormation;
    Formation _rightFormation;
	int _leftSquads;
	int _rightSquads;
    SquadSide _whichSideWin;
    int _teamAWins;
    int _teamBWins;
    
    float _speedUpRate;
	void initBattle();
    void setFormationLabelText(SquadSide,int row,int col,Label*);
public:
    // there's no 'id' in cpp, so we recommend returning the class instance pointer
    static cocos2d::Scene* createScene();

    // Here's a difference. Method 'init' in cocos2d-x returns bool, instead of returning 'id' in cocos2d-iphone
    virtual bool init();
    
    void menuItemCallback(int index);
    void menuSubItemCallback(SquadType);
    
    void menuStartBattle(int battleTimes);
    void menuBackToLevelSelectCallback();
	void menuRandomFormation(bool isPlayer);
        
    void update(float);
    
    void resetTest();
    
    void showFormationSelect();
    
    void showLevelSelect();
    
    void showResults();
    
    // implement the "static create()" method manually
    CREATE_FUNC(HelloWorld);
};

#endif // __HELLOWORLD_SCENE_H__
