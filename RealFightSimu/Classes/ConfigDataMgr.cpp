//
//  ConfigDataMgr.cpp
//  RealFightSimu
//
//  Created by 孔 令锴 on 14-6-18.
//
//

#include "ConfigDataMgr.h"
#include "AssertConfigs.h"
#include "cocos2d.h"


ConfigDataMgr::~ConfigDataMgr()
{

}

ConfigDataMgr& ConfigDataMgr::Instance()
{
	static ConfigDataMgr g_Instane;
	return g_Instane;
}

void ConfigDataMgr::initAllConfigs()
{
	parseHeroConfig(getRootJson(CONFIG_FILE_WUJIANG.c_str()));
	parseSoldierConfig(getRootJson(CONFIG_FILE_SOLDIER.c_str()));
	parseArmorConfig(getRootJson(CONFIG_FILE_ARMOR.c_str()));
}

Json* ConfigDataMgr::getRootJson(const char* szFileName)
{
	ssize_t len = 0;
	unsigned char* szChar = cocos2d::FileUtils::getInstance()->getFileData(szFileName,"r", &len);
	return Json_create((char*)szChar);
}

void ConfigDataMgr::parseHeroConfig( Json* jJson )
{
	m_HeroConfigMap.clear();
	if(jJson && jJson->child)
	{
		Json* prJson = jJson->child;
		while (prJson)
		{
			int id = Json_getInt(prJson,"id",0);
			if (id != 0)
			{
				StructHeroConfig config;
				std::map<int,StructHeroConfig>::iterator it = m_HeroConfigMap.find(id);

				if(it == m_HeroConfigMap.end())
				{
					config.ID						= id;
					config.Name						= Json_getString(prJson,"Name","");
					config.IconID					= Json_getString(prJson,"SmallIcon","");
					config.ActionID					= Json_getString(prJson,"ActionID","");
					config.Color					= Json_getString(prJson,"Color","");
					config.Description				= Json_getString(prJson,"Description","");
					config.Star						= Json_getString(prJson,"Star","");
					config.Type						= Json_getInt(prJson,"SoldierType",0);
					config.InitialHP				= Json_getInt(prJson,"HP",0);
					config.InitialATK				= Json_getInt(prJson,"AttackPower",0);
					config.InitialDEF				= Json_getInt(prJson,"DefensePower",0);
					config.DodgeRate				= Json_getInt(prJson,"DodgeRate",0);
					config.CriticalRate				= Json_getInt(prJson,"CriticalRate",0);
					config.HitRate					= Json_getInt(prJson,"HitRate",0);
					config.AttackSpeed				= Json_getInt(prJson,"AttackSpeed",0);
					config.AttackRange				= Json_getInt(prJson,"AttackRange",0);
					config.SkillID					= Json_getString(prJson,"SkillID","");
					config.TalentID					= Json_getInt(prJson,"TalentID",0);
					config.GrowthHP					= Json_getInt(prJson,"HPGrowth",0);
					config.GrowthATK				= Json_getInt(prJson,"ATKGrowth",0);
					config.GrowthDEF				= Json_getInt(prJson,"DEFGrowth",0);
					config.RankUpRateHP				= Json_getFloat(prJson,"HPRankRate",0);
					config.RankUpRateATK			= Json_getFloat(prJson,"ATKRankRate",0);
					config.RankUpRateDEF			= Json_getFloat(prJson,"DEFRankRate",0);
					config.SpecialArmor				= Json_getString(prJson,"SpecialArmor","");
					config.Souls					= Json_getInt(prJson,"Souls",0);

					m_HeroConfigMap.insert(std::make_pair(id,config));
				}
				else
				{
					cocos2d::log("HERO Config里有重复的ID");
				}
			}
			prJson = prJson->next;
		}
	}
	Json_dispose(jJson);
}

void ConfigDataMgr::parseSoldierConfig(Json* json)
{
	m_SoldierConfigMap.clear();
	if(json && json->child)
	{
		Json* prJson = json->child;
		while (prJson)
		{
			int id = Json_getInt(prJson,"id",0);
			if (id != 0)
			{
				StructSoldierConfig config;
				std::map<int,StructSoldierConfig>::iterator it = m_SoldierConfigMap.find(id);

				if(it == m_SoldierConfigMap.end())
				{
					config.ID						= id;
					config.Name						= Json_getString(prJson,"Name","");
					config.IconID					= Json_getString(prJson,"SmallIcon","");
					config.ActionID					= Json_getString(prJson,"ActionID","");
					config.Color					= Json_getString(prJson,"Color","");
					config.Description				= Json_getString(prJson,"Description","");
					config.Star						= Json_getString(prJson,"Star","");
					config.Type						= Json_getInt(prJson,"SoldierType",0);
					config.InitialHP				= Json_getInt(prJson,"HP",0);
					config.InitialATK				= Json_getInt(prJson,"AttackPower",0);
					config.InitialDEF				= Json_getInt(prJson,"DefensePower",0);
					config.DodgeRate				= Json_getInt(prJson,"DodgeRate",0);
					config.CriticalRate				= Json_getInt(prJson,"CriticalRate",0);
					config.HitRate					= Json_getInt(prJson,"HitRate",0);
					config.AttackSpeed				= Json_getInt(prJson,"AttackSpeed",0);
					config.AttackRange				= Json_getInt(prJson,"AttackRange",0);
					config.SkillID					= Json_getString(prJson,"SkillID","");
					config.AIID						= Json_getInt(prJson,"AIID",0);
					config.GrowthHP					= Json_getInt(prJson,"HPGrowth",0);
					config.GrowthATK				= Json_getInt(prJson,"ATKGrowth",0);
					config.GrowthDEF				= Json_getInt(prJson,"DEFGrowth",0);
					config.RankUpRateHP				= Json_getFloat(prJson,"HPRankRate",0);
					config.RankUpRateATK			= Json_getFloat(prJson,"ATKRankRate",0);
					config.RankUpRateDEF			= Json_getFloat(prJson,"DEFRankRate",0);

					m_SoldierConfigMap.insert(std::make_pair(id,config));
				}
				else
				{
					cocos2d::log("Soldier Config里有重复的ID");
				}
			}
			prJson = prJson->next;
		}
	}
	Json_dispose(json);
}

void ConfigDataMgr::parseArmorConfig(Json* json)
{
	m_ArmorConfigMap.clear();
	if(json && json->child)
	{
		Json* prJson = json->child;
		while (prJson)
		{
			int id = Json_getInt(prJson,"id",0);
			if (id != 0)
			{
				StructArmorConfig config;
				std::map<int,StructArmorConfig>::iterator it = m_ArmorConfigMap.find(id);

				if(it == m_ArmorConfigMap.end())
				{
					config.ID						= id;
					config.Name						= Json_getString(prJson,"Name","");
					config.IconID					= Json_getString(prJson,"IconID","");
					config.Color					= Json_getString(prJson,"Color","");
					config.Description				= Json_getString(prJson,"Description","");
					config.Star						= Json_getString(prJson,"Star","");
					config.Type						= Json_getInt(prJson,"Type",0);
					config.InitialHP				= Json_getInt(prJson,"HP",0);
					config.InitialATK				= Json_getInt(prJson,"AttackPower",0);
					config.InitialDEF				= Json_getInt(prJson,"DefensePower",0);
					config.GrowthHP					= Json_getInt(prJson,"HPGrowth",0);
					config.GrowthATK				= Json_getInt(prJson,"ATKGrowth",0);
					config.GrowthDEF				= Json_getInt(prJson,"DEFGrowth",0);
					config.ExtraSkills				= Json_getString(prJson,"ExtraSkills","");
					config.SoldCoins				= Json_getInt(prJson,"SoldCoins",0);

					m_ArmorConfigMap.insert(std::make_pair(id,config));
				}
				else
				{
					cocos2d::log("Armor Config里有重复的ID");
				}
			}
			prJson = prJson->next;
		}
	}
	Json_dispose(json);
}


void ConfigDataMgr::parseItemConfig(Json* json)
{

}

void ConfigDataMgr::parseSoldierMaterialConfig(Json* json)
{

}

void ConfigDataMgr::parseArmorMaterialConfig(Json* json)
{

}

bool ConfigDataMgr::validConfigID(ConfigType type,int id)
{
	if(id <= 0 )
	{
		return false;
	}
	
	switch(type)
	{
		case CONFIG_TYPE_HERO:
			{
				std::map<int,StructHeroConfig>::iterator it = m_HeroConfigMap.find(id);
				if(it == m_HeroConfigMap.end())
				{
					// 没找到
					return false;
				}
				else
				{
					return true;
				}
			}
		case CONFIG_TYPE_SOLDIER:
			{
				std::map<int,StructSoldierConfig>::iterator it = m_SoldierConfigMap.find(id);
				if(it == m_SoldierConfigMap.end())
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		case CONFIG_TYPE_ARMOR:
			{
				std::map<int,StructArmorConfig>::iterator it = m_ArmorConfigMap.find(id);
				if(it == m_ArmorConfigMap.end())
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		default:
			return false;
	}

}