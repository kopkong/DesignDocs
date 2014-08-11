#ifndef __HOVER_H
#define __HOVER_H

#include "public.h"

typedef enum tagHoverState
{
	kHoverStateGrabbed,
	kHoverStateUngrabbed
} HoverState;

class Hover :public Sprite
{
	HoverState _state;

public:
	Hover();
	~Hover();

	bool initWithSpriteFrameName(const std::string);
	virtual void onEnter() override;
	virtual void onExit() override;

	bool onTouchBegan(Touch*,Event*);
	void onTouchMoved(Touch*,Event*);
	void onTouchEnded(Touch*,Event*);

	static Hover* createWithSpriteFrameName(const std::string);
};
#endif