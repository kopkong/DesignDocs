#ifndef __GameLayer_SCENE_H__
#define __GameLayer_SCENE_H__

#include "cocos2d.h"
#include <vector>
#include "public.h"
#include "CocosGUI.h"
#include "cocostudio/CocoStudio.h"
#include "MessageBox.h"
#include "ChessBoard.h"
#include "pthread.h"

USING_NS_CC;
using namespace cocos2d::ui;

class GameLayer : public cocos2d::Layer
{
private:
    Size _screenSize;
	bool _hasComputerPlayer;
	bool _isComputerTurn;

	Layout* _layout;
	ChessBoard* _WidgetChessBoard;
	Widget* _WidgetsettingsBoard;
	CheckBox* _CheckBoxSetting1;
	CheckBox* _CheckBoxSetting2;
	CheckBox* _CheckBoxSetting3;
	Slider* _SliderTime1;
	Slider* _SliderTime2;

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

	// 显示时间
	void uiRefreshTime(int time);

	GameSettings _gameSettings;

	TotalTimeOption _totalTimeOption;
	ExtraTimeOption _extraTimeOption;

	// 消息窗口
	FiveMessageBox* _messageDialog;

	// 电脑下棋
	void aiMove();

	EventCustom* _aiEvent;
	EventListenerCustom* _listener;
	EventListenerCustom* _listener2;

	static GomokuData aiData;
	static int aiResult;
	static bool aiInTheWork;
	pthread_t aiWordThreadID;
	static pthread_mutex_t mutex;
	static void* aiWorkThread(void *r);  

protected:
	~GameLayer();
    
public:
    static cocos2d::Scene* createScene(int level);

	CREATE_FUNC(GameLayer);

    virtual bool init();
    
	void updateTotalTime(float dt);

	void updateAIState(float dt);
    
	void loadLevel(int level);
    
    void resetGame();
    
    void initUI();

	void initState();

	// 处理自定义事件
	void dealWithCustomEvent();

	// 回合变化
	void turnChange();

	// 交换黑白方
	void changeSide();

	void endGame();

	// 所有UI按钮的touch处理事件
	void uiButtonTouchCallback(Ref* obj,TouchEventType eventType);

};

#endif // __GameLayer_SCENE_H__
