using System.ComponentModel.DataAnnotations;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;

public class GetJsSdkWeChatPayParametersInput
{
    public string MchId { get; set; }

    [Required]
    public string AppId { get; set; }

    [Required]
    public string PrepayId { get; set; }
}