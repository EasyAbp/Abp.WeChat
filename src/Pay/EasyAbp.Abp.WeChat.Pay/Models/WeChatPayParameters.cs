using System.IO;
using System.Linq;
using System.Xml.Linq;
using EasyAbp.Abp.WeChat.Common.Infrastructure;

namespace EasyAbp.Abp.WeChat.Pay.Models
{
    /// <summary>
    /// 适用于微信支付的参数定义，提供了微信支付需要用到的辅助方法。
    /// </summary>
    public class WeChatPayParameters : WeChatParameters
    {
        /// <summary>
        /// 将存储的所有参数和值以 XML 格式输出。
        /// </summary>
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