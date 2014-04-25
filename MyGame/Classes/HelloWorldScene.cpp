#include "HelloWorldScene.h"
#include <algorithm>
USING_NS_CC;

// Constant paras
const Size HEROSIZE(36,36);
const Size SOLDIERSIZE(24,24);

const Size SQUADBLOCKSIZE(170,256);

Scene* HelloWorld::createScene()
{
    // 'scene' is an autorelease object
    auto scene = Scene::create();
    
    // 'layer' is an autorelease object
    auto layer = HelloWorld::create();

    // add layer as a child to scene
    scene->addChild(layer);

    // return the scene
    return scene;
}

// on "init" you need to initialize your instance
bool HelloWorld::init()
{
    //////////////////////////////
    // 1. super init first
    if ( !Layer::init() )
    {
        return false;
    }
    
    Size visibleSize = Director::getInstance()->getVisibleSize();
    Point origin = Director::getInstance()->getVisibleOrigin();
    _screenSize = Size(1024,768);
    _battleFinished = false;
    
    // add background
    auto backGround = Sprite::create("bg.png");
    backGround->setPosition(visibleSize.width/2,visibleSize.height/2);
    this->addChild(backGround,0);
    
    
    /////////////////////////////
    // 2. add a menu item with "X" image, which is clicked to quit the program
    //    you may modify it.

    // add a "close" icon to exit the progress. it's an autorelease object
    auto closeItem = MenuItemImage::create(
                                           "CloseNormal.png",
                                           "CloseSelected.png",
                                           CC_CALLBACK_1(HelloWorld::menuCloseCallback, this));
    
	closeItem->setPosition(Point(origin.x + visibleSize.width - closeItem->getContentSize().width/2 ,
                                origin.y + closeItem->getContentSize().height/2));

    // create menu, it's an autorelease object
    auto menu = Menu::create(closeItem, NULL);
    menu->setPosition(Point::ZERO);
    this->addChild(menu, 1);
    
    initSquads();
    
    this->schedule(schedule_selector(HelloWorld::update));
    
    return true;
}

void HelloWorld::update(float dt){
    if(_battleFinished)
        return;
    
	if(_allSquadsInBattle.size() > 0){
        
        // Check battle finished or not
        if(checkSideWin(SquadSide::TeamA))
        {
            log("TeamA has won the battle!");
            _battleFinished = true;
        }
        if(checkSideWin(SquadSide::TeamB))
        {
            log("TeamB has won the battle!");
            _battleFinished = true;
        }
        
        for(unsigned int i = 0; i < _allSquadsInBattle.size(); i++)
        {
            Squad* sq = &_allSquadsInBattle[i];
			log("Squad[%s] state is %d", sq->getName().c_str(),sq->getState());
            switch(sq->getState()){
                case SquadState::Moving:
                {
                    //log("Squad %s is moving",sq->getName().c_str());
                    squadMove(sq,dt);
                    break;
                }
                case SquadState::BattleBegin:
                {
                    //log("Battle begin, squad %s is ready",sq->getName().c_str());
                    battleBegin(sq);
                    sq->setState(SquadState::Moving);
                    //log("Squad %s state is %d", sq->getName().c_str(), sq->getState());
                    break;
                }
				case SquadState::Wait:
                {
                    squadWait(sq);
                }
                case SquadState::Fighting:
                {
                    //log("Squad %s is fighting", sq->getName().c_str());
                    squadFighting(sq,dt);
                    break;
                }
				case SquadState::Eliminated:
					{
						
					}
                default:
                    break;
            }
        }
    }
}

void HelloWorld::battleBegin(Squad* sq){
    // If thers is any special effect or skill should be triggered at this time
    
    // Change the state to moving,
    // sq->setState(SquadState::Moving);
    
    // At the beginning , we set the target direction is straight forward.
    if(sq->faceToRight())
        sq->setTargetPosition(Point(1024,sq->getPosition().y));
    else
        sq->setTargetPosition(Point(0,sq->getPosition().y));
}

void HelloWorld::squadWait(Squad* sq){
    // the squad is waiting, but still keep searching the enemy
    
    searchEnemy(sq);
}

void HelloWorld::squadMove(Squad* sq,float dt){
    if(sq->getState() != SquadState::Moving)
        return;
        
    // Hero move
    if(!sq->getHeroDead()){
        int heroIndex = getHeroSpriteID(sq->getIndex());
        
		Point pos = moveSpriteUnit(heroIndex,sq,dt);
		sq->setPosition(pos);
    }
    
    // Soldier move
    for(unsigned int i = 0 ; i< sq->getSoldierCount();i++){
        int soldierIndex = getSoldierSpriteID(sq->getIndex(), i);

		Point pos =  moveSpriteUnit(soldierIndex,sq,dt);  
   
        // If hero died , update squad's position with the first soldier's position
        if(sq->getHeroDead() && i == 0)
        {
            sq->setPosition(pos);
        }
    }
    
    // Search enemy
    searchEnemy(sq);
}

void HelloWorld::squadFighting(Squad* pSquad, float dt){

    // Check state first
    if(pSquad->getState() != SquadState::Fighting)
        return;
    
    // Let hero to fight
    int heroID = getHeroSpriteID(pSquad->getIndex());
    int targetSquadIndex = pSquad->getTargetIndex();
    Squad* pTargetSquad = getSquadByIndex(targetSquadIndex);

	// If target squad is eliminated, return to waiting state
	if(pTargetSquad->getState() == SquadState::Eliminated)
	{
		pSquad->setState(SquadState::Wait);
		return;
	}
    
	if(unitAlive(heroID))
	{
		if (_allUnitsTargetIndex.at(heroID) == 0) // no aim target
		{
			pickTarget(heroID, pSquad, pTargetSquad);
		}
		else// Already has a target
		{
			attackTarget(heroID,pSquad,pTargetSquad,dt);
		}
	}

	// Let soldiers to fight
	for(unsigned int i = 0; i < pSquad->getSoldierCount(); i ++)
	{
		int soldierID = getSoldierSpriteID(pSquad->getIndex(),i);

		if(unitAlive(soldierID))
		{
			if(_allUnitsTargetIndex[soldierID] == 0) // no target
			{
				pickTarget(soldierID,pSquad,pTargetSquad);
			}
			else
			{
				attackTarget(soldierID,pSquad,pTargetSquad,dt);
			}
		}
	}

	// Check target has been eliminated or not
	bool heroDead = !unitAlive(getHeroSpriteID(targetSquadIndex));

	bool allSoldiersDead = true;
	for(unsigned int i = 0; i < pTargetSquad->getSoldierCount(); i ++)
	{
		int soldierID = getSoldierSpriteID(targetSquadIndex,i);

		if(unitAlive(soldierID))
		{
			allSoldiersDead = false;
			break;
		}
	}

	// Target squad is eliminated
	if( heroDead && allSoldiersDead)
	{
		pTargetSquad->setState(SquadState::Eliminated);

		// Our current Squad turn to wait
		pSquad->setState(SquadState::Wait);
	}
}

void HelloWorld::saveHeroSprite(int squadIndex,Sprite* sprite){
    _allUnitsSprite.insert(std::pair<int,Sprite*>(squadIndex * 100,sprite));
}

void HelloWorld::saveSoldierSprite(int squadIndex, int soldierIndex, cocos2d::Sprite * sprite){
    _allUnitsSprite.insert(std::pair<int,Sprite*>(squadIndex * 100 + 1 + soldierIndex,sprite));
}

int HelloWorld::getHeroSpriteID(int squadIndex) {
    return 100 * squadIndex;
}

int HelloWorld::getSoldierSpriteID(int squadIndex, int soldierIndex){
    return 100 * squadIndex + 1 + soldierIndex;
}

void HelloWorld::drawAll(){
    if(_allSquadsInBattle.size() > 0){
        for(unsigned int i = 0; i < _allSquadsInBattle.size(); i++)
        {
            drawSquad(&_allSquadsInBattle[i]);
        }
    }
}

void HelloWorld::drawSquad(Squad* sq){
    // Texture rect
    Sprite* hero = Sprite::create(sq->getSpriteTexture());
    float scale_x = HEROSIZE.width / hero->getContentSize().width ;
    float scale_y = HEROSIZE.height / hero->getContentSize().height;
    hero->setScale(scale_x,scale_y);
    
    if(sq->getFaceTo() != sq->getSpriteOrientation())
    {
        hero->setFlippedX(true);
    }
    
    hero->setPosition(sq->getPosition());
	saveHeroSprite(sq->getIndex(), hero);
    this->addChild(hero,1);
    
    for(unsigned int i = 0 ; i< sq->getSoldierCount();i++){
        auto soldier = Sprite::create(sq->getSpriteTexture());
        scale_x = SOLDIERSIZE.width / soldier->getContentSize().width ;
        scale_y = SOLDIERSIZE.height / soldier->getContentSize().height;
        
        soldier->setScale(scale_x,scale_y);
        if(sq->getFaceTo() != sq->getSpriteOrientation())
        {
            soldier->setFlippedX(true);
        }
        
		float distance = (HEROSIZE.width - SOLDIERSIZE.width)/2;
		float distanceFactor = 1.1f;

		int col = i / 5 ;
		int row  = i % 5;

        // the closet soldier's position
		float maxY = sq->getPosition().y + 2 * ( SOLDIERSIZE.height + distanceFactor * distance);

        float x ,y;
        if(sq->faceToRight())
        {
            x = sq->getPosition().x - 10 - (col + 1) * ( distanceFactor * distance + SOLDIERSIZE.width);
            y = maxY - (row + 1) * ( distanceFactor * distance + SOLDIERSIZE.height);
        }
        else{
            x = sq->getPosition().x + 10 + (col + 1) * ( distanceFactor * distance + SOLDIERSIZE.width);
            y = maxY - (row + 1) * ( distanceFactor * distance + SOLDIERSIZE.height);
        }
        
		Point p = Point(x,y);
		soldier->setPosition(p);
        saveSoldierSprite(sq->getIndex(), i, soldier);
        this->addChild(soldier,1);
    }
}

void HelloWorld::searchEnemy(Squad* sq){
    // First make sure squad is in moving state or waiting state
    bool inSearchingState = sq->getState() == SquadState::Moving ||
    sq->getState() == SquadState::Wait;
    
    if(!inSearchingState)
        return;

	// First pick alive emeny squads list
	std::vector<Squad*> rivalSquads;
	for(unsigned int i = 0 ; i < _allSquadsInBattle.size(); i++)
	{
		if(_allSquadsInBattle[i].getSquadSide() != sq->getSquadSide() &&
			_allSquadsInBattle[i].getState() != SquadState::Eliminated)
			rivalSquads.push_back(&_allSquadsInBattle[i]);
	}

	float minDistance = 1024 * 1024;
	for(unsigned int i = 0; i< rivalSquads.size() ; i++)
	{
		float distance = pow((sq->getPosition().x - rivalSquads[i]->getPosition().x),2) + pow((sq->getPosition().y - rivalSquads[i]->getPosition().y),2);

		if(distance < minDistance)
        {
            //log("Squad %s found it's target is Squad %s", sq->getName().c_str(), _squadListB[i].getName().c_str());
                
            // Locate the target and aim to it
            sq->setTargetIndex(rivalSquads[i]->getIndex());
            sq->setTargetPosition(rivalSquads[i]->getPosition());
			
			minDistance = distance;
		}
	}

	// Stop moving and turn to fighting state
    sq->setState(SquadState::Fighting);
}

void HelloWorld::initSquads(){
    
    int indexCount = 0;
    int leftSideSquads = 8;
    int rightSideSquads = 8;
    for(int i = 0; i < leftSideSquads; i++){ // Create left side squads
        char buff[100];
        sprintf(buff,"a%d",i);
        std::string name = buff;
        
        int col = i / 3;
        int row = i % 3;
        
		Point p(_screenSize.width/2 - 20 - SQUADBLOCKSIZE.width * col,
                _screenSize.height  - SQUADBLOCKSIZE.height/2 - SQUADBLOCKSIZE.height * row);
        
        //log("Squad %s position is(%f,%f)", name.c_str(), p.x,p.y);
        Squad a = Squad(name,p,indexCount);
        
        if(col == 0)
        {
            a.setSpriteTexture("dwarf_warrior.png");
            a.setSpriteOrientation(Orientation::Left);
            initSquadProperty(&a, SquadType::Footman);
        }
        if(col == 1)
        {
            a.setSpriteTexture("viking.png");
            a.setSpriteOrientation(Orientation::Right);
            initSquadProperty(&a, SquadType::Knight);
        }
        if(col == 2)
        {
            a.setSpriteTexture("archer.png");
            a.setSpriteOrientation(Orientation::Right);
            initSquadProperty(&a, SquadType::Archer);
        }
        
        indexCount ++;
		a.setSquadSide(SquadSide::TeamA);
		_allSquadsInBattle.push_back(a);
    }
    

    for(int i = 0; i < rightSideSquads; i++){ // Create right side squads
        char buff[100];
        sprintf(buff,"b%d",i);
        std::string name = buff;
        
        int col = i / 3;
        int row = i % 3;
        
		Point p(_screenSize.width/2 + 20 + SQUADBLOCKSIZE.width * col,
                _screenSize.height  - SQUADBLOCKSIZE.height/2 - SQUADBLOCKSIZE.height * row);
        
        //log("Squad %s position is(%f,%f)", name.c_str(), p.x,p.y);
        Squad b = Squad(name,p,indexCount);
        indexCount ++;
        if(col == 0)
        {
            b.setSpriteTexture("orc.png");
            b.setSpriteOrientation(Orientation::Left);
            initSquadProperty(&b,Footman);
        }
        if(col == 1)
        {
            b.setSpriteTexture("centaur.png");
            b.setSpriteOrientation(Orientation::Left);
            initSquadProperty(&b,Knight);
        }
        if(col == 2)
        {
            b.setSpriteTexture("dragon.png");
            b.setSpriteOrientation(Orientation::Left);
            initSquadProperty(&b,Archer);
        }

		b.setSquadSide(SquadSide::TeamB);
		_allSquadsInBattle.push_back(b);
    }
    
    drawAll();
}

void HelloWorld::initSquadProperty(Squad * pSquad, SquadType type){
    // Init some basci properties here,
    // like speed , attack , range , etc.
    
    int basicUnitWidth = SOLDIERSIZE.width;
    switch(type){
        case SquadType::Footman:
        {
            pSquad->setSoldierType(SquadType::Footman);
            pSquad->setSpeed(basicUnitWidth);
            pSquad->setAttackRange(basicUnitWidth);
            break;
        }
        case SquadType::Knight:
        {
            pSquad->setSoldierType(SquadType::Knight);
            pSquad->setSpeed(basicUnitWidth * 1.4);
            pSquad->setAttackRange(basicUnitWidth);
            break;
        }
        case SquadType::Archer:
        {
            pSquad->setSoldierType(SquadType::Archer);
            pSquad->setSpeed(basicUnitWidth * 0.8);
            pSquad->setAttackRange(basicUnitWidth * 4);
            break;
        }
        default:
        {
            break;
        }
    }
    
    // Set every unit's health
    int heroID = getHeroSpriteID(pSquad->getIndex());
    
    _allUnitsHealth.insert(std::pair<int,int>(heroID,pSquad->getHeroHealth()));
    _allUnitsTargetIndex.insert(std::pair<int,int>(heroID,0)); // no target yet
    
    for(unsigned int i= 0 ; i < pSquad->getSoldierCount(); i ++){
        int soldierID = getSoldierSpriteID(pSquad->getIndex(), i);
        _allUnitsHealth.insert(std::pair<int,int>(soldierID,pSquad->getSoldierHealth()));
        _allUnitsTargetIndex.insert(std::pair<int,int>(soldierID,0));
    }
}

Squad* HelloWorld::getSquadByIndex(int squadIndex){
    for(unsigned int i = 0; i < _allSquadsInBattle.size() ; i ++){
        if(_allSquadsInBattle[i].getIndex() == squadIndex)
        {
            return &(_allSquadsInBattle[i]);
        }
    }
    
    return NULL;
}

void HelloWorld::attackTarget(int selfID, Squad* pSelfSquad, Squad* pTargetSquad,float dt){
    
    // Get the target ID
    int targetUnitID = _allUnitsTargetIndex[selfID];
    
    if(_allUnitsHealth[targetUnitID] <= 0 ){ // target is dead
        
        // Set target sprite visible to false
        _allUnitsSprite[targetUnitID]->setVisible(false);
        
        // Current hero won't have any target at this time
        _allUnitsTargetIndex[selfID] = 0;
        
    }
    else // target is not dead
    {
        // Check target is inside the attack range
        Sprite* selfSprite = _allUnitsSprite[selfID];
        Sprite* targetSprite = _allUnitsSprite[targetUnitID];
        float range = pow(targetSprite->getPositionX() - selfSprite->getPositionX(),2) +
        pow(targetSprite->getPositionY() - selfSprite->getPositionY(),2);
        
        // Far away from attack range, go near to target
        if(range > pSelfSquad->getAttackRange() * pSelfSquad->getAttackRange())
        {
            // Move to the target
			moveSpriteUnit(selfID,pSelfSquad,dt);
        }
		else // Inside the attack range
		{
			// Attack, Consider play some effect here
			// ...

			if(selfID % 100 == 0) // It's a hero
				_allUnitsHealth[targetUnitID] -= pSelfSquad->getAttackPoint() * 2;
			else
				_allUnitsHealth[targetUnitID] -= pSelfSquad->getAttackPoint() ;

			log("UnitA[%d] attacks unitB[%d]",selfID,targetUnitID);
			log("UnitB[%d] health is %d",targetUnitID,_allUnitsHealth[targetUnitID]);
		}
    }

}

Point HelloWorld::moveSpriteUnit(int selfID,Squad* pSelfSquad,float dt){
    int targetID = _allUnitsTargetIndex[selfID];
    Sprite * selfSprite = _allUnitsSprite[selfID];
    float speed = pSelfSquad->getSpeed();

    Point nextPosition ;
    if(targetID == 0) // No target
    {
        // move straight forward
        if(pSelfSquad->faceToRight())
        {
            nextPosition = Point(selfSprite->getPositionX() + dt * pSelfSquad->getSpeed(),selfSprite->getPositionY());
        }
        else{
            nextPosition = Point(selfSprite->getPositionX() - dt * pSelfSquad->getSpeed(),selfSprite->getPositionY());
        }
        
        selfSprite->setPosition(nextPosition);
        return nextPosition;
    }
    
    Sprite * targetSprite = _allUnitsSprite[targetID];
    
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
        tan = abs(direction.y) / abs(direction.x);
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
        
    // If hero will move outside the screen, stop moving.
    if(nextPosition.x > _screenSize.width ||
        nextPosition.x < 0 ||
        nextPosition.y > _screenSize.height ||
        nextPosition.y < 0)
    {
        //pSelfSquad ->setState(SquadState::Wait);
        
		// Do not move anymore, so next position will be the same position
		nextPosition = selfSprite->getPosition();

		log("Squad %s reached the edge and stop moving",pSelfSquad->getName().c_str());
    }

	// Set the new position for the sprite
	selfSprite->setPosition(nextPosition);
    
    return nextPosition;
}

void HelloWorld::pickTarget(int unitSpriteID,Squad* pSelfSquad,Squad* pTargetSquad){
    int enemyHeroID = getHeroSpriteID(pTargetSquad->getIndex());
    
    // Alive units in the target Squad
    std::vector<int> aliveUnits;
    if(_allUnitsHealth[enemyHeroID] > 0 )
    {
        aliveUnits.push_back(enemyHeroID);
    }
    
    // Count there are how many alive soldiers
    for(unsigned int i = 0; i < pTargetSquad->getSoldierCount(); i ++)
    {
        int enemySoldierID = getSoldierSpriteID(pTargetSquad->getIndex(), i);
        if(_allUnitsHealth.at(enemySoldierID) > 0 )
        {
            aliveUnits.push_back(enemySoldierID);
        }
    }
    
    if(aliveUnits.size() == 0){
        // Don't have any alive units
        pSelfSquad->setState(SquadState::Wait);
    }
    else if( aliveUnits.size() == 1)
    {
        // Only one alive unit
        _allUnitsTargetIndex[unitSpriteID] = aliveUnits[0];
    }
    else
    {
        // More than one alive units
        // pick a random unit
        int r = (int) (CCRANDOM_0_1() * aliveUnits.size());
        
        _allUnitsTargetIndex[unitSpriteID] = aliveUnits[r];
    }

}

bool HelloWorld::unitAlive(int unitID)
{
	if( _allUnitsHealth.find(unitID) == _allUnitsHealth.end())
		return false;

	return _allUnitsHealth[unitID] > 0;
}

bool HelloWorld::checkSideWin(SquadSide side){
    bool win = true;
    
    for(unsigned int i = 0 ; i < _allSquadsInBattle.size(); i++)
	{
		if(_allSquadsInBattle[i].getSquadSide() != side &&
           _allSquadsInBattle[i].getState() != SquadState::Eliminated)
        {
			win = false;
            break;
        }
	}
    
    return win;
}
                           
void HelloWorld::menuCloseCallback(Ref* pSender)
{
    Director::getInstance()->end();

#if (CC_TARGET_PLATFORM == CC_PLATFORM_IOS)
    exit(0);
#endif
}
