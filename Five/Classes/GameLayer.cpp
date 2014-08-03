#include "GameLayer.h"
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

	_whoseTurn = TurnOwner::PlayerOne;

	_totalTimeOption = TotalTimeOption::_15M;
	_extraTimeOption = ExtraTimeOption::_1M;

	_gameSettings.TotalTime = TOTALTIME_SECONDS[_totalTimeOption];
	_gameSettings.ExtraTime = EXTRATIME_SECONDS[_extraTimeOption];
	_gameSettings.hasForbidden = false;
}

void GameLayer::initUI()
{
	// load UI
	Layout* layout = static_cast<Layout*>(
		cocostudio::GUIReader::getInstance()->widgetFromJsonFile(UI_LAYTOU_MAIN.c_str())); 
    
    _screenSize = Director::getInstance()->getVisibleSize();
	Size rootSize = layout->getSize();
	this->addChild(layout);
	Node* rootChild = layout->getChildren().at(0);

	// 
	{
		_WidgetdialogMessage = static_cast<Widget*>(rootChild->getChildByTag(24));

		// Add a Label
		_resultText = Sprite::createWithSpriteFrame(SpriteFrameCache::getInstance()->getSpriteFrameByName("playerOneWin"));
		_resultText->setPosition(135,45);
		_WidgetdialogMessage->addChild(_resultText);

		_WidgetdialogMessage->setZOrder(2);
		//_WidgetdialogMessage->setVisible(false);
	}

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

	// Add 15 * 15 buttons on chess board
	{
		Widget* chessBoardWidget = static_cast<Widget*>(rootChild->getChildByTag(10));
		Point leftTopCorner = Point(chessBoardWidget->getPosition().x - chessBoardWidget->getSize().width / 2, 
			chessBoardWidget->getPosition().y + chessBoardWidget->getSize().height / 2);

		Point pieceOffset(34,34);
		Size cellSize(50,50);

		int index = 0;
		for(int row = 0 ; row < 15; row ++)
		{
			for(int column = 0 ; column < 15; column ++)
			{
				MenuItemImage* cell = MenuItemImage::create();
				float positionX = leftTopCorner.x + column * cellSize.width + pieceOffset.x;
				float positionY = leftTopCorner.y - row * cellSize.height - pieceOffset.y;
				cell->setPosition(positionX,positionY);
				cell->setCallback(CC_CALLBACK_0(GameLayer::cellTouchCallback,this,index));
				cell->setNormalSpriteFrame(SpriteFrameCache::getInstance()->getSpriteFrameByName("transparentPiece"));

				_vectorPieces.pushBack(cell);
				index ++;
			}
		}

		auto menu2 = Menu::createWithArray(_vectorPieces);
		menu2->setPosition(Point::ZERO);
		this->addChild(menu2,1);
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

}

void GameLayer::initTexture()
{
	SpriteFrameCache* frameCache = SpriteFrameCache::getInstance();

	SpriteFrame* blackNormalSprite = SpriteFrame::create(UI_PIECES,Rect(0,0,66,66));
	SpriteFrame* blackSelectedSprite = SpriteFrame::create(UI_PIECES,Rect(0,66,66,66));
	SpriteFrame* whiteNormalSprite = SpriteFrame::create(UI_PIECES,Rect(66,0,66,66));
	SpriteFrame* whiteSelectedSprite = SpriteFrame::create(UI_PIECES,Rect(66,66,66,66));
	SpriteFrame* transparentSprite = SpriteFrame::create(UI_TRANSPARENTPIECE,Rect(0,0,50,50));

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

	frameCache->addSpriteFrame(playerOneWin,"playerOneWin");
	frameCache->addSpriteFrame(playerTwoWin,"playerTwoWin");
	frameCache->addSpriteFrame(playerOneForbidden,"playerOneForbidden");
	frameCache->addSpriteFrame(playerOneTimeout,"playerOneTimeout");
	frameCache->addSpriteFrame(playerTwoTimeout,"playerTwoTimeout");

	frameCache->addSpriteFrame(blackNormalSprite,"blackPieceNormal");
	frameCache->addSpriteFrame(blackSelectedSprite,"blackPieceSelected");
	frameCache->addSpriteFrame(whiteNormalSprite,"whitePieceNormal");
	frameCache->addSpriteFrame(whiteSelectedSprite,"whitePieceSelected");
	frameCache->addSpriteFrame(transparentSprite,"transparentPiece");

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

	this->schedule(schedule_selector(GameLayer::updateTotalTime),1.0f);
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
				timeoutLose(TurnOwner::PlayerOne);
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
				timeoutLose(TurnOwner::PlayerTwo);
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

void GameLayer::winGame(TurnOwner owner)
{
	_gameRunning = false;

	_WidgetdialogMessage->setVisible(true);

	if(owner == TurnOwner::PlayerOne)
		_resultText->setSpriteFrame("playerOneWin");
	else
		_resultText->setSpriteFrame("playerTwoWin");

	//log("Player %d win the game!",owner);
}

void GameLayer::forbiddenLose(TurnOwner owner)
{
	_gameRunning = false;

	_WidgetdialogMessage->setVisible(true);

	_resultText->setSpriteFrame("playerOneForbidden");
}

void GameLayer::timeoutLose(TurnOwner owner)
{
	_gameRunning = false;

	_WidgetdialogMessage->setVisible(true);

	if(owner == TurnOwner::PlayerOne)
		_resultText->setSpriteFrame("playerOneTimeout");
	else
		_resultText->setSpriteFrame("playerTwoTimeout");

	//log("Player %d lose the game!",owner);
}

void GameLayer::cellTouchCallback(int index)
{
	if(!_gameRunning)
		return;

	log("Touch cell %d",index);
	if(index >=0 && index < _vectorPieces.size())
	{
		int row = index /15;
		int column = index % 15;
		log("Row: %d, Column: %d",row,column);

		MenuItemImage* cell = static_cast<MenuItemImage*>(_vectorPieces.at(index));

		if(_whoseTurn == TurnOwner::PlayerOne)
		{
			cell->setNormalSpriteFrame(SpriteFrameCache::getInstance()->getSpriteFrameByName("blackPieceNormal"));
			cell->setEnabled(false);

			Rule::getInstance()->setData(row,column,_playerOneSide);

			if(Rule::getInstance()->isFinished())
			{
				if(Rule::getInstance()->getWinner() == _playerOneSide)
					winGame(TurnOwner::PlayerOne);
				else
					forbiddenLose(TurnOwner::PlayerOne);
			}
		}
		else
		{
			cell->setNormalSpriteFrame(SpriteFrameCache::getInstance()->getSpriteFrameByName("whitePieceNormal"));
			cell->setEnabled(false);

			Rule::getInstance()->setData(row,column,_playerTwoSide);

			if(Rule::getInstance()->isFinished())
			{
				if(Rule::getInstance()->getWinner() == _playerTwoSide)
					winGame(TurnOwner::PlayerTwo);
				else
					forbiddenLose(TurnOwner::PlayerTwo);

			}
		}

		changeTurn();
		
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
