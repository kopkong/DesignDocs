using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class BuffInfo
    {
        static string[] BuffAttributeName = 
        { 
            "Default", "HP", "MaxHP", "Attack", "Defense",
            "MoveSpeed", "AttackSpeed", "AttackRange", "CriticalRate", "DodgeRate",
            "HitRate", "SuckBlood", "Stun", "God", "Sheep","Shield"
        };

        public int BuffID { get; set; }

        public int BuffValue { get; set; }

        public int Time { get; set; }

        public int TargetType { get; set; }

       
    }
}
