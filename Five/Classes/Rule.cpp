#include "rule.h"

// ���ӵ���ֵ
static const std::string BlackValue = "1";
static const std::string WhiteValue = "2";
static const std::string EmptyValue = "0";
static const std::string OutsideValue = "9";

static int FourDirections[4][2] = 
{
	1,0,		// ��ֱ����
	0,1,		// ˮƽ����
	1,-1,		// ��б�ܷ��� "/"
	1,1			// ��б�ܷ��� "\"
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
		// ����Ƿ�����5��
		checkSum(row,column,side);

		if(!isFinished())
		{
			if(side == PieceSide::BlackSide && _hasForbidden)// �����֣���������и�
				checkForbidden(row,column,side);
		}
	}
}

void Rule::checkSum(int row,int column,PieceSide side)
{
	int value = (int)side;

	for(int i = 0; i< 4 ; i ++)
	{
		int count = countNumber(row,column,FourDirections[i],value);

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
			if(count > 5 && _hasForbidden)
			{
				// �������ˣ�
				blackBanLose();
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
	int tmpRow = row;
	int tmpColumn = column;

	// ���������
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
	int threeThreeCount = 0;
	int fourFourCount = 0;
	for(int i = 0; i< 4 ; i ++)
	{
		std::string baseString;
		buildBasePieceString(row,column,FourDirections[i],baseString);

		// ��һ�������ϵ����Ľ�
		if(matchBanRuleOne(baseString))
		{
			blackBanLose();
			break;
		}
			
		if(matchBanRuleTwo(baseString))
			threeThreeCount++;

		if(matchBanRuleThree(baseString))
			fourFourCount++;
	}

	// ���� ���� ���Ľ�
	if(threeThreeCount >= 2 || fourFourCount >= 2)
	{
		blackBanLose();
	}

}

bool Rule::matchBanRuleOne(const std::string baseString)
{
	const int maxPattern = 14;
	// ģʽ�ַ���
	std::string patterns[maxPattern] = {
		//�ںڿպںڿպں�
		"0110110110",
		"9110110110",
		"2110110110",
		"0110110119",
		"0110110112",
		"9110110119",
		"2110110112",

		//�ڿպںںڿպ�
		"010111010",
		"910111010",
		"210111010",
		"010111019",
		"010111012",
		"910111019",
		"210111012",
	};

	// ģʽ����
	for(int i = 0; i < maxPattern; i++)
	{
		if(baseString.find(patterns[i]) != std::string::npos)
		{
			// �ɹ�ƥ��
			log("match the pattern %s",patterns[i].c_str());

			return true;
		}
	}

	return false;
}

bool Rule::matchBanRuleTwo(const std::string baseString)
{
	const int maxPattern = 15;
	// ģʽ�ַ���
	std::string patterns[maxPattern] = {
		//�ںں�
		"0011100",
		"9011100",
		"2011100",
		"0011109",
		"0011102",

		//�ڿպں�
		"00101100",
		"90101100",
		"20101100",
		"00101109",
		"00101102",

		//�ںڿպ�
		"00110100",
		"90110100",
		"20110100",
		"00110109",
		"00110102",

	};

	// ģʽ����
	for(int i = 0; i < maxPattern; i++)
	{
		if(baseString.find(patterns[i]) != std::string::npos)
		{
			// �ɹ�ƥ��
			log("match the pattern %s",patterns[i].c_str());

			return true;
		}
	}

	return false;
}

bool Rule::matchBanRuleThree(const std::string baseString)
{
	const int maxPattern = 20;

	// ģʽ�ַ���
	std::string patterns[maxPattern] = {
		//�ںںں�
		"011110",
		"911110",
		"211110",
		"011119",
		"011112",

		//�ںڿպں�
		"0110110",
		"9110110",
		"2110110",
		"0110119",
		"0110112",

		//�ڿպںں�
		"0101110",
		"9101110",
		"2101110",
		"0101119",
		"0101112",

		//�ںںڿպ�
		"0111010",
		"9111010",
		"2111010",
		"0111019",
		"0111012",

	};

	// ģʽ����
	for(int i = 0; i < maxPattern; i++)
	{
		if(baseString.find(patterns[i]) != std::string::npos)
		{
			// �ɹ�ƥ��
			log("match the pattern %s",patterns[i].c_str());

			return true;
		}
	}

	return false;
}

// �������Ƿ񱻶���
bool Rule::behindBlocked(int row,int column,const int direction[2],int value)
{
	int behindRow = row - direction[0];
	int behindColumn = column - direction[1];

	// �����ˣ�������
	if(behindRow < 0 || behindRow >= 15 || behindColumn < 0 || behindColumn >= 15)
		return true;
	
	int behindValue = _goBangData[behindRow][behindColumn];

	// �а��ӣ�������
	if(behindValue != value || behindValue != 0)
		return true;

	// û�ж�����
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

void Rule::buildBasePieceString(int row,int column,int direction[2],std::string& baseString)
{
	// ��������5�� ����5�� ��11�����ӵ�ֵ

	// ������
	for(int i = 0; i< 5 ; i++)
	{
		int nextRow = row - ( 5 - i ) * direction[0];
		int nextColumn = column - ( 5 - i ) * direction[1];

		baseString += getStringValue(nextRow,nextColumn);
	}

	// �µ��Ǹ��ӱ���
	baseString += BlackValue;

	// ������
	for(int i = 6 ; i < 11 ; i++)
	{
		int nextRow = row + ( i -5 ) * direction[0];
		int nextColumn = column + ( i -5 ) * direction[1];

		baseString += getStringValue(nextRow,nextColumn);
	}
}

std::string Rule::getStringValue(int row,int column)
{
	// �����ˣ�
	if(row < 0 || row >= 15 || column < 0 || column >=15)
		return OutsideValue;
	else
	{
		int v = _goBangData[row][column];
		
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

void Rule::blackBanLose()
{
	_state = GameState::Finished;
	_winner = PieceSide::WhiteSide;
}