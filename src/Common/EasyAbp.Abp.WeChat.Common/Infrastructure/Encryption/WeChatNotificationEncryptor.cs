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

    public WeChatNotificationEncryptor(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public virtual async Task<TModel> DecryptPostDataAsync<TModel>(string token, string encodingAesKey, string appId,
        string msgSignature, string timestamp, string notice, string postData) where TModel : ExtensibleObject, new()
    {
        if (postData == null)
        {
            throw new UserFriendlyException("没有找到加密内容");
        }

        string decryptXml = null;

        var crypt = new WXBizMsgCrypt(token, encodingAesKey, appId);

        var errCode = crypt.DecryptMsg(
            msgSignature,
            timestamp,
            notice,
            postData,
            ref decryptXml);

        if (errCode != 0)
        {
            throw new BusinessException($"解密失败，错误码: {errCode}");
        }

        var doc = XDocument.Parse(decryptXml);
        var node_cdata = doc.DescendantNodes().OfType<XCData>().ToList();

        foreach (var node in node_cdata)
        {
            node.Parent.Add(node.Value);
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

        return model;
    }
}