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
#endif

#if (CC_TARGET_PLATFORM == CC_PLATFORM_MAC)
    static const string RESOURCES_PATH = "/Users/KopKong/Documents/DesignDocs/RealFightSimu/Resources/";
#elif 
    static const string RESOURCES_PATH = "";
#endif


static const string UILAYTOU_MAIN_CONFIG = RESOURCES_PATH + "NewUi_1/NewUi_1.ExportJson";

#endif

