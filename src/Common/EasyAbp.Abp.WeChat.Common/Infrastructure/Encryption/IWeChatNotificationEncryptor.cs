using System.Threading.Tasks;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.Encryption;

public interface IWeChatNotificationEncryptor
{
    Task<string> EncryptAsync(string token, string encodingAesKey, string appId, string xml);

    Task<TModel> DecryptAsync<TModel>(string token, string encodingAesKey, string appId, string msgSignature,
        string timestamp, string nonce, string encryptedXml) where TModel : ExtensibleObject, new();
}