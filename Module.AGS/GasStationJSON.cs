using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AGS
{
    public class StationInfo
    {
        public int StationId { get; set; }
        public string Address { get; set; }
        public decimal PriceAI92 { get; set; }
        public decimal PriceAI95 { get; set; }
        public decimal PriceAI98 { get; set; }
        public decimal PriceDiesel { get; set; }
        public int RemainderAI92 { get; set; }
        public int RemainderAI95 { get; set; }
        public int RemainderAI98 { get; set; }
        public int RemainderDiesel { get; set; }
    }

}
