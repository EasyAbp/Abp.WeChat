using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure
{
    public class DefaultWeChatPayApiRequester : IWeChatPayApiRequester, ISingletonDependency
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultWeChatPayApiRequester(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<XmlDocument> RequestAsync(string url, string body)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(body)
            };

            var client = _httpClientFactory.CreateClient("WeChatPay");
            var responseMessage = await client.SendAsync(request);
            
            if(responseMessage.StatusCode == HttpStatusCode.GatewayTimeout) throw new HttpRequestException("微信支付网关超时，请稍后重试。");

            var readAsString = await responseMessage.Content.ReadAsStringAsync();
            var newXmlDocument = new XmlDocument();
            newXmlDocument.LoadXml(readAsString);
            return newXmlDocument;
        }
    }
}