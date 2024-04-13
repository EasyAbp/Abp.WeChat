using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Extensions;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Security;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.ApiRequests
{
    /// <summary>
    /// <see cref="IWeChatPayApiRequester"/> 的默认实现。
    /// </summary>
    [Dependency(TryRegister = true)]
    public class DefaultWeChatPayApiRequester : IWeChatPayApiRequester, ITransientDependency
    {
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAbpWeChatPayOptionsProvider _optionsProvider;
        private readonly IWeChatPayAuthorizationGenerator _authorizationGenerator;
        private readonly ILogger<DefaultWeChatPayApiRequester> _logger;

        public DefaultWeChatPayApiRequester(
            IHttpClientFactory httpClientFactory,
            IAbpWeChatPayOptionsProvider optionsProvider,
            IWeChatPayAuthorizationGenerator authorizationGenerator,
            ILogger<DefaultWeChatPayApiRequester> logger)
        {
            _httpClientFactory = httpClientFactory;
            _optionsProvider = optionsProvider;
            _authorizationGenerator = authorizationGenerator;
            _logger = logger;
        }

        public async Task<string> RequestAsync(HttpMethod method, string url, string body, string mchId = null)
        {
            var response = await RequestRawAsync(method, url, body, mchId);
            await LogFailureResponseAsync(response);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<TResponse> RequestAsync<TResponse>(HttpMethod method, string url, string body,
            string mchId = null)
        {
            var responseString = await RequestAsync(method, url, body, mchId);

            return JsonConvert.DeserializeObject<TResponse>(responseString);
        }

        public async Task<HttpResponseMessage> RequestRawAsync(HttpMethod method, string url, string body = null,
            string mchId = null)
        {
            var request = CreateRequest(method, url, body);

            // Setting the request header for the http client.
            var options = await _optionsProvider.GetAsync(mchId);
            var language = options.AcceptLanguage ?? ApiLanguages.DefaultLanguage;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(language));
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue("EasyAbp.Abp.WeChat.Pay", "1.0.0"));
            request.Headers.Add("Authorization",
                await _authorizationGenerator.GenerateAuthorizationAsync(method, url, body, mchId));

            // Sending the request.
            var client = _httpClientFactory.CreateClient();
            return await client.SendAsync(request);
        }

        public Task<string> RequestAsync(HttpMethod method, string url, object body, string mchId = null)
        {
            return RequestAsync(method, url, HandleRequestObject(method, body), mchId);
        }

        public Task<TResponse> RequestAsync<TResponse>(HttpMethod method, string url, object body, string mchId = null)
        {
            return RequestAsync<TResponse>(method, url, HandleRequestObject(method, body), mchId);
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string url, string body)
        {
            if (method == HttpMethod.Post || method == HttpMethod.Put)
            {
                return new HttpRequestMessage(method, url)
                {
                    Content = new StringContent(body, Encoding.UTF8, "application/json")
                };
            }

            if (method == HttpMethod.Get)
            {
                return new HttpRequestMessage(HttpMethod.Get, $"{url}?{body}");
            }

            return new HttpRequestMessage(HttpMethod.Get, url);
        }

        private string HandleRequestObject(HttpMethod method, object body)
        {
            if (method == HttpMethod.Post || method == HttpMethod.Put)
            {
                return JsonConvert.SerializeObject(body, JsonSerializerSettings);
            }

            if (method != HttpMethod.Get) return null;
            if (body is string bodyStr)
            {
                return bodyStr;
            }

            // Convert the object to query string.
            return WeChatReflectionHelper.ConvertToQueryString(body);
        }

        protected virtual async Task LogFailureResponseAsync(HttpResponseMessage responseMessage)
        {
            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Accepted:
                case HttpStatusCode.NoContent:
                    return;
                default:
                    _logger.LogError("微信支付接口调用失败，HTTP状态码：{StatusCode}，返回内容：{Content}",
                        responseMessage.StatusCode, await responseMessage.Content.ReadAsStringAsync());
                    break;
            }
        }
    }
}