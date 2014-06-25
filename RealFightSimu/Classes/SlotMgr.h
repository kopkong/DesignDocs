//
//  SlotMgr.h
//  RealFightSimu
//
//  Created by 孔 令锴 on 14-6-20.
//
//

#ifndef RealFightSimu_SlotMgr_h
#define RealFightSimu_SlotMgr_h

#include "Player.h"
#include <map>
#include "SlotGlobalConfig.h"
#include "Slot.h"

typedef std::map<SLOTINDEX,Slot> SlotItems ;

class SlotsMgr
{
private:
	Player* m_Player;
    
    SlotItems m_SlotsContainer[SLOTTYPE_ALL];
    int m_SlotsSize[SLOTTYPE_ALL];
	const char* getSlotDataKeyByType(SLOTTYPE);
    
protected:
	~SlotsMgr();
    
public:
    static SlotsMgr& Instance();
    
    void initPlayerSlots();
    
    void savePlayerSlots();
    
    void loadSlots(SLOTTYPE);
    
    void saveSlots(SLOTTYPE);
    
    void addSlot(SLOTTYPE,Slot&);
    
    void removeSlot(SLOTTYPE,SLOTINDEX);
    
    // 从背包里找到一个可以放东西的slotindex
    SLOTINDEX findAvailableSlot(SLOTTYPE);
};


#endif
