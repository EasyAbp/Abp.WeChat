using System.ComponentModel.DataAnnotations;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;

public class PaidNotifyInput
{
    public string MchId { get; set; }

    [Required]
    public string Xml { get; set; }
}