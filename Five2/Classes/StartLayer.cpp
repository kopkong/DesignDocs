#include "StartLayer.h"
#include "AssertConfigs.h"
#include "GameLayer.h"

StartLayer::~StartLayer()
{

}

Scene* StartLayer::createScene()
{
    // 'scene' is an autorelease object
    auto scene = Scene::create();
    
    // 'layer' is an autorelease object
    auto layer = StartLayer::create();

    // add layer as a child to scene
    scene->addChild(layer);

    // return the scene
    return scene;
}

// on "init" you need to initialize your instance
bool StartLayer::init()
{
    //////////////////////////////
    // 1. super init first
    if ( !Layer::init() )
    {
        return false;
    }

	initUI();

    return true;
}

void StartLayer::initUI()
{
	_layout = static_cast<Layout*>(
		cocostudio::GUIReader::getInstance()->widgetFromJsonFile(UI_LAYOUT_STARTPAGE.c_str())); 
    
	Node* rootChild = _layout->getChildren().at(0);

	// 添加监听事件
	{
		Button* button1 = static_cast<Button*>(rootChild->getChildByTag(UI_TAGID_STARTPAGE_AIBUTTON));
		Button* button2 = static_cast<Button*>(rootChild->getChildByTag(UI_TAGID_STARTPAGE_PVPBUTTON));

		button1->addTouchEventListener(this,toucheventselector(StartLayer::uiButtonTouchCallback));
		button2->addTouchEventListener(this,toucheventselector(StartLayer::uiButtonTouchCallback));
	}

	this->addChild(_layout);
}


void StartLayer::uiButtonTouchCallback(Ref* obj,TouchEventType eventType)
{
	auto button = dynamic_cast<Widget*>(obj);
	int tag = button->getTag();

	switch(eventType)
	{
	case TouchEventType::TOUCH_EVENT_ENDED:
		{
			if(tag == UI_TAGID_STARTPAGE_AIBUTTON)
			{
				auto scene = GameLayer::createScene(1);
				Director::getInstance()->replaceScene(scene);
			}

			if(tag == UI_TAGID_STARTPAGE_PVPBUTTON)
			{
				auto scene = GameLayer::createScene(0);
				Director::getInstance()->replaceScene(scene);
			}
			break;
		}
	default:
		break;
	}

}