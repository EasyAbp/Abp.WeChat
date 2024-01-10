using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling
{
    /// <summary>
    /// 定义了微信支付回调处理器。
    /// </summary>
    public interface IWeChatPayEventHandler
    {
        WeChatHandlerType Type { get; }

        Task<WeChatRequestHandlingResult> HandleAsync<TResource>(WeChatPayEventModel<TResource> model);
    }
}