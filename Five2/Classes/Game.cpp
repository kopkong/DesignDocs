#include "Game.h"
#include "Rule.h"
#include "AI.h"

Game::Game()
{
	_currentSide = PieceSide::NoneSide;
}

Game::~Game()
{

}

Game* Game::getInstance()
{
	static Game instance;
	return &instance;
}

void Game::init(GameSettings settings)
{
	_steps = 0;
	_result = GameResult::NoResult;

	_hasForbidden = settings.hasForbidden;
	_currentSide = PieceSide::BlackSide;

	// AI
	_hasAI = settings.hasAI;
	_aiSide = settings.aiSide;
	
	for(int i = 0 ; i < 15; i++)
		for(int j = 0 ; j < 15; j++)
			_data[i][j] = 0;

	//清空记录步数的栈
	while(!_stepContent.empty())
	{
		_stepContent.pop();
	}
}

bool Game::setData(int index,PieceSide side)
{
	int row,column = 0;
	getRCByIndex(index,row,column);

	if(_data[row][column] == 0)
	{
		_data[row][column] = (int)side;
		_steps ++;

		// 记步
		StepContent c;
		c.side = side;
		c.index = index;
		_stepContent.push(c);
	}

	if(_steps >= 9)
	{
		// 检查是否连到5子
		_result = Rule::getInstance()->checkSum(row,column,side,_data,_hasForbidden);

		if(!isFinished())
		{
			if(side == PieceSide::BlackSide && _hasForbidden)// 检查禁手，如果有则判负
				_result = Rule::getInstance()->checkForbidden(row,column,side,_data);
		}
	}

	if(_result == NoResult)
	{
		if(_currentSide == BlackSide)
			_currentSide = WhiteSide;
		else if(_currentSide == WhiteSide)
			_currentSide = BlackSide;

		return true;
	}

	//没有下一个回合了
	return false;
}

bool Game::isFinished()
{
	return _result != GameResult::NoResult;
}

void Game::setTimeout(PieceSide side)
{
	if(side == BlackSide)
		_result = BlackTimeOut;
	else if (side == WhiteSide)
		_result = WhiteTimeOut;
}

PieceSide Game::getWinner()
{
	if(_result == BlackWin || WhiteTimeOut )
		return PieceSide::WhiteSide;
	else
		return PieceSide::BlackSide;
}

GameResult Game::getResult()
{
	return _result;
}

PieceSide Game::getTurn()
{
	return _currentSide;
}

void Game::backTurn(int turns,std::queue<StepContent>& stepContents)
{
	if(turns > _steps || turns <= 0)
		return ;

	{
		int c = turns;
		while(c > 0)
		{
			_stepContent.pop();
			StepContent s = _stepContent.top();

			int row,column = 0;
			getRCByIndex(s.index,row,column);

			_data[row][column] = 0;

			// 存入队列
			stepContents.push(s);
		}

		_steps -= turns;
	}
}

bool Game::isPlayerTurn()
{
	if(_hasAI)
	{
		if(_currentSide == _aiSide)
			return false;
		else
			return true;
	}

	return true;
}

int Game::getAIChoosedNumber()
{
	return AI::getInstance()->thinkNextMove(_data,_aiSide);
}

void Game::copyData(GomokuData* tmpData)
{
	memcpy(tmpData,&_data,sizeof(GomokuData));
}