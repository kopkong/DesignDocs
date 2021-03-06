﻿#ifndef _AssertConfigs_h
#define _AssertConfigs_h

#include <string>
using namespace std;

#if (CC_TARGET_PLATFORM == CC_PLATFORM_WIN32)
    static const string RESOURCES_PATH = "../Resources/";
#elif (CC_TARGET_PLATFORM == CC_PLATFORM_MAC)
    static const string RESOURCES_PATH = "/Users/KopKong/Documents/DesignDocs/RealFightSimu/Resources/";
#else
    static const string RESOURCES_PATH = "";
#endif

static const int UI_TAGID_MAIN_CHESSBOARD		=	10;
static const int UI_TAGID_MAIN_SETTINGBOARD		=	35;
static const int UI_MAIN_BUTTONTAG_START		=	34;
static const int UI_MAIN_BUTTONTAG_SETTING1		=	39;
static const int UI_MAIN_BUTTONTAG_SETTING2		=	41;
static const int UI_MAIN_BUTTONTAG_SETTING3		=	42;
static const int UI_MAIN_SLIDERTAG_TOTALTIME	=	61;
static const int UI_MAIN_SLIDERTAG_EXTRATIME	=	57;

static const int UI_TAGID_STARTPAGE_PVPBUTTON   =	69;
static const int UI_TAGID_STARTPAGE_AIBUTTON	=	68;

static const string UI_LAYOUT_MAIN				=	RESOURCES_PATH + "GamePage/GamePage.json";
static const string UI_LAYOUT_STARTPAGE			=	RESOURCES_PATH + "StartPage/StartPage.json";

static const string UI_ICON_PIECES				=	RESOURCES_PATH + "Textures/pieces.png";
static const string UI_ICON_TRANSPARENTPIECE	=	RESOURCES_PATH + "Textures/touming.png";
static const string UI_ICON_PIECE_HOVER			=	RESOURCES_PATH + "Textures/stone-hover.png";

static const string BMFONT_FILE_PATH			=	RESOURCES_PATH + "Textures/1.fnt";
static const string RESULT_TEXT_FILE_PATH		=	RESOURCES_PATH + "Textures/result-text.png";
static const string BOXES_FILE_PATH				=	RESOURCES_PATH + "Textures/boxes.png";
static const string BOX_BLACK_PATH				=	RESOURCES_PATH + "Textures/box-b.png";
static const string BOX_WHITE_PATH				=	RESOURCES_PATH + "Textures/box-w.png";
static const string TOTALTIME_TEXT_PATH			=	RESOURCES_PATH + "Textures/t1.png";
static const string EXTRATIME_TEXT_PATH			=	RESOURCES_PATH + "Textures/t2.png";
static const string MESSAGEBOX_BACKGROUND_PATH  =	RESOURCES_PATH + "Textures/msg-board.png";
static const string CHESSBOARD_BACKGROUND_PATH  =	RESOURCES_PATH + "Textures/chessBoard.jpg";

static const string USER_DEFAULT_DATAFILE		=	RESOURCES_PATH  + "UserData/data";
static const string DATABASE_FILE				=	RESOURCES_PATH	+ "UserData/user.db";

static const string SPRITECACHE_NAME_CHESSBOARD		=	"chessBoard";
static const string SPRITECACHE_NAME_BLACKWIN		=	"blackWin";
static const string SPRITECACHE_NAME_WHITEWIN		=	"whiteWin";
static const string SPRITECACHE_NAME_BLACKTIMEOUT	=	"blackTimeout";
static const string SPRITECACHE_NAME_WHITETIMEOUT	=	"whiteTimeout";
static const string SPRITECACHE_NAME_FORBIDDENLOSE	=	"blackForbidden";
static const string SPRITECACHE_NAME_WHITESTONE		=	"whiteStone";
static const string SPRITECACHE_NAME_BLACKSTONE		=	"blackStone";
static const string SPRITECACHE_NAME_HOVERSTONE		=	"hoverStone";

#endif