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
    public partial class UCGeneralBag : UserControl
    {
        public UCGeneralBag()
        {
            InitializeComponent();
        }

        public void InitList()
        {
            foreach (int i in PlayerDataMgr.Instance.GetPlayerBag(SlotType.SlotType_General).Keys)
            {
                Console.WriteLine("Add general index {0}", i);
                //UCGeneralCell uc = new UCGeneralCell(i);
               
                
            }
        }
    }
}
