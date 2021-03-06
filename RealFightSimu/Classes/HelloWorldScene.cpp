#include "HelloWorldScene.h"
#include "Player.h"
#include "SlotMgr.h"
#include "CocosGUI.h"
#include "cocostudio/CocoStudio.h"

#include "AssertConfigs.h"
#include "ConfigDataMgr.h"
#include "ConfigDataMgr.h"
#include "DatabaseHelper.h"
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

	cocos2d::ui::Layout* _layout = static_cast<cocos2d::ui::Layout*>(
        cocostudio::GUIReader::getInstance()->widgetFromJsonFile(UI_LAYTOU_MAIN.c_str()));
	Size screenSize = CCDirector::getInstance()->getWinSize();
	Size rootSize = _layout->getSize();
	this->setPosition(Point((screenSize.width - rootSize.width) / 2,
		(screenSize.height - rootSize.height) / 2));
	this->addChild(_layout);

	ConfigDataMgr::Instance().initAllConfigs();
	DataBaseHelper::Instance().initDataBase();
	DataBaseHelper::Instance().addNewPlayer("�������");

	PlayerDataMgr::Instance().initPlayerData();
	PlayerDataMgr::Instance().savePlayerData();
    
    SlotsMgr::Instance().initPlayerSlots();
    SlotsMgr::Instance().savePlayerSlots();

    return true;
}


void HelloWorld::menuCloseCallback(Ref* pSender)
{
	

#if (CC_TARGET_PLATFORM == CC_PLATFORM_WP8) || (CC_TARGET_PLATFORM == CC_PLATFORM_WINRT)
	MessageBox("You pressed the close button. Windows Store Apps do not implement a close button.","Alert");
    return;
#endif

    Director::getInstance()->end();

#if (CC_TARGET_PLATFORM == CC_PLATFORM_IOS)
    exit(0);
#endif
}
