using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Encryption;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.Abp.WeChat.Common.RequestHandling;

public class WeChatEventRequestHandlingServiceBase<TOptions> where TOptions : IAbpWeChatOptions
{
    protected IWeChatNotificationEncryptor WeChatNotificationEncryptor { get; }

    public WeChatEventRequestHandlingServiceBase(IWeChatNotificationEncryptor weChatNotificationEncryptor)
    {
        WeChatNotificationEncryptor = weChatNotificationEncryptor;
    }

    protected virtual async Task<T> DecryptMsgAsync<T>(TOptions options,
        WeChatEventRequestModel request) where T : ExtensibleObject, new()
    {
        return await WeChatNotificationEncryptor.DecryptAsync<T>(
            options.Token,
            options.EncodingAesKey,
            options.AppId,
            request.MsgSignature,
            request.Timestamp,
            request.Nonce,
            request.PostData);
    }
}