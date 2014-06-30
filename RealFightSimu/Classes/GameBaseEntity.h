#ifndef __GAME_BASE_ENTITY_H__
#define __GAME_BASE_ENTITY_H__

#include "cocos2d.h"

class GameBaseEntity : public cocos2d::Node
{
private:
	// 唯一的ID
	int m_ID;

	// 下一个有效的ID
	static int m_NextValidID;

	// 设置ID
	void setID(int val);

public:
	GameBaseEntity(int id)
	{
		setID(id);
	}

	virtual ~GameBaseEntity(){}

	// 每一个子类都要实现这个函数
	virtual void update() = 0;

	int ID() const {return m_ID;}
};

#endif