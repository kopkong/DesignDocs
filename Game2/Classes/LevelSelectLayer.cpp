//
//  LevelSelectLayer.cpp
//  Game2
//
//  Created by 孔 令锴 on 14-5-17.
//
//

#include "LevelSelectLayer.h"
#include "Resources.h"
#include "GameLayer.h"

Scene* LevelSelectLayer::createScene()
{
	// 'scene' is an autorelease object
	auto scene = Scene::create();

	// 'layer' is an autorelease object
	auto layer = LevelSelectLayer::create();

	// add layer as a child to scene
	scene->addChild(layer);

	// return the scene
	return scene;
}

// on "init" you need to initialize your instance
bool LevelSelectLayer::init()
{
	//////////////////////////////
	// 1. super init first
	if ( !Layer::init() )
	{
		return false;
	}

	Size visibleSize = Director::getInstance()->getVisibleSize();

	// BackGround
	auto bg = Sprite::create(Resources::getInstance()->getMenuLayerBackGround());
	bg->setPosition(visibleSize.width/2, visibleSize.height/2);
	this->addChild(bg,0);

	// Add Three Levels
	auto buttonLevel1 = MenuItemImage::create(Resources::getInstance()->getLevelButton1(),Resources::getInstance()->getLevelButton1(),CC_CALLBACK_0(LevelSelectLayer::enterGame,this,1));
	buttonLevel1->setPosition(visibleSize.width/2, visibleSize.height/2 + 100);
	buttonLevel1->setCallback(CC_CALLBACK_0(LevelSelectLayer::enterGame,this,1));

	auto buttonLevel2 = MenuItemImage::create(Resources::getInstance()->getLevelButton2(),Resources::getInstance()->getLevelButton2(),CC_CALLBACK_0(LevelSelectLayer::enterGame,this,2));
	buttonLevel2->setPosition(visibleSize.width/2, visibleSize.height/2);
	buttonLevel2->setCallback(CC_CALLBACK_0(LevelSelectLayer::enterGame,this,2));

	auto buttonLevel3 = MenuItemImage::create(Resources::getInstance()->getLevelButton3(),Resources::getInstance()->getLevelButton3(),CC_CALLBACK_0(LevelSelectLayer::enterGame,this,3));
	buttonLevel3->setPosition(visibleSize.width/2, visibleSize.height/2 -100);
	buttonLevel3->setCallback(CC_CALLBACK_0(LevelSelectLayer::enterGame,this,3));

	auto menu = Menu::create(buttonLevel1,buttonLevel2,buttonLevel3,NULL);
	menu->setPosition(Point::ZERO);
	this->addChild(menu,1);


	return true;
}

void LevelSelectLayer::enterGame(int level)
{
	Scene* newScene = TransitionMoveInR::create(0.2f,GameLayer::createScene(level));
	Director::getInstance()->replaceScene(newScene);
}

void LevelSelectLayer::backToMenu()
{

}

void LevelSelectLayer::update(float dt)
{

}