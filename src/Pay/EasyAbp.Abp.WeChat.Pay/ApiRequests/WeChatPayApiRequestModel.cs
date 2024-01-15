using System;
using System.Net.Http;
using System.Text;

namespace EasyAbp.Abp.WeChat.Pay.ApiRequests;

public class WeChatPayApiRequestModel
{
    public HttpMethod Method { get; protected set; }

    public string Url { get; protected set; }

    public string Body { get; protected set; }

    public string Timestamp { get; protected set; }

    public string RandomString { get; protected set; }

    public WeChatPayApiRequestModel(HttpMethod method,
        string url,
        string body,
        string timestamp,
        string randomString)
    {
        Method = method;
        Timestamp = timestamp;
        RandomString = randomString;
        Url = url;
        Body = body;

        if (method != HttpMethod.Get) return;

        Body = null;
        if (!string.IsNullOrEmpty(body))
        {
            Url = $"{url}?{body}";
        }
    }

    public string GetPendingSignatureString()
    {
        var sb = new StringBuilder();

        sb.Append(Method.Method.ToUpper()).Append("\n");
        sb.Append(new Uri(Url).PathAndQuery).Append("\n");
        sb.Append(Timestamp).Append("\n");
        sb.Append(RandomString).Append("\n");
        sb.Append(Body).Append("\n");

        return sb.ToString();
    }
}