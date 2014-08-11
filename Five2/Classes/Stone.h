#ifndef __STONE_H_
#define __STONE_H_

#include "public.h"

class Stone:public Sprite
{
public:
	Stone();
	~Stone();

	bool initWithSpriteFrameName(const std::string);
	virtual void onEnter() override;
	virtual void onExit() override;

	static Stone* createWithSpriteFrameName(const std::string);
};

#endif