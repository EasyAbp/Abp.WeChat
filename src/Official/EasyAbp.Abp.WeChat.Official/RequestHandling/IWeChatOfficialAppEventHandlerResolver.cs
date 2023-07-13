using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Official.RequestHandling;

public interface IWeChatOfficialAppEventHandlerResolver
{
    Task<List<IWeChatOfficialAppEventHandler>> GetAppEventHandlersAsync([CanBeNull] string msgType);
}