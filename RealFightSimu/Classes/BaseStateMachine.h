#ifndef __BASE_STATE_MACHINE_H__
#define __BASE_STATE_MACHINE_H__

#include "BaseState.h"

template<class entity_type>
class BaseStateMachine
{
private:

	// StateMachine的拥有者
	entity_type*	m_pOwner;

	// Owner的状态
	BaseState<entity_type>* m_pCurrentState;

	// 上一个状态
	BaseState<entity_type>* m_pPreviousState;

	// 全局状态，响应所有更新事件
	BaseState<entity_type>* m_pGlobalState;


public:
	BaseStateMachine(entity_type* owner):m_pOwner(owner),
		m_pCurrentState(NULL),m_pPreviousState(NULL),
		m_pGlobalState(NULL)
	{}

	virtual ~BaseStateMachine(){}

	void setCurrentState(BaseState<entity_type>* s){m_pCurrentState = s;}

	void setPreviousState(BaseState<entity_type>* s){m_pPreviousState = s;}

	void setGlobalState(BaseState<entity_type>* s){m_pGlobalState =s;}

	void update()
	{
		if(m_pGlobalState)
			m_pGlobalState->execute(m_pOwner);

		if(m_pCurrentState)
			m_pCurrentState->execute(m_pOwner);
	}

	void changeState(BaseState<entity_type>* pNewState)
	{
		CC_ASSERT(pNewState && "State is NULL");

		m_pPreviousState = m_pCurrentState;

		m_pCurrentState->exit(m_pOwner);

		m_pCurrentState = pNewState;

		m_pCurrentState->enter(m_pOwner);

	};

	void revertToPrevState()
	{
		changeState(m_pPreviousState);
	}

	bool isInState(const BaseState<entity_type>& st)const
	{
		return typeid(*m_pCurrentState) = typeid(st);
	}

	BaseState<entity_type>* CurrentState() const{return m_pCurrentState;}
	BaseState<entity_type>* GlobalState() const{return m_pGlobalState;}
	BaseState<entity_type>* PreviousState() const{return m_pPreviousState;}
};


#endif