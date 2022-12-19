using System.ComponentModel.DataAnnotations;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;

public class RefundNotifyInput
{
    public string MchId { get; set; }

    [Required]
    public string Xml { get; set; }
}