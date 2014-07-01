using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApplication1;

namespace WindowsFormsApplication1
{
    public partial class UCGeneralCell : UserControl
    {
        private int Index;

        public UCGeneralCell(int index)
        {
            InitializeComponent();

            Index = index;

            InitData();
        }

        private void InitData()
        {
            Slot slotData = PlayerDataMgr.Instance.GetPlayerBag(SlotType.SlotType_General)[Index];

            BTN_General.Text = ConfigDataMgr.Instance._MapGeneral[slotData.ConfigID].Name;
            LB_Lv.Text = slotData.Lv.ToString();
            LB_Rank.Text = slotData.Rank.ToString();
        }
    }
}
