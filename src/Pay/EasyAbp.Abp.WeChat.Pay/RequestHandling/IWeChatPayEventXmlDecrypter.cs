using System.Threading.Tasks;
using System.Xml;
using EasyAbp.Abp.WeChat.Pay.Options;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling;

public interface IWeChatPayEventXmlDecrypter
{
    Task<(bool, XmlDocument)> TryDecryptAsync(XmlDocument weChatRequestXmlData, AbpWeChatPayOptions options);
}