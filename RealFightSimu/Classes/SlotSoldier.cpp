//
//  SlotSoldier.cpp
//  RealFightSimu
//
//  Created by ¿× ÁîïÇ on 14-6-25.
//
//

#include "SlotSoldier.h"
#include "Utility.h"
#include "ConfigDataMgr.h"
#include "ConfigStruct.h"

void SlotSoldier::init()
{

}

void SlotSoldier::parserStringData()
{
	if(m_DataString.size() > 0 )
	{
		std::string value[MAXSLOTDATASIZE];
		splitString(m_DataString, ',', &value[0], m_SlotDataLength);

		for(int p = 0; p< m_SlotDataLength; ++p)
		{
			switch((SOLDIERSLOTENUMRATION)p)	
			{
			case SOLDIERSLOTENUMRATION_CONFIGID:
				{
					m_ConfigID = atoi(value[p].c_str());
					m_SlotData.push_back(m_ConfigID);

					CC_ASSERT(ConfigDataMgr::Instance().validConfigID(CONFIG_TYPE_SOLDIER,m_ConfigID) ,"ConfigID ²»ºÏ·¨");
					break;
				}
			case SOLDIERSLOTENUMRATION_SLOTINDEX:
				{
					m_Index = (SLOTINDEX)atoi(value[p].c_str());
					m_SlotData.push_back(m_Index);
					break;
				}
			case SOLDIERSLOTENUMRATION_LEVEL:
				{
					m_Level = atoi(value[p].c_str());
					m_SlotData.push_back(m_Level);
					break;
				}
			case SOLDIERSLOTENUMRATION_RANK:
				{
					m_Rank  = atoi(value[p].c_str());
					m_SlotData.push_back(m_Rank);
					break;
				}
			default:
				break;
			}
		}
	}
}

void SlotSoldier::update()
{

}

void SlotSoldier::updateLevel(int level)
{
	m_Level = level;
	m_SlotData[SOLDIERSLOTENUMRATION_LEVEL] = level;
}

void SlotSoldier::updateRank(int rank)
{
	m_Rank = rank;
	m_SlotData[SOLDIERSLOTENUMRATION_RANK] = rank;
}

void SlotSoldier::updateSlotIndex(SLOTINDEX index)
{
	m_Index = index;
	m_SlotData[SOLDIERSLOTENUMRATION_SLOTINDEX] = (int)index;
}

float SlotSoldier::computeHP()
{
	float hp;
	StructSoldierConfig config = ConfigDataMgr::Instance().m_SoldierConfigMap[m_ConfigID];
	hp = (config.InitialHP + m_Level * config.GrowthHP) * ( 1 * config.RankUpRateHP * (m_Rank - 1));

	return hp;
}

float SlotSoldier::computeATK()
{
	float atk;
	StructSoldierConfig config = ConfigDataMgr::Instance().m_SoldierConfigMap[m_ConfigID];
	atk =  (config.InitialATK + m_Level * config.GrowthATK) * ( 1 * config.RankUpRateATK * (m_Rank - 1));

	return atk;
}

float SlotSoldier::computeDEF()
{
	float def;
	StructSoldierConfig config = ConfigDataMgr::Instance().m_SoldierConfigMap[m_ConfigID];
	def =  (config.InitialDEF + m_Level * config.GrowthDEF) * ( 1 * config.RankUpRateDEF * (m_Rank - 1));

	return def;
}