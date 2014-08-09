#ifndef __CHESSBOARD_H_
#define __CHESSBOARD_H_

#include "public.h"
#include <map>
#include "Stone.h"

class ChessBoard: public Sprite
{
private:
	std::map<int,Rect> _stoneLocationMap;
	EventCustom* _endEvent;
	EventCustom* _turnChangeEvent;
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

	void addStoneByIndex(int index,PieceSide);
};

#endif