using System;
using System.Threading.Tasks;
using Bogus;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Services;
using EasyAbp.Abp.WeChat.Pay.Services.MarketingTools.VoucherService;
using EasyAbp.Abp.WeChat.Pay.Services.MarketingTools.VoucherService.ParametersModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Pay.Tests.Services;

public sealed class VoucherStockType
{
    public const string Normal = "NORMAL";
}

public class VoucherWeServiceTests : AbpWeChatPayTestBase
{
    private readonly IAbpWeChatPayServiceFactory _weChatPayServiceFactory;

    public VoucherWeServiceTests()
    {
        _weChatPayServiceFactory = GetRequiredService<IAbpWeChatPayServiceFactory>();
    }

    [Fact]
    public async Task CreateCouponBatchAsync_Test()
    {
        // Arrange
        var service = await _weChatPayServiceFactory.CreateAsync<VoucherWeService>();
        var options = ServiceProvider.GetRequiredService<IOptions<AbpWeChatPayOptions>>().Value;

        // Act
        var response = await service.CreateCouponBatchAsync(new CreateCouponBatchRequest
        {
            StockName = "测试代金券-1",
            BelongMerchant = options.MchId,
            AvailableBeginTime = DateTime.Now.AddDays(1),
            AvailableEndTime = DateTime.Now.AddDays(2),
            NoCash = true,
            StockType = VoucherStockType.Normal,
            OutRequestNo = new Faker().Random.String2(64),
            StockUseRule = new CreateCouponBatchRequest.InnerStockUseRule
            {
                MaxAmount = 10000,
                MaxCoupons = 100,
                MaxCouponsPerUser = 1,
                NaturalPersonLimit = true,
                PreventApiAbuse = true
            },
            CouponUseRule = new CreateCouponBatchRequest.InnerCouponUseRule
            {
                FixedNormalCoupon = new CreateCouponBatchRequest.InnerCouponUseRule.InnerFixedNormalCoupon
                {
                    CouponAmount = 100,
                    TransactionMinimum = 10000
                },
                AvailableMerchants = new[] { options.MchId }
            }
        });

        // Assert
        response.Message.ShouldBeNullOrEmpty();
        response.StockId.ShouldNotBeNullOrEmpty();
    }
}