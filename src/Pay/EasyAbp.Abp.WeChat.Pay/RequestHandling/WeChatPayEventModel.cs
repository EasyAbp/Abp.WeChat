using EasyAbp.Abp.WeChat.Pay.Options;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling;

public class WeChatPayEventModel
{
    public AbpWeChatPayOptions Options { get; set; }
}