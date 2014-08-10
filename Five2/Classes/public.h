#ifndef _Public_h
#define _Public_h

#include "cocos2d.h"
USING_NS_CC;

typedef int GomokuData[15][15];

inline int getIndexByRC(int r ,int c)
{
	return r* 15 + c;
}

inline void getRCByIndex(int index, int& r, int& c)
{
	r = index /15;
	c = index % 15;
}

// 谁的回合
enum TurnOwner
{
	NonePlayer,
	PlayerOne = 1,
	PlayerTwo
};

// 黑方还是白方
enum PieceSide
{
	NoneSide,
	BlackSide,
	WhiteSide
};

// 游戏设置
struct GameSettings
{
	int TotalTime;
	int ExtraTime;
	bool hasForbidden;
	bool hasAI;
	int aiLevel;
	PieceSide aiSide;
};

enum TotalTimeOption
{
	_5M,
	_10M,
	_15M,
	_20M,
	_25M,
	_30M
};

enum ExtraTimeOption
{
	_30S,
	_1M,
	_2M
};

enum GameResult
{
	NotStarted,
	NoResult,
	BlackWin,
	WhiteWin,
	BlackTimeOut,
	WhiteTimeOut,
	BlackBan,
	Draw
};

struct StepContent
{
	PieceSide side; // 哪方下的子
	int index;      // 棋子的位置
};

struct AIDataStruct
{
	GomokuData* data;
	PieceSide side;
	int result;
};

static const int TOTALTIME_SECONDS[6] = {300,600,900,1200,1500,1800};
static const int EXTRATIME_SECONDS[3] = {30,60,120};

static const std::string TOTALTIME_FRAMENAME[6] = {"time5m","time10m","time15m","time20m","time25m","time30m"};
static const std::string EXTRATIME_FRAMENAME[3] = {"time30s","time1m","time2m"};


#endif