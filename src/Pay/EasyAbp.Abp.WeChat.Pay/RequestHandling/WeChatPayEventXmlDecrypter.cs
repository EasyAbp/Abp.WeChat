using System;
using System.Threading.Tasks;
using System.Xml;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Security.Extensions;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling;

public class WeChatPayEventXmlDecrypter : IWeChatPayEventXmlDecrypter, ITransientDependency
{
    public virtual async Task<(bool, XmlDocument)> TryDecryptAsync(XmlDocument weChatRequestXmlData,
        AbpWeChatPayOptions options)
    {
        var encryptedXml = weChatRequestXmlData.SelectSingleNode("/xml/req_info")?.InnerText;

        if (encryptedXml is null)
        {
            return (false, null);
        }

        var decryptedString = WeChatPaySecurityUtility.Decrypt(encryptedXml, options.ApiKey.ToMd5().ToLower());

        if (decryptedString.IsNullOrEmpty())
        {
            return (false, null);
        }

        try
        {
            var decryptedXmlDocument = new XmlDocument();
            decryptedXmlDocument.LoadXml(decryptedString);

            return (true, decryptedXmlDocument);
        }
        catch
        {
            return (false, null);
        }
    }
}