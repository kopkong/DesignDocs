using System;
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

        Dictionary<FormationPosition, PositionPair> map = new Dictionary<FormationPosition, PositionPair>();
        Dictionary<int, PositionPair> _formation = new Dictionary<int, PositionPair>();

        public string TeamName{get;set;}

        public int TeamBattlePowerPoint{get;set;}

        public Dictionary<int, PositionPair> FormationMap
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
                _formation.Add(pair.Key,map[(FormationPosition)pair.Value.ExtraData]);
                battlePoint += Formula.ComputeBattlePowerPoint(pair.Key);
            }

            TeamName = PlayerDataMgr.Instance.GetPlayer().Name;
            TeamBattlePowerPoint = battlePoint;
        }

        public void InitNPCFormation(int levelConfigID)
        {
            string enemyStr = ConfigDataMgr.Instance._MapLevel[levelConfigID].Enemy;

            // Parse string
            string[] itemsStr = enemyStr.Split(';');

            foreach (string s in itemsStr)
            {
                string[] item = s.Replace("()", "").Split(',');
                
            }

        }

        private void InitMap()
        {
            map.Add(FormationPosition.A1, new PositionPair(0, 3));
            map.Add(FormationPosition.A2, new PositionPair(1, 3));
            map.Add(FormationPosition.A3, new PositionPair(2, 3));
            map.Add(FormationPosition.A4, new PositionPair(3, 3));
            map.Add(FormationPosition.A5, new PositionPair(4, 3));

            map.Add(FormationPosition.B1, new PositionPair(0, 2));
            map.Add(FormationPosition.B2, new PositionPair(1, 2));
            map.Add(FormationPosition.B3, new PositionPair(2, 2));
            map.Add(FormationPosition.B4, new PositionPair(3, 2));
            map.Add(FormationPosition.B5, new PositionPair(4, 2));

            map.Add(FormationPosition.C1, new PositionPair(0, 1));
            map.Add(FormationPosition.C2, new PositionPair(1, 1));
            map.Add(FormationPosition.C3, new PositionPair(2, 1));
            map.Add(FormationPosition.C4, new PositionPair(3, 1));
            map.Add(FormationPosition.C5, new PositionPair(4, 1));

            map.Add(FormationPosition.D1, new PositionPair(0, 0));
            map.Add(FormationPosition.D2, new PositionPair(1, 0));
            map.Add(FormationPosition.D3, new PositionPair(2, 0));
            map.Add(FormationPosition.D4, new PositionPair(3, 0));
            map.Add(FormationPosition.D5, new PositionPair(4, 0));
        }
    }
}
