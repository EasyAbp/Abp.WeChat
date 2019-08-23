using Zony.Abp.WeiXin.Pay.Models;

namespace Zony.Abp.WeiXin.Pay.Infrastructure
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