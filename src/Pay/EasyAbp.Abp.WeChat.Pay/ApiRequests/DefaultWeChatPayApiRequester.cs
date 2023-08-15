using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
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
            var request = CreateRequest(method, url, body, mchId);

            // Setting the request header for the http client.
            var options = await _optionsProvider.GetAsync(mchId);
            var language = options.AcceptLanguage ?? ApiLanguages.DefaultLanguage;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.AcceptLanguage.Add(new StringWithQualityHeaderValue(language));
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue("EasyAbp.Abp.WeChat.Pay", "1.0.0"));
            request.Headers.Add("Authorization", await _authorizationGenerator.GenerateAuthorizationAsync(method, url, body, mchId));

            // Sending the request.
            var client = await _httpClientFactory.CreateAsync(mchId);
            var response = await client.SendAsync(request);

            await ValidateResponseAsync(response);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<TResponse> RequestAsync<TResponse>(HttpMethod method, string url, string body, string mchId = null)
        {
            var responseString = await RequestAsync(method, url, body, mchId);

            return JsonConvert.DeserializeObject<TResponse>(responseString);
        }

        public Task<string> RequestAsync(HttpMethod method, string url, object body, string mchId = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<TResponse> RequestAsync<TResponse>(HttpMethod method, string url, object body, string mchId = null)
        {
            throw new System.NotImplementedException();
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string url, string body, string mchId = null)
        {
            if (method == HttpMethod.Post || method == HttpMethod.Put)
            {
                return new HttpRequestMessage(method, url)
                {
                    Content = new StringContent(body, Encoding.UTF8, "application/json")
                };
            }

            return new HttpRequestMessage(HttpMethod.Get, url);
        }

        protected virtual async Task ValidateResponseAsync(HttpResponseMessage responseMessage)
        {
            await Task.CompletedTask;
        }
    }
}