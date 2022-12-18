using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.ApiRequests
{
    [Dependency(TryRegister = true)]
    public class DefaultWeChatPayApiRequester : IWeChatPayApiRequester, ITransientDependency
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultWeChatPayApiRequester(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public virtual async Task<XmlDocument> RequestAsync(string url, string body)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(body)
            };

            var client = _httpClientFactory.CreateClient("WeChatPay");
            var responseMessage = await client.SendAsync(request);
            var readAsString = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"微信支付接口请求失败。\n错误码: {responseMessage.StatusCode}，\n响应内容: {readAsString}");
            }

            var newXmlDocument = new XmlDocument();
            try
            {
                newXmlDocument.LoadXml(readAsString);
            }
            catch (XmlException e)
            {
                throw new HttpRequestException($"请求接口失败，返回的不是一个标准的 XML 文档。\n响应内容: {readAsString}");
            }

            return newXmlDocument;
        }
    }
}