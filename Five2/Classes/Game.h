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

	// �ǲ����˻�
	bool _hasAI;

	// AI�Ǻ��ӻ��ǰ���
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

	// ����(����)
	void backTurn(int turns,std::queue<StepContent>&);

	int getAIChoosedNumber();

	PieceSide getTurn();

	bool isPlayerTurn();

	void copyData(GomokuData* tmpData);
};

#endif