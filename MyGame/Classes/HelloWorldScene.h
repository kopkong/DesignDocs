#ifndef __HELLOWORLD_SCENE_H__
#define __HELLOWORLD_SCENE_H__

#include "cocos2d.h"
#include "Squad.h"

class HelloWorld : public cocos2d::Layer
{
private:
    std::vector<Squad> _squadListA;
    std::vector<Squad> _squadListB;
    Size _screenSize;
    
    
    
    std::map<int,Sprite*> _allUnitsSprite;
    std::map<int,int> _allUnitsHealth;
    void squadMove(Squad* sq,float);
    void battleBegin(Squad* sq);
    void saveHeroSprite(int squadIndex, Sprite*);
    void saveSoldierSprite(int squadIndex,int soldierIndex, Sprite*);
    int getHeroSpriteID(int squadIndex);
    int getSoldierSpriteID(int squadIndex,int soldierIndex);
    void initSquads();
    void searchEnemy(Squad*);
    
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
