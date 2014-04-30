#include "HelloWorldScene.h"
#include "Resources.h"
USING_NS_CC;

Size CellSize(70,100);

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
    
    showFormationSelect();
    this->schedule(schedule_selector(HelloWorld::update));
    
    return true;
}

void HelloWorld::update(float dt){
    if(_inLeveltSelect)
        return;
    
    if(_inDisplayResult)
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
            _whichSideWin = SquadSide::TeamA;
        }
        if(Battle::getInstance()->sideWin(SquadSide::TeamB))
        {
            log("TeamB has won the battle!");
            Battle::getInstance()->endBattle();
            _whichSideWin = SquadSide::TeamB;
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
        showResults();
    }
    
}

void HelloWorld::initBattle()
{
	Battle::getInstance()->reset();

	Battle::getInstance()->initSquads(_leftFormation,_rightFormation);

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


void HelloWorld::showFormationSelect()
{
    _inFormationSelect = true;
    
    this->removeAllChildren();
    _menuItems.clear();
    
    auto board = Sprite::create(Resources::getInstance()->getFormationBoard());
	board->setPosition(_screenSize.width/2,_screenSize.height/2);
	this->addChild(board,0);
    
    int index = 0;
    for(int row = 0; row < 4; row ++)
    {
        for(int col =0 ; col < 5 ; col ++)
        {
            Label* label = Label::create();
            label->setString("空");
            label->setFontSize(48);
            label->setColor(Color3B(1.0,0.0,0.0));
        
            auto item = MenuItemLabel::create(label);
            
            Point p(_screenSize.width/2 - 105 - CellSize.width * col,
                    _screenSize.height  - 230 - CellSize.height * row);
            
            item->setPosition(p);
            item->setCallback(CC_CALLBACK_0(HelloWorld::menuItemCallback,this,index));
            
            _menuLabels.pushBack(label);
            _menuItems.pushBack(item);
            
            index ++;
        }
    }
    
    for(int row = 0; row < 4 ; row ++)
    {
        for(int col = 0 ; col < 5 ; col ++)
        {
            Label* label = Label::create();
            label->setString("空");
            label->setFontSize(48);
            label->setColor(Color3B(1.0,0.0,0.0));
            
            auto item = MenuItemLabel::create(label);
            
            Point p(_screenSize.width/2 + 105 + CellSize.width * col,
                    _screenSize.height  - 230 - CellSize.height * row);
            
            item->setPosition(p);
            item->setCallback(CC_CALLBACK_0(HelloWorld::menuItemCallback,this,index));
            
            _menuLabels.pushBack(label);
            _menuItems.pushBack(item);
            
            index ++;
        }
        
    }
    
    auto menu = Menu::createWithArray(_menuItems);
    menu->setPosition(Point::ZERO);
    this->addChild(menu,1,MenuTag::Level1);
    
    auto buttonItem1 = MenuItemImage::create(Resources::getInstance()->getStartBattleButton(),Resources::getInstance()->getStartBattleButton(),CC_CALLBACK_0(HelloWorld::menuStartBattle,this));
    buttonItem1->setPosition(_screenSize.width/2 - 60,_screenSize.height);
    auto menu2 = Menu::create(buttonItem1,NULL);
    menu2->setPosition(Point::ZERO);
    this->addChild(menu,1,MenuTag::Level1);
}

void HelloWorld::menuItemCallback(int index)
{
    this->removeChildByTag(MenuTag::Level2);
    if(index >=0 && index < _menuLabels.size())
    {
        _currentItemIndex = index;
        Point pos = _menuItems.at(index)->getPosition();
        
        // Show subItems
        std::string png0 = Resources::getInstance()->getMenuItem0();
        auto item0 = MenuItemImage::create(png0,png0);
        item0->setCallback(CC_CALLBACK_0(HelloWorld::menuSubItemCallback,this,0));
        item0->setPosition(pos.x + 50, pos.y + 60);
        
        std::string png1 = Resources::getInstance()->getMenuItem1();
        auto item1 = MenuItemImage::create(png1,png1,
            CC_CALLBACK_0(HelloWorld::menuSubItemCallback,this,1));
        item1->setPosition(pos.x + 65, pos.y + 20);
        
        std::string png2 = Resources::getInstance()->getMenuItem2();
        auto item2 = MenuItemImage::create(png2,png2,
            CC_CALLBACK_0(HelloWorld::menuSubItemCallback,this,2));
        item2->setPosition(pos.x + 65, pos.y - 20);
        
        std::string png3 = Resources::getInstance()->getMenuItem3();
        auto item3 = MenuItemImage::create(png3,png3,
            CC_CALLBACK_0(HelloWorld::menuSubItemCallback,this,3));
        item3->setPosition(pos.x + 50, pos.y - 60);
        
        auto menu = Menu::create(item0,item1,item2,item3,NULL);
        menu->setPosition(Point::ZERO);
        this->addChild(menu,1,MenuTag::Level2);
    }
    
}

void HelloWorld::menuSubItemCallback(int subIndex)
{
    if(_currentItemIndex >= 0)
    {
        std::string itemString[4] = {"空","步","骑","弓"};
        _menuLabels.at(_currentItemIndex)->setString(itemString[subIndex]);
    }
    
    _currentItemIndex = -1;
    
    // Clear sub items
    this->removeChildByTag(MenuTag::Level2);
}

void HelloWorld::showResults()
{
    _inDisplayResult = true;
    
    //this->removeAllChildren();
    
    std::string winMessages[2] = {"队伍1获得了胜利！","队伍2获得了胜利！"};
    
    auto label = LabelTTF::create(winMessages[_whichSideWin], "HeiTi", 40);
    label->setPosition(_screenSize.width/2, _screenSize.height/2);
    label->setColor(Color3B(1.0,0.0,0.0));
    this->addChild(label,1);
    
    auto item = MenuItemFont::create("返回选择界面", CC_CALLBACK_0(HelloWorld::menuBackToLevelSelectCallback,this));
    item->setPosition(_screenSize.width/2, _screenSize.height/2 - 200);
    auto menu = Menu::create(item,NULL);
    menu->setPosition(Point::ZERO);
    this->addChild(menu,1);
}

void HelloWorld::menuBackToLevelSelectCallback()
{
    _inDisplayResult = false;
    _inLeveltSelect = true;
    
    showFormationSelect();
}

void HelloWorld::menuStartBattle()
{
    // Constructe formations
    
}