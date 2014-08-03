#ifndef __FiveMessageBox_H__
#define __FiveMessageBox_H__

#include "public.h"

class FiveMessageBox:public Sprite
{
private:
	Sprite* backGroundImage;
	Sprite* textImage;

public:
	FiveMessageBox();
	~FiveMessageBox();

	static FiveMessageBox* CreateWithImage(const std::string& fileName);
	static FiveMessageBox* CreateWithSpriteFrameName(const std::string& frameName);

	void setTextSpriteFrame(const std::string& frameName);
};

#endif