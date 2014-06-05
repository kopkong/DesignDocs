#ifndef __GAME_BASE_ENTITY_H__
#define __GAME_BASE_ENTITY_H__

#include "cocos2d.h"

class GameBaseEntity : public cocos2d::Node
{
private:
	// ÿһ��ʵ�嶼������һ��Ψһ��ID
	int m_ID;

	// ��һ����ЧID��ÿ��ʵ����ʱ��������ֵ
	static int m_NextValidID;

	// �ڹ��캯���е����������ȷ��ÿ�ζ�����ȷ������ID
	void setID(int val);


public:
	GameBaseEntity(int id)
	{
		setID(id);
	}

	virtual ~GameBaseEntity(){}

	// ÿһ��ʵ�������Լ�ʵ��update����
	virtual void update() = 0;


	int ID() const {return m_ID;}
};

#endif