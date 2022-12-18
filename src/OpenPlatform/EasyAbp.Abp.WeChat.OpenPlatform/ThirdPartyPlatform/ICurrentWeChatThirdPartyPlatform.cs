using System;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform;

public interface ICurrentWeChatThirdPartyPlatform
{
    [CanBeNull]
    string ComponentAppId { get; }

    IDisposable Change([CanBeNull] string componentAppId);
}