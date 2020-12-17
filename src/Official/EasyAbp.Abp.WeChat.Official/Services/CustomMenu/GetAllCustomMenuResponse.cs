using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.CustomMenu
{
    public class GetAllCustomMenuResponse
    {
        [JsonProperty("menu")] 
        public GetAllCustomMenuInner Menu { get; set; }
    }
    
    public class GetAllCustomMenuInner
    {
        [JsonProperty("button")] 
        public List<CustomButton> Buttons { get; set; }
    }
}