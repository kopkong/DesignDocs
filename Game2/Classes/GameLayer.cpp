#include "GameLayer.h"
#include "Resources.h"
#include "GameCore.h"
#include "LayerConfig.h"
#include <string>
USING_NS_CC;

static int _currentLevel = 0;

GameLayer::~GameLayer()
{
	delete _player;
	delete _monster;
	_roundInfo->release();
	_countDownMsg->release();
	_gameBatchNode->release();
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
    
    _screenSize = Director::getInstance()->getVisibleSize();

	loadLevel(_currentLevel);

    return true;
}

void GameLayer::loadLevel(int level)
{
	SudokuFactory::getInstance()->setWorkingLevel(level);

	// generate the sudoku data 
	_sudokuDataStruct = SudokuFactory::getInstance()->generateSudoku();

	// set start is false
	_gameStarted = false;

	// Create Player
    _player = new Player("KLK",100);
	_player->setImage(Resources::getInstance()->getPlayerImage());
    CoreGame::getInstance()->addPlayer(_player);
    
    // Create Monster
    _monster = new Monster("Monster",25);
	_monster->setImage(Resources::getInstance()->getMonsterImage("1"));
    CoreGame::getInstance()->addMonster(_monster);

	initScreen();
}

void GameLayer::resetGame()
{
	_gameStarted = true;
    CoreGame::getInstance()->reset();
        
    _countDown = 9;
    
    this->schedule(schedule_selector(GameLayer::updateInRound),1.0);
}

void GameLayer::displayRound()
{
    __String* s1 = __String::createWithFormat("Round %d",CoreGame::getInstance()->getCurrentRound());
    _roundInfo->setString(s1->getCString());
    
    __String* s2 = __String::createWithFormat("%d",_countDown);
    log("Count down %d",_countDown);
    _countDownMsg->setString(s2->getCString());
}

void GameLayer::displayCharacters()
{
	// display player
	auto playerIcon = Sprite::create(_player->getImage());
	playerIcon->setPosition(100,_screenSize.height - 100);
	this->addChild(playerIcon,GAMELAYERUITOPTAG);

	// display monster
	auto monsterIcon = Sprite::create(_monster->getImage());
	monsterIcon->setPosition(_screenSize.width - 100 , _screenSize.height - 100);
	this->addChild(monsterIcon,GAMELAYERUITOPTAG);

	// Init Player HP bar 
	// first show the bottom
	auto playerHPBarBottom = Sprite::create(Resources::getInstance()->getHPBar2());
	Point posPlayerHPbar(playerIcon->getPositionX() ,playerIcon->getPositionY() - playerIcon->getContentSize().height/2);
	playerHPBarBottom->setPosition(posPlayerHPbar);
	this->addChild(playerHPBarBottom,GAMELAYERUIBOTTOMTAG);

	_playerHPbar = Sprite::create(Resources::getInstance()->getHPBar1());
	_playerHPbar->setPosition(posPlayerHPbar);
	this->addChild(_playerHPbar,GAMELAYERUITOPTAG);

	// Init Monster HP bar here
	auto monsterHPBarBottom = Sprite::create(Resources::getInstance()->getHPBar2());
	Point posMonsterHPbar(monsterIcon->getPositionX() ,monsterIcon->getPositionY() - monsterIcon->getContentSize().height/2);
	monsterHPBarBottom->setPosition(posMonsterHPbar);
	this->addChild(monsterHPBarBottom,GAMELAYERUIBOTTOMTAG);

	_monsterHPbar = Sprite::create(Resources::getInstance()->getHPBar1());
	_monsterHPbar->setPosition(posMonsterHPbar);
	this->addChild(_monsterHPbar,GAMELAYERUITOPTAG);
}

void GameLayer::displayHP()
{


}

void GameLayer::initLabelUI()
{
	 // Create label
    _roundInfo = Label::create();
    _roundInfo->setPosition(_screenSize.width/2 - 50, _screenSize.height - 50);
    _roundInfo->setString(ROUNDSTARTSTRING);
    this->addChild(_roundInfo,GAMELAYERUITOPTAG);
    
    // Create CountDown Message
    _countDownMsg = Label::createWithBMFont(Resources::getInstance()->getNumberFont(),"9");
    _countDownMsg->setPosition(_screenSize.width/2 +50 , _screenSize.height - 50);
    this->addChild(_countDownMsg,GAMELAYERUITOPTAG);

}

void GameLayer::initScreen()
{
    // display background
    auto bg = Sprite::create(Resources::getInstance()->getGameLayerBackGround());
    bg->setPosition(_screenSize.width/2,_screenSize.height/2);
    this->addChild(bg,0);
        
    float left = 250;
    float top = _screenSize.height - 150;
    
    SpriteFrameCache::getInstance()->addSpriteFramesWithFile(Resources::getInstance()->getNumbersPlist());
    //_gameBatchNode = SpriteBatchNode::create(Resources::getInstance()->getNumbersImage());
    //this->addChild(_gameBatchNode,GAMELAYERNUMBERTAG);
    
    for(int i = 0 ; i<9 ;i++)
    {
        for(int j = 0;j<9;j++)
        {
			string s ;
			s.insert(s.begin(),_sudokuDataStruct.Initials[9 * i + j]);
            MenuItemImage* item = MenuItemImage::create();
            
            float x = left + 60*j;
            float y = top - 60*i;
            
            if(i >= 3)
                y -= 5;
            
            if(i >= 6)
                y -= 5;
            
            if(j >=3)
                x += 5;
            
            if(j>=6)
                x+= 5;
            
            if(s.compare(".") != 0 )
            {
                SpriteFrame* frame = SpriteFrameCache::getInstance()->getSpriteFrameByName(Resources::getInstance()->getNumberFrameName(s));
                item->setNormalSpriteFrame(frame);
            }
            else
            {
                SpriteFrame* frame = SpriteFrameCache::getInstance()->getSpriteFrameByName(Resources::getInstance()->getEmptyFrameName());
                item->setNormalSpriteFrame(frame);
				item->setCallback(CC_CALLBACK_0(GameLayer::callBack_SelectNumber,this,i*9 + j));
				_emptyItems.insert(i * 9 + j,item);
            }
            
            item->setScale(0.83);
            item->setPosition(x,y);
            _menuItems.pushBack(item);
        }
    }
     
    // Create start button
    auto menuItem = MenuItemImage::create(Resources::getInstance()->getStartBattleButton(),Resources::getInstance()->getStartBattleButton(),CC_CALLBACK_0(GameLayer::callBack_StartGame,this));
    menuItem->setPosition(_screenSize.width/2, 50);
    _menuItems.pushBack(menuItem);
    
	auto menu = Menu::createWithArray(_menuItems);
    menu->setPosition(Point::ZERO);
    this->addChild(menu,GAMELAYERUITOPTAG);

	initLabelUI();
	displayCharacters();
}

void GameLayer::update(float dt)
{

}

void GameLayer::updateInRound(float dt)
{
    _countDown--;
    displayRound();
    
    if(_countDown ==0)
        roundCountDownOver();
}

void GameLayer::roundCountDownOver()
{
    CoreGame::getInstance()->monsterSelectNumber(0);
    CoreGame::getInstance()->playerSelectNumber(0);
    CoreGame::getInstance()->roundEnd();
    
    // reset count down
    _countDown = 10;
}

void GameLayer::callBack_StartGame()
{
    resetGame();
}

void GameLayer::callBack_SelectNumber(int i)
{
	//log("Player select cell index %d, the number is %s",i,_sudokuDataStruct.getAnswerAtIndex(i).c_str());
	if(! _gameStarted)
	{
		log("Game is not started yet!");
		return;
	}

	MenuItemImage* cell = _emptyItems.at(i);
    
	OrbitCamera *orbit = OrbitCamera::create(2,0.5,0,0,180,0,0);
	DelayTime *delay = DelayTime::create(1);
	FiniteTimeAction *sequence = Sequence::create(delay,
		CallFuncN::create(CC_CALLBACK_0(GameLayer::callBack_ChangeImage,this,i))
		,NULL);
	FiniteTimeAction *spawn = Spawn::create(orbit,sequence,NULL);

	// Play Rotate Z animation
	cell->runAction(spawn);

	// disable menuItme
	cell->setEnabled(false);
}

void GameLayer::callBack_ChangeImage(int i)
{
	MenuItemImage* cell = _emptyItems.at(i);

	SpriteFrame* frame = SpriteFrameCache::getInstance()->getSpriteFrameByName(Resources::getInstance()->getNumberFrameName(_sudokuDataStruct.getAnswerAtIndex(i)));
	
	cell->setScale(-0.83,0.83);	cell->setNormalSpriteFrame(frame);

}
