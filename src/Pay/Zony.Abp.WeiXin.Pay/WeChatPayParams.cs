using System;
using System.Collections.Generic;

namespace Zony.Abp.WeiXin.Pay
{
    /// <summary>
    /// 微信支付参数集合，所有接口参数都必须传入。
    /// </summary>
    public class WeChatPayParams
    {
        private readonly SortedDictionary<string, string> _sortedDictionary;

        public WeChatPayParams()
        {
            _sortedDictionary = new SortedDictionary<string, string>(StringComparer.Ordinal);
        }

        public void AddParameter(string key,string strValue)
        {
            if (string.IsNullOrEmpty(strValue)) return;
            if (_sortedDictionary.ContainsKey(key)) return;
            
            _sortedDictionary.Add(key,strValue);
        }
    }
}