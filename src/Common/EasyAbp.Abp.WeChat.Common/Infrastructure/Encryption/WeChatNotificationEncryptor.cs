using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure.Encryption;

public class WeChatNotificationEncryptor : IWeChatNotificationEncryptor, ITransientDependency
{
    private readonly IJsonSerializer _jsonSerializer;

    protected static readonly Random Random = new();

    public WeChatNotificationEncryptor(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public virtual Task<string> EncryptAsync(string token, string encodingAesKey, string appId, string xml)
    {
        var crypt = new WXBizMsgCrypt(token, encodingAesKey, appId);

        string encryptedXml = null;

        var errCode = crypt.EncryptMsg(xml,
            ((int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds).ToString(),
            GenerateNonce(), ref encryptedXml);

        if (errCode != 0)
        {
            throw new UserFriendlyException($"加密失败，错误码: {errCode}");
        }

        return Task.FromResult(encryptedXml);
    }

    public virtual Task<TModel> DecryptAsync<TModel>(string token, string encodingAesKey, string appId,
        string msgSignature, string timestamp, string nonce, string encryptedXml)
        where TModel : ExtensibleObject, new()
    {
        if (encryptedXml == null)
        {
            throw new UserFriendlyException("没有找到加密内容");
        }

        string decryptXml = null;

        var crypt = new WXBizMsgCrypt(token, encodingAesKey, appId);

        var errCode = crypt.DecryptMsg(
            msgSignature,
            timestamp,
            nonce,
            encryptedXml,
            ref decryptXml);

        if (errCode != 0)
        {
            throw new UserFriendlyException($"解密失败，错误码: {errCode}");
        }

        var doc = XDocument.Parse(decryptXml);

        foreach (var node in doc.DescendantNodes().OfType<XCData>().ToList())
        {
            node.Parent!.Add(node.Value);
            node.Remove();
        }

        var dict = _jsonSerializer
            .Deserialize<Dictionary<string, Dictionary<string, object>>>(JsonConvert.SerializeXNode(doc.FirstNode))
            .FirstOrDefault().Value;

        var model = new TModel();
        foreach (var pair in dict)
        {
            model.SetProperty(pair.Key, pair.Value);
        }

        return Task.FromResult(model);
    }

    protected virtual string GenerateNonce()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        return new string(Enumerable.Repeat(chars, 32).Select(s => s[Random.Next(s.Length)]).ToArray());
    }
}