using System.Threading;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Models;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform;

public class CurrentWeChatThirdPartyPlatformAccessor : ICurrentWeChatThirdPartyPlatformAccessor
{
    public static CurrentWeChatThirdPartyPlatformAccessor Instance { get; } = new();

    public WeChatThirdPartyPlatformInfo Current
    {
        get => _currentScope.Value;
        set => _currentScope.Value = value;
    }

    private readonly AsyncLocal<WeChatThirdPartyPlatformInfo> _currentScope;

    private CurrentWeChatThirdPartyPlatformAccessor()
    {
        _currentScope = new AsyncLocal<WeChatThirdPartyPlatformInfo>();
    }
}