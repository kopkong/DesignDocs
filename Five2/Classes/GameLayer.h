#ifndef __GameLayer_SCENE_H__
#define __GameLayer_SCENE_H__

#include "cocos2d.h"
#include <vector>
#include "Rule.h"
#include "public.h"
#include "CocosGUI.h"
#include "cocostudio/CocoStudio.h"
#include "MessageBox.h"

USING_NS_CC;
using namespace cocos2d::ui;

class GameLayer : public cocos2d::Layer
{
private:
    Size _screenSize;
	bool _gameRunning;
	TurnOwner _whoseTurn;
	int _currentCellIndex;

	Layout* _layout;
	Widget* _WidgetChessBoard;
	Widget* _WidgetsettingsBoard;
	ImageView* _ImageViewplayerOneColor;
	ImageView* _ImageViewplayerTwoColor;
	ImageView* _ImageViewtotoalTimeSettings;
	ImageView* _ImageViewextralTimeSettings;

	TextAtlas* _TextAtlasplayerOneTimeLabel;
	TextAtlas* _TextAtlasplayerTwoTimeLabel;

	PieceSide _playerOneSide;
	PieceSide _playerTwoSide;

	int _playerOneTotalTime;
	int _playerTwoTotalTime;

	int _playerOneExtraTurnTime;
	int _playerTwoExtraTurnTime;

	// 所有UI按钮的touch处理事件
	void uiButtonTouchCallback(Ref* obj,TouchEventType eventType);

	// 棋子button的touch处理事件
	void stoneTouchCallback(Ref* obj,TouchEventType eventType);

	// 显示时间
	void uiRefreshTime(int time);

	void putStone(Button*);

	GameSettings _gameSettings;

	TotalTimeOption _totalTimeOption;
	ExtraTimeOption _extraTimeOption;

	// 消息窗口
	FiveMessageBox* _messageDialog;

protected:
	~GameLayer();
    
public:
    static cocos2d::Scene* createScene(int level);

	CREATE_FUNC(GameLayer);

    virtual bool init();
    
	void updateTotalTime(float dt);

	void update(float dt);
    
	void loadLevel(int level);
    
    void resetGame();
    
    void initUI();

	void initPieces();

	void initState();

	void initTexture();

	// 交换回合
	void changeTurn();

	// 交换黑白方
	void changeSide();

	void winGame(PieceSide);
    
	void forbiddenLose();

	void timeoutLose(PieceSide);

};

#endif // __GameLayer_SCENE_H__
