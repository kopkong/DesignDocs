//
//  ConfigDataMgr.h
//  RealFightSimu
//
//  Created by 孔 令锴 on 14-6-18.
//
//

#ifndef __RealFightSimu__ConfigDataMgr__
#define __RealFightSimu__ConfigDataMgr__

#include <iostream>
#include <map>
#include "ConfigStruct.h"
#include "json.h"

enum ConfigType
{
	CONFIG_TYPE_HERO,
	CONFIG_TYPE_ITME,
	CONFIG_TYPE_ARMOR,
	CONFIG_TYPE_ARMY,
	CONFIG_TYPE_ARMORMAT,
	CONFIG_TYPE_ARMYMAT
};

class ConfigDataMgr
{
private:
	Json* getRootJson(const char* szFileName);
	void parseHeroConfig(Json*);

protected:
    ~ConfigDataMgr();
    
public:
	std::map<int,StructHeroConfig> m_HeroConfigMap;

    static ConfigDataMgr& Instance();
	void initAllConfigs();

	bool validConfigID(ConfigType cType, int id);
};

#endif /* defined(__RealFightSimu__ConfigDataMgr__) */
