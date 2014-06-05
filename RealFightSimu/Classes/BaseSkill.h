#ifndef __BASE_SKILL_H__
#define __BASE_SKILL_H__

#include <string>
#include <vector>
#include "BaseSoldier.h"

class BaseSkill
{
private:
	std::string m_Name;

	// 冷却时间
	int m_CoolDown;

	// 技能的拥有者，由Own施放技能
	BaseSoldier* m_pOwner;

	// 技能的作用目标，可以是自己或者敌人
	std::vector<BaseSoldier*> m_pTargets;

	// 技能范围
	float m_Range;

	// 施放技能的位置
	cocos2d::Point m_Position;

	// 

public:

	// 决定技能施放的目标对象是谁
	virtual void setTargets() =0;

	// 执行技能，对目标造成伤害或其他影响
	virtual void execute() =0;

	// 完成技能施放，重置CD状态
	virtual void finish() = 0;
};

#endif