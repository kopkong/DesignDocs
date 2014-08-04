#include "rule.h"


Rule::Rule()
{

}

Rule::~Rule()
{
}

Rule* Rule::getInstance()
{
	static Rule instance;
	return &instance;
}

void Rule::Init(GameSettings settings)
{
	for(int i = 0 ; i < 15; i++)
		for(int j = 0 ; j < 15; j++)
			_goBangData[i][j] = 0;

	_steps = 0;
	_winner = PieceSide::NoneSide;
	_state = GameState::Running;

	_hasForbidden = settings.hasForbidden;
}

void Rule::setData(int row,int column,PieceSide side)
{
	if(_goBangData[row][column] == 0)
	{
		_goBangData[row][column] = (int)side;
		_steps ++;
	}

	if(_steps >= 9)
	{
		// 检查是否连到5子
		checkSum(row,column,side);

		if(!isFinished())
		{
			if(side == PieceSide::BlackSide && _hasForbidden)// 检查禁手，如果有则判负
				checkForbidden(row,column,side);
		}
	}
}

void Rule::checkSum(int row,int column,PieceSide side)
{
	int fourDirections[4][2] = 
	{
		1,0,		// 垂直方向
		0,1,		// 水平方向
		1,-1,		// 正斜杠方向 "/"
		1,1			// 反斜杠方向 "\"
	};

	int value = (int)side;

	for(int i = 0; i< 4 ; i ++)
	{
		int count = countNumber(row,column,fourDirections[i],value);

		if(side == PieceSide::WhiteSide)
		{
			if(count >= 5 )
			{
				// 白棋赢了
				_state = GameState::Finished;
				_winner = side;
			}
		}
		else
		{
			if(count > 5 && _hasForbidden)
			{
				// 黑棋输了！
				_state = GameState::Finished;
				return;
			}
			else if(count == 5)
			{
				// 黑棋赢了
				_state = GameState::Finished;
				_winner = side;
				return;
			}
		}
	}
}

int Rule::countNumber(int row,int column,int direction[2],int value)
{
	int count = 1;
	int tmpRow = row;
	int tmpColumn = column;

	// 正方向计数
	while(nextSame(tmpRow,tmpColumn,direction,value))
	{
		count ++;
	}

	// 反方向
	tmpRow = row;
	tmpColumn = column;
	int reverseDirection[2] = {-direction[0], -direction[1]};
	while(nextSame(tmpRow,tmpColumn,reverseDirection,value))
	{
		count++;
	}
	
	return count;
}

bool Rule::nextSame(int& row,int& column,const int direction[2],int value)
{
	row = row + direction[0];
	column = column + direction[1];

	// 出界了！
	if(row < 0 || row >= 15 || column < 0 || column >= 15)
		return false;

	if(_goBangData[row][column] == value)
		return true;
	else
		return false;

	return false;
}

void Rule::checkForbidden(int row,int column,PieceSide side)
{
	//return true;
}


// 该方向是否3连
bool Rule::straightThree(int row,int column,int direction[2])
{
	// 有堵死就不会3连
	if(behindBlocked(row,column,direction,(int)PieceSide::BlackSide))
		return false;

	// 格子的数值
	int blackValue = (int)PieceSide::BlackSide;
	int whiteValue = (int)PieceSide::WhiteSide;
	int emptyValue = 0;
	int outsideValue = 9;

	// 先保存正方向的后面5个格子的值
	int nextValue[5] = {0};
	for(int i = 0 ; i < 5 ; i++)
	{
		int nextRow = row + (i + 1) * direction[0];
		int nextColumn = column + (i + 1) * direction[1];
		int nextValue;

		// 出界了！
		if(nextRow < 0 || nextRow >= 15 || nextColumn < 0 || nextColumn >=15)
			nextValue = outsideValue;
		else
			nextValue = _goBangData[nextRow][nextColumn];
	}

	// 前方被堵死，不会3连
	//if(nextValue[0] == outsideValue || nextValue[0] == whiteValue ||
	//	nextValue[1] == outsideValue || nextValue[1] == whiteValue
	//	|| nextValue[2] == outsideValue || nextValue[2] == whiteValue)
	//	return false;

	// Pattern1 ***

	// Pattern2 **-*

	// Pattern3 *-**

	return false;
}

// 该方向是否4连
bool Rule::straightFour(int row,int column,int direction[2])
{
	int count = 1;
	while(nextSame(row,column,direction,(int)PieceSide::BlackSide))
	{
		count ++;
	}

	if(count == 4)
		return true;
	else 
		return false;
}

// 反方向是否被堵死
bool Rule::behindBlocked(int row,int column,const int direction[2],int value)
{
	int behindRow = row - direction[0];
	int behindColumn = column - direction[1];

	// 出界了！堵死！
	if(behindRow < 0 || behindRow >= 15 || behindColumn < 0 || behindColumn >= 15)
		return true;
	
	int behindValue = _goBangData[behindRow][behindColumn];

	// 有白子！堵死！
	if(behindValue != value || behindValue != 0)
		return true;

	// 没有堵死！
	return false;
}

bool Rule::isFinished()
{
	return _state == GameState::Finished;
}

PieceSide Rule::getWinner()
{
	return _winner;
}