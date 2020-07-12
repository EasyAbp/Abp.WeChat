using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure
{
    /// <summary>
    /// 定义了微信支付回调处理器。
    /// </summary>
    public interface IWeChatPayHandler
    {
        Task HandleAsync(WeChatPayHandlerContext context);

        WeChatHandlerType Type { get; }
    }
}