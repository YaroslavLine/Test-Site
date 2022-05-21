using Newtonsoft.Json;

namespace TestSite.Core.Models
{
    class ReCapthaResult
    {
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}