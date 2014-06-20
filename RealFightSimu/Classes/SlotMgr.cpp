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
    m_SlotsSize[SLOTTYPE::HERO] = GetIntergerByKey(SLOT_DATAKEY_HEROSIZE);
    m_SlotsSize[SLOTTYPE::ITEM] = GetIntergerByKey(SLOT_DATAKEY_ITEMSIZE);
    m_SlotsSize[SLOTTYPE::ARMOR] = GetIntergerByKey(SLOT_DATAKEY_ARMORSIZE);
    m_SlotsSize[SLOTTYPE::ARMY] = GetIntergerByKey(SLOT_DATAKEY_ARMYSIZE);
    m_SlotsSize[SLOTTYPE::ARMORMAT] = GetIntergerByKey(SLOT_DATAKEY_ARMORMATSIZE);
    m_SlotsSize[SLOTTYPE::ARMYMAT] = GetIntergerByKey(SLOT_DATAKEY_ARMYMATSIZE);
    
    loadSlots(SLOTTYPE::HERO);
}

void SlotsMgr::savePlayerSlots()
{
    // save Slot Size
    SetIntergerByKey(SLOT_DATAKEY_HEROSIZE, m_SlotsSize[SLOTTYPE::HERO]);
    SetIntergerByKey(SLOT_DATAKEY_ITEMSIZE, m_SlotsSize[SLOTTYPE::ITEM]);
    SetIntergerByKey(SLOT_DATAKEY_ARMORSIZE, m_SlotsSize[SLOTTYPE::ARMOR]);
    SetIntergerByKey(SLOT_DATAKEY_ARMYSIZE, m_SlotsSize[SLOTTYPE::ARMY]);
    SetIntergerByKey(SLOT_DATAKEY_ARMORMATSIZE, m_SlotsSize[SLOTTYPE::ARMORMAT]);
    SetIntergerByKey(SLOT_DATAKEY_ARMYMATSIZE, m_SlotsSize[SLOTTYPE::ARMYMAT]);
    
    saveSlots(SLOTTYPE::HERO);
}


void SlotsMgr::loadSlots(SLOTTYPE type)
{
    string dataString;

    switch(type)
    {
        case SLOTTYPE::HERO:
            dataString = GetStringByKey(SLOT_DATAKEY_HERODATA);
            break;
        case SLOTTYPE::ITEM:
            dataString = GetStringByKey(SLOT_DATAKEY_ITEMDATA);
            break;
        default:
            break;
    }
    
    if (dataString.size() > 0 && m_SlotsSize[type] >= 0) {
        // 解析数据,本来是字符串
        
        string items[MAXSLOTDATASIZE];
        splitString(dataString, ';', &items[0], m_SlotsSize[type]);
        
        for(int p = 0; p< m_SlotsSize[type] ; ++ p)
        {
            switch(type)
            {
                case SLOTTYPE::HERO:
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
        if (type == SLOTTYPE::HERO)
        {
            // 给玩家一个初始的默认英雄
            SlotHero s(PLAYER_DEFAULT_HERODATASTRING);
            
            addSlot(SLOTTYPE::HERO, s);
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
    
    switch (type) {
        case SLOTTYPE::HERO:
        {
            SetStringByKey(SLOT_DATAKEY_HERODATA, allItems);
            break;
        }
        default:
            break;
    }
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
    std::map<SLOTINDEX,Slot>::iterator it = m_SlotsContainer[type].find(index);
    
    if( it != m_SlotsContainer[type].end())
    {
        // 找到了并删除它
        m_SlotsContainer[type].erase(it);
        
        m_SlotsSize[type] = m_SlotsSize[type] - 1;
    }

}