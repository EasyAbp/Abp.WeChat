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
        var request = new CreateOrderRequest
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
            Payer = new CreateOrderRequest.CreateOrderPayerModel
            {
                OpenId = AbpWeChatPayTestConsts.OpenId // 请替换为测试用户的 OpenId，具体 Id 可以在微信公众号平台-用户管理进行查看。
            }
        };

        // Act
        var response = await service.CreateOrderAsync(request);

        // Assert
        response.ShouldNotBeNull();
        response.PrepayId.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public async Task QueryOrderByOutTradeNumberAsync_Test()
    {
        // Arrange
        var service = await _weChatPayServiceFactory.CreateAsync<JsPaymentService>();

        // Act
        var response = await service.QueryOrderByOutTradeNumberAsync(new QueryOrderByOutTradeNumberRequest
        {
            MchId = AbpWeChatPayTestConsts.MchId,
            OutTradeNo = "5dmsi17l83n34fku5z49phcpwa9kpz"
        });

        // Assert
        response.ShouldNotBeNull();
        response.TradeState.ShouldBe("SUCCESS");
    }

    [Fact]
    public async Task QueryOrderByTransactionIdAsync_Test()
    {
        // Arrange
        var service = await _weChatPayServiceFactory.CreateAsync<JsPaymentService>();

        // Act
        var response = await service.QueryOrderByWechatNumberAsync(new QueryOrderByWechatNumberRequest
        {
            MchId = AbpWeChatPayTestConsts.MchId,
            TransactionId = "4200002055202401099853138759"
        });

        // Assert
        response.ShouldNotBeNull();
        response.TradeState.ShouldBe("SUCCESS");
    }

    [Fact]
    public async Task CloseOrderAsync_Test()
    {
        // Arrange
        var service = await _weChatPayServiceFactory.CreateAsync<JsPaymentService>();

        // Act
        var response = await service.CloseOrderAsync(new CloseOrderRequest
        {
            MchId = AbpWeChatPayTestConsts.MchId,
            OutTradeNo = "1ne2k7qitdr78k9zytjpz0tm7qfg8p"
        });

        // Assert
        response.ShouldBeNull();
    }
    
    [Fact]
    public async Task RefundAsync_Test()
    {
        // Arrange
        var service = await _weChatPayServiceFactory.CreateAsync<JsPaymentService>();
        var request = new RefundOrderRequest
        {
            OutRefundNo = RandomStringHelper.GetRandomString(),
            OutTradeNo = "kel9xerwcjib2zs8eixyazuis3qsmo",
            NotifyUrl = AbpWeChatPayTestConsts.RefundNotifyUrl,
            Amount = new RefundOrderRequest.AmountInfo
            {
                Refund = 1,
                Total = 1,
                Currency = "CNY"
            }
        };
            
        // Act
        var response = await service.RefundAsync(request);

        // Assert
        response.ShouldNotBeNull();
        response.RefundId.ShouldNotBeNullOrEmpty();
    }
    
    [Fact]
    public async Task QueryRefundOrderAsync_Test()
    {
        // Arrange
        var service = await _weChatPayServiceFactory.CreateAsync<JsPaymentService>();

        // Act
        var response = await service.QueryRefundOrderAsync(new QueryRefundOrderRequest
        {
            OutRefundNo = "r8z61t50kwbg9l1l5ay9s7i8qjyc89"
        });

        // Assert
        response.ShouldNotBeNull();
    }

    [Fact]
    public async Task GetTransactionBillAsync_Test()
    {
        // Arrange
        var service = await _weChatPayServiceFactory.CreateAsync<JsPaymentService>();
        
        // Act
        var response = await service.GetTransactionBillAsync(new GetTransactionBillRequest
        {
            BillDate = "2024-01-09"
        });
        
        // Assert
        response.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task GetFundFlowBillAsync_Test()
    {
        // Arrange
        var service = await _weChatPayServiceFactory.CreateAsync<JsPaymentService>();
        
        // Act
        var response = await service.GetFundFlowBillAsync(new GetFundFlowBillRequest
        {
            BillDate = "2024-01-09"
        });
        
        // Assert
        response.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task DownloadBillFileAsync_Test()
    {
        // Arrange
        var service = await _weChatPayServiceFactory.CreateAsync<JsPaymentService>();
        var billResponse = await service.GetTransactionBillAsync(new GetTransactionBillRequest
        {
            BillDate = "2024-01-09"
        });
        
        // Act
        var response = await service.DownloadBillFileAsync(billResponse.DownloadUrl);
        
        // Assert
        response.ShouldNotBeNull();
        response.Length.ShouldBeGreaterThan(0);
    }
}