//
//  SlotMgr.cpp
//  RealFightSimu
//
//  Created by 孔 令锴 on 14-6-18.
//
//

#include "SlotMgr.h"
#include "Marco.h"
#include "Utility.h"
#include "SlotHero.h"
#include "SlotSoldier.h"
#include "DatabaseHelper.h"

SlotsMgr::~SlotsMgr()
{
    delete m_Player;
}

SlotsMgr& SlotsMgr::Instance()
{
    static SlotsMgr g_Instance;
    return g_Instance;
}

void SlotsMgr::initPlayerSlots()
{
    
    // 先加载每个SlotContainer的Size
    m_SlotsSize[SLOTTYPE_HERO] = GetIntergerByKey(SLOT_DATAKEY_HEROSIZE);
    m_SlotsSize[SLOTTYPE_ITEM] = GetIntergerByKey(SLOT_DATAKEY_ITEMSIZE);
    m_SlotsSize[SLOTTYPE_ARMOR] = GetIntergerByKey(SLOT_DATAKEY_ARMORSIZE);
    m_SlotsSize[SLOTTYPE_SOLDIER] = GetIntergerByKey(SLOT_DATAKEY_SOLDIERSIZE);
    m_SlotsSize[SLOTTYPE_ARMORMAT] = GetIntergerByKey(SLOT_DATAKEY_ARMORMATSIZE);
    m_SlotsSize[SLOTTYPE_SOLDIERMAT] = GetIntergerByKey(SLOT_DATAKEY_SOLDIERMATSIZE);
    
    loadSlots(SLOTTYPE_HERO);
	loadSlots(SLOTTYPE_SOLDIER);
}

void SlotsMgr::savePlayerSlots()
{
    // save Slot Size
	//cocos2d::UserDefault::getInstance()->setIntegerForKey(SLOT_DATAKEY_HEROSIZE, m_SlotsSize[SLOTTYPE_HERO]);
    SetIntergerByKey(SLOT_DATAKEY_HEROSIZE, m_SlotsSize[SLOTTYPE_HERO]);
    SetIntergerByKey(SLOT_DATAKEY_ITEMSIZE, m_SlotsSize[SLOTTYPE_ITEM]);
    SetIntergerByKey(SLOT_DATAKEY_ARMORSIZE, m_SlotsSize[SLOTTYPE_ARMOR]);
    SetIntergerByKey(SLOT_DATAKEY_SOLDIERSIZE, m_SlotsSize[SLOTTYPE_SOLDIER]);
    SetIntergerByKey(SLOT_DATAKEY_ARMORMATSIZE, m_SlotsSize[SLOTTYPE_ARMORMAT]);
    SetIntergerByKey(SLOT_DATAKEY_SOLDIERMATSIZE, m_SlotsSize[SLOTTYPE_SOLDIERMAT]);
    
    saveSlots(SLOTTYPE_HERO);
	saveSlots(SLOTTYPE_SOLDIER);
}

void SlotsMgr::loadSlots(SLOTTYPE type)
{
    string dataString = GetStringByKey(getSlotDataKeyByType(type));
    
    if (dataString.size() > 0 && m_SlotsSize[type] >= 0) {
        // 解析数据,本来是字符串
        
        string items[MAXSLOTDATASIZE];
        splitString(dataString, ';', &items[0], m_SlotsSize[type]);
        
        for(int p = 0; p< m_SlotsSize[type] ; ++ p)
        {
            switch(type)
            {
                case SLOTTYPE_HERO:
                {
                    SlotHero s(items[p]);
                    m_SlotsContainer[type].insert(std::pair<SLOTINDEX,Slot>(s.m_Index,s));
                }
                default:
                    break;
            }
        }
    }
    else
    {
        if (type == SLOTTYPE_HERO)
        {
            // 给玩家一个初始的默认英雄
            SlotHero s(PLAYER_DEFAULT_HERODATASTRING);
            
            addSlot(SLOTTYPE_HERO, s);
        }
		if(type == SLOTTYPE_SOLDIER)
		{
			SlotSoldier s(PLAYER_DEFAULT_SOLDIERDATASTRING);

			addSlot(SLOTTYPE_SOLDIER,s);
		}
    }
}

void SlotsMgr::saveSlots(SLOTTYPE type)
{
    string allItems;
    for(std::map<SLOTINDEX,Slot>::iterator it = m_SlotsContainer[type].begin();
        it!= m_SlotsContainer[type].end(); ++it)
    {
        allItems += it->second.m_DataString + ";";
    }
    
	//cocos2d::log("Saving datakey %s",getSlotDataKeyByType(type));
	SetStringByKey(getSlotDataKeyByType(type), allItems);
    
}

void SlotsMgr::addSlot(SLOTTYPE type, Slot& slot)
{
    CC_ASSERT(m_SlotsSize[type] < 100 && "背包已经满了，不能在添加了");
    
    m_SlotsSize[type] = m_SlotsSize[type] + 1;
    
    SLOTINDEX availableSlot  = findAvailableSlot(type);
    
    if (availableSlot != NOSLOT) {
        slot.updateSlotIndex(availableSlot);
		m_SlotsContainer[type].insert(std::pair<SLOTINDEX,Slot>(slot.m_Index,slot));
    }
    else
    {
        // 背包里已经没有空余的格子了
    }
    
}

SLOTINDEX SlotsMgr::findAvailableSlot(SLOTTYPE type)
{
    for(int i = SLOTINDEX::SLOT1; i < MAXSLOT; i++)
    {
        std::map<SLOTINDEX,Slot>::iterator it = m_SlotsContainer[type].find((SLOTINDEX)i);
        
        if( it == m_SlotsContainer[type].end())
        {
            // 找到一个可用的位置
            return (SLOTINDEX)i;
        }
    }
    
    return NOSLOT;
}

void SlotsMgr::removeSlot(SLOTTYPE type, SLOTINDEX index)
{
	SlotItems::iterator it = m_SlotsContainer[type].find(index);

	if(it!= m_SlotsContainer[type].end())
	{
		// 找到了并删除它
        m_SlotsContainer[type].erase(it);
        
        m_SlotsSize[type] = m_SlotsSize[type] - 1;
    }

}

bool SlotsMgr::isSlotExists(SLOTTYPE type, SLOTINDEX index)
{
	SlotItems::iterator it = m_SlotsContainer[type].find(index);

	if(it!= m_SlotsContainer[type].end())
	{
		return true;
	}

	return false;
}

const char* SlotsMgr::getSlotDataKeyByType(SLOTTYPE type)
{
	std::string datakeyArray[(int)SLOTTYPE_ALL] = {SLOT_DATAKEY_HERODATA,SLOT_DATAKEY_ITEMDATA,SLOT_DATAKEY_ARMORDATA,SLOT_DATAKEY_SOLDIERDATA,SLOT_DATAKEY_ARMORMATDATA,SLOT_DATAKEY_SOLDIERMATDATA,SLOT_DATAKEY_FORMATIONDATA};

	return datakeyArray[(int)type].c_str();
}