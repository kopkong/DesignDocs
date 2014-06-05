#ifndef __GAME_BASE_ENTITY_H__
#define __GAME_BASE_ENTITY_H__

#include "cocos2d.h"

class GameBaseEntity : public cocos2d::Node
{
private:
	// 每一个实体都必须有一个唯一的ID
	int m_ID;

	// 下一个有效ID，每次实例化时会更新这个值
	static int m_NextValidID;

	// 在构造函数中调用这个方法确保每次都能正确的设置ID
	void setID(int val);


public:
	GameBaseEntity(int id)
	{
		setID(id);
	}

	virtual ~GameBaseEntity(){}

	// 每一个实例必须自己实现update函数
	virtual void update() = 0;


	int ID() const {return m_ID;}
};

#endif