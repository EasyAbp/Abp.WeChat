using Volo.Abp.Modularity;

namespace EasyAbp.Abp.WeChat.Pay
{
    [DependsOn(typeof(AbpWeChatPayAbstractionsModule), typeof(AbpWeChatPayModule))]
    public class AbpWeChatPayHttpApiModule : AbpModule
    {
    }
}