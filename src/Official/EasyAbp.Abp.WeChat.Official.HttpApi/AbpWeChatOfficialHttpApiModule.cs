using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.WeChat.Official.HttpApi
{
    [DependsOn(typeof(AbpWeChatOfficialModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class AbpWeChatOfficialHttpApiModule : AbpModule
    {
    }
}