#ifndef _Rule_h
#define _Rule_h

#include "public.h"

// GoBang rules
class Rule
{
private:
	bool nextSame(int& row,int& column,const int direction[2],int value,GomokuData data);

	// 另一个方向是否已经被堵死
	bool behindBlocked(int row,int column,const int direction[2],int value,GomokuData data);

	int countNumber(int row,int column,int direction[2],int value,GomokuData data);

	// 匹配禁手规则1 （在同一个方向的四四禁）
	bool matchBanRuleOne(const std::string baseString);

	// 匹配禁手规则2 (在不同方向的三三禁）
	bool matchBanRuleTwo(const std::string baseString);

	// 匹配禁手规则2 (在不同方向的四四禁）
	bool matchBanRuleThree(const std::string baseString);	

	void buildBasePieceString(int row,int column,int direction[2],std::string &str,GomokuData data);

	std::string getStringValue(int row,int column,GomokuData data);

	// 黑方禁手判负
	//void blackBanLose();

protected:
	Rule();
	~Rule();

public:
	static Rule* getInstance();

	//void setData(int row,int column,PieceSide side,GomokuData&);

	// 计数规则
	GameResult checkSum(int row,int column,PieceSide side,GomokuData data,bool);

	// 禁手规则
	GameResult checkForbidden(int row,int column,PieceSide side,GomokuData data);

};

#endif