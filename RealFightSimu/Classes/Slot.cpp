//
//  Slot.cpp
//  RealFightSimu
//
//  Created by 孔 令锴 on 14-6-20.
//
//

#include "Slot.h"
#include "Utility.h"

Slot::Slot(int length)
{
    m_SlotDataLength = length;
    
    m_Index = SLOTINDEX::NOSLOT;

}

void Slot::parserStringData()
{
    
}

void Slot::init()
{
    
}

void Slot::update()
{
    
}

void Slot::updateSlotIndex(SLOTINDEX index)
{
    m_Index = index;
}

void Slot::convertDataArryToString()
{
    int *array;
    std::copy(m_SlotData.begin(), m_SlotData.end(), array);
    composeString(array, m_SlotDataLength, ",", &m_DataString);
}