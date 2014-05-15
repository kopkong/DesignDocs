#ifndef __MyGame__Unit__
#define __MyGame__Unit__

#include "cocos2d.h"
#include "UnitState.h"
USING_NS_CC;


struct UnitData
{
	CC_SYNTHESIZE(int, _index, Index); // Unit Id in one battle
	CC_SYNTHESIZE(int, _healthPoint, HealthPoint);
	CC_SYNTHESIZE(int, _targetIndex, TargetIndex);
	CC_SYNTHESIZE(float, _attackInterval,AttackInterval);
	CC_SYNTHESIZE(float, _timeSinceLastAttack,ElapsedTimeSinceLastAttack);
	CC_SYNTHESIZE(int, _doAttackTimes, DoAttackTimes);
	CC_SYNTHESIZE(int, _tryAttackTimes, TryAttackTimes);
};

class Unit
{
public:
    Unit();
	Unit(UnitState* state);
    ~Unit();

    Unit(int,int,float);

private :
	friend class UnitState;
	void changeState(UnitState* state);
	UnitState* m_State;
	UnitData m_UnitData;
};

#endif /* #define __MyGame__Unit__ */