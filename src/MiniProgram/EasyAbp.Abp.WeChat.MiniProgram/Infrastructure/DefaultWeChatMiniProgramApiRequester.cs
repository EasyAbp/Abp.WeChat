using System;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.Models;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure
{
    [Dependency(TryRegister = true)]
    public class DefaultWeChatMiniProgramApiRequester : IWeChatMiniProgramApiRequester, ITransientDependency
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAccessTokenAccessor _accessTokenAccessor;

        public DefaultWeChatMiniProgramApiRequester(IHttpClientFactory httpClientFactory,
            IAccessTokenAccessor accessTokenAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _accessTokenAccessor = accessTokenAccessor;
        }

        public async Task<TResponse> RequestAsync<TResponse>(string targetUrl, HttpMethod method, IMiniProgramRequest miniProgramRequest = null, bool withAccessToken = true)
        {
            var client = _httpClientFactory.CreateClient();

            targetUrl = targetUrl.EnsureEndsWith('?');

            if (withAccessToken)
            {
                targetUrl += $"access_token={await _accessTokenAccessor.GetAccessTokenAsync()}";
            }

            var requestMsg = method == HttpMethod.Get
                ? BuildHttpGetRequestMessage(targetUrl, miniProgramRequest)
                : BuildHttpPostRequestMessage(targetUrl, miniProgramRequest);

            var resultStr = await (await client.SendAsync(requestMsg)).Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(resultStr);
        }

        private HttpRequestMessage BuildHttpGetRequestMessage(string targetUrl, IMiniProgramRequest miniProgramRequest)
        {
            if (miniProgramRequest == null) return new HttpRequestMessage(HttpMethod.Get, targetUrl);

            var requestUrl = BuildQueryString(targetUrl, miniProgramRequest);
            return new HttpRequestMessage(HttpMethod.Get, requestUrl);
        }

        private HttpRequestMessage BuildHttpPostRequestMessage(string targetUrl, IMiniProgramRequest miniProgramRequest)
        {
            return new HttpRequestMessage(HttpMethod.Post, targetUrl)
            {
                Content = new StringContent(miniProgramRequest.ToString())
            };
        }

        private string BuildQueryString(string targetUrl, IMiniProgramRequest request)
        {
            if (request == null) return targetUrl;

            var type = request.GetType();
            var properties = type.GetProperties();

            if (properties.Length > 0)
            {
                targetUrl = targetUrl.EnsureEndsWith('&');
            }
            
            var queryStringBuilder = new StringBuilder(targetUrl);

            foreach (var propertyInfo in properties)
            {
                var jsonProperty = propertyInfo.GetCustomAttribute<JsonPropertyAttribute>();
                var propertyName = jsonProperty != null ? jsonProperty.PropertyName : propertyInfo.Name;

                queryStringBuilder.Append($"{propertyName}={propertyInfo.GetValue(request)}&");
            }

            return queryStringBuilder.ToString().TrimEnd('&');
        }
    }
}