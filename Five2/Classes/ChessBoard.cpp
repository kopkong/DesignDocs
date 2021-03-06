﻿#include "ChessBoard.h"
#include "Game.h"

#include "AssertConfigs.h"

const static Size CellSize(50,50);
const static int RenderIndex_Stone = 2;

ChessBoard::ChessBoard()
{
	for(int row = 0 ; row < 15; row ++)
	{
		for(int column = 0 ; column < 15; column ++)
		{
			float positionX = column * CellSize.width + 9  ;
			float positionY = row * CellSize.height + 2 ;
			Rect r = Rect(positionX,positionY,CellSize.width,CellSize.height);

			int index = getIndexByRC(row,column);
			_stoneLocationMap.insert(std::pair<int,Rect>(index,r));
			_stoneLocationOccupied.insert(std::pair<int,bool>(index,false));
		}
	}
}

ChessBoard::~ChessBoard()
{

}

Rect ChessBoard::getRect()
{
	auto s = getTexture()->getContentSize();
	return Rect(0,0, s.width,s.height);
}

ChessBoard* ChessBoard::createWithSpriteFrameName(const std::string frameName)
{
	ChessBoard* pChessBoard = new ChessBoard();
	pChessBoard->initWithSpriteFrameName(frameName);

	return pChessBoard;
}

bool ChessBoard::initWithSpriteFrameName(const std::string frameName)
{
	Sprite::initWithSpriteFrameName(frameName);

	return true;
}

void ChessBoard::onEnter()
{
	Sprite::onEnter();

	auto listener = EventListenerTouchOneByOne::create();
	listener->setSwallowTouches(true);

	listener->onTouchBegan = CC_CALLBACK_2(ChessBoard::onTouchBegan,this);
	listener->onTouchMoved = CC_CALLBACK_2(ChessBoard::onTouchMoved,this);
	listener->onTouchEnded = CC_CALLBACK_2(ChessBoard::onTouchEnded,this);

	_eventDispatcher->addEventListenerWithSceneGraphPriority(listener,this);

	_endEvent = new EventCustom("event_game_end");
	_turnChangeEvent = new EventCustom("event_turn_change");

	_listener = EventListenerCustom::create("event_ai_setStone",[=](EventCustom* event){
		int* pIndex = static_cast<int*>(event->getUserData());
		addStoneByIndex(*pIndex,Game::getInstance()->getTurn());
	});

	_eventDispatcher->addEventListenerWithFixedPriority(_listener,1);
}

bool ChessBoard::containsTouchLocation(Touch* touch)
{
	return getRect().containsPoint(convertTouchToNodeSpaceAR(touch));
}

void ChessBoard::onExit()
{
	Sprite::onExit();
}

bool ChessBoard::onTouchBegan(Touch* touch, Event* event)
{
	log("ChessBoard::onTouchBegan id = %d, x = %f, y = %f", touch->getID(), touch->getLocation().x, touch->getLocation().y);

	if(!containsTouchLocation(touch)) return false;
	if(!Game::getInstance()->isPlayerTurn()) return false;
	if(Game::getInstance()->isFinished()) return false;
	
	for(std::map<int,Rect>::iterator it = _stoneLocationMap.begin(); it != _stoneLocationMap.end(); it++)
	{
		if(it->second.containsPoint(convertTouchToNodeSpaceAR(touch)))
		{
			_hoverStone = Stone::createWithSpriteFrameName(SPRITECACHE_NAME_HOVERSTONE);
			_hoverStone->setAnchorPoint(Point::ZERO);
			_hoverStone->setPosition(it->second.getMinX(),it->second.getMinY());
			addChild(_hoverStone,RenderIndex_Stone);
		}
	}

	log("onTouchBegan return true");
	return true;
}

void ChessBoard::onTouchMoved(Touch* touch, Event* event)
{
	log("ChessBoard::onTouchMoved id = %d, x = %f, y = %f", touch->getID(), touch->getLocation().x, touch->getLocation().y);

	for(std::map<int,Rect>::iterator it = _stoneLocationMap.begin(); it != _stoneLocationMap.end(); it++)
	{
		if(it->second.containsPoint(convertTouchToNodeSpaceAR(touch)))
		{
			_hoverStone->setPosition(it->second.getMinX(),it->second.getMinY());
			//_hoverStone->setVisible(true);
		}
	}

}

void ChessBoard::onTouchEnded(Touch* touch, Event* event)
{
	log("ChessBoard::onTouchEnded id = %d, x = %f, y = %f", touch->getID(), touch->getLocation().x, touch->getLocation().y);
	
	for(std::map<int,Rect>::iterator it = _stoneLocationMap.begin(); it != _stoneLocationMap.end(); it++)
	{
		if(it->second.containsPoint(convertTouchToNodeSpaceAR(touch)))
		{
			// 不再显示悬浮框了
			_hoverStone->setVisible(false);

			// 已经在这个位置下过子了
			if(_stoneLocationOccupied[it->first])
				break;

			addStoneByIndex(it->first,Game::getInstance()->getTurn());
		}
	}
}

void ChessBoard::clearStones()
{
	this->removeAllChildren();
}

void ChessBoard::removeStoneByIndex(int index)
{
	
}

void ChessBoard::addStoneByIndex(int index, PieceSide side)
{
	log("Set side: %d stone at index:%d",side,index);

	// 图片偏移，为了显示阴影效果
	Point offset = Point(3,12);
	Sprite* stone = Sprite::create();

	// 设置黑子或者白子的图片
	if(side == BlackSide)
		stone->setSpriteFrame(SPRITECACHE_NAME_BLACKSTONE);
	else
		stone->setSpriteFrame(SPRITECACHE_NAME_WHITESTONE);

	stone->setAnchorPoint(Point::ZERO);
	
	stone->setPosition(_stoneLocationMap.at(index).getMinX() - offset.x ,
		_stoneLocationMap.at(index).getMinY() - offset.y);

	this->addChild(stone,RenderIndex_Stone);
	_stoneLocationOccupied[index] = true;

	// 落子并判断是否结束
	if(Game::getInstance()->setData(index,side))
		_eventDispatcher->dispatchEvent(_turnChangeEvent);
	else
		_eventDispatcher->dispatchEvent(_endEvent);
}