#ifndef __START_LAYER_H
#define __START_LAYER_H

#include "Rule.h"
#include "public.h"
#include "CocosGUI.h"
#include "cocostudio/CocoStudio.h"

USING_NS_CC;
using namespace cocos2d::ui;

class StartLayer : public cocos2d::Layer
{
private:
	Layout* _layout;

	// 所有UI按钮的touch处理事件
	void uiButtonTouchCallback(Ref* obj,TouchEventType eventType);

protected:
	~StartLayer();
    
public:
    static cocos2d::Scene* createScene();

	CREATE_FUNC(StartLayer);

    virtual bool init();

	void initUI();
};

#endif