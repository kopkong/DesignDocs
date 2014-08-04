#ifndef _Rule_h
#define _Rule_h

#include "public.h"

// GoBang rules
class Rule
{
private:
	int _goBangData[15][15];
	int _steps;

	bool nextSame(int& row,int& column,const int direction[2],int value);

	// 另一个方向是否已经被堵死
	bool behindBlocked(int row,int column,const int direction[2],int value);

	int countNumber(int row,int column,int direction[2],int value);

	// 3连
	bool straightThree(int row,int column,int direction[2]);
	// 4连
	bool straightFour(int row,int column,int direction[2]);

	// 计数规则
	void checkSum(int row,int column,PieceSide side);

	// 禁手规则
	void checkForbidden(int row,int column,PieceSide side);

	GameState _state;

	PieceSide _winner;

	bool _hasForbidden;

protected:
	Rule();
	~Rule();

public:
	static Rule* getInstance();

	void Init(GameSettings);

	void setData(int row,int column,PieceSide side);

	bool isFinished();

	PieceSide getWinner();

};

#endif