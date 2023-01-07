using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.ApiRequests
{
    [Dependency(TryRegister = true)]
    public class DefaultWeChatPayApiRequester : IWeChatPayApiRequester, ITransientDependency
    {
        private readonly IAbpWeChatPayHttpClientFactory _httpClientFactory;

        public DefaultWeChatPayApiRequester(IAbpWeChatPayHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public virtual async Task<XmlDocument> RequestAsync(string url, string body, string mchId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(body, Encoding.UTF8, "application/xml")
            };

            var client = await _httpClientFactory.CreateAsync(mchId);
            var responseMessage = await client.SendAsync(request);
            var readAsString = await responseMessage.Content.ReadAsStringAsync();

            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"微信支付接口请求失败。\n错误码: {responseMessage.StatusCode}，\n响应内容: {readAsString}");
            }

            var newXmlDocument = new XmlDocument();
            try
            {
                newXmlDocument.LoadXml(readAsString);
            }
            catch (XmlException)
            {
                throw new HttpRequestException($"请求接口失败，返回的不是一个标准的 XML 文档。\n响应内容: {readAsString}");
            }

            return newXmlDocument;
        }
    }
}