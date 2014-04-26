#ifndef __MyGame__Battle__
#define __MyGame__Battle__

#include <iostream>
#include "cocos2d.h"
#include "Squad.h"
#include "Unit.h"

USING_NS_CC;

class Battle
{
private:
	const static int NONETARGET = -1;

	std::vector<Squad> _allSquads;
	std::map<int,Unit> _allUnits;

	bool _battleFinished;
	Size _battleFieldSize;
	Size _squadSize;
	Size _heroSize;
	Size _soldierSize;

	void initSquadProperty(Squad*,SquadType);
	void initSquadSprite(Squad*);

	void pickTarget(int unitID,Squad*, Squad*);
	Point move(int unitID,Squad*,float dt);
	void attackTarget(int,Squad*,Squad*,float);
	void doAttack(int unitID, int targetID,Squad*,float);
	bool targetSquadEliminated(Squad*);

	void printStats();

protected:
	Battle(void);
	~Battle();

public:
	static Battle * getInstance();

	// Reset everything for one battle
	virtual void reset();
	
	virtual void initSquads(int leftSquads, int rightSquads);

	virtual void startBattle(Squad*);				// Let the squad to be ready for a battle

	virtual void wholeSquadWaiting(Squad*);

	virtual void searchEnemy(Squad*);

	virtual void wholeSquadMove(Squad*,float);

	virtual void wholeSquadFight(Squad*,float);

	virtual void SquadFightEnd(Squad*);		// End a fight for a squad

	virtual void clearSquad(Squad*);			// Make sure the squad is cleared in the battle field

	/*		help methods below		*/

	virtual unsigned int getSquadNumbersInBattle();

	virtual std::map<int,Unit> getAllUnits();

	virtual bool unitAlive(int);

	virtual int getHeroUnitID(int squadIndex);

	virtual int getSoldierUnitID(int squadIndex,int soldierIndex);

	virtual Squad* getSquadByIndex(int index);

	virtual bool sideWin(SquadSide);

	virtual bool battleFinished();

	virtual void endBattle();

	virtual void end();
};

#endif /* defined(__MyGame__Battle__) */