using EasyAbp.Abp.WeChat.Common;
using Volo.Abp.BlobStoring;
using Volo.Abp.Json.Newtonsoft;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.WeChat.Pay
{
    [DependsOn(
        typeof(AbpWeChatCommonModule),
        typeof(AbpBlobStoringModule),
        typeof(AbpWeChatPayAbstractionsModule),
        typeof(AbpJsonNewtonsoftModule)
    )]
    public class AbpWeChatPayModule : AbpModule
    {
    }
}