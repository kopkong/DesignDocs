#include "GameLayer.h"
#include "Resources.h"
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
    display();
    return true;
}

void GameLayer::loadLevel(int level)
{
	SudokuFactory::getInstance()->setWorkingLevel(level);

	// generate the sudoku data 
	_sudokuDataStruct = SudokuFactory::getInstance()->generateSudoku();
}

void GameLayer::display()
{
    // display background
    auto bg = Sprite::create(Resources::getInstance()->getGameLayerBackGround());
    bg->setPosition(_screenSize.width/2,_screenSize.height/2);
    this->addChild(bg,0);
    
    // dislpay grid
    auto grid = Sprite::create(Resources::getInstance()->getGrid());
    grid->setPosition(_screenSize.width/2,_screenSize.height/2);
    grid->setScale(2.0);
    this->addChild(grid,1);
    
    float left = _screenSize.width/2 - grid->getContentSize().width + 20;
    float top = _screenSize.height/2 + grid->getContentSize().height - 20;
       
    for(int i = 0 ; i<9 ;i++)
    {
        for(int j = 0;j<9;j++)
        {
			string s ;
			s.insert(s.begin(),_sudokuDataStruct.Initials[9*i + j]);
            if(s.compare(".") != 0 )
            {
                Label* label = Label::createWithBMFont(Resources::getInstance()->getNumberFont(),s);
                label->setPosition(Point(left + j * 50 ,top - i * 50));
                this->addChild(label,2);
            }
            
        }
    }
    
}

void GameLayer::update(float dt)
{

}
