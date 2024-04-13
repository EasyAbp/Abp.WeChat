using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.RequestHandling;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Models;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling;

public abstract class WeChatPayPaidEventHandlerBase : IWeChatPayEventHandler<WeChatPayPaidEventModel>
{
    public virtual WeChatHandlerType Type => WeChatHandlerType.Paid;

    public abstract Task<WeChatRequestHandlingResult> HandleAsync(WeChatPayEventModel<WeChatPayPaidEventModel> model);
}