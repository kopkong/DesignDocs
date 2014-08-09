#ifndef __GAME_H_
#define __GAME_H_

#include "public.h"

class Game
{
private:
	GomokuData _data;
	GameResult _result;

	int _steps;
	bool _hasForbidden;
	PieceSide _currentSide;

protected:
	Game();
	~Game();

public:
	static Game* getInstance();
	void init(GameSettings);
	void setData(int index,PieceSide side);
	bool isFinished();

	void setTimeout(PieceSide side);
	GameResult getResult();
	PieceSide getWinner();

	// ÏÂ»ØºÏ
	bool nextTurn();
	PieceSide getTurn();

	bool isRunnint();
};

#endif