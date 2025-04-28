using Newtonsoft.Json;

namespace Flat_Rock_Technology___Test_task
{
    public class Product
    {
        [JsonProperty("productName")]
        public string ProductName { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("rating")]
        public string Rating { get; set; }
    }
}
