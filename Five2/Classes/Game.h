#ifndef __GAME_H_
#define __GAME_H_

#include "public.h"
#include <stack>

class Game
{
private:
	GomokuData _data;
	GameResult _result;

	int _steps;
	bool _hasForbidden;
	PieceSide _currentSide;
	std::stack<StepContent> _stepContent;

	// 是不是人机
	bool _hasAI;

	// AI是黑子还是白子
	PieceSide _aiSide;

protected:
	Game();
	~Game();

public:
	static Game* getInstance();
	void init(GameSettings);
	bool setData(int index,PieceSide side);
	bool isFinished();

	void setTimeout(PieceSide side);
	GameResult getResult();
	PieceSide getWinner();

	// 悔棋(步数)
	void backTurn(int turns,std::queue<StepContent>&);

	int getAIChoosedNumber();

	PieceSide getTurn();

	bool isPlayerTurn();

	void copyData(GomokuData* tmpData);
};

#endif