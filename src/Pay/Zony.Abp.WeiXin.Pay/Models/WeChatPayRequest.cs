using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Zony.Abp.WeiXin.Pay.Models
{
    /// <summary>
    /// 微信支付参数集合，所有接口参数都必须传入。
    /// </summary>
    public abstract class WeChatPayRequest
    {
        private readonly SortedDictionary<string, string> _sortedDictionary;

        public WeChatPayRequest()
        {
            _sortedDictionary = new SortedDictionary<string, string>(StringComparer.Ordinal);
        }

        public virtual void AddParameter(string key, string strValue)
        {
            if (string.IsNullOrEmpty(strValue)) return;
            if (_sortedDictionary.ContainsKey(key)) return;

            _sortedDictionary.Add(key, strValue);
        }

        public virtual string GetWaitForSignatureStr()
        {
            var sb = new StringBuilder();
            foreach (var kv in _sortedDictionary)
            {
                sb.Append(kv.Key).Append('=').Append(kv.Value).Append('&');
            }

            return sb.ToString().TrimEnd('&');
        }

        public virtual string ToXmlStr()
        {
            var xElement = new XElement("xml", _sortedDictionary.Select(kv => new XElement(kv.Key, kv.Value)));

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