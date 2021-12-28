using System.Collections.Generic;
using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Official.Services.CustomMenu.Request
{
    public class CreateCustomMenuRequest : OfficialCommonRequest
    {
        [JsonProperty("button")]
        public IList<CustomButton> Buttons { get; set; }
    }
}