#ifndef __GameLayer_SCENE_H__
#define __GameLayer_SCENE_H__

#include "cocos2d.h"
#include "SudokuFactory.h"

class GameLayer : public cocos2d::Layer
{
private:
	SudokuProduction _sudokuDataStruct;
    Size _screenSize;
public:
    static cocos2d::Scene* createScene(int level);
	CREATE_FUNC(GameLayer);

    virtual bool init();  
	void update(float dt);
	void loadLevel(int level);
    void display();
    
};

#endif // __GameLayer_SCENE_H__
