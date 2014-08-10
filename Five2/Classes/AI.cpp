#include "AI.h"
#include "Rule.h"

AI::AI()
{

}

AI::~AI()
{

}

AI* AI::getInstance()
{
	static AI instance;
	return &instance;
}

int AI::thinkNextMove(GomokuData data, PieceSide aiSide)
{
	for(int i = 0 ; i < 15 ; i ++)
		for (int j=0;j<15;j++)
			if(data[i][j] == 0)
				return getIndexByRC(i,j);

	return 0;
}