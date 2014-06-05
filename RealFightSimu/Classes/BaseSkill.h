#ifndef __BASE_SKILL_H__
#define __BASE_SKILL_H__

#include <string>
#include <vector>
#include "BaseSoldier.h"

class BaseSkill
{
private:
	std::string m_Name;

	// ��ȴʱ��
	int m_CoolDown;

	// ���ܵ�ӵ���ߣ���Ownʩ�ż���
	BaseSoldier* m_pOwner;

	// ���ܵ�����Ŀ�꣬�������Լ����ߵ���
	std::vector<BaseSoldier*> m_pTargets;

	// ���ܷ�Χ
	float m_Range;

	// ʩ�ż��ܵ�λ��
	cocos2d::Point m_Position;

	// 

public:

	// ��������ʩ�ŵ�Ŀ�������˭
	virtual void setTargets() =0;

	// ִ�м��ܣ���Ŀ������˺�������Ӱ��
	virtual void execute() =0;

	// ��ɼ���ʩ�ţ�����CD״̬
	virtual void finish() = 0;
};

#endif