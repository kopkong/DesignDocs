#include "GameLayer.h"
#include "Resources.h"
#include "GameCore.h"
#include "LayerConfig.h"
#include <string>
USING_NS_CC;

static int _currentLevel = 0;

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
    initScreen();
    
    return true;
}

void GameLayer::loadLevel(int level)
{
	SudokuFactory::getInstance()->setWorkingLevel(level);

	// generate the sudoku data 
	_sudokuDataStruct = SudokuFactory::getInstance()->generateSudoku();
}

void GameLayer::resetGame()
{
    CoreGame::getInstance()->reset();
    
    // Create Player
    Player p("KLK",100);
    CoreGame::getInstance()->addPlayer(&p);
    
    // Create Monster
    Monster m("Monster",25);
    CoreGame::getInstance()->addMonster(&m);
    
    _countDown = 9;
    
    this->schedule(schedule_selector(GameLayer::updateInRound),1.0);
}

void GameLayer::displayRound()
{
    __String* s1 = __String::createWithFormat("第%d回合",CoreGame::getInstance()->getCurrentRound());
    _roundInfo->setString(s1->getCString());
    
    __String* s2 = __String::createWithFormat("%d",_countDown);
    log("Count down %d",_countDown);
    _countDownMsg->setString(s2->getCString());
}

void GameLayer::initScreen()
{
    // display background
    auto bg = Sprite::create(Resources::getInstance()->getGameLayerBackGround());
    bg->setPosition(_screenSize.width/2,_screenSize.height/2);
    this->addChild(bg,0);
    
    // dislpay grid
    //auto grid = Sprite::create(Resources::getInstance()->getGrid());
    //grid->setPosition(_screenSize.width/2,_screenSize.height/2);
    //grid->setScale(2.0);
    //this->addChild(grid,1);
    
    //float left = _screenSize.width/2 - grid->getContentSize().width + 20;
    //float top = _screenSize.height/2 + grid->getContentSize().height - 20;
    
    float left = 450;
    float top = _screenSize.height - 150;
    
    SpriteFrameCache::getInstance()->addSpriteFramesWithFile(Resources::getInstance()->getNumbersPlist());
    _gameBatchNode = SpriteBatchNode::create(Resources::getInstance()->getNumbersImage());
    this->addChild(_gameBatchNode,GAMELAYERNUMBERTAG);
    
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
            }
            
            item->setScale(0.83);
            item->setPosition(x,y);
            _menuItems.pushBack(item);
        }
    }
    
    // Create label
    _roundInfo = Label::create();
    _roundInfo->setPosition(100,_screenSize.height -50);
    _roundInfo->setString(ROUNDSTARTSTRING);
    this->addChild(_roundInfo,GAMELAYERUITAG);
    
    // Create CountDown Message
    _countDownMsg = Label::createWithBMFont(Resources::getInstance()->getNumberFont(),"9");
    _countDownMsg->setPosition(100,_screenSize.height - 100);
    this->addChild(_countDownMsg,GAMELAYERUITAG);
 
    // Create start button
    auto menuItem = MenuItemImage::create(Resources::getInstance()->getStartBattleButton(),Resources::getInstance()->getStartBattleButton(),CC_CALLBACK_0(GameLayer::buttonDown_Start,this));
    menuItem->setPosition(_screenSize.width/2, _screenSize.height-50);
    _menuItems.pushBack(menuItem);
    
    auto menu = Menu::createWithArray(_menuItems);
    menu->setPosition(Point::ZERO);
    this->addChild(menu,GAMELAYERUITAG);
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

void GameLayer::buttonDown_Start()
{
    resetGame();
}
