using System;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.MiniProgram.Models;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.MiniProgram.ApiRequests
{
    [Dependency(TryRegister = true)]
    public class DefaultWeChatMiniProgramApiRequester : IWeChatMiniProgramApiRequester, ITransientDependency
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAccessTokenProvider _accessTokenProvider;

        public DefaultWeChatMiniProgramApiRequester(
            IHttpClientFactory httpClientFactory,
            IAccessTokenProvider accessTokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _accessTokenProvider = accessTokenProvider;
        }

        public virtual async Task<TResponse> RequestAsync<TResponse>(string targetUrl, HttpMethod method,
            IMiniProgramRequest miniProgramRequest, IAbpWeChatOptions abpWeChatOptions)
        {
            var responseMessage = await RequestGetHttpResponseMessageAsync(
                targetUrl, method, miniProgramRequest, abpWeChatOptions);

            var resultStr = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(resultStr);
        }

        public virtual async Task<TResponse> RequestGetBinaryDataAsync<TResponse>(string targetUrl, HttpMethod method,
            IMiniProgramRequest miniProgramRequest, IAbpWeChatOptions abpWeChatOptions)
            where TResponse : IHasBinaryData
        {
            var responseMessage =
                await RequestGetHttpResponseMessageAsync(targetUrl, method, miniProgramRequest, abpWeChatOptions);

            var resultStr = await responseMessage.Content.ReadAsStringAsync();

            try
            {
                return JsonConvert.DeserializeObject<TResponse>(resultStr);
            }
            catch (Exception)
            {
                var result = JsonConvert.DeserializeObject<TResponse>("{}");
                // var result = default(TResponse);

                result.BinaryData = await responseMessage.Content.ReadAsByteArrayAsync();

                return result;
            }
        }

        private async Task<HttpResponseMessage> RequestGetHttpResponseMessageAsync(string targetUrl, HttpMethod method,
            IMiniProgramRequest miniProgramRequest, IAbpWeChatOptions abpWeChatOptions)
        {
            var client = _httpClientFactory.CreateClient(AbpWeChatConsts.HttpClientName);

            targetUrl = targetUrl.EnsureEndsWith('?');

            if (abpWeChatOptions is not null)
            {
                targetUrl +=
                    $"access_token={await _accessTokenProvider.GetAsync(abpWeChatOptions.AppId, abpWeChatOptions.AppSecret)}";
            }

            var requestMsg = method == HttpMethod.Get
                ? BuildHttpGetRequestMessage(targetUrl, miniProgramRequest)
                : BuildHttpPostRequestMessage(targetUrl, miniProgramRequest);

            return await client.SendAsync(requestMsg);
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
                Content = miniProgramRequest.ToStringContent()
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