#include "HelloWorldScene.h"
USING_NS_CC;

Scene* HelloWorld::createScene()
{
    // 'scene' is an autorelease object
    auto scene = Scene::create();
    
    // 'layer' is an autorelease object
    auto layer = HelloWorld::create();

    // add layer as a child to scene
    scene->addChild(layer);

    // return the scene
    return scene;
}

// on "init" you need to initialize your instance
bool HelloWorld::init()
{
    //////////////////////////////
    // 1. super init first
    if ( !Layer::init() )
    {
        return false;
    }
    
    _screenSize = Size(1024,768);
    
    showLevelSelect();
    this->schedule(schedule_selector(HelloWorld::update));
    
    return true;
}

void HelloWorld::showLevelSelect()
{
    _inLeveltSelect = true;
    _level = 0;
    this->removeAllChildren();
    
    // Display level selector
    auto itemLevel1 = MenuItemFont::create("Level 1",CC_CALLBACK_0(HelloWorld::menuLevel1Callback,this));
    itemLevel1->setPosition(100,_screenSize.height - 100);
    
    auto itemLevel2 = MenuItemFont::create("选择难度 2",CC_CALLBACK_0(HelloWorld::menuLevel2Callback,this));
    itemLevel2->setPosition(100, _screenSize.height - 200);
    
    auto itemLevel3 = MenuItemFont::create("选择难度 3",CC_CALLBACK_0(HelloWorld::menuLevel3Callback,this));
    itemLevel3->setPosition(100, _screenSize.height - 300);
    
    // create menu, it's an autorelease object
    auto menu = Menu::create(itemLevel1, NULL);
    menu->setPosition(Point::ZERO);
    this->addChild(menu, 1);
    
}

void HelloWorld::update(float dt){
    if(_inLeveltSelect)
        return;
    
    if(Battle::getInstance()->battleFinished())
    {
        resetTest();
        return;
    }

	unsigned int squadNumbers = Battle::getInstance()->getSquadNumbersInBattle();
	if( squadNumbers > 0){
        
        // Check battle finished or not
        if(Battle::getInstance()->sideWin(SquadSide::TeamA))
        {
            log("TeamA has won the battle!");
            Battle::getInstance()->endBattle();
        }
        if(Battle::getInstance()->sideWin(SquadSide::TeamB))
        {
            log("TeamB has won the battle!");
            Battle::getInstance()->endBattle();
        }
        
        for(unsigned int i = 0; i < squadNumbers; i++)
        {
            Squad* sq = Battle::getInstance()->getSquadByIndex(i);

            switch(sq->getState()){
                case SquadState::Moving:
					{
						Battle::getInstance()->wholeSquadMove(sq,dt);
						break;
					}
                case SquadState::BattleBegin:
					{
						Battle::getInstance()->startBattle(sq);
						break;
					}
				case SquadState::Wait:
					{
						Battle::getInstance()->wholeSquadWaiting(sq);
						break;
					}
                case SquadState::Fighting:
					{
						Battle::getInstance()->wholeSquadFight(sq,dt);
						break;
					}
				case SquadState::Eliminated:
					{
						Battle::getInstance()->clearSquad(sq);
						break;
					}
                default:
                    break;
            }
        }
    }
}

void HelloWorld::resetTest()
{
    if(_testTimes < _allTestTimes)
    {
        _testTimes ++;
        initBattle();
    }
    else
    {
        // Do more things here,
        // Print the output on the screen , etc.
        showLevelSelect();
    }
    
}

void HelloWorld::initBattle()
{
	Battle::getInstance()->reset();

	Battle::getInstance()->initSquads(_leftSideSquads,_rightSideSquads);

    this->removeAllChildren();
    
	// add background
	auto backGround = Sprite::create("bg.png");
	backGround->setPosition(_screenSize.width/2,_screenSize.height/2);
	this->addChild(backGround,0);

	// Add all sprites
	std::map<int,Unit> units = Battle::getInstance()->getAllUnits();

	for(std::map<int,Unit>::iterator it = units.begin();
		it !=  units.end(); ++it )
	{
		Sprite * s = it->second.getSprite();
		this->addChild(s,1);
	}
}

void HelloWorld::menuLevel1Callback()
{
    if(_level == 1) // 4 v 4
    {
        _testTimes = 0 ;
        _allTestTimes = 1;
        _inLeveltSelect = false;
        _leftSideSquads = 4;
        _rightSideSquads = 4;
        resetTest();
    }
}

void HelloWorld::menuLevel2Callback()
{
    if(_level == 2) // 8 v 8
    {
        _testTimes = 0;
        _allTestTimes = 1;
        _inLeveltSelect = false;
        _leftSideSquads = 8;
        _rightSideSquads = 8;
        resetTest();
    }
}

void HelloWorld::menuLevel3Callback()
{
    if(_level == 3) // 20 v 20
    {
        _testTimes = 0;
        _allTestTimes = 1;
        _inLeveltSelect = false;
        _leftSideSquads = 20;
        _rightSideSquads = 20;
        resetTest();
    }
}

    
void HelloWorld::menuCloseCallback(Ref* pSender)
{
    Director::getInstance()->end();
	Battle::getInstance()->end();

#if (CC_TARGET_PLATFORM == CC_PLATFORM_IOS)
    exit(0);
#endif
}
