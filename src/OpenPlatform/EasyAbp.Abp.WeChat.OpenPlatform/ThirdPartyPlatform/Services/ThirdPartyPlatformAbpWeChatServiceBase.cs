using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.ApiRequests;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.ComponentAccessToken;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Services;

public abstract class ThirdPartyPlatformAbpWeChatServiceBase :
    AbpWeChatServiceBase<AbpWeChatThirdPartyPlatformOptions, IWeChatThirdPartyPlatformApiRequester>
{
    protected IComponentAccessTokenProvider ComponentAccessTokenProvider =>
        LazyServiceProvider.LazyGetRequiredService<IComponentAccessTokenProvider>();

    protected ThirdPartyPlatformAbpWeChatServiceBase(AbpWeChatThirdPartyPlatformOptions options, IAbpLazyServiceProvider lazyServiceProvider) : base(options, lazyServiceProvider)
    {
    }
}