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

void Rule::Init()
{
	for(int i = 0 ; i < 15; i++)
		for(int j = 0 ; j < 15; j++)
			_goBangData[i][j] = 0;

	_steps = 0;
	_winner = PieceSide::NoneSide;
	_state = GameState::Running;
}

void Rule::setData(int row,int column,PieceSide side)
{
	if(_goBangData[row][column] == 0)
	{
		_goBangData[row][column] = (int)side;
		_steps ++;
	}

	if(side == PieceSide::BlackSide)// �����֣���������и�
		checkForbidden(row,column,side);

	if(_steps >= 9)
	{
		// ����Ƿ�����5��
		checkSum(row,column,side);
	}
}

void Rule::checkSum(int row,int column,PieceSide side)
{
	int fourDirections[4][2] = 
	{
		1,0,		// ��ֱ����
		0,1,		// ˮƽ����
		1,-1,		// ��б�ܷ��� "/"
		1,1			// ��б�ܷ��� "\"
	};

	int value = (int)side;

	for(int i = 0; i< 4 ; i ++)
	{
		int count = countNumber(row,column,fourDirections[i],value);

		if(side == PieceSide::WhiteSide)
		{
			if(count >= 5 )
			{
				// ����Ӯ��
				_state = GameState::Finished;
				_winner = side;
			}
		}
		else
		{
			if(count > 5)
			{
				// �������ˣ�
				_state = GameState::Finished;
				return;
			}
			else if(count == 5)
			{
				// ����Ӯ��
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
	bool isEnd = false;
	int tmpRow = row;
	int tmpColumn = column;

	// ȷ����һ����ûԽ���߽�
	while(nextSame(tmpRow,tmpColumn,direction,value))
	{
		count ++;
	}

	// ������
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

	// �����ˣ�
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

bool Rule::isFinished()
{
	return _state == GameState::Finished;
}

PieceSide Rule::getWinner()
{
	return _winner;
}