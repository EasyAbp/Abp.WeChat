using System.Net.Http;
using EasyAbp.Abp.WeChat.Common.Extensions;
using EasyAbp.Abp.WeChat.Pay.ApiRequests;
using Newtonsoft.Json;
using Shouldly;
using Volo.Abp;
using Xunit;

namespace EasyAbp.Abp.WeChat.Pay.Tests;

public class WeChatPayApiRequestModelTests
{
    [Fact]
    public void GetPendingSignatureString_Test()
    {
        var timestamp = DateTimeHelper.GetNowTimeStamp().ToString();
        var randomString = RandomStringHelper.GetRandomString();
        var url = @"https://api.mch.weixin.qq.com/v3/pay/transactions/id/thisisid";

        var request = new WeChatPayApiRequestModel(HttpMethod.Get,
            url,
            string.Empty,
            timestamp,
            randomString);

        request.ShouldNotBeNull();
        request.Body.ShouldBeNull();
        request.Url.ShouldBe(url);
        request.RandomString.ShouldBe(randomString);
        request.Timestamp.ShouldBe(timestamp);

        var pendingSignatureString = request.GetPendingSignatureString();
        pendingSignatureString.ShouldNotBeNullOrEmpty();
    }
}