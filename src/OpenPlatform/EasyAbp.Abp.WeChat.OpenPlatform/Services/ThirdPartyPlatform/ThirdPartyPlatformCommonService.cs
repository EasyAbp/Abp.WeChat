using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.ComponentAccessToken;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.ThirdPartyPlatform;

public abstract class ThirdPartyPlatformCommonService : CommonService
{
    protected IComponentAccessTokenProvider ComponentAccessTokenProvider =>
        LazyLoadService(ref _componentAccessTokenProvider);

    private IComponentAccessTokenProvider _componentAccessTokenProvider;
}