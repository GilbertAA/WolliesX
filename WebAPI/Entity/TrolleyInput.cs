using System.Collections.Generic;
using Newtonsoft.Json;

namespace WebAPI.Entity
{
    public class TrolleyInput
    {
        [JsonProperty("products")]
        public List<TrolleyProduct> products { get; set; }
        [JsonProperty("specials")]
        public List<Special> specials { get; set; }
        [JsonProperty("quantities")]
        public List<Quantity> quantities { get; set; }
    }

    public class TrolleyProduct
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("price")]
        public long price { get; set; }
    }

    public class Quantity
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("quantity")]
        public long quantity { get; set; }
    }

    public class Special
    {
        [JsonProperty("quantities")]
        public List<Quantity> quantities { get; set; }
        [JsonProperty("total")]
        public long total { get; set; }
    }
}
