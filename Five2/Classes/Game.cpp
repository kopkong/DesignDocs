#include "Game.h"
#include "Rule.h"

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
	for(int i = 0 ; i < 15; i++)
		for(int j = 0 ; j < 15; j++)
			_data[i][j] = 0;

	_steps = 0;
	_result = GameResult::NoResult;

	_hasForbidden = settings.hasForbidden;
	_currentSide = PieceSide::BlackSide;
}

void Game::setData(int index,PieceSide side)
{
	int row = index /15;
	int column = index % 15;

	if(_data[row][column] == 0)
	{
		_data[row][column] = (int)side;
		_steps ++;
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

bool Game::nextTurn()
{
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

PieceSide Game::getTurn()
{
	return _currentSide;
}