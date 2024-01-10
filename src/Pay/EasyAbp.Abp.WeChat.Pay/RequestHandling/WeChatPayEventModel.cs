using EasyAbp.Abp.WeChat.Pay.Options;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling;

public class WeChatPayEventModel<TResource>
{
    public AbpWeChatPayOptions Options { get; set; }

    public TResource Resource { get; set; }
}