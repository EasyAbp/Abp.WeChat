using Zony.Abp.WeChat.Pay.Models;

namespace Zony.Abp.WeChat.Pay.Infrastructure
 {
     public interface ISignatureGenerator
     {
         /// <summary>
         /// 根据开发人员的接口请求参数，生成签名数据。
         /// </summary>
         /// <returns>生成的签名数据。</returns>
         string Generate(WeChatPayRequest payRequest);
     }
 }