using System.Collections.Generic;
using Newtonsoft.Json;
using Zony.Abp.WeChat.Official.Infrastructure.Models;

namespace Zony.Abp.WeChat.Official.Services.CustomMenu
{
    public class CreateCustomMenuRequest : OfficialCommonRequest
    {
        [JsonProperty("button")]
        public IList<CustomButton> Buttons { get; set; }
    }
}