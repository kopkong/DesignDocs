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
};

#endif /* defined(__RealFightSimu__ConfigDataMgr__) */
