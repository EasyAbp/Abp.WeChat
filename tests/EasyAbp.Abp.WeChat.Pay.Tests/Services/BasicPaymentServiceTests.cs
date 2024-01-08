using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Extensions;
using EasyAbp.Abp.WeChat.Pay.Services;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment.Models;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;
using Shouldly;
using Xunit;
using CreateOrderRequest = EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment.Models.CreateOrderRequest;

namespace EasyAbp.Abp.WeChat.Pay.Tests.Services;

public class BasicPaymentServiceTests : AbpWeChatPayTestBase
{
    private readonly IAbpWeChatPayServiceFactory _weChatPayServiceFactory;

    public BasicPaymentServiceTests()
    {
        _weChatPayServiceFactory = GetRequiredService<IAbpWeChatPayServiceFactory>();
    }

    [Fact]
    public async Task CreateOrderAsync_Test()
    {
        // Arrange
        var service = await _weChatPayServiceFactory.CreateAsync<JsPaymentService>();

        // Act
        var response = await service.CreateOrderAsync(new CreateOrderRequest
        {
            MchId = service.MchId,
            OutTradeNo = RandomStringHelper.GetRandomString(),
            NotifyUrl = AbpWeChatPayTestConsts.NotifyUrl,
            AppId = AbpWeChatPayTestConsts.AppId, // 请替换为你的 AppId
            Description = "Image形象店-深圳腾大-QQ公仔",
            Amount = new CreateOrderAmountModel
            {
                Total = 1,
                Currency = "CNY"
            },
            Payer = new CreateOrderPayerModel
            {
                OpenId = AbpWeChatPayTestConsts.OpenId // 请替换为测试用户的 OpenId，具体 Id 可以在微信公众号平台-用户管理进行查看。
            }
        });

        // Assert
        response.ShouldNotBeNull();
        response.PrepayId.ShouldNotBeNullOrEmpty();
    }
}