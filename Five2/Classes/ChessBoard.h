#ifndef __CHESSBOARD_H_
#define __CHESSBOARD_H_

#include "public.h"
#include <map>
#include "Stone.h"

class ChessBoard: public Sprite
{
private:
	std::map<int,Rect> _stoneLocationMap;
	std::map<int,bool> _stoneLocationOccupied;
	EventCustom* _endEvent;
	EventCustom* _turnChangeEvent;

	EventListenerCustom* _listener;
	Stone* _hoverStone;
public:
	ChessBoard();
	~ChessBoard();

	Rect getRect();
	bool initWithSpriteFrameName(const std::string);
	virtual void onEnter() override;
	virtual void onExit() override;

	bool containsTouchLocation(Touch* touch);
	bool onTouchBegan(Touch*,Event*);
	void onTouchMoved(Touch*,Event*);
	void onTouchEnded(Touch*,Event*);

	static ChessBoard* createWithSpriteFrameName(const std::string);

	void clearStones();

	void removeStoneByIndex(int index);

	void addStoneByIndex(int index, PieceSide side);
};

#endif