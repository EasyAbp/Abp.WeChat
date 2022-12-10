using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.OptionsResolve;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure;

public class DefaultAccessTokenAccessor : IAccessTokenAccessor, ISingletonDependency
{
    private readonly IAccessTokenProvider _accessTokenProvider;
    private readonly IWeChatOpenPlatformOptionsResolver _weChatOpenPlatformOptionsResolver;

    public DefaultAccessTokenAccessor(
        IAccessTokenProvider accessTokenProvider,
        IWeChatOpenPlatformOptionsResolver weChatOpenPlatformOptionsResolver)
    {
        _accessTokenProvider = accessTokenProvider;
        _weChatOpenPlatformOptionsResolver = weChatOpenPlatformOptionsResolver;
    }

    public virtual async Task<string> GetAccessTokenAsync()
    {
        var options = await _weChatOpenPlatformOptionsResolver.ResolveAsync();

        return await _accessTokenProvider.GetAccessTokenAsync(options.AppId, options.AppSecret);
    }
}