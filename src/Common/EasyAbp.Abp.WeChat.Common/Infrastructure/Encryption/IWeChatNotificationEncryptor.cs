using System.Threading.Tasks;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.Encryption;

public interface IWeChatNotificationEncryptor
{
    Task<TModel> DecryptPostDataAsync<TModel>(string token, string encodingAesKey, string appId,
        string msgSignature, string timestamp, string notice, string postData) where TModel : ExtensibleObject, new();
}