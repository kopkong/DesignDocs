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

	// �������µ���
	int thinkNextMove(GomokuData data,PieceSide aiSide);

};

#endif