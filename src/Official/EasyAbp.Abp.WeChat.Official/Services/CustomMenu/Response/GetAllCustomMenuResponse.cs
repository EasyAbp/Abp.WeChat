using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.CustomMenu.Response
{
    public class GetAllCustomMenuResponse
    {
        [JsonPropertyName("menu")]
        [JsonProperty("menu")] 
        public GetAllCustomMenuInner Menu { get; set; }
    }
    
    public class GetAllCustomMenuInner
    {
        [JsonPropertyName("button")]
        [JsonProperty("button")] 
        public List<CustomButton> Buttons { get; set; }
    }
}