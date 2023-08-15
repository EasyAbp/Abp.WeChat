using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.ParametersModel;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment;

public interface IBasicPaymentService : IAbpWeChatPayService
{
    Task CreateOrderAsync(CreateOrderRequest request);
}