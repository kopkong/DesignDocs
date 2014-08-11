#include "Hover.h"

Hover::Hover()
{

}

Hover::~Hover()
{

}

Hover* Hover::createWithSpriteFrameName(const std::string frameName)
{
	Hover* pHover = new Hover();
	pHover->initWithSpriteFrameName(frameName);
	pHover->autorelease();

	return pHover;
}


bool Hover::initWithSpriteFrameName(const std::string frameName)
{
	if(Sprite::initWithSpriteFrameName(frameName))
	{
		_state = kHoverStateUngrabbed;
	}

	return true;
}

void Hover::onEnter()
{
	Sprite::onEnter();

	auto listener = EventListenerTouchOneByOne::create();
	listener->setSwallowTouches(true);

	listener->onTouchBegan = CC_CALLBACK_2(Hover::onTouchBegan,this);
	listener->onTouchMoved = CC_CALLBACK_2(Hover::onTouchMoved,this);
	listener->onTouchEnded = CC_CALLBACK_2(Hover::onTouchEnded,this);

	_eventDispatcher->addEventListenerWithSceneGraphPriority(listener,this);
}

void Hover::onExit()
{
	Sprite::onExit();
}

bool Hover::onTouchBegan(Touch* touch, Event* event)
{
	log("Hover::onTouchBegan id = %d, x = %f, y = %f", touch->getID(), touch->getLocation().x, touch->getLocation().y);

	if(_state!= kHoverStateUngrabbed) return false;
	
	_state = kHoverStateGrabbed;
	return true;
}

void Hover::onTouchMoved(Touch* touch, Event* event)
{
	
}

void Hover::onTouchEnded(Touch* touch, Event* event)
{
	_state = kHoverStateUngrabbed;
}