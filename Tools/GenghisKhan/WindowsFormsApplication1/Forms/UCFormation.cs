using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1.Forms
{
    public partial class UCFormation : UserControl
    {
        public UCFormation()
        {
            InitializeComponent();
        }

        private Formation _formation;

        public Formation CurrentFormation
        {
            get
            {
                return _formation;
            }
        }

        public void InitPlayerFormation()
        {
            _formation = new Formation();
            _formation.InitPlayerFormation();

            foreach (KeyValuePair<GeneralInfo, PositionPair> pair in _formation.FormationMap)
            {
                Label lb_General = new Label();
                lb_General.Text = pair.Key.GeneralConfig.Name;

                int r = pair.Value.RowIndex;
                int c = pair.Value.ColumnIndex;

                lb_General.TextAlign = ContentAlignment.MiddleCenter;
                tableLayoutPanel1.Controls.Add(lb_General, c,r);
                Console.WriteLine("添加了Formation table{0}{1}, 武将{2}", r,c,lb_General.Text);
            }

            LB_TeamName.Text = _formation.TeamName;
            LB_BattlePower.Text = _formation.TeamBattlePowerPoint.ToString();
        }

        public void InitNPCFormation(int levelConfigID)
        {
            _formation = new Formation();
            _formation.InitNPCFormation(levelConfigID);

            foreach (KeyValuePair<GeneralInfo, PositionPair> pair in _formation.FormationMap)
            {
                Label lb_General = new Label();
                lb_General.Text = pair.Key.GeneralConfig.Name;

                int r = pair.Value.RowIndex;
                int c = pair.Value.ColumnIndex;

                lb_General.TextAlign = ContentAlignment.MiddleCenter;
                tableLayoutPanel1.Controls.Add(lb_General, c, r);
                Console.WriteLine("添加了Formation table{0}{1}, 武将{2}", r, c, lb_General.Text);
            }

            LB_TeamName.Text = _formation.TeamName;
            LB_BattlePower.Text = _formation.TeamBattlePowerPoint.ToString();
        }

        public void RefreshBattlePowerPoint()
        {
            _formation.ReComputePlayerTeamBattlePowerPoint();
            LB_BattlePower.Text = _formation.TeamBattlePowerPoint.ToString();
        }
        
    }
}
