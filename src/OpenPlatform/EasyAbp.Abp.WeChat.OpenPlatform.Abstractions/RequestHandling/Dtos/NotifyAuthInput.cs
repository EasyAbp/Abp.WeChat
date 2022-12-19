using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;

namespace EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling.Dtos;

[Serializable]
public class NotifyAuthInput
{
    public string ComponentAppId { get; set; }

    [Required]
    public WeChatEventRequestModel EventRequest { get; set; }
}