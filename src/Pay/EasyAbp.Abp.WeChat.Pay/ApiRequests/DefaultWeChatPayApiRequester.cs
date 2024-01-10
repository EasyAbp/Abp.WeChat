using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Extensions;
using EasyAbp.Abp.WeChat.Pay.Exceptions;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Security;
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

        private readonly IAbpWeChatPayHttpClientFactory _httpClientFactory;
        private readonly IWeChatPayAuthorizationGenerator _authorizationGenerator;
        private readonly IAbpWeChatPayOptionsProvider _optionsProvider;

        public DefaultWeChatPayApiRequester(IAbpWeChatPayHttpClientFactory httpClientFactory,
            IAbpWeChatPayOptionsProvider optionsProvider,
            IWeChatPayAuthorizationGenerator authorizationGenerator)
        {
            _httpClientFactory = httpClientFactory;
            _optionsProvider = optionsProvider;
            _authorizationGenerator = authorizationGenerator;
        }

        public async Task<string> RequestAsync(HttpMethod method, string url, string body, string mchId = null)
        {
            var response = await RequestRawAsync(method, url, body, mchId);
            await ValidateResponseAsync(response);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<TResponse> RequestAsync<TResponse>(HttpMethod method, string url, string body,
            string mchId = null)
        {
            var responseString = await RequestAsync(method, url, body, mchId);

            return JsonConvert.DeserializeObject<TResponse>(responseString);
        }

        public async Task<HttpResponseMessage> RequestRawAsync(HttpMethod method, string url, string body = null, string mchId = null)
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
            var client = await _httpClientFactory.CreateAsync(mchId);
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

        protected virtual Task ValidateResponseAsync(HttpResponseMessage responseMessage)
        {
            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.OK:
                    return Task.CompletedTask;
                case HttpStatusCode.Accepted:
                    return Task.CompletedTask;
                case HttpStatusCode.NoContent:
                    return Task.CompletedTask;
                default:
                    throw new CallWeChatPayApiException("微信支付 API 调用失败，状态码为非 200。");
            }
        }
    }
}