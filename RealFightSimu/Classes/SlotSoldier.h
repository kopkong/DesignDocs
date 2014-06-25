//
//  SlotSoldier.h
//  RealFightSimu
//
//  Created by �� ���� on 14-6-18.
//
//

#ifndef RealFightSimu_SlotSoldier_h
#define RealFightSimu_SlotSoldier_h

#include "Slot.h"

enum SOLDIERSLOTENUMRATION
{
	SOLDIERSLOTENUMRATION_CONFIGID,
	SOLDIERSLOTENUMRATION_SLOTINDEX,
	SOLDIERSLOTENUMRATION_LEVEL,
	SOLDIERSLOTENUMRATION_RANK,
	SOLDIERSLOTENUMRATION_BINDHEROSLOT,
	SOLDIERSLOTENUMRATION_ALL
};

class SlotSoldier:public Slot
{
private:
	// ʿ����ǰ�ĵȼ�
	int m_Level;

	int m_Rank;

	// ʿ��������佫SlotIndex
	SLOTINDEX m_BindHeroSlot;

public:
	SlotSoldier(std::string s):Slot((int)SOLDIERSLOTENUMRATION_ALL)
	{
		m_DataString = s;
		parserStringData();
	}

	virtual void parserStringData() override;

	virtual void init() override;

	virtual void update() override;

	virtual void updateSlotIndex(SLOTINDEX) override;

	void updateLevel(int level);

	void updateRank(int rank);

	// �����佫��
	void updateHeroBind(SLOTINDEX heorSlot);

	float computeHP();

	float computeATK();

	float computeDEF();
};

#endif