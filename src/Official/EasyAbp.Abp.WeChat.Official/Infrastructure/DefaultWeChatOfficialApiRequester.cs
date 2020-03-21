using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;

namespace EasyAbp.Abp.WeChat.Official.Infrastructure
{
    public class DefaultWeChatOfficialApiRequester : IWeChatOfficialApiRequester
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAccessTokenAccessor _accessTokenAccessor;

        public DefaultWeChatOfficialApiRequester(IHttpClientFactory httpClientFactory,
            IAccessTokenAccessor accessTokenAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _accessTokenAccessor = accessTokenAccessor;
        }

        public async Task<TResponse> RequestAsync<TResponse>(string targetUrl, HttpMethod method, IOfficialRequest officialRequest = null)
        {
            var client = _httpClientFactory.CreateClient();

            targetUrl = $"{targetUrl}access_token={await _accessTokenAccessor.GetAccessTokenAsync()}";

            var requestMsg = method == HttpMethod.Get
                ? BuildHttpGetRequestMessage(targetUrl, officialRequest)
                : BuildHttpPostRequestMessage(targetUrl, officialRequest);

            var resultStr = await (await client.SendAsync(requestMsg)).Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(resultStr);
        }

        private HttpRequestMessage BuildHttpGetRequestMessage(string targetUrl, IOfficialRequest officialRequest)
        {
            if (officialRequest == null) return new HttpRequestMessage(HttpMethod.Get, targetUrl);

            var requestUrl = BuildQueryString(targetUrl, officialRequest);
            return new HttpRequestMessage(HttpMethod.Get, requestUrl);
        }

        private HttpRequestMessage BuildHttpPostRequestMessage(string targetUrl, IOfficialRequest officialRequest)
        {
            return new HttpRequestMessage(HttpMethod.Post, targetUrl)
            {
                Content = new StringContent(officialRequest.ToString())
            };
        }

        private string BuildQueryString(string targetUrl, IOfficialRequest request)
        {
            if (request == null) return targetUrl;

            var type = request.GetType();
            var properties = type.GetProperties();
            var queryStringBuilder = new StringBuilder();

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