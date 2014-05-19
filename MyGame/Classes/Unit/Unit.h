#ifndef __MyGame__Unit__
#define __MyGame__Unit__

#include "cocos2d.h"
#include "UnitState.h"
USING_NS_CC;


struct UnitData
{

};

class Unit
{
public:
    CC_SYNTHESIZE(int, _index, Index); // Unit Id in one battle
	CC_SYNTHESIZE(int, _healthPoint, HealthPoint);
	CC_SYNTHESIZE(int, _targetIndex, TargetIndex);
	CC_SYNTHESIZE(float, _attackInterval,AttackInterval);
	CC_SYNTHESIZE(float, _timeSinceLastAttack,ElapsedTimeSinceLastAttack);
	CC_SYNTHESIZE(int, _doAttackTimes, DoAttackTimes);
	CC_SYNTHESIZE(int, _tryAttackTimes, TryAttackTimes);
    CC_SYNTHESIZE(Sprite*,_sprite,Sprite);
    
    Unit();
    ~Unit();

    Unit(int,int,float);
};

#endif /* #define __MyGame__Unit__ */