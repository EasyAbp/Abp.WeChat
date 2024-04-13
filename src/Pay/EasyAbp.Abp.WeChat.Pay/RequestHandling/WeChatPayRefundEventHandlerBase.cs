using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Models;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling;

public abstract class WeChatPayRefundEventHandlerBase : IWeChatPayEventHandler<WeChatPayRefundEventModel>
{
    public virtual WeChatHandlerType Type => WeChatHandlerType.Refund;

    public abstract Task<WeChatRequestHandlingResult> HandleAsync(WeChatPayEventModel<WeChatPayRefundEventModel> model);
}