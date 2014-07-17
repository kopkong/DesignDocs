#include "HelloWorldScene.h"
#include "CCTexture2D.h"
#include "Resources.h"
USING_NS_CC;

Size CellSize(80,100);

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
	_inFormationSelect = false;
	_inDisplayResult = false;
    
    showFormationSelect();
    this->schedule(schedule_selector(HelloWorld::update));
    
    return true;
}

void HelloWorld::update(float dt){
    if(_inFormationSelect)
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
            _whichSideWin = SquadSide::TeamA;
            _teamAWins ++;
            Battle::getInstance()->endBattle();
        }
        if(Battle::getInstance()->sideWin(SquadSide::TeamB))
        {
            log("TeamB has won the battle!");
            _whichSideWin = SquadSide::TeamB;
            _teamBWins ++;
            Battle::getInstance()->endBattle();
        }
        
        for(unsigned int i = 0; i < squadNumbers; i++)
        {
            Squad* sq = Battle::getInstance()->getSquadByIndex(i);

            switch(sq->getState()){
                case SquadState::Moving:
					{
						Battle::getInstance()->wholeSquadMove(sq,dt * _speedUpRate);
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
						Battle::getInstance()->wholeSquadFight(sq,dt * _speedUpRate);
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

	if(_leftSquads <=0 || _rightSquads <=0)
	{
		log("There is no squads to Fight!");
		return;
	}

	Battle::getInstance()->initSquads(_leftFormation,_rightFormation);

    this->removeAllChildren();
    
	// add background
	//auto backGround = Sprite::create("bg.png");
	//backGround->setPosition(_screenSize.width/2,_screenSize.height/2);
	//this->addChild(backGround,0);

	Sprite* pRepeatTex = Sprite::create(Resources::getInstance()->getBackground());
	pRepeatTex->setAnchorPoint(Point::ZERO);
	
	Texture2D::TexParams params={
		GL_LINEAR,//minFilter纹理缩小过滤器
		GL_LINEAR,//magFilter纹理放大过滤器
		GL_REPEAT,//wrapS横向纹理寻址模式
		GL_REPEAT //wrapT纵向纹理寻址模式
	};
	pRepeatTex->getTexture()->setTexParameters(params);
	// 设置平铺的大小
	pRepeatTex->setTextureRect(CCRectMake(0,0,64*16,64*12));
	this->addChild(pRepeatTex,0);

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
	_menuLabels.clear();
    
    auto board = Sprite::create(Resources::getInstance()->getFormationBoard());
	board->setPosition(_screenSize.width/2,_screenSize.height/2);
	this->addChild(board,0);
    
    int index = 0;
    for(int row = 0; row < 4; row ++)
    {
        for(int col =0 ; col < 5 ; col ++)
        {
            LabelTTF* label = LabelTTF::create();
			setFormationLabelText(SquadSide::TeamA,row,col,label);

            auto item = MenuItemLabel::create(label);
            
            Point p(_screenSize.width/2 - 90 - CellSize.width * col,
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
            LabelTTF* label = LabelTTF::create();
            setFormationLabelText(SquadSide::TeamB,row,col,label);
            auto item = MenuItemLabel::create(label);
            
            Point p(_screenSize.width/2 + 90 + CellSize.width * col,
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
    
	// Start battle button
    auto buttonItem1 = MenuItemImage::create(Resources::getInstance()->getStartBattleButton(),Resources::getInstance()->getStartBattleButton(),CC_CALLBACK_0(HelloWorld::menuStartBattle,this,1));
    buttonItem1->setPosition(_screenSize.width/2 - 100 ,100);

    // Start battle 10 times button
    auto buttonItem2 = MenuItemImage::create(Resources::getInstance()->getStartBattleButton2(),Resources::getInstance()->getStartBattleButton2(),CC_CALLBACK_0(HelloWorld::menuStartBattle,this,10));
    buttonItem2->setPosition(_screenSize.width/2 + 100 , 100);

	// Random formation for player
	auto buttonItem3 = MenuItemImage::create(Resources::getInstance()->getRandomPlayerFormationButton(),Resources::getInstance()->getRandomPlayerFormationButton(),
		CC_CALLBACK_0(HelloWorld::menuRandomFormation,this,true));
	buttonItem3->setPosition(_screenSize.width/2 - 250, _screenSize.height - 140);

	auto buttonItem4 = MenuItemImage::create(Resources::getInstance()->getRandomNPCFormationButton(),Resources::getInstance()->getRandomNPCFormationButton(),
		CC_CALLBACK_0(HelloWorld::menuRandomFormation,this,false));
	buttonItem4->setPosition(_screenSize.width/2 + 250, _screenSize.height - 140);
    
    auto menu2 = Menu::create(buttonItem1,buttonItem2,buttonItem3,buttonItem4,NULL);
    
    menu2->setPosition(Point::ZERO);
    this->addChild(menu2,1,MenuTag::Level1);
    
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
        item0->setCallback(CC_CALLBACK_0(HelloWorld::menuSubItemCallback,this,SquadType::None));
        item0->setPosition(pos.x + 50, pos.y + 60);
        
        std::string png1 = Resources::getInstance()->getMenuItem1();
        auto item1 = MenuItemImage::create(png1,png1,
            CC_CALLBACK_0(HelloWorld::menuSubItemCallback,this,SquadType::Footman));
        item1->setPosition(pos.x + 65, pos.y + 20);
        
        std::string png2 = Resources::getInstance()->getMenuItem2();
        auto item2 = MenuItemImage::create(png2,png2,
            CC_CALLBACK_0(HelloWorld::menuSubItemCallback,this,SquadType::Knight));
        item2->setPosition(pos.x + 65, pos.y - 20);
        
        std::string png3 = Resources::getInstance()->getMenuItem3();
        auto item3 = MenuItemImage::create(png3,png3,
            CC_CALLBACK_0(HelloWorld::menuSubItemCallback,this,SquadType::Archer));
        item3->setPosition(pos.x + 50, pos.y - 60);
        
        auto menu = Menu::create(item0,item1,item2,item3,NULL);
        menu->setPosition(Point::ZERO);
        this->addChild(menu,1,MenuTag::Level2);
    }
    
}

void HelloWorld::menuSubItemCallback(SquadType type)
{
    if(_currentItemIndex >= 0)
    {
        _menuLabels.at(_currentItemIndex)->setString(Resources::getInstance()->getStringSquadType(type));
    }
    
    _currentItemIndex = -1;
    
    // Clear sub items
    this->removeChildByTag(MenuTag::Level2);
}

void HelloWorld::showResults()
{
    _inDisplayResult = true;
    
    //this->removeAllChildren();
    char buffer[100];
    
    //std::string winMessages[2] = {"队伍1获得了胜利！","队伍2获得了胜利！"};
    sprintf(buffer,"已进行 %d 次战斗，玩家胜利 %d 次",_testTimes,_teamAWins);
    std::string playerTeamMessage = buffer;
    
    auto label = LabelTTF::create(playerTeamMessage, Resources::getInstance()->getFontName(), 40);
    label->setPosition(_screenSize.width/2, _screenSize.height/2 + 100);
    label->setColor(Color3B(1.0,0.0,0.0));
    this->addChild(label,1);
    
    sprintf(buffer,"已进行 %d 次战斗，NPC胜利 %d 次",_testTimes,_teamBWins);
    std::string npcTeamMessage = buffer;
    
    auto label2 = LabelTTF::create(npcTeamMessage,Resources::getInstance()->getFontName(),40);
    label2->setPosition(_screenSize.width/2, _screenSize.height/2);
    label2->setColor(Color3B(1.0,0.0,0.0));
    this->addChild(label2,1);
    
    auto item = MenuItemFont::create("返回选择界面", CC_CALLBACK_0(HelloWorld::menuBackToLevelSelectCallback,this));
    item->setPosition(_screenSize.width/2, _screenSize.height/2 - 200);
	item->setColor(Color3B(1.0,0.0,0.0));
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

void HelloWorld::menuStartBattle(int battleTimes)
{
	// Left side
	_leftSquads = 0;
	_rightSquads = 0;
    _teamAWins = 0;
    _teamBWins = 0;
    _speedUpRate = 1.0f;
	int index = 0;

	// Left Team
	for(int row = 0; row < 4 ; row ++)
	{
		for(int col = 0 ; col < 5 ; col ++)
		{
			std::string text = _menuLabels.at(index)->getString();
			if(text.compare(Resources::getInstance()->getStringSquadType(SquadType::None)) != 0)
			{
				_leftSquads ++;
				
				if(text.compare(Resources::getInstance()->getStringSquadType(SquadType::Footman)) == 0)
				{
					_leftFormation[row][col] = SquadType::Footman;
				}

				if(text.compare(Resources::getInstance()->getStringSquadType(SquadType::Knight)) == 0)
				{
					_leftFormation[row][col] = SquadType::Knight;
				}

				if(text.compare(Resources::getInstance()->getStringSquadType(SquadType::Archer)) == 0)
				{
					_leftFormation[row][col] = SquadType::Archer;
				}
			}
			else
			{
				_leftFormation[row][col] = SquadType::None;
			}

			index ++;
		}
	}


	// Right Team
	for(int row = 0; row < 4 ; row ++)
	{
		for(int col = 0 ; col < 5 ; col ++)
		{
			std::string text = _menuLabels.at(index)->getString();
			if(text.compare(Resources::getInstance()->getStringSquadType(SquadType::None)) != 0)
			{
				_rightSquads ++;

				if(text.compare(Resources::getInstance()->getStringSquadType(SquadType::Footman)) == 0)
				{
					_rightFormation[row][col] = SquadType::Footman;
				}

				if(text.compare(Resources::getInstance()->getStringSquadType(SquadType::Knight)) == 0)
				{
					_rightFormation[row][col] = SquadType::Knight;
				}

				if(text.compare(Resources::getInstance()->getStringSquadType(SquadType::Archer)) == 0)
				{
					_rightFormation[row][col] = SquadType::Archer;
				}
			}
			else
			{
				_rightFormation[row][col] = SquadType::None;
			}

			index ++;
		}
	}

	_testTimes = 0;
	_allTestTimes = battleTimes;
    
    // If fight times more than one, then speed up
    if(_allTestTimes > 1)
        _speedUpRate = 4.0f;
    
	_inFormationSelect = false;
	resetTest();
}

void HelloWorld::menuRandomFormation(bool isPlayer)
{
	int start  = 0;
	int end = 20;

	if(isPlayer)
	{
		// Label index is 0 - 19
	}
	else
	{
		// Label index is 20 -39
		start = 20;
		end = 39;
	}

	for(int i = start; i < end; i++)
	{
		int a = (int)(CCRANDOM_0_1() * 999999);
		int r =  a % 3 + 1;
		_menuLabels.at(i)->setString(Resources::getInstance()->getStringSquadType((SquadType)r));
	}

}

void HelloWorld::setFormationLabelText(SquadSide side,int row,int col,LabelTTF* label)
{
	SquadType typeSquad = SquadType::None;
	if(side == SquadSide::TeamA && _leftFormation[row][col] >0 )
	{
		typeSquad = _leftFormation[row][col];
	}

	if(side == SquadSide::TeamB && _rightFormation[row][col] > 0)
	{
		typeSquad = _rightFormation[row][col];
	}

	label->setString(Resources::getInstance()->getStringSquadType(typeSquad));
	label->setFontSize(24);
	label->setFontName(Resources::getInstance()->getFontName());
	label->setColor(Color3B(1.0,0.0,0.0));
}