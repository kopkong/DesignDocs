#ifndef __GameLayer_SCENE_H__
#define __GameLayer_SCENE_H__

#include "cocos2d.h"
#include "SudokuFactory.h"
#include "Player.h"
#include "Monster.h"
#include <vector>

USING_NS_CC;

class GameLayer : public cocos2d::Layer
{
private:
	SudokuProduction _sudokuDataStruct;
    Size _screenSize;
	bool _gameStarted;

    Label* _roundInfo;
    Label* _countDownMsg;
    int _countDown;


    SpriteBatchNode* _gameBatchNode;
    Map<int,MenuItemImage*> _emptyItems;
	Vector<MenuItem*> _menuItems;


	Player* _player;
	Monster* _monster;
	Sprite* _playerHPbar;
	Sprite* _monsterHPbar;

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
    
    void displayRound();

	void displayCharacters();

	void displayHP();
    
    void updateInRound(float dt);

	void initLabelUI();
    
    void roundCountDownOver();
};

#endif // __GameLayer_SCENE_H__
