#include "GameLayer.h"
#include "Resources.h"
#include "GameCore.h"
#include "LayerConfig.h"
#include "Particle.h"
#include "Animation.h"
#include <string>
USING_NS_CC;

static int _currentLevel = 0;

GameLayer::~GameLayer()
{
	delete _player;
	delete _monster;
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
    
    this->schedule(schedule_selector(GameLayer::update));

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
    _player = new Player("Player",100);
	_player->setImage(Resources::getInstance()->getPlayerImage());
    _player->setImageGrayScale(Resources::getInstance()->getPlayerImageGrayScale());
    _player->setAttackInterval(9);
    CoreGame::getInstance()->addPlayer(_player);

    // Create Monster
    _monster = new Monster("Monster",25);
	_monster->setImage(Resources::getInstance()->getMonsterImage("1"));
    _monster->setImageGrayScale(Resources::getInstance()->getMonsterImageGrayScale("1"));
    _monster->setAttackInterval(8);
    CoreGame::getInstance()->addMonster(_monster);
    
    _player->setPosition(100,_screenSize.height - 100);
    this->addChild(_player,GAMELAYERUIBOTTOMTAG);
    
    _monster->setPosition(_screenSize.width - 100 , _screenSize.height - 100);
    this->addChild(_monster,GAMELAYERUIBOTTOMTAG);

	initScreen();
}

void GameLayer::resetGame()
{
	_gameStarted = true;
    CoreGame::getInstance()->reset();

    _playerAttackAvailable = false;
    _monsterAttackAvailable = false;
    
    // 为玩家和怪物分别添加计时器
    this->schedule(schedule_selector(GameLayer::updatePlayerCoolDown),1.0);
    this->schedule(schedule_selector(GameLayer::updateMonsterCoolDown),1.0);
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

}

void GameLayer::update(float dt)
{
    if(CoreGame::getInstance()->getState() == CoreGameState::Finishing)
    {
        log("Game needs to end");
        
        endGame();
    }
}

void GameLayer::updatePlayerCoolDown(float dt)
{
    if(_playerAttackAvailable)
    {
        log("Waiting for player to attack");
    }
    else
    {
        if(_player->getCoolDown() == 0)
            _playerAttackAvailable = true;
        else
        {
            _player->updateCoolDown();
        }
    }
}

void GameLayer::updateMonsterCoolDown(float dt)
{
    if(_monsterAttackAvailable)
    {
        monsterAIAttack();
    }
    else
    {
        if(_monster->getCoolDown() ==0)
            _monsterAttackAvailable = true;
        else
        {
            _monster->updateCoolDown();
        }
    }
}

void GameLayer::monsterAIAttack()
{
    vector<int> keys = _emptyItems.keys();
    random_shuffle(keys.begin(),keys.end());
    int i = keys[0];
    
    MenuItemImage* cell = _emptyItems.at(i);
    cell->setScale(-0.83,0.83);
    
    Action* flipNumber;
    CKAnimation::getFlipNumberAnimation(1.0,CC_CALLBACK_0(GameLayer::callBack_ChangeImage,this,i),flipNumber);
    
	// flip number animation
	cell->runAction(flipNumber);
    
    // set selected number
    int n = atoi(_sudokuDataStruct.getAnswerAtIndex(i).c_str());
    CoreGame::getInstance()->monsterSelectNumber(n);
    
    // fire to monster
    firePlayer(cell->getPosition());
    
    // reset player cooldown
    _monsterAttackAvailable = false;
    _monster->refreshCoolDown();

}

void GameLayer::fireMonster(Point pos)
{
    FireBall* f = new FireBall();
    f->setPosition(pos - Point(_screenSize.width/2, _screenSize.height/2));
    
    Action* fireMonster;
    CKAnimation::getParticleMoveAnimation(1.5,_monster->getPosition(), CC_CALLBACK_0(GameLayer::callBack_FireMonsterEnd,this), fireMonster);
    
    this->addChild(f,GAMELAYERPARTICLETAG);
    f->runAction(fireMonster);
}

void GameLayer::firePlayer(Point pos)
{
    IceBall* ice = new IceBall();
    ice->setPosition(pos - Point(_screenSize.width/2, _screenSize.height/2));
    
    Action* firePlayer;
    CKAnimation::getParticleMoveAnimation(1.5, _player->getPosition(), CC_CALLBACK_0(GameLayer::callBack_FirePlayerEnd, this), firePlayer);
    this->addChild(ice,GAMELAYERPARTICLETAG);
    ice->runAction(firePlayer);
}

void GameLayer::endGame()
{
    unschedule(schedule_selector(GameLayer::updatePlayerCoolDown));
    unschedule(schedule_selector(GameLayer::updateMonsterCoolDown));
    
    CoreGame::getInstance()->end();
}

void GameLayer::callBack_FireMonsterEnd()
{
    CoreGame::getInstance()->playerAttack();
}

void GameLayer::callBack_FirePlayerEnd()
{
    CoreGame::getInstance()->monsterAttack();
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
    
    if(!_playerAttackAvailable)
    {
        //log("Can't attack now, waiting for cool down");
        return;
    }

	MenuItemImage* cell = _emptyItems.at(i);
    cell->setScale(-0.83,0.83);
    
    Action* flipNumber;
    CKAnimation::getFlipNumberAnimation(1.0,CC_CALLBACK_0(GameLayer::callBack_ChangeImage,this,i),flipNumber);

	// flip number animation
	cell->runAction(flipNumber);
    
    // set selected number
    int n = atoi(_sudokuDataStruct.getAnswerAtIndex(i).c_str());
    CoreGame::getInstance()->playerSelectNumber(n);
    
    // fire to monster
    fireMonster(cell->getPosition());
    
    // reset player cooldown
    _playerAttackAvailable = false;
    _player->refreshCoolDown();
}

void GameLayer::callBack_ChangeImage(int i)
{
	MenuItemImage* cell = _emptyItems.at(i);

	SpriteFrame* frame = SpriteFrameCache::getInstance()->getSpriteFrameByName(Resources::getInstance()->getNumberFrameName(_sudokuDataStruct.getAnswerAtIndex(i)));
	
    cell->setNormalSpriteFrame(frame);
    
    // disable menuItem
	cell->setEnabled(false);
    
    // remove cell from dict
    _emptyItems.erase(i);
}
