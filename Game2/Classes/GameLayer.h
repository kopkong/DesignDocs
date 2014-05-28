#ifndef __GameLayer_SCENE_H__
#define __GameLayer_SCENE_H__

#include "cocos2d.h"
#include "SudokuFactory.h"
#include "Player.h"
#include <vector>

USING_NS_CC;

class GameLayer : public cocos2d::Layer
{
private:
	SudokuProduction _sudokuDataStruct;
    Size _screenSize;
	bool _gameStarted;

    bool _playerAttackAvailable;
    bool _monsterAttackAvailable;

    SpriteBatchNode* _gameBatchNode;
    Map<int,MenuItemImage*> _emptyItems;
	Vector<MenuItem*> _menuItems;

	Player* _player;
	Monster* _monster;
    
    void monsterAIAttack();
    
    void fireMonster(Point);
    void firePlayer(Point);

    void callBack_FireMonsterEnd();
    void callBack_FirePlayerEnd();
	void callBack_StartGame();
	void callBack_SelectNumber(int i);
	void callBack_ChangeImage(int i);

protected:
	~GameLayer();
    
public:
    static cocos2d::Scene* createScene(int level);
	CREATE_FUNC(GameLayer);

    virtual bool init();
    
	void update(float dt);
    
	void loadLevel(int level);
    
    void resetGame();
    
    void initScreen();
    
    void updatePlayerCoolDown(float dt);
    
    void updateMonsterCoolDown(float dt);
    
    void endGame();
    
};

#endif // __GameLayer_SCENE_H__
