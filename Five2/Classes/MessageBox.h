#ifndef __FiveMessageBox_H__
#define __FiveMessageBox_H__

#include "public.h"
#include "AssertConfigs.h"

class FiveMessageBox:public Node
{
private:
	Sprite* _backGroundImage;
	Sprite* _textImage;
	Point _textPosition;

protected:
	~FiveMessageBox();

public:
	FiveMessageBox();
	//static FiveMessageBox* create();
	//static FiveMessageBox* createWithImage(const std::string& fileName);
	//static FiveMessageBox* createWithSpriteFrameName(const std::string& frameName);

	void setTextSpriteFrame(const std::string& frameName);
	void setStringText(const std::string& str);
	void setTextPosition(Point& pos);

	void showBlackWin();

	void showWhiteWin();

	void showBlackForbiddenLose();

	void showBlackTimeout();

	void showWhiteTimeout();

	virtual void onEnter() override;
};

#endif