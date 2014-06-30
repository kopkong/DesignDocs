#ifndef __BASE_SOLDIER_H__
#define __BASE_SOLDIER_H__

#include "GameBaseEntity.h"
#include "BaseStateMachine.h"
#include "BaseSoldierState.h"

//class BaseSoldier : public GameBaseEntity
//{
//private:
//
//	// ������״̬��
//	BaseStateMachine<BaseSoldier>* m_pStateMachine;
//
//	// ��ʾ�õ�2D��Դ
//	cocos2d::Sprite* m_pTexture;
//
//	// �����糡���е�λ��
//	cocos2d::Point m_Position;
//
//	// HP ���� 
//	int m_MaxHP;
//
//	// ��ǰHP
//	int m_CurrentHP;
//
//	// ������
//	int m_ATK;
//
//	// ������
//	int m_DEF;
//
//	// �ƶ��ٶ�
//	float m_MoveSpeed;
//
//	// �����ٶ�
//	float m_AtkSpeed;
//
//	// ������Χ
//	float m_AtkRange;
//
//
//public:
//	BaseSoldier(int id):GameBaseEntity(id)
//	{
//		m_pStateMachine = new BaseStateMachine<BaseSoldier>(this);
//
//		//m_pStateMachine->setCurrentState
//
//	};
//
//	//BaseStateMachine<BaseSoldier>* getFSM() {return m_pStateMachine;}
//
//	void update();
//
//	// ����onEnter,��addChild��ʱ����øú���
//	//void onEnter() override;
//};

class BaseSoldier : public GameBaseEntity
{
private:
	cocos2d::Sprite* m_pTexture;
	BaseStateMachine<BaseSoldier>* m_pStateMachine;

public:
	BaseSoldier(int id):GameBaseEntity(id)
	{
		m_pStateMachine = new BaseStateMachine<BaseSoldier>(this);

		//m_pStateMachine->setCurrentState(

	};
	void update();

};

#endif