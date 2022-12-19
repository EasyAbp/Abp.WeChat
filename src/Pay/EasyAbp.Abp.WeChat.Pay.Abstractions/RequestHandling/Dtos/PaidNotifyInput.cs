using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;

[Serializable]
public class PaidNotifyInput
{
    public string MchId { get; set; }

    [Required]
    public string Xml { get; set; }
}