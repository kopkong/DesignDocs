//
//  SlotHero.cpp
//  RealFightSimu
//
//  Created by 孔 令锴 on 14-6-18.
//
//

#include "SlotHero.h"
#include "Utility.h"
#include "ConfigDataMgr.h"
#include "ConfigStruct.h"

void SlotHero::init()
{
    //m_SlotDataLength = (int)HEROSLOTENUMRATION::HEROSLOTENUMRATION_ALL;
}

void SlotHero::parserStringData()
{
    if(m_DataString.size() > 0)
    {
        std::string value[MAXSLOTDATASIZE];
        splitString(m_DataString, ',', &value[0], m_SlotDataLength);
        
        //CC_ASSERT(m_SlotDataLength == value->size() && "Data String size incorrect!");
        
        for(int p = 0; p< m_SlotDataLength; ++p)
        {
            switch((HEROSLOTENUMRATION)p)
            {
                case HEROSLOTENUMRATION_CONFIGID:
                {
                    m_ConfigID = atoi(value[p].c_str());
                    m_SlotData.push_back(m_ConfigID);

					CC_ASSERT(ConfigDataMgr::Instance().validConfigID(CONFIG_TYPE_HERO,m_ConfigID));
                    break;
                }
                case HEROSLOTENUMRATION_SLOTINDEX:
                {
                    m_Index = (SLOTINDEX)atoi(value[p].c_str());
                    m_SlotData.push_back(m_Index);
                    break;
                }
                case HEROSLOTENUMRATION_LEVEL:
                {
                    m_Level = atoi(value[p].c_str());
                    m_SlotData.push_back(m_Level);
                    break;
                }
                case HEROSLOTENUMRATION_RANK:
                {
                    m_Rank  = atoi(value[p].c_str());
                    m_SlotData.push_back(m_Rank);
                    break;
                }
                case HEROSLOTENUMRATION_ISONTHEFIELD:
                {
                    m_IsOnField = atoi(value[p].c_str());
                    m_SlotData.push_back(m_IsOnField);
                    break;
                }
                default:
                    break;
            }
        }
    }
}

void SlotHero::update()
{
    
}

bool SlotHero::isInPlayerFormation()
{
    return m_IsOnField == SLOTHERO_ONFIELD;
}

void SlotHero::updateLevel(int level)
{
    m_Level = level;
    m_SlotData[HEROSLOTENUMRATION_LEVEL] = level;
}

void SlotHero::updateRank(int rank)
{
    m_Rank = rank;
    m_SlotData[HEROSLOTENUMRATION_RANK] = rank;
}

void SlotHero::updateSlotIndex(SLOTINDEX index)
{
    m_Index = index;
    m_SlotData[HEROSLOTENUMRATION_SLOTINDEX] = (int)index;
}

void SlotHero::updateIsOnTheField(int isOnField)
{
    m_IsOnField = isOnField;
    m_SlotData[HEROSLOTENUMRATION_ISONTHEFIELD] = isOnField;
}

float SlotHero::computeHP()
{
	float hp;
	StructHeroConfig config = ConfigDataMgr::Instance().m_HeroConfigMap[m_ConfigID];
	hp = (config.InitialHP + m_Level * config.GrowthHP) * ( 1 * config.RankUpRateHP * (m_Rank - 1));

	return hp;
}

float SlotHero::computeATK()
{
	float atk;
	StructHeroConfig config = ConfigDataMgr::Instance().m_HeroConfigMap[m_ConfigID];
	atk =  (config.InitialATK + m_Level * config.GrowthATK) * ( 1 * config.RankUpRateATK * (m_Rank - 1));

	return atk;
}

float SlotHero::computeDEF()
{
	float def;
	StructHeroConfig config = ConfigDataMgr::Instance().m_HeroConfigMap[m_ConfigID];
	def =  (config.InitialDEF + m_Level * config.GrowthDEF) * ( 1 * config.RankUpRateDEF * (m_Rank - 1));

	return def;
}


