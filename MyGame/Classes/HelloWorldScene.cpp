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
    
    Size visibleSize = Director::getInstance()->getVisibleSize();
    Point origin = Director::getInstance()->getVisibleOrigin();
    _screenSize = Size(1024,768);
    
    /////////////////////////////
    // 2. add a menu item with "X" image, which is clicked to quit the program
    //    you may modify it.

    // add a "close" icon to exit the progress. it's an autorelease object
    auto closeItem = MenuItemImage::create(
                                           "CloseNormal.png",
                                           "CloseSelected.png",
                                           CC_CALLBACK_1(HelloWorld::menuCloseCallback, this));
    
	closeItem->setPosition(Point(origin.x + visibleSize.width - closeItem->getContentSize().width/2 ,
                                origin.y + closeItem->getContentSize().height/2));

    // create menu, it's an autorelease object
    auto menu = Menu::create(closeItem, NULL);
    menu->setPosition(Point::ZERO);
    this->addChild(menu, 1);
    
    initBattle();

    this->schedule(schedule_selector(HelloWorld::update));
    
    return true;
}

void HelloWorld::update(float dt){
    if(Battle::getInstance()->battleFinished())
        return;

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

			//if(sq->getState() != SquadState::Eliminated)
			//	log("Squad[%s] state is %d", sq->getName().c_str(),sq->getState());

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

void HelloWorld::initBattle()
{
	Battle::getInstance()->reset();

	Battle::getInstance()->initSquads(9,9);

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
                           
void HelloWorld::menuCloseCallback(Ref* pSender)
{
    Director::getInstance()->end();
	Battle::getInstance()->end();

#if (CC_TARGET_PLATFORM == CC_PLATFORM_IOS)
    exit(0);
#endif
}
