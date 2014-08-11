#include "Stone.h"

Stone::Stone()
{

}

Stone::~Stone()
{

}

Stone* Stone::createWithSpriteFrameName(const std::string frameName)
{
	Stone* pStone = new Stone();
	pStone->initWithSpriteFrameName(frameName);
	pStone->autorelease();

	return pStone;
}


bool Stone::initWithSpriteFrameName(const std::string frameName)
{
	Sprite::initWithSpriteFrameName(frameName);
	return true;
}

void Stone::onEnter()
{
	Sprite::onEnter();
}

void Stone::onExit()
{
	Sprite::onExit();
}