#include "GameLayer.h"
#include <string>
#include "AssertConfigs.h"
#include "Game.h"


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
	initUI();
	dealWithCustomEvent();
	
	if(_gameSettings.hasAI)
		this->schedule(schedule_selector(GameLayer::updateAIState),0.2);

    return true;
}

void GameLayer::initState()
{
	_totalTimeOption = TotalTimeOption::_15M;
	_extraTimeOption = ExtraTimeOption::_1M;

	_gameSettings.TotalTime = TOTALTIME_SECONDS[_totalTimeOption];
	_gameSettings.ExtraTime = EXTRATIME_SECONDS[_extraTimeOption];
	_gameSettings.hasForbidden = false;

	if(_currentLevel > 0) // 有AI
	{
		_gameSettings.hasAI = true;
		_gameSettings.aiSide = WhiteSide;
		_gameSettings.aiLevel = _currentLevel;
	}
	else
	{
		_gameSettings.hasAI = false;
	}
}

void GameLayer::initUI()
{
	// load UI
	_layout = static_cast<Layout*>(
		cocostudio::GUIReader::getInstance()->widgetFromJsonFile(UI_LAYOUT_MAIN.c_str())); 
    
    _screenSize = Director::getInstance()->getVisibleSize();
	Node* rootChild = _layout->getChildren().at(0);

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
		_WidgetsettingsBoard = static_cast<Widget*>(rootChild->getChildByTag(UI_TAGID_MAIN_SETTINGBOARD));
		Button* startButton = static_cast<Button*>(_WidgetsettingsBoard->getChildByTag(UI_MAIN_BUTTONTAG_START));
		startButton->addTouchEventListener(this,toucheventselector(GameLayer::uiButtonTouchCallback));

		_ImageViewtotoalTimeSettings = static_cast<ImageView*>(_WidgetsettingsBoard->getChildByName("Setting4_Text_TotalTime"));
		_ImageViewextralTimeSettings = static_cast<ImageView*>(_WidgetsettingsBoard->getChildByName("Setting4_Text_ExtraTime"));

		_ImageViewtotoalTimeSettings->loadTexture(TOTALTIME_FRAMENAME[(int)_totalTimeOption],TextureResType::UI_TEX_TYPE_PLIST);
		_ImageViewextralTimeSettings->loadTexture(EXTRATIME_FRAMENAME[(int)_extraTimeOption],TextureResType::UI_TEX_TYPE_PLIST);

		_CheckBoxSetting1 = static_cast<CheckBox*>(_WidgetsettingsBoard->getChildByTag(UI_MAIN_BUTTONTAG_SETTING1));
		_CheckBoxSetting2 = static_cast<CheckBox*>(_WidgetsettingsBoard->getChildByTag(UI_MAIN_BUTTONTAG_SETTING2));
		_CheckBoxSetting3 = static_cast<CheckBox*>(_WidgetsettingsBoard->getChildByTag(UI_MAIN_BUTTONTAG_SETTING3));

		_CheckBoxSetting1->addTouchEventListener(this,toucheventselector(GameLayer::uiButtonTouchCallback));
		_CheckBoxSetting2->addTouchEventListener(this,toucheventselector(GameLayer::uiButtonTouchCallback));
		_CheckBoxSetting3->addTouchEventListener(this,toucheventselector(GameLayer::uiButtonTouchCallback));
			
		_SliderTime1 = static_cast<Slider*>(_WidgetsettingsBoard->getChildByName("Slider_TotalTime"));
		_SliderTime2 = static_cast<Slider*>(_WidgetsettingsBoard->getChildByName("Slider_ExtraTime"));

		_SliderTime1->addTouchEventListener(this,toucheventselector(GameLayer::uiButtonTouchCallback));
		_SliderTime2->addTouchEventListener(this,toucheventselector(GameLayer::uiButtonTouchCallback));
	}

	// 棋盘
	{
		Widget* panelChessBoard = static_cast<Widget*>(rootChild->getChildByTag(UI_TAGID_MAIN_CHESSBOARD));
		ChessBoard* _WidgetChessBoard = ChessBoard::createWithSpriteFrameName(SPRITECACHE_NAME_CHESSBOARD);
		_WidgetChessBoard->setAnchorPoint(Point::ZERO);
		_WidgetChessBoard->setPosition(Point::ZERO);
		panelChessBoard->addChild(_WidgetChessBoard);
	}

	this->addChild(_layout);
}

void GameLayer::resetGame()
{
	_gameSettings.TotalTime = TOTALTIME_SECONDS[_totalTimeOption];
	_gameSettings.ExtraTime = EXTRATIME_SECONDS[_extraTimeOption];

	_playerOneTotalTime = _gameSettings.TotalTime;
	_playerTwoTotalTime = _gameSettings.TotalTime;

	_playerOneExtraTurnTime = _gameSettings.ExtraTime;
	_playerTwoExtraTurnTime = _gameSettings.ExtraTime;

	uiRefreshTime(_gameSettings.TotalTime);

	// 重置先手玩家
	_playerOneSide = PieceSide::BlackSide;
	_playerTwoSide = PieceSide::WhiteSide;
	
	// 提示框不可见
	_messageDialog->setVisible(false);
	
	// 设置面板不可用
	{
		size_t allSettingWidgetCount = _WidgetsettingsBoard->getChildrenCount();
		for(size_t i = 0; i < allSettingWidgetCount; i++)
		{
			Widget* w = static_cast<Widget*>(_WidgetsettingsBoard->getChildren().at(i));
			w->setTouchEnabled(false);
			w->setOpacity(150);
		}
	}

	// 重置棋子
	{
		Node* rootChild = _layout->getChildren().at(0);
		Widget* panelChessBoard = static_cast<Widget*>(rootChild->getChildByTag(UI_TAGID_MAIN_CHESSBOARD));

		_WidgetChessBoard = static_cast<ChessBoard*>(panelChessBoard->getChildren().at(0));
		_WidgetChessBoard->clearStones();
	}

	// 初始化游戏
	Game::getInstance()->init(_gameSettings);
	turnChange();

	if(!this->isScheduled(schedule_selector(GameLayer::updateTotalTime)))
	{
		this->schedule(schedule_selector(GameLayer::updateTotalTime),1.0f);
	}
}

void GameLayer::dealWithCustomEvent()
{
	_listener = EventListenerCustom::create("event_game_end",[=](EventCustom* event){
		endGame();
	});

	_listener2 = EventListenerCustom::create("event_turn_change",[=](EventCustom* event){
		turnChange();
		bool isPlayerTurn = Game::getInstance()->isPlayerTurn();

		//log("event_turn_change, currentside is %d",Game::getInstance()->getTurn());
		//log("isPlayerTurn = %i",isPlayerTurn);

		if(!isPlayerTurn) // 如果是电脑的回合
		{
			aiMove();
		}
	});

	_eventDispatcher->addEventListenerWithFixedPriority(_listener,1);
	_eventDispatcher->addEventListenerWithFixedPriority(_listener2, 1);

	_aiEvent = new EventCustom("event_ai_setStone");
}

void GameLayer::updateAIState(float dt)
{
	// 刷新AI状态
	if(!Game::getInstance()->isFinished() && aiResult != -1)
	{
		log("aiEvent send index = %d",aiResult);

		_aiEvent->setUserData(&aiResult);
		_eventDispatcher->dispatchEvent(_aiEvent);

		aiResult = -1;
	}

}

void GameLayer::updateTotalTime(float dt)
{
	if(Game::getInstance()->isFinished())
		return;

	if(Game::getInstance()->getTurn() == _playerOneSide)
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
				Game::getInstance()->setTimeout(_playerOneSide);
				endGame();
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
				Game::getInstance()->setTimeout(_playerTwoSide);
				endGame();
			}
		}
	}
}

// 交换回合
void GameLayer::turnChange()
{
	if(Game::getInstance()->getTurn() == _playerOneSide)
	{
		if(_playerOneTotalTime <= 0)
		{
			_playerOneExtraTurnTime = _gameSettings.ExtraTime;
			uiRefreshTime(_gameSettings.ExtraTime);
		}

		_ImageViewplayerOneColor->setVisible(true);
		_ImageViewplayerTwoColor->setVisible(false);
	}
	else if(Game::getInstance()->getTurn() == _playerTwoSide)
	{
		if(_playerTwoTotalTime <= 0)
		{
			_playerTwoExtraTurnTime = _gameSettings.ExtraTime;
			uiRefreshTime(_gameSettings.ExtraTime);
		}

		_ImageViewplayerOneColor->setVisible(false);
		_ImageViewplayerTwoColor->setVisible(true);
	}
	else
	{
		// 不在任何人的回合内
	}
}

void GameLayer::endGame()
{
	// 显示结果
	_messageDialog->setVisible(true);
	_messageDialog->showResult();

	//设置面板重新启用
	{
		size_t allSettingWidgetCount = _WidgetsettingsBoard->getChildrenCount();
		for(size_t i = 0; i < allSettingWidgetCount; i++)
		{
			Widget* w = static_cast<Widget*>(_WidgetsettingsBoard->getChildren().at(i));
			w->setTouchEnabled(true);
			w->setOpacity(255);
		}
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

void GameLayer::aiMove()
{
	pthread_mutex_init(&mutex,NULL);

	Game::getInstance()->copyData(&aiData);

	log("start ai working thread");
	pthread_create(&aiWordThreadID,NULL,&aiWorkThread,0);

}

void GameLayer::uiRefreshTime(int time)
{
	char buf[100] = {0};
	int minutes = time/60;
	int seconds = time%60;
	sprintf(buf,"%i:%i",minutes,seconds);

	if(Game::getInstance()->getTurn() == _playerOneSide)
	{
		_TextAtlasplayerOneTimeLabel->setStringValue(buf);
	}
	else if(Game::getInstance()->getTurn() == _playerTwoSide)
	{
		_TextAtlasplayerTwoTimeLabel->setStringValue(buf);
	}
	else
	{
		_TextAtlasplayerOneTimeLabel->setStringValue(buf);
		_TextAtlasplayerTwoTimeLabel->setStringValue(buf);
	}
}

void* GameLayer::aiWorkThread(void * arg)
{
	pthread_mutex_lock(&mutex);
#if WIN32
	Sleep(1000);
#else
	usleep(1000);
#endif

	for(int i = 0 ; i < 15 ; i ++)
	{
		for (int j=0;j<15;j++)
		{
			if(aiData[i][j] == 0)
			{
				log("Ai set Stone at (%d,%d)",i,j);
				aiResult = getIndexByRC(i,j);
				
				i = 100;
				j = 100;
			}
		}
	}
	pthread_mutex_unlock(&mutex);
	return NULL;
}

GomokuData GameLayer::aiData;
pthread_mutex_t GameLayer::mutex;
int GameLayer::aiResult = -1;
bool GameLayer::aiInTheWork = false;