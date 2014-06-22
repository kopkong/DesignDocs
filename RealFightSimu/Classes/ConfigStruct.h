//
//  ConfigStruct.h
//  RealFightSimu
//
//  Created by їЧ БопЗ on 14-6-18.
//
//

#ifndef __RealFightSimu__ConfigStruct__
#define __RealFightSimu__ConfigStruct__

#include <string>

struct StructHeroConfig
{
	int ID;
	std::string Name;
	std::string IconID;
	std::string ActionID;
	std::string Color;
	std::string Description;
	std::string Star;
	int Type;
	int InitialHP;
	int InitialATK;
	int InitialDEF;
	int DodgeRate;
	int CriticalRate;
	int AttackSpeed;
	int AttackRange;
	std::string SkillID;
	std::string TalentID;
	int GrowthHP;
	int GrowthATK;
	int GrowthDEF;
	float RankUpRateHP;
	float RankUpRateATK;
	float RankUpRateDEF;
	std::string SpecialArmor;
	int Souls;

	static StructHeroConfig getEmptyInstance()
	{
		StructHeroConfig c;
		c.ID = 0;
		return c;
	}
};

#endif