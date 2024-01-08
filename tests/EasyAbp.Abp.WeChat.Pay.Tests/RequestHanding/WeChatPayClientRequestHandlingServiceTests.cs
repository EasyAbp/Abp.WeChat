using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Extensions;
using EasyAbp.Abp.WeChat.Pay.RequestHandling;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.Pay.Services;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment.Models;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;
using Shouldly;
using Xunit;
using CreateOrderRequest = EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment.Models.CreateOrderRequest;

namespace EasyAbp.Abp.WeChat.Pay.Tests.RequestHanding;

public class WeChatPayClientRequestHandlingServiceTests : AbpWeChatPayTestBase
{
    private readonly IWeChatPayClientRequestHandlingService _weChatPayClientRequestHandlingService;
    private readonly IAbpWeChatPayServiceFactory _weChatPayServiceFactory;

    public WeChatPayClientRequestHandlingServiceTests()
    {
        _weChatPayClientRequestHandlingService = GetRequiredService<IWeChatPayClientRequestHandlingService>();
        _weChatPayServiceFactory = GetRequiredService<IAbpWeChatPayServiceFactory>();
    }

    [Fact]
    public async Task GetJsSdkWeChatPayParametersAsync_Test()
    {
        // Arrange
        var service = await _weChatPayServiceFactory.CreateAsync<JsPaymentService>();
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

        var input = new GetJsSdkWeChatPayParametersInput
        {
            MchId = AbpWeChatPayTestConsts.MchId,
            PrepayId = response.PrepayId,
            AppId = AbpWeChatPayTestConsts.AppId,
        };

        // Act
        var sdkResponse = await _weChatPayClientRequestHandlingService.GetJsSdkWeChatPayParametersAsync(input);

        // Assert
        sdkResponse.ShouldNotBeNull();
        sdkResponse.Success.ShouldBeTrue();
        sdkResponse.PaySign.ShouldNotBeNullOrEmpty();
    }
}