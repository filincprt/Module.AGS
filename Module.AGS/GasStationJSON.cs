using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module.AGS
{
    public class StationInfo
    {
        [JsonProperty("station_id")]
        public int StationId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("price_AI92")]
        public decimal PriceAI92 { get; set; }

        [JsonProperty("price_AI95")]
        public decimal PriceAI95 { get; set; }

        [JsonProperty("price_AI98")]
        public decimal PriceAI98 { get; set; }

        [JsonProperty("price_diesel")]
        public decimal PriceDiesel { get; set; }

        [JsonProperty("remainder_AI92")]
        public int RemainderAI92 { get; set; }

        [JsonProperty("remainder_AI95")]
        public int RemainderAI95 { get; set; }

        [JsonProperty("remainder_AI98")]
        public int RemainderAI98 { get; set; }

        [JsonProperty("remainder_diesel")]
        public int RemainderDiesel { get; set; }
    }


}
