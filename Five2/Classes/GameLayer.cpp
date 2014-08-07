﻿#include "GameLayer.h"
#include <string>

#include "AssertConfigs.h"


static int _currentLevel = 0;

GameLayer::~GameLayer()
{

}

Scene* GameLayer::createScene(int level)
{
	_currentLevel = level;

    // 'scene' is an autorelease object
    auto scene = Scene::create();
    
    // 'layer' is an autorelease object
    auto layer = GameLayer::create();

    // add layer as a child to scene
    scene->addChild(layer);

    // return the scene
    return scene;
}

// on "init" you need to initialize your instance
bool GameLayer::init()
{
    //////////////////////////////
    // 1. super init first
    if ( !Layer::init() )
    {
        return false;
    }

	initState();
	initTexture();
	initUI();

	this->schedule(schedule_selector(GameLayer::update));

    return true;
}

void GameLayer::initState()
{
	// set start is false
	_gameRunning = false;

	_totalTimeOption = TotalTimeOption::_15M;
	_extraTimeOption = ExtraTimeOption::_1M;

	_gameSettings.TotalTime = TOTALTIME_SECONDS[_totalTimeOption];
	_gameSettings.ExtraTime = EXTRATIME_SECONDS[_extraTimeOption];
	_gameSettings.hasForbidden = false;

	// 初始化电脑选手
	if(_currentLevel > 0)
	{
		_hasComputerPlayer = true;
		_isComputerTurn = false;
	}
}

void GameLayer::initUI()
{
	// load UI
	_layout = static_cast<Layout*>(
		cocostudio::GUIReader::getInstance()->widgetFromJsonFile(UI_LAYOUT_MAIN.c_str())); 
    
    _screenSize = Director::getInstance()->getVisibleSize();
	//Size rootSize = _layout->getSize();
	Node* rootChild = _layout->getChildren().at(0);

	// 棋盘
	_WidgetChessBoard = static_cast<Widget*>(rootChild->getChildByTag(UI_MAIN_CHESSBOARDTAG));

	// Player Boxes
	{
		Widget* playerOneWidget = static_cast<Widget*>(rootChild->getChildByTag(25));
		Widget* playerTwoWidget = static_cast<Widget*>(rootChild->getChildByTag(27));

		_ImageViewplayerOneColor = static_cast<ImageView*>(playerOneWidget->getChildByName("PlayerOneColor"));
		_ImageViewplayerTwoColor = static_cast<ImageView*>(playerTwoWidget->getChildByName("PlayerTwoColor"));

		_ImageViewplayerOneColor->loadTexture(BOX_BLACK_PATH);
		_ImageViewplayerTwoColor->loadTexture(BOX_WHITE_PATH);

		// 时间便签
		_TextAtlasplayerOneTimeLabel = static_cast<TextAtlas*>(playerOneWidget->getChildByName("PlayerOneTime"));
		_TextAtlasplayerTwoTimeLabel = static_cast<TextAtlas*>(playerTwoWidget->getChildByName("PlayerTwoTime"));
	}

	// 输赢信息面板
	{
		_messageDialog = new FiveMessageBox();
		//_messageDialog->setAnchorPoint(0.5,0.5);
		_messageDialog->setPosition(Point(392,544));
		this->addChild(_messageDialog,3);

		//_messageDialog->showBlackWin();
		_messageDialog->setVisible(false);
	}

	// Setting 面板 ， Button监听事件
	{
		_WidgetsettingsBoard = static_cast<Widget*>(rootChild->getChildByTag(UI_MAIN_SETTINGBOARDTAG));
		Button* startButton = static_cast<Button*>(_WidgetsettingsBoard->getChildByTag(UI_MAIN_BUTTONTAG_START));
		startButton->addTouchEventListener(this,toucheventselector(GameLayer::uiButtonTouchCallback));

		_ImageViewtotoalTimeSettings = static_cast<ImageView*>(_WidgetsettingsBoard->getChildByName("Setting4_Text_TotalTime"));
		_ImageViewextralTimeSettings = static_cast<ImageView*>(_WidgetsettingsBoard->getChildByName("Setting4_Text_ExtraTime"));

		_ImageViewtotoalTimeSettings->loadTexture(TOTALTIME_FRAMENAME[(int)_totalTimeOption],TextureResType::UI_TEX_TYPE_PLIST);
		_ImageViewextralTimeSettings->loadTexture(EXTRATIME_FRAMENAME[(int)_extraTimeOption],TextureResType::UI_TEX_TYPE_PLIST);

		CheckBox* setting1 = static_cast<CheckBox*>(_WidgetsettingsBoard->getChildByTag(UI_MAIN_BUTTONTAG_SETTING1));
		CheckBox* setting2 = static_cast<CheckBox*>(_WidgetsettingsBoard->getChildByTag(UI_MAIN_BUTTONTAG_SETTING2));
		CheckBox* setting3 = static_cast<CheckBox*>(_WidgetsettingsBoard->getChildByTag(UI_MAIN_BUTTONTAG_SETTING3));

		setting1->addTouchEventListener(this,toucheventselector(GameLayer::uiButtonTouchCallback));
		setting2->addTouchEventListener(this,toucheventselector(GameLayer::uiButtonTouchCallback));
		setting3->addTouchEventListener(this,toucheventselector(GameLayer::uiButtonTouchCallback));
			
		Slider* slider1 = static_cast<Slider*>(_WidgetsettingsBoard->getChildByName("Slider_TotalTime"));
		Slider* slider2 = static_cast<Slider*>(_WidgetsettingsBoard->getChildByName("Slider_ExtraTime"));

		slider1->addTouchEventListener(this,toucheventselector(GameLayer::uiButtonTouchCallback));
		slider2->addTouchEventListener(this,toucheventselector(GameLayer::uiButtonTouchCallback));
	}

	this->addChild(_layout);
}

void GameLayer::initPieces()
{
	_WidgetChessBoard->removeAllChildren();

	// Add 15 * 15 buttons on chess board
	{
		Point leftTopCorner(39,728);
		Point pieceOffset(5,2);
		Size cellSize(50,50);

		int index = 0;
		for(int row = 0 ; row < 15; row ++)
		{
			for(int column = 0 ; column < 15; column ++)
			{
				Button* stone = Button::create();
				stone->setTag(index);
				float positionX = leftTopCorner.x + column * cellSize.width - pieceOffset.x  ;
				float positionY = leftTopCorner.y - row * cellSize.height - pieceOffset.y   ;
				stone->setPosition(Point(positionX,positionY));
				stone->loadTextureNormal(SPRITECACHE_NAME_HOVERSTONE,TextureResType::UI_TEX_TYPE_PLIST);
				stone->addTouchEventListener(this,toucheventselector(GameLayer::stoneTouchCallback));
				stone->setVisible(false);
				index ++;

				_WidgetChessBoard->addChild(stone,2);
			}
		}
	}

}

void GameLayer::initTexture()
{
	SpriteFrameCache* frameCache = SpriteFrameCache::getInstance();

	SpriteFrame* blackNormalSprite = SpriteFrame::create(UI_ICON_PIECES,Rect(0,0,66,66));
	SpriteFrame* blackSelectedSprite = SpriteFrame::create(UI_ICON_PIECES,Rect(0,66,66,66));
	SpriteFrame* whiteNormalSprite = SpriteFrame::create(UI_ICON_PIECES,Rect(66,0,66,66));
	SpriteFrame* whiteSelectedSprite = SpriteFrame::create(UI_ICON_PIECES,Rect(66,66,66,66));
	SpriteFrame* transparentSprite = SpriteFrame::create(UI_ICON_TRANSPARENTPIECE,Rect(0,0,50,50));
	SpriteFrame* hoverSprite = SpriteFrame::create(UI_ICON_PIECE_HOVER,Rect(0,0,50,50));

	SpriteFrame* playerOneWin = SpriteFrame::create(RESULT_TEXT_FILE_PATH,Rect(0,0,205,40));
	SpriteFrame* playerTwoWin = SpriteFrame::create(RESULT_TEXT_FILE_PATH,Rect(0,40,205,40));
	SpriteFrame* playerOneForbidden = SpriteFrame::create(RESULT_TEXT_FILE_PATH,Rect(0,80,205,40));
	SpriteFrame* playerOneTimeout = SpriteFrame::create(RESULT_TEXT_FILE_PATH,Rect(0,120,205,40));
	SpriteFrame* playerTwoTimeout = SpriteFrame::create(RESULT_TEXT_FILE_PATH,Rect(0,160,205,40));

	SpriteFrame* playerOneBox = SpriteFrame::create(BOXES_FILE_PATH,Rect(0,0,51,40));
	SpriteFrame* playerTwoBox = SpriteFrame::create(BOXES_FILE_PATH,Rect(0,40,51,40));

	SpriteFrame* time5m			= SpriteFrame::create(TOTALTIME_TEXT_PATH,Rect(0,0,48,24));
	SpriteFrame* time10m		= SpriteFrame::create(TOTALTIME_TEXT_PATH,Rect(48,0,48,24));
	SpriteFrame* time15m		= SpriteFrame::create(TOTALTIME_TEXT_PATH,Rect(96,0,48,24));
	SpriteFrame* time20m		= SpriteFrame::create(TOTALTIME_TEXT_PATH,Rect(144,0,48,24));
	SpriteFrame* time25m		= SpriteFrame::create(TOTALTIME_TEXT_PATH,Rect(192,0,48,24));
	SpriteFrame* time30m		= SpriteFrame::create(TOTALTIME_TEXT_PATH,Rect(240,0,48,24));

	SpriteFrame* timem30s		= SpriteFrame::create(EXTRATIME_TEXT_PATH,Rect(0,0,47,23));
	SpriteFrame* time1m			= SpriteFrame::create(EXTRATIME_TEXT_PATH,Rect(47,0,47,23));
	SpriteFrame* time2m			= SpriteFrame::create(EXTRATIME_TEXT_PATH,Rect(94,0,47,23));

	frameCache->addSpriteFrame(playerOneWin,SPRITECACHE_NAME_BLACKWIN);
	frameCache->addSpriteFrame(playerTwoWin,SPRITECACHE_NAME_WHITEWIN);
	frameCache->addSpriteFrame(playerOneForbidden,SPRITECACHE_NAME_FORBIDDENLOSE);
	frameCache->addSpriteFrame(playerOneTimeout,SPRITECACHE_NAME_BLACKTIMEOUT);
	frameCache->addSpriteFrame(playerTwoTimeout,SPRITECACHE_NAME_WHITETIMEOUT);

	//frameCache->addSpriteFrame(blackNormalSprite,"blackPieceNormal");
	//frameCache->addSpriteFrame(blackSelectedSprite,"blackPieceSelected");
	//frameCache->addSpriteFrame(whiteNormalSprite,"whitePieceNormal");
	//frameCache->addSpriteFrame(whiteSelectedSprite,"whitePieceSelected");
	//frameCache->addSpriteFrame(transparentSprite,"transparentPiece");
	//frameCache->addSpriteFrame(hoverSprite,"hoverSprite");
	frameCache->addSpriteFrame(blackNormalSprite,SPRITECACHE_NAME_BLACKSTONE);
	frameCache->addSpriteFrame(whiteNormalSprite,SPRITECACHE_NAME_WHITESTONE);
	frameCache->addSpriteFrame(hoverSprite,SPRITECACHE_NAME_HOVERSTONE);

	frameCache->addSpriteFrame(playerOneBox,"playerOneBox");
	frameCache->addSpriteFrame(playerTwoBox,"playerTwoBox");

	frameCache->addSpriteFrame(timem30s,"time30s");
	frameCache->addSpriteFrame(time1m,"time1m");
	frameCache->addSpriteFrame(time2m,"time2m");
	frameCache->addSpriteFrame(time5m,"time5m");
	frameCache->addSpriteFrame(time10m,"time10m");
	frameCache->addSpriteFrame(time15m,"time15m");
	frameCache->addSpriteFrame(time20m,"time20m");
	frameCache->addSpriteFrame(time25m,"time25m");
	frameCache->addSpriteFrame(time30m,"time30m");
}

void GameLayer::resetGame()
{
	_gameRunning = true;

	_playerOneSide = PieceSide::BlackSide;
	_playerTwoSide = PieceSide::WhiteSide;

	_gameSettings.TotalTime = TOTALTIME_SECONDS[_totalTimeOption];
	_gameSettings.ExtraTime = EXTRATIME_SECONDS[_extraTimeOption];

	_playerOneTotalTime = _gameSettings.TotalTime;
	_playerTwoTotalTime = _gameSettings.TotalTime;

	_playerOneExtraTurnTime = _gameSettings.ExtraTime;
	_playerTwoExtraTurnTime = _gameSettings.ExtraTime;

	uiRefreshTime(_gameSettings.TotalTime);

	// 重置先手玩家
	_whoseTurn = TurnOwner::PlayerOne;

	// 重置棋子
	initPieces();

	// 重置规则
	Rule::getInstance()->Init(_gameSettings);

	// 提示框不可见
	_messageDialog->setVisible(false);
	

	if(!this->isScheduled(schedule_selector(GameLayer::updateTotalTime)))
	{
		this->schedule(schedule_selector(GameLayer::updateTotalTime),1.0f);
	}
}

void GameLayer::update(float dt)
{
	// 在游戏还没开始前读取设置信息
	if(!_gameRunning)
	{
		//int totalTimePercent = _SliderTotalTime->getPercent();
		//int extraTimePercent = _SliderExtraTime->getPercent();
	}
}

void GameLayer::updateTotalTime(float dt)
{
	if(!_gameRunning)
		return;

	if(_whoseTurn == TurnOwner::PlayerOne)
	{
		if(_playerOneTotalTime > 0 )
		{
			_playerOneTotalTime --;
			uiRefreshTime(_playerOneTotalTime);
		}
		else
		{
			if( _playerOneExtraTurnTime > 0)
			{
				_playerOneExtraTurnTime --;
				uiRefreshTime(_playerOneExtraTurnTime);
			}
			else
			{
				timeoutLose(_playerOneSide);
			}
		}
	}
	else
	{
		if(_playerTwoTotalTime > 0 )
		{
			_playerTwoTotalTime --;
			uiRefreshTime(_playerTwoTotalTime);
		}
		else
		{
			if( _playerTwoExtraTurnTime > 0)
			{
				_playerTwoExtraTurnTime --;
				uiRefreshTime(_playerTwoExtraTurnTime);
			}
			else
			{
				timeoutLose(_playerTwoSide);
			}
		}
	}
}

// 交换回合
void GameLayer::changeTurn()
{
	if(_whoseTurn == TurnOwner::PlayerOne)
	{
		if(_playerOneTotalTime <= 0)
		{
			_playerOneExtraTurnTime = _gameSettings.ExtraTime;
			uiRefreshTime(_gameSettings.ExtraTime);
		}

		_whoseTurn = TurnOwner::PlayerTwo;
		_ImageViewplayerOneColor->setVisible(false);
		_ImageViewplayerTwoColor->setVisible(true);
	}
	else
	{
		if(_playerTwoTotalTime <= 0)
		{
			_playerTwoExtraTurnTime = _gameSettings.ExtraTime;
			uiRefreshTime(_gameSettings.ExtraTime);
		}
		_whoseTurn = TurnOwner::PlayerOne;
		_ImageViewplayerOneColor->setVisible(true);
		_ImageViewplayerTwoColor->setVisible(false);
	}
}

void GameLayer::winGame(PieceSide side)
{
	_gameRunning = false;

	_messageDialog->setVisible(true);

	if(side == PieceSide::BlackSide)
		_messageDialog->showBlackWin();
	else
		_messageDialog->showWhiteWin();

	//log("Player %d win the game!",owner);
}

void GameLayer::forbiddenLose()
{
	_gameRunning = false;
	_messageDialog->setVisible(true);
	_messageDialog->showBlackForbiddenLose();
}

void GameLayer::timeoutLose(PieceSide side)
{
	_gameRunning = false;
	_messageDialog->setVisible(true);

	if(side == PieceSide::BlackSide)
		_messageDialog->showBlackTimeout();
	else
		_messageDialog->showWhiteTimeout();
}

void GameLayer::stoneTouchCallback(Ref* obj,TouchEventType eventType)
{
	if(!_gameRunning || _isComputerTurn)
		return;

	Button* stone = dynamic_cast<Button*>(obj);

	switch(eventType)
	{
	case TouchEventType::TOUCH_EVENT_BEGAN:
		{
			stone->setVisible(true);
			break;
		}
	case TouchEventType::TOUCH_EVENT_ENDED:
		{
			putStone(stone);
			break;
		}
	default:
		break;
	}
	
}

void GameLayer::uiButtonTouchCallback(Ref* obj,TouchEventType eventType)
{
	auto widget = dynamic_cast<Widget*>(obj);
	int tag = widget->getTag();

	switch(eventType)
	{
	case TouchEventType::TOUCH_EVENT_ENDED:
		{
			log("Touch event ended!");
			if(tag == UI_MAIN_BUTTONTAG_START)
			{
				resetGame();
			}
			else if(tag == UI_MAIN_BUTTONTAG_SETTING1)
			{
				_gameSettings.hasForbidden = !_gameSettings.hasForbidden;
			}
			else if(tag == UI_MAIN_SLIDERTAG_TOTALTIME)
			{
				int percent = static_cast<Slider*>(obj)->getPercent();
				if(percent < 17)
					_totalTimeOption = TotalTimeOption::_5M;
				else if(percent < 34)
					_totalTimeOption = TotalTimeOption::_10M;
				else if(percent <= 50)
					_totalTimeOption = TotalTimeOption::_15M;
				else if(percent < 67)
					_totalTimeOption = TotalTimeOption::_20M;
				else if(percent < 84)
					_totalTimeOption = TotalTimeOption::_25M;
				else
					_totalTimeOption = TotalTimeOption::_30M;

				_ImageViewtotoalTimeSettings->loadTexture(TOTALTIME_FRAMENAME[(int)_totalTimeOption],TextureResType::UI_TEX_TYPE_PLIST);
			}
			else if(tag == UI_MAIN_SLIDERTAG_EXTRATIME)
			{
				int percent = static_cast<Slider*>(obj)->getPercent();

				if(percent < 34)
					_extraTimeOption = ExtraTimeOption::_30S;
				else if(percent < 67)
					_extraTimeOption = ExtraTimeOption::_1M;
				else
					_extraTimeOption = ExtraTimeOption::_2M;

				_ImageViewextralTimeSettings->loadTexture(EXTRATIME_FRAMENAME[(int)_extraTimeOption],TextureResType::UI_TEX_TYPE_PLIST);
			}

			break;
		}
	case TouchEventType::TOUCH_EVENT_MOVED:
		{
			log("Touch event moved!");
		}
	default:
		break;
	}
}

void GameLayer::putStone(Button* stone)
{
	int index = stone->getTag();
	log("Touch stone %d",index);

	int row = index /15;
	int column = index % 15;
	log("Row: %d, Column: %d",row,column);

	// 图片需往右下偏移
	stone->setPosition(stone->getPosition() - Point(-3,3));

	if(_whoseTurn == TurnOwner::PlayerOne)
	{
		stone->loadTextureNormal(SPRITECACHE_NAME_BLACKSTONE,TextureResType::UI_TEX_TYPE_PLIST);
		//stone->loadTextureDisabled(SPRITECACHE_NAME_BLACKSTONE,TextureResType::UI_TEX_TYPE_PLIST);
		stone->setTouchEnabled(false);

		Rule::getInstance()->setData(row,column,_playerOneSide);

		if(Rule::getInstance()->isFinished())
		{
			if(Rule::getInstance()->getWinner() == _playerOneSide)
				winGame(_playerOneSide);
			else
				forbiddenLose();
		}
	}
	else
	{
		stone->loadTextureNormal(SPRITECACHE_NAME_WHITESTONE,TextureResType::UI_TEX_TYPE_PLIST);
		stone->setTouchEnabled(false);

		Rule::getInstance()->setData(row,column,_playerTwoSide);

		if(Rule::getInstance()->isFinished())
		{
			if(Rule::getInstance()->getWinner() == _playerTwoSide)
				winGame(_playerTwoSide);
			else
				forbiddenLose();
		}
	}

	changeTurn();
}

void GameLayer::computerMove(int level)
{


}


void GameLayer::uiRefreshTime(int time)
{
	char buf[100] = {0};
	int minutes = time/60;
	int seconds = time%60;
	sprintf(buf,"%i:%i",minutes,seconds);

	if(_whoseTurn == TurnOwner::PlayerOne)
	{
		_TextAtlasplayerOneTimeLabel->setStringValue(buf);
	}
	else if(_whoseTurn == TurnOwner::PlayerTwo)
	{
		_TextAtlasplayerTwoTimeLabel->setStringValue(buf);
	}
	else
	{
		_TextAtlasplayerOneTimeLabel->setStringValue(buf);
		_TextAtlasplayerTwoTimeLabel->setStringValue(buf);
	}
}
