//
//  SlotSoldier.h
//  RealFightSimu
//
//  Created by 孔 令锴 on 14-6-18.
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
	// 士兵当前的等级
	int m_Level;

	int m_Rank;

	// 士兵跟随的武将SlotIndex
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

	// 更新武将绑定
	void updateHeroBind(SLOTINDEX heorSlot);

	float computeHP();

	float computeATK();

	float computeDEF();
};

#endif