using System;
using System.Collections.Generic;
using System.Text;

namespace EasyAbp.Abp.WeChat.Common.Infrastructure
{
    public class WeChatParameters
    {
        protected SortedDictionary<string, string> SortedDictionary { get; }

        public WeChatParameters()
        {
            SortedDictionary = new SortedDictionary<string, string>(StringComparer.Ordinal);
        }
        
        public virtual void AddParameter(string key, string strValue)
        {
            if (string.IsNullOrEmpty(strValue)) return;
            if (SortedDictionary.ContainsKey(key)) return;

            SortedDictionary.Add(key, strValue);
        }
        
        public virtual void AddParameter<T>(string key, T intValue)
        {
            SortedDictionary.Add(key,intValue.ToString());
        }
        
        public virtual string GetWaitForSignatureStr()
        {
            var sb = new StringBuilder();
            foreach (var kv in SortedDictionary)
            {
                sb.Append(kv.Key).Append('=').Append(kv.Value).Append('&');
            }

            return sb.ToString().TrimEnd('&');
        }
    }
}