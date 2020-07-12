using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure
{
    /// <summary>
    /// 定义了微信支付退款回调的处理器。
    /// </summary>
    public interface IWeChatPayRefundHandler
    {
        Task HandleAsync(WeChatPayHandlerContext context);
    }
}