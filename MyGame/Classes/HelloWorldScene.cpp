#include "HelloWorldScene.h"
USING_NS_CC;

// Constant paras
const Size HEROSIZE(48,48);
const Size SOLDIERSIZE(32,32);

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

    // add background
    auto backGround = Sprite::create("grey.png");
    backGround->setPosition(visibleSize.width/2,visibleSize.height/2);
    this->addChild(backGround,0);
    
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

    /////////////////////////////
    // 3. add your codes below...
    
    // Create one squad
    for(int i = 0; i < 4; i++){
        char buff[100];
        sprintf(buff,"a%d",i);
        std::string name = buff;
        Point p(0 + random() % 200, 200 * i + random() % 250);
        log("Squad %s position is(%f,%f)", name.c_str(), p.x,p.y);
        Squad a = Squad(name,p);
        _squadListA.push_back(a);
    }
    
    drawAll();
    
    this->schedule(schedule_selector(HelloWorld::update));
    
    return true;
}

void HelloWorld::update(float dt){
    
    // Select target
    
    // Move squad
    
    // Attack
    
    
    
    // Action;
}


void HelloWorld::drawAll(){
    if(_squadListA.size() > 0){
        for(int i = 0; i < _squadListA.size(); i++)
        {
            drawSquad(_squadListA[i]);
        }
    }
    
    if(_squadListB.size() > 0){
        for(int i = 0; i < _squadListB.size(); i++)
        {
            drawSquad(_squadListB[i]);
        }
    }
    
}

void HelloWorld::drawSquad(Squad sq){
    // Texture rect
    auto hero = Sprite::create("hero.png");
    
    hero->setPosition(sq.getPosition());
    this->addChild(hero,1);
    
    for(unsigned int i = 1 ; i<= sq.getSoldiersCount();i++){
        auto soldier = Sprite::create("soldier.png");
        
        float xMin = sq.getPosition().x - 100;
        float yMin = sq.getPosition().y - 100;
        
        Point p(xMin + random()%100 ,yMin + random()%200 );
        soldier->setPosition(p);
        this->addChild(soldier,1);
    }
}


void HelloWorld::menuCloseCallback(Ref* pSender)
{
    Director::getInstance()->end();

#if (CC_TARGET_PLATFORM == CC_PLATFORM_IOS)
    exit(0);
#endif
}
