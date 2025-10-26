using System.ComponentModel.DataAnnotations;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;

public interface IHasMchId
{
    [Required] string MchId { get; set; }
}