using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.ApiRequests;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Pay.Tests.ApiRequests;

public class WeChatPayApiRequestTests : AbpWeChatPayTestBase
{
    private readonly IWeChatPayApiRequester _weChatPayApiRequester;

    public WeChatPayApiRequestTests()
    {
        _weChatPayApiRequester = GetRequiredService<IWeChatPayApiRequester>();
    }

    [Fact]
    public async Task GetCertificatesAsync_Test()
    {
        var response = await _weChatPayApiRequester.RequestAsync(HttpMethod.Get,
            "https://api.mch.weixin.qq.com/v3/certificates");

        response.ShouldNotBeNull(response);
    }
}