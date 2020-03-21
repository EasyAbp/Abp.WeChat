using System.Collections.Generic;
using Newtonsoft.Json;
using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;

namespace EasyAbp.Abp.WeChat.Official.Services.CustomMenu
{
    public class CreateCustomMenuRequest : OfficialCommonRequest
    {
        [JsonProperty("button")]
        public IList<CustomButton> Buttons { get; set; }
    }
}