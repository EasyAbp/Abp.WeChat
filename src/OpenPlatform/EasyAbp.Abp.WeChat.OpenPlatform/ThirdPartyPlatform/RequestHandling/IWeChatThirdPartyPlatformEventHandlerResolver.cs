using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;

public interface IWeChatThirdPartyPlatformEventHandlerResolver
{
    Task<List<IWeChatThirdPartyPlatformAuthEventHandler>> GetAuthEventHandlersAsync([CanBeNull] string infoType);

    Task<List<IWeChatThirdPartyPlatformAppEventHandler>> GetAppEventHandlersAsync([CanBeNull] string msgType);
}