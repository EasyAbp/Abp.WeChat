using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling;

public interface IWeChatPayEventRequestHandlingService
{
    Task<WeChatRequestHandlingResult> PaidNotifyAsync(PaidNotifyInput input);

    Task<WeChatRequestHandlingResult> RefundNotifyAsync(RefundNotifyInput input);
}