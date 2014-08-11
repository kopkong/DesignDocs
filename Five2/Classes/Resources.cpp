#include "Resources.h"
#include "AssertConfigs.h"

GameResources::GameResources()
{

}

GameResources::~GameResources()
{
}

GameResources* GameResources::getInstance()
{
	static GameResources instance;
	return &instance;
}

void GameResources::initTexture()
{
	SpriteFrameCache* frameCache = SpriteFrameCache::getInstance();

	SpriteFrame* chessboard = SpriteFrame::create(CHESSBOARD_BACKGROUND_PATH,Rect(0,0,758,758));
	SpriteFrame* blackNormalSprite = SpriteFrame::create(UI_ICON_PIECES,Rect(0,0,66,66));
	SpriteFrame* blackSelectedSprite = SpriteFrame::create(UI_ICON_PIECES,Rect(0,66,66,66));
	SpriteFrame* whiteNormalSprite = SpriteFrame::create(UI_ICON_PIECES,Rect(66,0,66,66));
	SpriteFrame* whiteSelectedSprite = SpriteFrame::create(UI_ICON_PIECES,Rect(66,66,66,66));
	SpriteFrame* transparentSprite = SpriteFrame::create(UI_ICON_TRANSPARENTPIECE,Rect(0,0,50,50));
	SpriteFrame* hoverSprite = SpriteFrame::create(UI_ICON_PIECE_HOVER,Rect(0,0,50,50));

	SpriteFrame* playerOneWin = SpriteFrame::create(RESULT_TEXT_FILE_PATH,Rect(0,0,205,40));
	SpriteFrame* playerTwoWin = SpriteFrame::create(RESULT_TEXT_FILE_PATH,Rect(0,40,205,40));
	SpriteFrame* playerOneForbidden = SpriteFrame::create(RESULT_TEXT_FILE_PATH,Rect(0,80,205,40));
	SpriteFrame* playerOneTimeout = SpriteFrame::create(RESULT_TEXT_FILE_PATH,Rect(0,120,205,40));
	SpriteFrame* playerTwoTimeout = SpriteFrame::create(RESULT_TEXT_FILE_PATH,Rect(0,160,205,40));

	SpriteFrame* playerOneBox = SpriteFrame::create(BOXES_FILE_PATH,Rect(0,0,51,40));
	SpriteFrame* playerTwoBox = SpriteFrame::create(BOXES_FILE_PATH,Rect(0,40,51,40));

	SpriteFrame* time5m			= SpriteFrame::create(TOTALTIME_TEXT_PATH,Rect(0,0,48,24));
	SpriteFrame* time10m		= SpriteFrame::create(TOTALTIME_TEXT_PATH,Rect(48,0,48,24));
	SpriteFrame* time15m		= SpriteFrame::create(TOTALTIME_TEXT_PATH,Rect(96,0,48,24));
	SpriteFrame* time20m		= SpriteFrame::create(TOTALTIME_TEXT_PATH,Rect(144,0,48,24));
	SpriteFrame* time25m		= SpriteFrame::create(TOTALTIME_TEXT_PATH,Rect(192,0,48,24));
	SpriteFrame* time30m		= SpriteFrame::create(TOTALTIME_TEXT_PATH,Rect(240,0,48,24));

	SpriteFrame* timem30s		= SpriteFrame::create(EXTRATIME_TEXT_PATH,Rect(0,0,47,23));
	SpriteFrame* time1m			= SpriteFrame::create(EXTRATIME_TEXT_PATH,Rect(47,0,47,23));
	SpriteFrame* time2m			= SpriteFrame::create(EXTRATIME_TEXT_PATH,Rect(94,0,47,23));

	frameCache->addSpriteFrame(chessboard,SPRITECACHE_NAME_CHESSBOARD);
	frameCache->addSpriteFrame(playerOneWin,SPRITECACHE_NAME_BLACKWIN);
	frameCache->addSpriteFrame(playerTwoWin,SPRITECACHE_NAME_WHITEWIN);
	frameCache->addSpriteFrame(playerOneForbidden,SPRITECACHE_NAME_FORBIDDENLOSE);
	frameCache->addSpriteFrame(playerOneTimeout,SPRITECACHE_NAME_BLACKTIMEOUT);
	frameCache->addSpriteFrame(playerTwoTimeout,SPRITECACHE_NAME_WHITETIMEOUT);

	frameCache->addSpriteFrame(blackNormalSprite,SPRITECACHE_NAME_BLACKSTONE);
	frameCache->addSpriteFrame(whiteNormalSprite,SPRITECACHE_NAME_WHITESTONE);
	frameCache->addSpriteFrame(hoverSprite,SPRITECACHE_NAME_HOVERSTONE);

	frameCache->addSpriteFrame(playerOneBox,"playerOneBox");
	frameCache->addSpriteFrame(playerTwoBox,"playerTwoBox");

	frameCache->addSpriteFrame(timem30s,"time30s");
	frameCache->addSpriteFrame(time1m,"time1m");
	frameCache->addSpriteFrame(time2m,"time2m");
	frameCache->addSpriteFrame(time5m,"time5m");
	frameCache->addSpriteFrame(time10m,"time10m");
	frameCache->addSpriteFrame(time15m,"time15m");
	frameCache->addSpriteFrame(time20m,"time20m");
	frameCache->addSpriteFrame(time25m,"time25m");
	frameCache->addSpriteFrame(time30m,"time30m");

}