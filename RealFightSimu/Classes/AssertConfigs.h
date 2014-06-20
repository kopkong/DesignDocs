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



static const string CONFIG_FILE_WUJIANG = RESOURCES_PATH + "Config/wujiang.json";
static const string CONFIG_FILE_SOLDIER = RESOURCES_PATH + "Config/soldier.json";

#endif

