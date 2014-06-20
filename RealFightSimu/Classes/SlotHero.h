//
//  SlotHero.h
//  RealFightSimu
//
//  Created by 孔 令锴 on 14-6-18.
//
//

#ifndef RealFightSimu_SlotHero_h
#define RealFightSimu_SlotHero_h

#include "Slot.h"

enum HEROSLOTENUMRATION
{
    HEROSLOTENUMRATION_CONFIGID,
    HEROSLOTENUMRATION_SLOTINDEX,
    HEROSLOTENUMRATION_LEVEL,
    HEROSLOTENUMRATION_RANK,
    HEROSLOTENUMRATION_ISONTHEFIELD, // 是否上阵 0或者1
    HEROSLOTENUMRATION_ALL
};

class SlotHero:public Slot
{
private:
    
    // 武将当前的等级
    int m_Level;
    
    // 武将的进阶等级
    int m_Rank;
    
    // 武将的品级,从config读取
    int m_Star;
    
    // 是否已上阵
    int m_IsOnField;
    
    // 属性数据
    
    // 初始数据
    float m_InitialHP;
    float m_InitialATK;
    float m_InitialDEF;
    
    // 成长率
    float m_GrowthHP;
    float m_GrowthATK;
    float m_GrowthDEF;
    
    // 进阶成长倍率
    float m_PromoteRateHP;
    float m_PromoteRateATK;
    float m_PromoteRateDEF;
    
    // 闪避几率
    float m_DodgeRate;
    
    // 爆击几率
    float m_CriticalRate;
    
public:
    SlotHero(std::string s):Slot((int)HEROSLOTENUMRATION_ALL)
    {
        m_DataString = s;
        parserStringData();
    }
    
    virtual void parserStringData() override;
    
    virtual void init() override;
    
    virtual void update() override;
    
    virtual void updateSlotIndex(SLOTINDEX) override;
    
    void updateLevel(int level);
    
    void updateRank(int rank);
    
    void updateIsOnTheField(int);
    
    void generateDataString();
    
    bool isInPlayerFormation();
    
    float computeHP();
    
    float computeATK();
    
    float computeDEF();
    
    float computeDodgeRate();
    
    float computeCriticalRate();
    
};

#endif
