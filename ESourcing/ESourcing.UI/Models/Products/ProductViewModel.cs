using Newtonsoft.Json;

namespace ESourcing.UI.Models.Products
{
    public class ProductViewModel : BaseViewModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("category")]
        public string Category { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("imageFile")]
        public string ImageFile { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}
