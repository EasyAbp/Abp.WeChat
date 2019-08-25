using System.IO;
using System.Linq;
using System.Xml.Linq;
using Zony.Abp.WeChat.Common.Infrastructure;

namespace Zony.Abp.WeChat.Pay.Models
{
    /// <summary>
    /// 微信支付参数集合，所有接口参数都必须传入。
    /// </summary>
    public class WeChatPayParameters : WeChatParameters
    {
        public virtual string ToXmlStr()
        {
            var xElement = new XElement("xml", SortedDictionary.Select(kv => new XElement(kv.Key, kv.Value)));

            using (var memoryStream = new MemoryStream())
            {
                xElement.Save(memoryStream);
                memoryStream.Position = 0;
                using (StreamReader reader = new StreamReader(memoryStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}