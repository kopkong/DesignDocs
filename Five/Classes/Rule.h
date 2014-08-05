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

	// 另一个方向是否已经被堵死
	bool behindBlocked(int row,int column,const int direction[2],int value);

	int countNumber(int row,int column,int direction[2],int value);

	// 匹配禁手规则1 （在同一个方向的四四禁）
	bool matchBanRuleOne(const std::string baseString);

	// 匹配禁手规则2 (在不同方向的三三禁）
	bool matchBanRuleTwo(const std::string baseString);

	// 匹配禁手规则2 (在不同方向的四四禁）
	bool matchBanRuleThree(const std::string baseString);

	// 是否活四
	// bool hasLiveFour(int row,int column,int direction[2]);

	// 计数规则
	void checkSum(int row,int column,PieceSide side);

	// 禁手规则
	void checkForbidden(int row,int column,PieceSide side);

	GameState _state;

	PieceSide _winner;

	bool _hasForbidden;

	void buildBasePieceString(int row,int column,int direction[2],std::string &str);

	std::string getStringValue(int row,int column);

	// 黑方禁手判负
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