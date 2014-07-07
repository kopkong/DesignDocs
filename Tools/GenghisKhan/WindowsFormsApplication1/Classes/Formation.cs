﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class PositionPair
    {
        public PositionPair(int r, int c)
        {
            this.RowIndex = r;
            this.ColumnIndex = c;
        }
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
    }

    public class Formation
    {
        public Formation()
        {
            InitMap();
        }

        Dictionary<FormationPosition, PositionPair> leftMap = new Dictionary<FormationPosition, PositionPair>();
        Dictionary<FormationPosition, PositionPair> rightMap = new Dictionary<FormationPosition, PositionPair>();
        Dictionary<GeneralInfo, PositionPair> _formation = new Dictionary<GeneralInfo, PositionPair>();

        public string TeamName{get;set;}

        public int TeamBattlePowerPoint{get;set;}

        public Dictionary<GeneralInfo, PositionPair> FormationMap
        {
            get
            {
                return _formation;
            }
        }

        public void InitPlayerFormation()
        {
            IEnumerable<KeyValuePair<int, Slot>> onBattleGenerals = PlayerDataMgr.Instance.GetPlayerBag(SlotType.SlotType_General).Where(
                x => x.Value.ExtraData > 0);

            int battlePoint = 0;
            foreach (KeyValuePair<int, Slot> pair in onBattleGenerals)
            {
                SlotGeneral slot = (SlotGeneral)(pair.Value);
                GeneralInfo g = EntityInfoFactory.GetGeneralInfoFromPlayerSlot(pair.Key);
                SoldierInfo s = EntityInfoFactory.GetSoldierInfoFromPlayerSlot(slot.SoldierIndex);

                _formation.Add(g, leftMap[(FormationPosition)pair.Value.ExtraData]);
                battlePoint += Formula.ComputeBattlePowerPoint(g,s);
            }

            TeamName = PlayerDataMgr.Instance.GetPlayer().Name;
            TeamBattlePowerPoint = battlePoint;
        }

        public void InitNPCFormation(int levelConfigID)
        {
            string enemyStr = DBConfigMgr.Instance.MapLevel[levelConfigID].Enemy;

            // Parse string
            string[] itemsStr = enemyStr.Split(';');
            int battlePoint = 0;
            foreach (string s in itemsStr)
            {
                if (s.Length <= 0)
                    continue;

                string[] item = s.Replace("(", "").Replace(")","").Split(',');

                int pos = Convert.ToInt32(item[0]);
                int gConfigID = Convert.ToInt32(item[1]);
                int gLevel = Convert.ToInt32(item[2]);
                int sConfigID = Convert.ToInt32(item[3]);
                int sLevel = Convert.ToInt32(item[4]);
                int sCount = Convert.ToInt32(item[5]);

                GeneralInfo gInfo = EntityInfoFactory.GetGeneralInfoFromConfig(gConfigID, gLevel, sCount);
                SoldierInfo sInfo = EntityInfoFactory.GetSoldierInfoFromConfig(sConfigID, sLevel, sCount);

                FormationPosition p = (FormationPosition)pos;
                _formation.Add(gInfo, rightMap[p]);
                battlePoint += Formula.ComputeBattlePowerPoint(gInfo, sInfo);
            }

            TeamName = DBConfigMgr.Instance.MapLevel[levelConfigID].Name;
            TeamBattlePowerPoint = battlePoint;
        }

        private void InitMap()
        {
            leftMap.Add(FormationPosition.A1, new PositionPair(0, 3));
            leftMap.Add(FormationPosition.A2, new PositionPair(0, 2));
            leftMap.Add(FormationPosition.A3, new PositionPair(0, 1));
            leftMap.Add(FormationPosition.A4, new PositionPair(0, 0));

            leftMap.Add(FormationPosition.B1, new PositionPair(1, 3));
            leftMap.Add(FormationPosition.B2, new PositionPair(1, 2));
            leftMap.Add(FormationPosition.B3, new PositionPair(1, 1));
            leftMap.Add(FormationPosition.B4, new PositionPair(1, 0));

            leftMap.Add(FormationPosition.C1, new PositionPair(2, 3));
            leftMap.Add(FormationPosition.C2, new PositionPair(2, 2));
            leftMap.Add(FormationPosition.C3, new PositionPair(2, 1));
            leftMap.Add(FormationPosition.C4, new PositionPair(2, 0));

            leftMap.Add(FormationPosition.D1, new PositionPair(3, 3));
            leftMap.Add(FormationPosition.D2, new PositionPair(3, 2));
            leftMap.Add(FormationPosition.D3, new PositionPair(3, 1));
            leftMap.Add(FormationPosition.D4, new PositionPair(3, 0));

            leftMap.Add(FormationPosition.E1, new PositionPair(4, 3));
            leftMap.Add(FormationPosition.E2, new PositionPair(4, 2));
            leftMap.Add(FormationPosition.E3, new PositionPair(4, 1));
            leftMap.Add(FormationPosition.E4, new PositionPair(4, 0));

            ////////////////////////////////////////////////////////////
            // 以下是右边的占位
            ////////////////////////////////////////////////////////////
            rightMap.Add(FormationPosition.A1, new PositionPair(0, 0));
            rightMap.Add(FormationPosition.A2, new PositionPair(0, 1));
            rightMap.Add(FormationPosition.A3, new PositionPair(0, 2));
            rightMap.Add(FormationPosition.A4, new PositionPair(0, 3));

            rightMap.Add(FormationPosition.B1, new PositionPair(1, 0));
            rightMap.Add(FormationPosition.B2, new PositionPair(1, 1));
            rightMap.Add(FormationPosition.B3, new PositionPair(1, 2));
            rightMap.Add(FormationPosition.B4, new PositionPair(1, 3));

            rightMap.Add(FormationPosition.C1, new PositionPair(2, 0));
            rightMap.Add(FormationPosition.C2, new PositionPair(2, 1));
            rightMap.Add(FormationPosition.C3, new PositionPair(2, 2));
            rightMap.Add(FormationPosition.C4, new PositionPair(2, 3));

            rightMap.Add(FormationPosition.D1, new PositionPair(3, 0));
            rightMap.Add(FormationPosition.D2, new PositionPair(3, 1));
            rightMap.Add(FormationPosition.D3, new PositionPair(3, 2));
            rightMap.Add(FormationPosition.D4, new PositionPair(3, 3));

            rightMap.Add(FormationPosition.E1, new PositionPair(4, 0));
            rightMap.Add(FormationPosition.E2, new PositionPair(4, 1));
            rightMap.Add(FormationPosition.E3, new PositionPair(4, 2));
            rightMap.Add(FormationPosition.E4, new PositionPair(4, 3));
        }
    }
}