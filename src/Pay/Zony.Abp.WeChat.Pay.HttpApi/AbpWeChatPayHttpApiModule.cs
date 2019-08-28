using Volo.Abp.Modularity;

namespace Zony.Abp.WeChat.Pay.HttpApi
{
    [DependsOn(typeof(AbpWeChatPayModule))]
    public class AbpWeChatPayHttpApiModule : AbpModule
    {
    }
}