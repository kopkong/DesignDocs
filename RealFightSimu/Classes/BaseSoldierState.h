#ifndef __BASE_SOLDIER_STATE_H__
#define __BASE_SOLDIER_STATE_H__

#include "BaseState.h"
#include "BaseSoldier.h"

class JustEnterBattleField: public BaseState<BaseSoldier>
{
private:
	JustEnterBattleField(){};

	JustEnterBattleField(const JustEnterBattleField&);
	JustEnterBattleField& operator=(const JustEnterBattleField&);
public:

	static JustEnterBattleField* Instance();

	virtual void enter(BaseSoldier*);

	virtual void execute(BaseSoldier*);

	virtual void exit(BaseSoldier*);
};

#endif