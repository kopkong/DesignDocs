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

	// ��һ�������Ƿ��Ѿ�������
	bool behindBlocked(int row,int column,const int direction[2],int value);

	int countNumber(int row,int column,int direction[2],int value);

	// ƥ����ֹ���1 ����ͬһ����������Ľ���
	bool matchBanRuleOne(const std::string baseString);

	// ƥ����ֹ���2 (�ڲ�ͬ�������������
	bool matchBanRuleTwo(const std::string baseString);

	// ƥ����ֹ���2 (�ڲ�ͬ��������Ľ���
	bool matchBanRuleThree(const std::string baseString);

	// �Ƿ����
	// bool hasLiveFour(int row,int column,int direction[2]);

	// ��������
	void checkSum(int row,int column,PieceSide side);

	// ���ֹ���
	void checkForbidden(int row,int column,PieceSide side);

	GameState _state;

	PieceSide _winner;

	bool _hasForbidden;

	void buildBasePieceString(int row,int column,int direction[2],std::string &str);

	std::string getStringValue(int row,int column);

	// �ڷ������и�
	void blackBanLose();

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