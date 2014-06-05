#ifndef __NORMAL_ATTACK_H__
#define __NORMAL_ATTACK_H__

#include "BaseSkill.h"

class NormalAttack: public BaseSkill
{

public:
	NormalAttack(BaseSoldier* p){
		m_Name = "NormalAttack";
		m_pOwner = p;
	}
	~NormalAttack();

	virtual void setTargets();

	virtual void execute();

	virtual void finish();
};

#endif