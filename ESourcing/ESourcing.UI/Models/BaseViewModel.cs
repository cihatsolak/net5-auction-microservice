using Newtonsoft.Json;

namespace ESourcing.UI.Models
{
    public abstract class BaseViewModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
