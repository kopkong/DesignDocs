//
//  Slot.h
//  RealFightSimu
//
//  Created by 孔 令锴 on 14-6-20.
//
//

#ifndef __RealFightSimu__Slot__
#define __RealFightSimu__Slot__

#include "SlotGlobalConfig.h"
#include "cocos2d.h"
#include <vector>
#include <string>

class Slot
{
public:
    Slot(int length);
    
    // 配置ID
    int m_ConfigID;
    
    // 在背包里面的位置
    SLOTINDEX m_Index;
    
    // 数据长度
    int m_SlotDataLength;
    
    // 保存的数据
    std::vector<int> m_SlotData;
    
    // 数据字符串，保存在xml文件中的格式
    std::string m_DataString;
    
    // 解析数据
    virtual void parserStringData();
    
    // 将int数据转换成字符串
    virtual void convertDataArryToString();

    // 初始化数据
    virtual void init();
    
    // 更新数据
    virtual void update();
    
    // 更新位置
    virtual void updateSlotIndex(SLOTINDEX);
};

#endif /* defined(__RealFightSimu__Slot__) */
