using Volo.Abp.Modularity;

namespace EasyAbp.Abp.WeChat.Pay.HttpApi
{
    [DependsOn(typeof(AbpWeChatPayModule))]
    public class AbpWeChatPayHttpApiModule : AbpModule
    {
    }
}