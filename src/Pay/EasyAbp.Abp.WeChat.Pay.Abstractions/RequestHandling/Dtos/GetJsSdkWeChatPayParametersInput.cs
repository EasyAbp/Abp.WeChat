using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;

[Serializable]
public class GetJsSdkWeChatPayParametersInput
{
    public string MchId { get; set; }

    [Required]
    public string AppId { get; set; }

    [Required]
    public string PrepayId { get; set; }
}