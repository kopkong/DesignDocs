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

        public void InitPlayerFormation()
        {
            Formation f = new Formation();
            f.InitPlayerFormation();

            foreach (int index in f.FormationMap.Keys)
            {
                Label lb_General = new Label();
                lb_General.Text = ConfigDataMgr.Instance._MapGeneral[index].Name;

                int r = f.FormationMap[index].RowIndex;
                int c = f.FormationMap[index].ColumnIndex;

                lb_General.TextAlign = ContentAlignment.MiddleCenter;
                tableLayoutPanel1.Controls.Add(lb_General, c,r);
                Console.WriteLine("添加了Formation table{0}{1}, 武将{2}", r,c,lb_General.Text);
            }

            LB_TeamName.Text = f.TeamName;
            LB_BattlePower.Text = f.TeamBattlePowerPoint.ToString();

            
        }

        public void InitNPCFormation()
        {

        }
        
    }
}
