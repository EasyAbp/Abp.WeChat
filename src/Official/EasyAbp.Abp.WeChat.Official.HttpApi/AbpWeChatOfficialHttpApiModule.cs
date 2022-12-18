using EasyAbp.Abp.WeChat.OpenPlatform;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.WeChat.Official
{
    [DependsOn(typeof(AbpWeChatOfficialAbstractionsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class AbpWeChatOfficialHttpApiModule : AbpModule
    {
    }
}