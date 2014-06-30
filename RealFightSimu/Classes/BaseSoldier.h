#ifndef __BASE_SOLDIER_H__
#define __BASE_SOLDIER_H__

#include "GameBaseEntity.h"
#include "BaseStateMachine.h"
#include "BaseSoldierState.h"

//class BaseSoldier : public GameBaseEntity
//{
//private:
//
//	// 基本的状态机
//	BaseStateMachine<BaseSoldier>* m_pStateMachine;
//
//	// 显示用的2D资源
//	cocos2d::Sprite* m_pTexture;
//
//	// 在世界场景中的位置
//	cocos2d::Point m_Position;
//
//	// HP 上限 
//	int m_MaxHP;
//
//	// 当前HP
//	int m_CurrentHP;
//
//	// 攻击力
//	int m_ATK;
//
//	// 防御力
//	int m_DEF;
//
//	// 移动速度
//	float m_MoveSpeed;
//
//	// 攻击速度
//	float m_AtkSpeed;
//
//	// 攻击范围
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
//	// 重载onEnter,在addChild的时候调用该函数
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