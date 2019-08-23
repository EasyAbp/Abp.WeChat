using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using Volo.Abp.DependencyInjection;

namespace Zony.Abp.WeChat.Pay.Infrastructure
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

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.SendAsync(request);
            
            if(responseMessage.StatusCode == HttpStatusCode.GatewayTimeout) throw new HttpRequestException("微信支付网关超时，请稍后重试。");
            var readAsStream = await responseMessage.Content.ReadAsStreamAsync();

            var newXmlDocument = new XmlDocument();
            newXmlDocument.Load(readAsStream);
            return newXmlDocument;
        }
    }
}