#ifndef __HELLOWORLD_SCENE_H__
#define __HELLOWORLD_SCENE_H__

#include "cocos2d.h"
#include "Squad.h"

class HelloWorld : public cocos2d::Layer
{
private:
	std::vector<Squad> _allSquadsInBattle;
    Size _screenSize;
    bool _battleFinished;
    
    std::map<int,Sprite*> _allUnitsSprite;
    std::map<int,int> _allUnitsHealth;
    std::map<int,int> _allUnitsTargetIndex; // Every unit in the battle should have one target
    
    void squadMove(Squad* sq,float);
    void battleBegin(Squad* sq);
    void squadWait(Squad* sq);
    void searchEnemy(Squad*);
    void squadFighting(Squad* sq, float dt);
    
    void saveHeroSprite(int squadIndex, Sprite*);
    void saveSoldierSprite(int squadIndex,int soldierIndex, Sprite*);
    int getHeroSpriteID(int squadIndex);
    int getSoldierSpriteID(int squadIndex,int soldierIndex);
    void initSquads();
    void initSquadProperty(Squad*,SquadType);
    Squad* getSquadByIndex(int squadIndex);
    
    Point moveSpriteUnit(int selfID,Squad* pSelfSquad,float dt); // Move a unit sprite
    
    void pickTarget(int selfID, Squad*,Squad*);
    void attackTarget(int selfID, Squad* pSelfSquad, Squad* pTargetSquad,float dt);
    
    bool unitAlive(int);
    bool soldierUnitAlive(int);
    bool checkSideWin(SquadSide);
    
public:
    // there's no 'id' in cpp, so we recommend returning the class instance pointer
    static cocos2d::Scene* createScene();

    // Here's a difference. Method 'init' in cocos2d-x returns bool, instead of returning 'id' in cocos2d-iphone
    virtual bool init();
    
    // a selector callback
    void menuCloseCallback(cocos2d::Ref* pSender);
    
    void drawAll();
    
    void drawSquad(Squad*);
    
    void update(float);
    
    // implement the "static create()" method manually
    CREATE_FUNC(HelloWorld);
};

#endif // __HELLOWORLD_SCENE_H__
