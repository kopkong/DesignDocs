#include "HelloWorldScene.h"
USING_NS_CC;

// Constant paras
const Size HEROSIZE(24,24);
const Size SOLDIERSIZE(16,16);

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
    if(_squadListA.size() > 0){
        for(int i = 0; i < _squadListA.size(); i++)
        {
            Squad* sq = &_squadListA[i];
            switch(sq->getState()){
                case SquadState::Moving:
                {
                    //log("Squad %s is moving",sq->getName().c_str());
                    squadMove(sq,dt);
                    break;
                }
                case SquadState::BattleBegin:
                {
                    log("Battle begin, squad %s is ready",sq->getName().c_str());
                    battleBegin(sq);
                    sq->setState(SquadState::Moving);
                    //log("Squad %s state is %d", sq->getName().c_str(), sq->getState());
                    break;
                }
                default:
                    break;
            }
            
        }
    }
    
//    if(_squadListB.size() > 0){
//        for(int i =0; i <_squadListB.size(); i ++)
//        {
//            squad(_squadListB[i],dt);
//        }
//    }
}

void HelloWorld::battleBegin(Squad* sq){
    // If thers is any special effect or skill should be triggered at this time
    
    // Change the state to moving,
    sq->setState(SquadState::Moving);
    
    // At the beginning , we set the target direction is straight forward.
    if(sq->faceToRight())
        sq->setTargetPosition(Point(1024,sq->getPosition().y));
    else
        sq->setTargetPosition(Point(0,sq->getPosition().y));
}

void HelloWorld::squadMove(Squad* sq,float dt){
    if(sq->getState() != SquadState::Moving)
        return;
    
    // Hero
    bool up ;
    
    Point direction = sq->getTargetPosition() - sq->getPosition();
    if(direction.x > 0)
    {
        sq->setFaceTo(SquadFaceTo::Right);
    }
    else{
        sq->setFaceTo(SquadFaceTo::Left);
    }
    
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
    
    // Hero
    if(!sq->getHeroDead()){
        int heroIndex = getHeroSpriteID(sq->getIndex());
        Sprite* heroSprite = _allUnitsSprite[heroIndex];
    
        Point heroNextPosition;
        if(sq->faceToRight()){
            heroNextPosition.x = heroSprite->getPositionX() + dt * sq->getSpeed();
        }
        else{
            heroNextPosition.x = heroSprite->getPositionX() - dt * sq->getSpeed();
        }
        
        if(up){
            heroNextPosition.y = heroSprite->getPositionY() + tan * dt * sq->getSpeed();
        }
        else{
            heroNextPosition.y = heroSprite->getPositionY() - tan * dt * sq->getSpeed();
        }
    
        // If hero will move outside the screen, stop moving.
        if(heroNextPosition.x > _screenSize.width - HEROSIZE.width ||
           heroNextPosition.x < HEROSIZE.width ||
           heroNextPosition.y > _screenSize.height - HEROSIZE.height ||
           heroNextPosition.y < HEROSIZE.height)
        {
            sq->setState(SquadState::Wait);
            log("Squad %s reached the edge and stop moving",sq->getName().c_str());
            return;
        }
        else{
            heroSprite->setPosition(heroNextPosition);
            
            // Here update the squad's position too.
            sq->setPosition(heroNextPosition);
        }
    }
    
    // XY direction 
    for(unsigned int i = 0 ; i< sq->getSoldierCount();i++){
        int soldierIndex = getSoldierSpriteID(sq->getIndex(), i);
        Sprite* soldierSprite = _allUnitsSprite[soldierIndex];
        Point soldierNextPosition;
        
        if(sq->faceToRight()){
            soldierNextPosition.x = soldierSprite->getPositionX() + dt * sq->getSpeed();
        }
        else{
            soldierNextPosition.x = soldierSprite->getPositionX() - dt * sq->getSpeed();
        }
        
        if(up){
            soldierNextPosition.y = soldierSprite->getPositionY() + tan * dt * sq->getSpeed();
        }
        else{
            soldierNextPosition.y = soldierSprite->getPositionY() - tan * dt * sq->getSpeed();
        }
        
        // If hero will  outside the screen, stop moving.
        if(soldierNextPosition.x > _screenSize.width - SOLDIERSIZE.width ||
           soldierNextPosition.x < SOLDIERSIZE.width ||
           soldierNextPosition.y > _screenSize.height - SOLDIERSIZE.height ||
           soldierNextPosition.y < SOLDIERSIZE.height)
        {
            sq->setState(SquadState::Wait);
            log("Squad %s reached the edge and stop moving",sq->getName().c_str());
            return;
        }
        else
        {
            soldierSprite->setPosition(soldierNextPosition);
            
            // If hero died , update squad's position with the first soldier's position
            if(sq->getHeroDead() && i == 0)
            {
                sq->setPosition(soldierNextPosition);
            }
        }
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
    if(_squadListA.size() > 0){
        for(int i = 0; i < _squadListA.size(); i++)
        {
            drawSquad(&_squadListA[i]);
        }
    }
    
    if(_squadListB.size() > 0){
        for(int i = 0; i < _squadListB.size(); i++)
        {
            drawSquad(&_squadListB[i]);
        }
    }
    
}

void HelloWorld::drawSquad(Squad* sq){
    // Texture rect
    Sprite* hero = Sprite::create("hero.png");
    hero->setScale(0.5,0.5);
    hero->setPosition(sq->getPosition());
	saveHeroSprite(sq->getIndex(), hero);
    this->addChild(hero,1);
    
    for(unsigned int i = 0 ; i< sq->getSoldierCount();i++){
        std::string typePng = "footman.png";
        if(sq->getSoldierType() == SquadType::Knight)
            typePng = "knight.png";
        if(sq->getSoldierType() == SquadType::Archer)
            typePng = "archer.png";
        
        auto soldier = Sprite::create(typePng);
        soldier->setScale(0.5,0.5);
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
        sq->_soldiersHealth.push_back(100);
        this->addChild(soldier,1);
    }
}

void HelloWorld::searchEnemy(Squad* sq){
    // First make sure squad is in moving state
    if(sq->getState() != SquadState::Moving)
        return;
    
    
    if(sq->getIndex() < 9)
    {
        // It's squad in ListA
        // search ListB
        
        for(int i= 0 ;i < _squadListB.size() ; i++)
        {
            float distance = pow((sq->getPosition().x - _squadListB[i].getPosition().x),2) + pow((sq->getPosition().y - _squadListB[i].getPosition().y),2);
            
            if(distance <= sq->getAttackRange())
            {
                log("Squad %s found it's target is Squad %s", sq->getName().c_str(), _squadListB[i].getName().c_str());
                
                // Locate the target and aim to it
                sq->setTargetPosition(_squadListB[i].getPosition());
            }
        }
    }
    else // Squad in ListB
    {
        // Search ListA
        for(int i= 0 ;i < _squadListA.size() ; i++)
        {
            float distance = pow((sq->getPosition().x - _squadListA[i].getPosition().x),2) + pow((sq->getPosition().y - _squadListA[i].getPosition().y),2);
            
            if(distance <= sq->getAttackRange())
            {
                log("Squad %s found it's target is Squad %s", sq->getName().c_str(), _squadListA[i].getName().c_str());
                
                // Locate the target and aim to it
                sq->setTargetPosition(_squadListA[i].getPosition());
            }
        }
    }
}

void HelloWorld::initSquads(){
    // Create left side squads
    int indexCount = 0;
    for(int i = 0; i < 9; i++){
        char buff[100];
        sprintf(buff,"a%d",i);
        std::string name = buff;
        
        int col = i / 3;
        int row = i % 3;
        
		Point p(_screenSize.width/2 - 20 - SQUADBLOCKSIZE.width * col,
                _screenSize.height  - SQUADBLOCKSIZE.height/2 - SQUADBLOCKSIZE.height * row);
        
        log("Squad %s position is(%f,%f)", name.c_str(), p.x,p.y);
        Squad a = Squad(name,p,indexCount);
        
        if(col == 0)
            a.setSoldierType(SquadType::Footman);
        if(col == 1)
            a.setSoldierType(SquadType::Knight);
        if(col == 2)
            a.setSoldierType(SquadType::Archer);
        
        indexCount ++;
        _squadListA.push_back(a);
    }
    
    // Create right side squads
    for(int i = 0; i < 9; i++){
        char buff[100];
        sprintf(buff,"b%d",i);
        std::string name = buff;
        
        int col = i / 3;
        int row = i % 3;
        
		Point p(_screenSize.width/2 + 20 + SQUADBLOCKSIZE.width * col,
                _screenSize.height  - SQUADBLOCKSIZE.height/2 - SQUADBLOCKSIZE.height * row);
        
        log("Squad %s position is(%f,%f)", name.c_str(), p.x,p.y);
        Squad b = Squad(name,p,indexCount);
        indexCount ++;
        if(col == 0)
            b.setSoldierType(SquadType::Footman);
        if(col == 1)
            b.setSoldierType(SquadType::Knight);
        if(col == 2)
            b.setSoldierType(SquadType::Archer);
        _squadListB.push_back(b);
    }
    
    drawAll();
}


void HelloWorld::menuCloseCallback(Ref* pSender)
{
    Director::getInstance()->end();

#if (CC_TARGET_PLATFORM == CC_PLATFORM_IOS)
    exit(0);
#endif
}
