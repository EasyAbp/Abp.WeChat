using System.Net.Http;
using EasyAbp.Abp.WeChat.Pay.ApiRequests;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Pay.Tests.ApiRequests;

public class WeChatPayApiRequestModelTests : AbpWeChatPayTestBase
{
    [Fact]
    public void GetPendingSignatureString_Test()
    {
        var model = new WeChatPayApiRequestModel(HttpMethod.Get,
            "https://api.mch.weixin.qq.com/v3/certificates",
            null, "1554208460", "593BEC0C930BF1AFEB40B4A08C8FB242");

        var pendingSignatureString = model.GetPendingSignatureString();
        pendingSignatureString.ShouldBe("GET\n/v3/certificates\n1554208460\n593BEC0C930BF1AFEB40B4A08C8FB242\n\n");
    }
}