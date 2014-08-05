#include "MessageBox.h"


FiveMessageBox::FiveMessageBox()
{
	_textPosition = Point(135,45);
}

FiveMessageBox::~FiveMessageBox()
{
	CC_SAFE_RELEASE(_backGroundImage);
	CC_SAFE_RELEASE(_textImage);
}

void FiveMessageBox::setTextSpriteFrame(const std::string& frameName)
{
	_textImage->setSpriteFrame(SpriteFrameCache::getInstance()->getSpriteFrameByName(frameName));
	//_textImage->setPosition(_textPosition);
}

void FiveMessageBox::setStringText(const std::string& str)
{

}

void FiveMessageBox::setTextPosition(Point& point)
{
	_textPosition = point;
	//_textImage->setPosition(_textPosition);
}

void FiveMessageBox::showBlackWin()
{
	setTextSpriteFrame(SPRITECACHE_NAME_BLACKWIN);
}

void FiveMessageBox::showWhiteWin()
{
	setTextSpriteFrame(SPRITECACHE_NAME_WHITEWIN);
}

void FiveMessageBox::showBlackForbiddenLose()
{
	setTextSpriteFrame(SPRITECACHE_NAME_FORBIDDENLOSE);
}

void FiveMessageBox::showBlackTimeout()
{
	setTextSpriteFrame(SPRITECACHE_NAME_BLACKTIMEOUT);
}

void FiveMessageBox::showWhiteTimeout()
{
	setTextSpriteFrame(SPRITECACHE_NAME_WHITETIMEOUT);
}

void FiveMessageBox::onEnter()
{
	_backGroundImage = Sprite::create(MESSAGEBOX_BACKGROUND_PATH);
	this->addChild(_backGroundImage);

	_textImage = Sprite::create();
	this->addChild(_textImage,2);
	//setTextSpriteFrame(SPRITECACHE_NAME_BLACKWIN);

}