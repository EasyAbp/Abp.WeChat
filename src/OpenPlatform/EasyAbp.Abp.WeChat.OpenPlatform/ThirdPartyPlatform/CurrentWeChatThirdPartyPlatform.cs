using System;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform;

public class CurrentWeChatThirdPartyPlatform : ICurrentWeChatThirdPartyPlatform, ITransientDependency
{
    private readonly ICurrentWeChatThirdPartyPlatformAccessor _currentWeChatThirdPartyPlatformAccessor;

    public string ComponentAppId => _currentWeChatThirdPartyPlatformAccessor.Current?.ComponentAppId;

    public CurrentWeChatThirdPartyPlatform(
        ICurrentWeChatThirdPartyPlatformAccessor currentWeChatThirdPartyPlatformAccessor)
    {
        _currentWeChatThirdPartyPlatformAccessor = currentWeChatThirdPartyPlatformAccessor;
    }

    public IDisposable Change(string componentAppId) => SetCurrent(componentAppId);

    private IDisposable SetCurrent([CanBeNull] string componentAppId)
    {
        var parentScope = _currentWeChatThirdPartyPlatformAccessor.Current;

        _currentWeChatThirdPartyPlatformAccessor.Current = new WeChatThirdPartyPlatformInfo(componentAppId);

        return new DisposeAction(() => _currentWeChatThirdPartyPlatformAccessor.Current = parentScope);
    }
}