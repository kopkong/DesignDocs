#include "GameLayer.h"
#include "Resources.h"

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
//    auto grid = Sprite::create(Resources::getInstance()->getGrid());
//    grid->setPosition(_screenSize.width/2,_screenSize.height/2);
//    grid->setScale(2.0);
//    this->addChild(grid,1);
    
    //float left = _screenSize.width/2 - grid->getContentSize().width + 20;
    //float top = _screenSize.height/2 + grid->getContentSize().height - 20;
    

    //for(int i=0;i<3;i++)
    //{
    //    for(int j=0;j<3;j++)
    //    {
            Rect r1(0,0,30,35);
            Sprite* numberOne = Sprite::create("font1.png",r1);
            numberOne->setPosition(_screenSize.width/2,_screenSize.height/2);
            this->addChild(numberOne,1);
    //    }
    
    //}

//    char table[81];
//    strcpy(table, _sudokuDataStruct.Initials.c_str());
//    
//    for(int i = 0 ; i<9 ;i++)
//    {
//        for(int j = 0;j<9;j++)
//        {
//            char str = table[i*9 + j];
//            char* pStr = &str;
//            if(strcmp(pStr,".") != 0)
//            {
//                Label* label = Label::createWithBMFont("font.fnt",pStr);
//                label->setPosition(Point(left + j * 40 ,top - i * 40));
//                this->addChild(label,2);
//            }
//            
//            pStr = NULL;
//        }
//    }
    
}

void GameLayer::update(float dt)
{

}
