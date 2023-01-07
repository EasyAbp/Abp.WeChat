using System;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.Official.Models;
using Newtonsoft.Json;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Official.ApiRequests
{
    [Dependency(TryRegister = true)]
    public class DefaultWeChatOfficialApiRequester : IWeChatOfficialApiRequester, ITransientDependency
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAccessTokenProvider _accessTokenProvider;

        public DefaultWeChatOfficialApiRequester(
            IHttpClientFactory httpClientFactory,
            IAccessTokenProvider accessTokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _accessTokenProvider = accessTokenProvider;
        }

        #region > Public Methods <

        public virtual async Task<string> RequestAsync(string targetUrl, HttpMethod method,
            IOfficialRequest officialRequest = null, IAbpWeChatOptions abpWeChatOptions = null)
        {
            var client = _httpClientFactory.CreateClient(AbpWeChatConsts.HttpClientName);

            targetUrl = targetUrl.EnsureEndsWith('?');

            if (abpWeChatOptions is not null)
            {
                targetUrl +=
                    $"access_token={await _accessTokenProvider.GetAsync(abpWeChatOptions.AppId, abpWeChatOptions.AppSecret)}";
            }

            var requestMsg = method == HttpMethod.Get
                ? BuildHttpGetRequestMessage(targetUrl, officialRequest)
                : BuildHttpPostRequestMessage(targetUrl, officialRequest);

            return await (await client.SendAsync(requestMsg)).Content.ReadAsStringAsync();
        }

        public virtual async Task<TResponse> RequestAsync<TResponse>(string targetUrl,
            HttpMethod method,
            IOfficialRequest officialRequest = null,
            IAbpWeChatOptions abpWeChatOptions = null)
        {
            var resultStr = await RequestAsync(targetUrl,
                method,
                officialRequest,
                abpWeChatOptions);

            return JsonConvert.DeserializeObject<TResponse>(resultStr);
        }

        public virtual async Task<TResponse> RequestFromDataAsync<TResponse>(string targetUrl,
            MultipartFormDataContent formDataContent,
            IOfficialRequest officialRequest = null,
            IAbpWeChatOptions abpWeChatOptions = null)
        {
            var client = _httpClientFactory.CreateClient(AbpWeChatConsts.HttpClientName);
            targetUrl = targetUrl.EnsureEndsWith('?');

            if (abpWeChatOptions is not null)
            {
                targetUrl +=
                    $"access_token={await _accessTokenProvider.GetAsync(abpWeChatOptions.AppId, abpWeChatOptions.AppSecret)}";
            }

            var requestMsg = BuildHttpGetRequestMessage(targetUrl, officialRequest);
            requestMsg.Method = HttpMethod.Post;

            // 处理 HttpClient 提交表单的问题。
            // 链接: https://www.cnblogs.com/myzony/p/12114507.html
            var boundaryValue = formDataContent.Headers.ContentType.Parameters.Single(p => p.Name == "boundary");
            boundaryValue.Value = boundaryValue.Value.Replace("\"", String.Empty);
            requestMsg.Content = formDataContent;

            var responseString = await (await client.SendAsync(requestMsg)).Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseString);
        }

        #endregion

        #region > Private Methods <

        private HttpRequestMessage BuildHttpGetRequestMessage(string targetUrl, IOfficialRequest officialRequest)
        {
            if (officialRequest == null)
            {
                return new HttpRequestMessage(HttpMethod.Get, targetUrl);
            }

            var requestUrl = BuildQueryString(targetUrl, officialRequest);
            return new HttpRequestMessage(HttpMethod.Get, requestUrl);
        }

        private HttpRequestMessage BuildHttpPostRequestMessage(string targetUrl, IOfficialRequest officialRequest)
        {
            return new HttpRequestMessage(HttpMethod.Post, targetUrl)
            {
                Content = officialRequest.ToStringContent()
            };
        }

        private string BuildQueryString(string targetUrl, IOfficialRequest request)
        {
            if (request == null)
            {
                return targetUrl;
            }

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

        #endregion
    }
}