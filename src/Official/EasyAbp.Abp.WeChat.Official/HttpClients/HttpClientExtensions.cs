using System.Net.Http;

namespace EasyAbp.Abp.WeChat.Official.HttpClients
{
    public static class HttpClientExtensions
    {
        public static void AddV2(this MultipartFormDataContent formData, HttpContent httpContent, string name, string fileName)
        {
            formData.Add(httpContent, $"\"{name}\"", $"\"{fileName}\"");
        }
    }
}