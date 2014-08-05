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
	Vector<MenuItem*> _vectorPieces;
	TurnOwner _whoseTurn;
	int _currentCellIndex;

	Layout* _layout;
	Menu* _piecesMenu;
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


	// ����UI��ť��touch�����¼�
	void uiButtonTouchCallback(Ref* obj,TouchEventType eventType);

	// ��ʾʱ��
	void uiRefreshTime(int time);

	GameSettings _gameSettings;

	TotalTimeOption _totalTimeOption;
	ExtraTimeOption _extraTimeOption;

	// ��Ϣ����
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

	// �����غ�
	void changeTurn();

	// �����ڰ׷�
	void changeSide();

	void winGame(PieceSide);
    
	void forbiddenLose();

	void timeoutLose(PieceSide);

	// �����Ӧλ������
	void cellTouchCallback(int index);
    
};

#endif // __GameLayer_SCENE_H__
