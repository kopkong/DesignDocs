using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    /// <summary>
    /// 一项东西，包含类型，ID，数量
    /// </summary>
    public class ItemPack
    {
        public int Type { get; set; }
        public int ID { get; set; }
        public int Count { get; set; }

        public ItemPack(string s)
        {
            if (s.Contains(','))
            {
                string[] ary = s.Split(',');
                this.Type = Convert.ToInt32(ary[0]);
                this.ID = Convert.ToInt32(ary[1]);
                this.Count = Convert.ToInt32(ary[2]);
            }
            else
            {
                this.Type = 0;
            }
        }
    }

    public class Commodity
    {
        
    }
}
