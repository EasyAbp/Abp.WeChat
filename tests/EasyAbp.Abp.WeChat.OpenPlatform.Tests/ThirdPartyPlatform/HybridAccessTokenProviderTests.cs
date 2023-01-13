using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.AccessToken;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using Shouldly;
using Volo.Abp;
using Xunit;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Tests.ThirdPartyPlatform;

public class HybridAccessTokenProviderTests : AbpWeChatOpenPlatformTestBase
{
    protected const string OptionsComponentAppId = AbpWeChatOpenPlatformTestsConsts.AppId;
    protected const string CustomComponentAppId = "my-component-appid";
    protected const string ComponentAccessToken = "my-component-access-token";
    protected const string AppAccessToken = "my-app-access-token";
    protected const string AuthorizerAppId = "my-authorizer-appid";

    private IAuthorizerAccessTokenCache _authorizerAccessTokenCache;
    private IAbpWeChatSharableCache _abpWeChatSharableCache;
    private readonly HybridAccessTokenProvider _hybridAccessTokenProvider;
    private readonly ICurrentWeChatThirdPartyPlatform _currentWeChatThirdPartyPlatform;

    public HybridAccessTokenProviderTests()
    {
        _hybridAccessTokenProvider = GetRequiredService<HybridAccessTokenProvider>();
        _currentWeChatThirdPartyPlatform = GetRequiredService<ICurrentWeChatThirdPartyPlatform>();
    }

    protected override void AfterAddApplication(IServiceCollection services)
    {
        base.AfterAddApplication(services);

        _authorizerAccessTokenCache = Substitute.For<IAuthorizerAccessTokenCache>();
        services.Replace(ServiceDescriptor.Transient(s => _authorizerAccessTokenCache));

        _abpWeChatSharableCache = Substitute.For<IAbpWeChatSharableCache>();
        services.Replace(ServiceDescriptor.Transient(s => _abpWeChatSharableCache));

        _authorizerAccessTokenCache.GetOrNullAsync(OptionsComponentAppId, AuthorizerAppId)
            .Returns(ComponentAccessToken);
        _abpWeChatSharableCache.GetOrNullAsync(Arg.Any<string>()).Returns(AppAccessToken);
    }

    [Fact]
    public async Task Should_Get_Authorizer_Access_Token()
    {
        (await _hybridAccessTokenProvider.GetAsync(AuthorizerAppId, null)).ShouldBe(ComponentAccessToken);

        using (_currentWeChatThirdPartyPlatform.Change(CustomComponentAppId))
        {
            await Should.ThrowAsync<UserFriendlyException>(
                () => _hybridAccessTokenProvider.GetAsync(AuthorizerAppId, null),
                "请实现 IAbpWeChatOptionsProvider<AbpWeChatThirdPartyPlatformOptions> 以支持多 appid 场景");
        }

        (await _hybridAccessTokenProvider.GetAsync(AuthorizerAppId, "123")).ShouldBe(AppAccessToken);
    }
}