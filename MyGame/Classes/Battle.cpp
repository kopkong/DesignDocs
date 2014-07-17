#include "Battle.h"
#include <algorithm>
#include "Resources.h"

Battle::Battle()
{

}

Battle::~Battle()
{

}

Battle* Battle::getInstance()
{
	static Battle s_Battle;
    return &s_Battle;
}

void Battle::reset()
{
	_allUnits.clear();
	_allSquads.clear();

	_battleFinished = false;
	_battleFieldSize = Size(1024,768);
	_squadSize = Size(100,192);
	_heroSize = Size(48,48);
	_soldierSize = Size(32,32);
}

void Battle::initSquads(Formation leftFormation, Formation rightFormation)
{
	char buff[100];
	int indexCount = 0;
    
    for(int row = 0; row< 4 ; row ++)
    {
        for(int col = 0; col < 5; col ++)
        {
            // Create left side squads
            if(leftFormation[row][col] != SquadType::None)
            {
                sprintf(buff,"%d(TeamA)",indexCount);
                std::string name = buff;
                
                Point p(_battleFieldSize.width/2 - 12 - _squadSize.width * col,
                        _battleFieldSize.height  - _squadSize.height/2 - _squadSize.height * row);
                
                Squad a = Squad(name,p,indexCount);
                
                if(leftFormation[row][col] == SquadType::Footman)
                {
                    a.setSoldierRes(Resources::getInstance()->getFootmanResourceA());
                    initSquadProperty(&a, SquadType::Footman);
                }
                
                if(leftFormation[row][col] == SquadType::Knight)
                {
                    a.setSoldierRes(Resources::getInstance()->getKnightResourceA());
                    initSquadProperty(&a, SquadType::Knight);
                }
                
                if(leftFormation[row][col] == SquadType::Archer)
                {
                    a.setSoldierRes(Resources::getInstance()->getArcherResourceA());
                    initSquadProperty(&a, SquadType::Archer);
                }
                
                indexCount ++;
                a.setSquadSide(SquadSide::TeamA);
                a.setTargetSquadIndex(NONETARGET);
                initSquadSprite(&a);
                _allSquads.push_back(a);
                
            }
            
            // Create right side suqads
            if(rightFormation[row][col] != SquadType::None)
            {
                sprintf(buff,"%d(TeamB)",indexCount);
                std::string name = buff;
                
                Point p(_battleFieldSize.width/2 + 12 + _squadSize.width * col,
                        _battleFieldSize.height  - _squadSize.height/2 - _squadSize.height * row);
                
                Squad b = Squad(name,p,indexCount);
                
                if(rightFormation[row][col] == SquadType::Footman)
                {
                    b.setSoldierRes(Resources::getInstance()->getFootmanResourceB());
                    initSquadProperty(&b,Footman);
                }
                if(rightFormation[row][col] == SquadType::Knight)
                {
                    b.setSoldierRes(Resources::getInstance()->getKnightResourceB());
                    initSquadProperty(&b,Knight);
                }
                if(rightFormation[row][col] == SquadType::Archer)
                {
                    b.setSoldierRes(Resources::getInstance()->getArcherResourceB());
                    initSquadProperty(&b,Archer);
                }
                
                indexCount ++;
                b.setSquadSide(SquadSide::TeamB);
                b.setTargetSquadIndex(NONETARGET);
                initSquadSprite(&b);
                _allSquads.push_back(b);
            }
        }
    }
}

void Battle::initSquadProperty(Squad * pSquad, SquadType type){
    // Init some basic properties here,
    // like speed , attack , range , etc.
    
    int basicUnitWidth = _soldierSize.width;
    switch(type){
        case SquadType::Footman:
        {
            pSquad->setSoldierType(SquadType::Footman);
            pSquad->setSpeed(basicUnitWidth);
            pSquad->setAttackRange(basicUnitWidth);
            pSquad->setAttackInterval(1.0f);
            break;
        }
        case SquadType::Knight:
        {
            pSquad->setSoldierType(SquadType::Knight);
            pSquad->setSpeed(basicUnitWidth * 1.4);
            pSquad->setAttackRange(basicUnitWidth * 1.2);
            pSquad->setAttackInterval(1.0f);
            break;
        }
        case SquadType::Archer:
        {
            pSquad->setSoldierType(SquadType::Archer);
            pSquad->setSpeed(basicUnitWidth * 0.8);
            pSquad->setAttackRange(basicUnitWidth * 4);
            pSquad->setAttackInterval(2.0f);
            break;
        }
        default:
        {
            break;
        }
    }
    
    // Set every hero's health
    int heroID = getHeroUnitID(pSquad->getIndex());
    
	Unit u = Unit(heroID,pSquad->getHeroHealth(),pSquad->getAttackInterval());
	
	_allUnits.insert(std::pair<int,Unit>(heroID,u));
    
    for(unsigned int i= 0 ; i < pSquad->getSoldierCount(); i ++){
        int soldierID = getSoldierUnitID(pSquad->getIndex(), i);

		Unit s = Unit(soldierID,pSquad->getSoldierHealth(),pSquad->getAttackInterval());

		_allUnits.insert(std::pair<int,Unit>(soldierID,s));
    }
}

void Battle::initSquadSprite(Squad* pSquad)
{
	// Texture rect
	//2D-Cartoon Vector Characters/archer.png
	Sprite* test = Sprite::create("D:\DesignDocs\MyGame\Resources\Character\FootmanA\1-1.png");
	Sprite* spriteHero = Sprite::create(pSquad->getSoldierRes().Idle[0]);
	float scale_x = _heroSize.width / spriteHero->getContentSize().width ;
	float scale_y = _heroSize.height / spriteHero->getContentSize().height;
	spriteHero->setScale(scale_x,scale_y);

	if(pSquad->getFaceTo() != Orientation::Right)
	{
		spriteHero->setFlippedX(true);
	}

	spriteHero->setPosition(pSquad->getPosition());
	int hUnitID = getHeroUnitID(pSquad->getIndex());
	_allUnits[hUnitID].setSprite(spriteHero);

	for(unsigned int i = 0 ; i< pSquad->getSoldierCount();i++){
		Sprite* spriteSoldier = Sprite::create(pSquad->getSoldierRes().Idle[0]);
		scale_x = _soldierSize.width / spriteSoldier->getContentSize().width ;
		scale_y = _soldierSize.height / spriteSoldier->getContentSize().height;

		spriteSoldier->setScale(scale_x,scale_y);
		if(pSquad->getFaceTo() != Orientation::Right)
		{
			spriteSoldier->setFlippedX(true);
		}

		float distance = (_heroSize.width - _soldierSize.width)/2;
		float distanceFactor = 1.1f;

		int col = i / 5 ;
		int row  = i % 5;

		// the closet soldier's position
		float maxY = pSquad->getPosition().y + 2 * ( _soldierSize.height + distanceFactor * distance);

		float x ,y;
		if(pSquad->faceToRight())
		{
			x = pSquad->getPosition().x - 10 - (col + 1) * ( distanceFactor * distance + _soldierSize.width);
			y = maxY - (row + 1) * ( distanceFactor * distance + _soldierSize.height);
		}
		else{
			x = pSquad->getPosition().x + 10 + (col + 1) * ( distanceFactor * distance + _soldierSize.width);
			y = maxY - (row + 1) * ( distanceFactor * distance + _soldierSize.height);
		}

		Point p = Point(x,y);
		spriteSoldier->setPosition(p);
		int soldierUnitID = getSoldierUnitID(pSquad->getIndex(),i);
		_allUnits[soldierUnitID].setSprite(spriteSoldier);
	}
}

void Battle::startBattle(Squad* pSelfSquad)
{
    //battleStartSearch(pSelfSquad, SquadType (*targetFormation)[5])
	//pSelfSquad->setState(SquadState::Moving);
}

void Battle::wholeSquadWaiting(Squad* pSelfSquad)
{
    // Play idle animation
    
    // Hero idle
    int heroUnitID = getHeroUnitID(pSelfSquad->getIndex());
	if(unitAlive(heroUnitID))
	{
        log("hero is in idle state");
		Animation* animation = Animation::create();
        for(unsigned int i=0;i<pSelfSquad->getSoldierRes().Idle.size();i++)
        {
            SpriteFrame * frame = SpriteFrameCache::getInstance()->getSpriteFrameByName(pSelfSquad->getSoldierRes().Idle[i].c_str());
            animation->addSpriteFrame(frame);
        }
        
        animation->setDelayPerUnit(1/10.0f);
        animation->setRestoreOriginalFrame(true);
        
	}
    
	// Soldier idle
	for(unsigned int i = 0 ; i< pSelfSquad->getSoldierCount();i++){
		int soldierUnitID = getSoldierUnitID(pSelfSquad->getIndex(), i);
        if(unitAlive(soldierUnitID))
        {
            
        }
		
    }
    
	searchEnemy(pSelfSquad);
}

void Battle::wholeSquadMove(Squad* pSquad,float dt)
{
	// Hero move
	int heroUnitID = getHeroUnitID(pSquad->getIndex());
	if(unitAlive(heroUnitID))
	{
		Point pos = move(heroUnitID,pSquad,dt);
		pSquad->setPosition(pos);
	}

	// Soldier move
	for(unsigned int i = 0 ; i< pSquad->getSoldierCount();i++){
		int soldierUnitID = getSoldierUnitID(pSquad->getIndex(), i);

		Point pos =  move(soldierUnitID,pSquad,dt);  

		// If hero died , update squad's position with the first soldier's position
		if(!unitAlive(heroUnitID) && i == 0)
		{
			pSquad->setPosition(pos);
		}
	}

	searchEnemy(pSquad);
}

void Battle::wholeSquadFight(Squad* pSquad,float dt)
{
	//log("Squad[%s] is fighting",pSquad->getName().c_str());
	//printStats();

	// Let hero to fight
	int heroID = getHeroUnitID(pSquad->getIndex());
	int targetSquadIndex = pSquad->getTargetSquadIndex();
	Squad* pTargetSquad = getSquadByIndex(targetSquadIndex);

	// If target squad is eliminated, return to waiting state
	if(pTargetSquad->getState() == SquadState::Eliminated)
	{
		SquadFightEnd(pSquad);
		return;
	}

	if(unitAlive(heroID))
	{
		if (_allUnits[heroID].getTargetIndex() == NONETARGET) // no aim target
		{
			//log("Hero unit[%d] is picking target",heroID);
			pickTarget(heroID, pSquad, pTargetSquad);
		}
		else// Already has a target
		{
			//log("Hero unit[%d] is attacking target",heroID);
			attackTarget(heroID,pSquad,pTargetSquad,dt);
		}
	}

	// Let soldiers to fight
	for(unsigned int i = 0; i < pSquad->getSoldierCount(); i ++)
	{
		int soldierID = getSoldierUnitID(pSquad->getIndex(),i);

		if(unitAlive(soldierID))
		{
			if(_allUnits[soldierID].getTargetIndex() == NONETARGET) // no target
			{
				//log("Soldier unit[%d] is picking target",soldierID);
				pickTarget(soldierID,pSquad,pTargetSquad);
			}
			else
			{
				//log("Soldier unit[%d] is attacking target",soldierID);
				attackTarget(soldierID,pSquad,pTargetSquad,dt);
			}
		}
	}
}

void Battle::SquadFightEnd(Squad* pSquad)
{
	pSquad->setState(SquadState::Wait);
	pSquad->setTargetSquadIndex(NONETARGET);

	// To do ...
	// Can do more things after one fight is finished ..
	// Add more codes here


}

void Battle::searchEnemy(Squad* pSquad)
{
	// First pick alive enemy squads list
	std::vector<Squad*> rivalSquads;
	for(unsigned int i = 0 ; i < _allSquads.size(); i++)
	{
		if(_allSquads[i].getSquadSide() != pSquad->getSquadSide() &&
			_allSquads[i].getState() != SquadState::Eliminated
           &&_allSquads[i].getMeleeSquads()<2)
        {
			rivalSquads.push_back(&_allSquads[i]);
        }
	}
    
    

	float minDistance = _battleFieldSize.width * _battleFieldSize.width;
	for(unsigned int i = 0; i< rivalSquads.size() ; i++)
	{
		float distance = pow((pSquad->getPosition().x - rivalSquads[i]->getPosition().x),2) + pow((pSquad->getPosition().y - rivalSquads[i]->getPosition().y),2);

		if(distance < minDistance)
		{
			//log("Squad %s found it's target is Squad %s", sq->getName().c_str(), _squadListB[i].getName().c_str());

			// Locate the target and aim to it
			pSquad->setTargetSquadIndex(rivalSquads[i]->getIndex());
			pSquad->setTargetPosition(rivalSquads[i]->getPosition());
            //rivalSquads[i]->setMeleeSquads(rivalSquads[i]->getMeleeSquads() + 1);

			minDistance = distance;
		}
	}

	// Stop moving and turn to fighting state
	pSquad->setState(SquadState::Fighting);

}

void Battle::pickTarget(int unitID, Squad* pSelfSquad, Squad* pTargetSquad)
{
	int enemyHeroID = getHeroUnitID(pTargetSquad->getIndex());

	// Alive units in the target Squad
	std::vector<int> aliveUnits;
	if(unitAlive(enemyHeroID))
	{
		aliveUnits.push_back(enemyHeroID);
	}

	// Count there are how many alive soldiers
	for(unsigned int i = 0; i < pTargetSquad->getSoldierCount(); i ++)
	{
		int enemySoldierID = getSoldierUnitID(pTargetSquad->getIndex(), i);
		if(unitAlive(enemySoldierID))
		{
			aliveUnits.push_back(enemySoldierID);
		}
	}

	if(aliveUnits.size() == 0){
		// Don't have any alive units
		SquadFightEnd(pSelfSquad);
	}
	else if( aliveUnits.size() == 1)
	{
		// Only one alive unit
		_allUnits[unitID].setTargetIndex(aliveUnits[0]) ;
	}
	else
	{
		// More than one alive units
		// pick a random unit
		int r = (int) (CCRANDOM_0_1() * aliveUnits.size());

		_allUnits[unitID].setTargetIndex(aliveUnits[r]) ;
	}
}

void Battle::attackTarget(int unitID, Squad* pSelfSquad, Squad* pTargetSquad,float dt)
{
	// Get the target ID
	int targetUnitID = _allUnits[unitID].getTargetIndex();

	if(!unitAlive(targetUnitID) ){ // target is dead

		// Set target sprite visible to false
		_allUnits[targetUnitID].getSprite()->setVisible(false);

		// Current hero won't have any target at this time
		_allUnits[unitID].setTargetIndex(NONETARGET);
	}
	else // target is not dead
	{
		// Check target is inside the attack range
		Sprite* selfSprite = _allUnits[unitID].getSprite();
		Sprite* targetSprite = _allUnits[targetUnitID].getSprite();

		float range = pow(targetSprite->getPositionX() - selfSprite->getPositionX(),2) +
			pow(targetSprite->getPositionY() - selfSprite->getPositionY(),2);

		// Far away from attack range, go near to target
		if(range > pSelfSquad->getAttackRange() * pSelfSquad->getAttackRange())
		{
			// Move to the target
			move(unitID,pSelfSquad,dt);
		}
		else // Inside the attack range
		{
			doAttack(unitID,targetUnitID,pSelfSquad,dt);
		}
	}
}

void Battle::doAttack(int unitID,int targetID, Squad* pSelfSquad, float dt)
{
	float elapsedTime = _allUnits[unitID].getElapsedTimeSinceLastAttack();
	_allUnits[unitID].setTryAttackTimes(_allUnits[unitID].getTryAttackTimes() + 1);

	if( elapsedTime + dt >= _allUnits[unitID].getAttackInterval()) // do next attack action
	{
		_allUnits[unitID].getSprite()->stopAllActions();

		_allUnits[unitID].setDoAttackTimes(_allUnits[unitID].getDoAttackTimes() + 1);
		_allUnits[unitID].setElapsedTimeSinceLastAttack(elapsedTime + dt - _allUnits[unitID].getAttackInterval());

		//log("Unit[%d](Team[%d]) attacks Target[%d]", unitID,pSelfSquad->getSquadSide(),targetID);

		// Play attack animation, rock and roll ..
		ActionInterval* action1 = RotateTo::create(_allUnits[unitID].getAttackInterval() /3 ,25);
		ActionInterval* action2 = RotateTo::create(_allUnits[unitID].getAttackInterval() /3  ,-25);
        ActionInterval* action3 = RotateTo::create(_allUnits[unitID].getAttackInterval() /3, 0);

		Action* actionAttack = Sequence::create(action1,action2,action3,NULL);

		_allUnits[unitID].getSprite()->runAction(actionAttack);

		int targetHealth = _allUnits[targetID].getHealthPoint();

		if(unitID % 100 == 0) // It's a hero
			targetHealth -= pSelfSquad->getAttackPoint() * 2;
		else
			targetHealth -= pSelfSquad->getAttackPoint() ;

		_allUnits[targetID].setHealthPoint(targetHealth);

		// Check if target is dead
		if(!unitAlive(targetID))
		{
			_allUnits[targetID].getSprite()->setVisible(false);
			//log("TargetUnit[%d] has been killed, set sprite visible to %d",targetID,_allUnits[targetID].getSprite()->isVisible());
			//printStats();

			_allUnits[unitID].setTargetIndex(NONETARGET);

			// Check Squad is eliminated or not
			if(targetSquadEliminated(getSquadByIndex(pSelfSquad->getTargetSquadIndex())))
			{
				SquadFightEnd(pSelfSquad);
			}
		}
	}
	else // It's not the time to do next attack
	{
		_allUnits[unitID].setElapsedTimeSinceLastAttack(elapsedTime + dt );
	}

}

Point Battle::move(int unitID,Squad* pSelfSquad, float dt)
{
	int targetID = _allUnits[unitID].getTargetIndex();
	Sprite * selfSprite = _allUnits[unitID].getSprite();
	float speed = pSelfSquad->getSpeed();

	Point nextPosition ;
	if(targetID == NONETARGET) // No target
	{
		// move straight forward
		if(pSelfSquad->faceToRight())
		{
			nextPosition = Point(selfSprite->getPositionX() + dt * speed, selfSprite->getPositionY());
		}
		else{
			nextPosition = Point(selfSprite->getPositionX() - dt * speed, selfSprite->getPositionY());
		}

		selfSprite->setPosition(nextPosition);
	}
	else
	{
		Sprite * targetSprite = _allUnits[targetID].getSprite();

		Point direction = targetSprite->getPosition() - selfSprite->getPosition();
		if(direction.x > 0)
		{
			pSelfSquad->setFaceTo(Orientation::Right);
		}
		else{
			pSelfSquad->setFaceTo(Orientation::Left);
		}

		bool up ;
		if(direction.y > 0)
		{
			up = true;
		}
		else
		{
			up = false;
		}

		// Compute tangent
		float tan = 0;
		if(abs(direction.x) > 0.0001)
		{
			tan = abs(direction.y) / abs(direction.x)					;
		}
		else
		{
			tan = 1;
		}

		if(pSelfSquad->faceToRight()){
			nextPosition.x = selfSprite->getPositionX() + dt * speed;
		}
		else{
			nextPosition.x = selfSprite->getPositionX() - dt *speed;
		}

		if(up){
			nextPosition.y = selfSprite->getPositionY() + tan * dt * speed;
		}
		else{
			nextPosition.y = selfSprite->getPositionY() - tan * dt * speed;
		}

		// If unit will move outside the screen, stop moving.
		if(nextPosition.x > _battleFieldSize.width ||
			nextPosition.x < 0 ||
			nextPosition.y > _battleFieldSize.height ||
			nextPosition.y < 0)
		{
			//pSelfSquad ->setState(SquadState::Wait);

			// Do not move anymore, so next position will be the same position
			nextPosition = selfSprite->getPosition();
		}

		// Set the new position for the sprite
		selfSprite->setPosition(nextPosition);
	}
	
	return nextPosition;
}

void Battle::clearSquad(Squad* pSelfSquad)
{
	int heroUnitID = getHeroUnitID(pSelfSquad->getIndex());
	_allUnits[heroUnitID].getSprite()->setVisible(false);

	for(unsigned int i = 0; i < pSelfSquad->getSoldierCount(); i ++)
	{
		int soldierUnitID = getSoldierUnitID(pSelfSquad->getIndex(),i);

		if(unitAlive(soldierUnitID))
		{
			_allUnits[soldierUnitID].getSprite()->setVisible(false);
		}
	}

}

/* Help methods */

int Battle::getHeroUnitID(int squadIndex)
{
	return 100 * squadIndex;
}

int Battle::getSoldierUnitID(int squadIndex, int soldierIndex)
{
	return 100 * squadIndex + 1 + soldierIndex;
}

Squad* Battle::getSquadByIndex(int squadIndex)
{
	for(unsigned int i=0 ; i < _allSquads.size() ; i ++)
	{
		if(_allSquads[i].getIndex() == squadIndex)
			return &_allSquads[i];
	}

	return NULL;
}

bool Battle::unitAlive(int unitID)
{
	return _allUnits[unitID].getHealthPoint() > 0 ;
}

bool Battle::sideWin(SquadSide side){
	bool win = true;

	for(unsigned int i = 0 ; i < _allSquads.size(); i++)
	{
		if(_allSquads[i].getSquadSide() != side &&
			_allSquads[i].getState() != SquadState::Eliminated)
		{
			win = false;
			break;
		}
	}

	return win;
}

bool Battle::targetSquadEliminated(Squad* pTargetSquad)
{
	int heroUnitID = getHeroUnitID(pTargetSquad->getIndex());
	bool heroDead = !unitAlive(heroUnitID);

	if(!heroDead)
		return false;

	bool allSoldiersDead = true;
	for(unsigned int i = 0; i < pTargetSquad->getSoldierCount(); i ++)
	{
		int soldierUnitID = getSoldierUnitID(pTargetSquad->getIndex(),i);

		if(unitAlive(soldierUnitID))
		{
			allSoldiersDead = false;
			return false;
		}
	}

	// Target squad is eliminated
	if( heroDead && allSoldiersDead)
	{
		pTargetSquad->setState(SquadState::Eliminated);
		return true;
	}
	else
	{
		return false;
	}

	return true;
}

bool Battle::battleFinished()
{
	return _battleFinished;
}

void Battle::endBattle()
{
	_battleFinished = true;

	printStats();
}

std::map<int,Unit> Battle::getAllUnits()
{
	return _allUnits;
}

unsigned int Battle::getSquadNumbersInBattle()
{
	return (unsigned int)_allSquads.size();
}

void Battle::end()
{

}

void Battle::actionIdleDone()
{
    
}

void Battle::printStats()
{
	// Print some statistics 
	log("==============Print Statistics =====================");
	for(std::map<int,Unit>::iterator it = _allUnits.begin();
		it !=  _allUnits.end(); ++it )
	{

		log("Unit[%d], health = %d, doAttackTimes = %d, tryAttackTimes = %d, SpriteVisible = %d",
			it->second.getIndex(), it->second.getHealthPoint(), it->second.getDoAttackTimes(),
			it->second.getTryAttackTimes(), it->second.getSprite()->isVisible());
	}

	log("==============End Statistics =====================\n");
}