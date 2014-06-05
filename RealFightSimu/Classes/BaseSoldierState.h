#ifndef __BASE_SOLDIER_STATE_H__
#define __BASE_SOLDIER_STATE_H__

#include "BaseSoldierState.h"

class Soldier;

class JustEnterBattleField: public BaseState<Soldier>
{
private:
	JustEnterBattleField(){}

	JustEnterBattleField(const JustEnterBattleField&);
	JustEnterBattleField& operator=(const JustEnterBattleField&);

public:

	 

};

#endif