#ifndef __RESOURCE_H_
#define __RESOURCE_H_

#include "public.h"

class GameResources
{

protected:
	GameResources();
	~GameResources();

public:
	static GameResources* getInstance();

	void initTexture();
};

#endif