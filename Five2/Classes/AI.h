#ifndef __AI_H_
#define __AI_H_

#include "public.h"

class AI
{
protected:
	AI();
	~AI();

public:
	static AI* getInstance();

	// 返回落下的子
	int thinkNextMove(GomokuData data,PieceSide aiSide);

};

#endif