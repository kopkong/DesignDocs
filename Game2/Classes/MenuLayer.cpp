//
//  MenuLayer.cpp
//  Game2
//
//  Created by 孔 令锴 on 14-5-17.
//
//

#include "MenuLayer.h"
#include "Resources.h"

Scene* MenuLayer::createScene()
{
    // 'scene' is an autorelease object
    auto scene = Scene::create();
    
    // 'layer' is an autorelease object
    auto layer = MenuLayer::create();
    
    // add layer as a child to scene
    scene->addChild(layer);
    
    // return the scene
    return scene;
}

// on "init" you need to initialize your instance
bool MenuLayer::init()
{
    //////////////////////////////
    // 1. super init first
    if ( !Layer::init() )
    {
        return false;
    }
    
    Size visibleSize = Director::getInstance()->getVisibleSize();
    //Point origin = Director::getInstance()->getVisibleOrigin();
    
    // BackGround
    auto bg = Sprite::create(Resources::getInstance()->getMenuLayerBackGround());
    bg->setAnchorPoint(Point::ZERO);
    bg->setPosition(visibleSize.width/2, visibleSize.height/2);
    
    // Add Three buttons
    auto buttonLevelSelect = MenuItemImage::create(Resources::getInstance()->getMenuButton1(),Resources::getInstance()->getMenuButton1(),CC_CALLBACK_0(MenuLayer::showLeveles,this));

    auto buttonRecords = MenuItemImage::create(Resources::getInstance()->getMenuButton2(),Resources::getInstance()->getMenuButton2(),CC_CALLBACK_0(MenuLayer::showRecords,this));
    
    auto buttonHelp = MenuItemImage::create(Resources::getInstance()->getMenuButton1(),Resources::getInstance()->getMenuButton1(),CC_CALLBACK_0(MenuLayer::showHelp,this));
    
    
    buttonLevelSelect->setPosition(visibleSize.width / 2.0, visibleSize.height / 2.0 + 200);
    buttonRecords->setPosition(visibleSize.width / 2.0, visibleSize.height / 2.0);
    buttonHelp->setPosition(visibleSize.width / 2.0, visibleSize.height / 2.0 - 200);
    
    
    auto menu = Menu::create(buttonLevelSelect,buttonRecords,buttonHelp,NULL);
    menu->setPosition(Point::ZERO);
    this->addChild(menu,1);
}

void MenuLayer::showLeveles()
{
    
}

void MenuLayer::showRecords()
{
    
}

void MenuLayer::showHelp()
{
    
}