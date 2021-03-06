﻿#include "rule.h"

// 格子的数值
static const std::string BlackValue = "1";
static const std::string WhiteValue = "2";
static const std::string EmptyValue = "0";
static const std::string OutsideValue = "9";

static int FourDirections[4][2] = 
{
	1,0,		// 垂直方向
	0,1,		// 水平方向
	1,-1,		// 正斜杠方向 "/"
	1,1			// 反斜杠方向 "\"
};

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

GameResult Rule::checkSum(int row,int column,PieceSide side,GomokuData data,bool hasBan)
{
	int value = (int)side;

	for(int i = 0; i< 4 ; i ++)
	{
		int count = countNumber(row,column,FourDirections[i],value,data);

		if(side == PieceSide::WhiteSide)
		{
			if(count >= 5 )
			{
				// 白棋赢了
				return GameResult::WhiteWin;
			}
		}
		else
		{
			if(count > 5 && hasBan)
			{
				// 黑棋输了！
				return GameResult::BlackBan;
			}
			else if(count == 5)
			{
				// 黑棋赢了
				return GameResult::BlackWin;
			}
		}
	}

	return NoResult;
}

int Rule::countNumber(int row,int column,int direction[2],int value,GomokuData data)
{
	int count = 1;
	int tmpRow = row;
	int tmpColumn = column;

	// 正方向计数
	while(nextSame(tmpRow,tmpColumn,direction,value,data))
	{
		count ++;
	}

	// 反方向
	tmpRow = row;
	tmpColumn = column;
	int reverseDirection[2] = {-direction[0], -direction[1]};
	while(nextSame(tmpRow,tmpColumn,reverseDirection,value,data))
	{
		count++;
	}
	
	return count;
}

bool Rule::nextSame(int& row,int& column,const int direction[2],int value,GomokuData data)
{
	row = row + direction[0];
	column = column + direction[1];

	// 出界了！
	if(row < 0 || row >= 15 || column < 0 || column >= 15)
		return false;

	if(data[row][column] == value)
		return true;
	else
		return false;

	return false;
}

GameResult Rule::checkForbidden(int row,int column,PieceSide side,GomokuData data)
{
	int threeThreeCount = 0;
	int fourFourCount = 0;
	for(int i = 0; i< 4 ; i ++)
	{
		std::string baseString;
		buildBasePieceString(row,column,FourDirections[i],baseString,data);

		// 在一个方向上的四四禁
		if(matchBanRuleOne(baseString))
		{
			return GameResult::BlackBan;
		}
			
		if(matchBanRuleTwo(baseString))
			threeThreeCount++;

		if(matchBanRuleThree(baseString))
			fourFourCount++;
	}

	// 三三 或者 四四禁
	if(threeThreeCount >= 2 || fourFourCount >= 2)
	{
		return GameResult::BlackBan;
	}

	return NoResult;
}

bool Rule::matchBanRuleOne(const std::string baseString)
{
	const int maxPattern = 14;
	// 模式字符串
	std::string patterns[maxPattern] = {
		//黑黑空黑黑空黑黑
		"0110110110",
		"9110110110",
		"2110110110",
		"0110110119",
		"0110110112",
		"9110110119",
		"2110110112",

		//黑空黑黑黑空黑
		"010111010",
		"910111010",
		"210111010",
		"010111019",
		"010111012",
		"910111019",
		"210111012",
	};

	// 模式搜索
	for(int i = 0; i < maxPattern; i++)
	{
		if(baseString.find(patterns[i]) != std::string::npos)
		{
			// 成功匹配
			log("match the pattern %s",patterns[i].c_str());

			return true;
		}
	}

	return false;
}

bool Rule::matchBanRuleTwo(const std::string baseString)
{
	const int maxPattern = 15;
	// 模式字符串
	std::string patterns[maxPattern] = {
		//黑黑黑
		"0011100",
		"9011100",
		"2011100",
		"0011109",
		"0011102",

		//黑空黑黑
		"00101100",
		"90101100",
		"20101100",
		"00101109",
		"00101102",

		//黑黑空黑
		"00110100",
		"90110100",
		"20110100",
		"00110109",
		"00110102",

	};

	// 模式搜索
	for(int i = 0; i < maxPattern; i++)
	{
		if(baseString.find(patterns[i]) != std::string::npos)
		{
			// 成功匹配
			log("match the pattern %s",patterns[i].c_str());

			return true;
		}
	}

	return false;
}

bool Rule::matchBanRuleThree(const std::string baseString)
{
	const int maxPattern = 20;

	// 模式字符串
	std::string patterns[maxPattern] = {
		//黑黑黑黑
		"011110",
		"911110",
		"211110",
		"011119",
		"011112",

		//黑黑空黑黑
		"0110110",
		"9110110",
		"2110110",
		"0110119",
		"0110112",

		//黑空黑黑黑
		"0101110",
		"9101110",
		"2101110",
		"0101119",
		"0101112",

		//黑黑黑空黑
		"0111010",
		"9111010",
		"2111010",
		"0111019",
		"0111012",

	};

	// 模式搜索
	for(int i = 0; i < maxPattern; i++)
	{
		if(baseString.find(patterns[i]) != std::string::npos)
		{
			// 成功匹配
			log("match the pattern %s",patterns[i].c_str());

			return true;
		}
	}

	return false;
}

// 反方向是否被堵死
bool Rule::behindBlocked(int row,int column,const int direction[2],int value,GomokuData data)
{
	int behindRow = row - direction[0];
	int behindColumn = column - direction[1];

	// 出界了！堵死！
	if(behindRow < 0 || behindRow >= 15 || behindColumn < 0 || behindColumn >= 15)
		return true;
	
	int behindValue = data[behindRow][behindColumn];

	// 有白子！堵死！
	if(behindValue != value || behindValue != 0)
		return true;

	// 没有堵死！
	return false;
}



void Rule::buildBasePieceString(int row,int column,int direction[2],std::string& baseString,GomokuData data)
{
	// 保存正面5个 反面5个 共11个格子的值

	// 反方向
	for(int i = 0; i< 5 ; i++)
	{
		int nextRow = row - ( 5 - i ) * direction[0];
		int nextColumn = column - ( 5 - i ) * direction[1];

		baseString += getStringValue(nextRow,nextColumn,data);
	}

	// 下的那个子本身
	baseString += BlackValue;

	// 正方向
	for(int i = 6 ; i < 11 ; i++)
	{
		int nextRow = row + ( i -5 ) * direction[0];
		int nextColumn = column + ( i -5 ) * direction[1];

		baseString += getStringValue(nextRow,nextColumn,data);
	}
}

std::string Rule::getStringValue(int row,int column,GomokuData data)
{
	// 出界了！
	if(row < 0 || row >= 15 || column < 0 || column >=15)
		return OutsideValue;
	else
	{
		int v = data[row][column];
		
		switch(v)
		{
			case 1:
				return BlackValue;
			case 2:
				return WhiteValue;
			case 0:
				return EmptyValue;
			default:
				return EmptyValue;
		}
	}
}

//void Rule::blackBanLose()
//{
//	_state = GameState::Finished;
//	_winner = PieceSide::WhiteSide;
//}