using Volo.Abp.Modularity;

namespace EasyAbp.Abp.WeChat.Pay
{
    [DependsOn(typeof(AbpWeChatPayAbstractionsModule))]
    public class AbpWeChatPayHttpApiModule : AbpModule
    {
    }
}