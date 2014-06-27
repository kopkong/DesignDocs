//
//  AssertConfigs.h
//  RealFightSimu
//
//  Created by 孔 令锴 on 14-6-18.
//
//

#ifndef RealFightSimu_AssertConfigs_h
#define RealFightSimu_AssertConfigs_h

#include <string>
using namespace std;

#if (CC_TARGET_PLATFORM == CC_PLATFORM_WIN32)
    static const string RESOURCES_PATH = "../Resources/";
#elif (CC_TARGET_PLATFORM == CC_PLATFORM_MAC)
    static const string RESOURCES_PATH = "/Users/KopKong/Documents/DesignDocs/RealFightSimu/Resources/";
#else
    static const string RESOURCES_PATH = "";
#endif


static const string UI_LAYTOU_MAIN = RESOURCES_PATH + "NewUi_1/NewUi_1.ExportJson";


static const string CONFIG_FILE_WUJIANG			=	RESOURCES_PATH	+ "Config/general.json";
static const string CONFIG_FILE_SOLDIER			=	RESOURCES_PATH	+ "Config/soldier.json";
static const string CONFIG_FILE_ARMOR			=	RESOURCES_PATH	+ "Config/armor.json";
static const string CONFIG_FILE_ITEM			=	RESOURCES_PATH	+ "Config/item.json";
static const string CONFIG_FILE_SOLDIERMATERIAL	=	RESOURCES_PATH	+ "Config/soldierMaterial.json";
static const string CONFIG_FILE_ARMORMATERIAL	=	RESOURCES_PATH	+ "Config/armroMaterial.json";

static const string USER_DEFAULT_DATAFILE		=	RESOURCES_PATH  + "UserData/data";
static const string DATABASE_FILE				=	RESOURCES_PATH	+ "UserData/user.db";
#endif

