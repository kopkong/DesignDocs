using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class EntityInfoFactory
    {
        static public SoldierInfo GetSoldierInfoFromConfig(int configID, int level, int count)
        {
            SoldierInfo s = new SoldierInfo();
            s.SoldierConfig = DBConfigMgr.Instance.MapSoldier[configID];
            s.Level = level;
            s.Rank = 0;
            s.SoldierCount = count;

            return s;
        }

        static public SoldierInfo GetSoldierInfoFromPlayerSlot(int slotIndex)
        {
            SoldierInfo s = new SoldierInfo();
            SlotSoldier s1 = (SlotSoldier)PlayerDataMgr.Instance.GetPlayerBag(SlotType.SlotType_Soldier)[slotIndex];
            s.SoldierConfig = DBConfigMgr.Instance.MapSoldier[s1.ConfigID];
            s.Level = s1.Lv;
            s.Rank = s1.Rank;
            s.SoldierCount = s1.Count;

            return s;
        }


        static public GeneralInfo GetGeneralInfoFromConfig(int configID, int level, int soldierCount)
        {
            GeneralInfo g = new GeneralInfo();
            g.GeneralConfig = DBConfigMgr.Instance.MapGeneral[configID];
            g.Level = level;
            g.Rank = 0;
            g.SoldierCount = soldierCount;

            return g;
        }

        static public GeneralInfo GetGeneralInfoFromPlayerSlot(int slotIndex)
        {
            GeneralInfo g = new GeneralInfo();
            SlotGeneral s1 = (SlotGeneral)PlayerDataMgr.Instance.GetPlayerBag(SlotType.SlotType_General)[slotIndex];
            SlotSoldier s2 = (SlotSoldier)PlayerDataMgr.Instance.GetPlayerBag(SlotType.SlotType_Soldier)[s1.SoldierIndex];

            g.GeneralConfig = s1.GeneralConfig;
            g.Level = s1.Lv;
            g.Rank = s1.Rank;
            g.SoldierConfigID = s2.ConfigID;
            g.SoldierCount = s2.Count;

            return g;
        }
    }
}
