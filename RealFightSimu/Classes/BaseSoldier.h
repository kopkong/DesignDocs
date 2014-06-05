#ifndef __BASE_SOLDIER_H__
#define __BASE_SOLDIER_H__

#include "GameBaseEntity.h"

class BaseSoldierState;

class BaseSoldier : public GameBaseEntity
{
private:

	// ������״̬
	BaseSoldierState* m_pCurrentState;

	// ��ʾ�õ�2D��Դ
	cocos2d::Sprite* m_pTexture;

	// �����糡���е�λ��
	cocos2d::Point m_Position;

	// HP ���� 
	int m_MaxHP;

	// ��ǰHP
	int m_CurrentHP;

	// ������
	int m_ATK;

	// ������
	int m_DEF;

	// �ƶ��ٶ�
	float m_MoveSpeed;

	// �����ٶ�
	float m_AtkSpeed;

	// ������Χ
	float m_AtkRange;


public:
	BaseSoldier();

	// ����onEnter,��addChild��ʱ����øú���
	virtual void onEnter() override;
};

#endif