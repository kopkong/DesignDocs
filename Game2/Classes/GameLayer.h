#ifndef __GameLayer_SCENE_H__
#define __GameLayer_SCENE_H__

#include "cocos2d.h"
#include "SudokuFactory.h"
#include <vector>

USING_NS_CC;

class GameLayer : public cocos2d::Layer
{
private:
	SudokuProduction _sudokuDataStruct;
    Size _screenSize;
    Label* _roundInfo;
    Label* _countDownMsg;
    int _countDown;
    SpriteBatchNode* _gameBatchNode;
    void buttonDown_Start();
    Vector<MenuItem*> _menuItems;
    
public:
    static cocos2d::Scene* createScene(int level);
	CREATE_FUNC(GameLayer);

    virtual bool init();
    
	void update(float dt);
    
	void loadLevel(int level);
    
    void resetGame();
    
    void initScreen();
    
    void displayRound();
    
    void updateInRound(float dt);
    
    void roundCountDownOver();
};

#endif // __GameLayer_SCENE_H__
