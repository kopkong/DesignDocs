/***
	�����ڽ�������ʾAI�ڸ�ʲô,
	�Լ��ͽ���Ԫ�ؽ���.
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