using System.Xml;
using EasyAbp.Abp.WeChat.Pay.Options;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling;

public class WeChatPayEventModel
{
    public AbpWeChatPayOptions Options { get; set; }

    public XmlDocument WeChatRequestXmlData { get; set; }

    [CanBeNull]
    public XmlDocument DecryptedXmlData { get; set; }
}