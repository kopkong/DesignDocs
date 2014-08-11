/***
	用来在界面上显示AI在干什么,
	以及和界面元素交互.
***/

#ifndef __AIPLAYER_H_
#define __AIPLAYER_H_

#include "public.h"

class AIPlayer :public Sprite
{
public:
	AIPlayer();
	~AIPlayer();

	bool initWithSpriteFrameName(const std::string);
	virtual void onEnter() override;
	virtual void onExit() override;

	static AIPlayer* createWithSpriteFrameName(const std::string);
	static AIPlayer* create();



};


#endif