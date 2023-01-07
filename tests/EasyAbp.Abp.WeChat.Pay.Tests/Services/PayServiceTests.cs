using System;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using EasyAbp.Abp.WeChat.Pay.Services.Pay;

namespace EasyAbp.Abp.WeChat.Pay.Tests.Services
{
    public class PayServiceTests : AbpWeChatPayTestBase
    {
        [Fact]
        public async Task UnifiedOrder_Test()
        {
            var ordinaryMerchantPayService = await WeChatPayServiceFactory.CreateAsync<OrdinaryMerchantPayWeService>();

            var result = await ordinaryMerchantPayService.UnifiedOrderAsync(
                AbpWeChatPayTestConsts.AppId, AbpWeChatPayTestConsts.MchId, "测试支付", null,
                DateTime.Now.ToString("yyyyMMddHHmmss"), 101, TradeType.JsApi, AbpWeChatPayTestConsts.OpenId);

            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task OrderRefund_Test()
        {
            var ordinaryMerchantPayService = await WeChatPayServiceFactory.CreateAsync<OrdinaryMerchantPayWeService>();

            var response = await ordinaryMerchantPayService.RefundAsync(
                AbpWeChatPayTestConsts.AppId,
                AbpWeChatPayTestConsts.MchId,
                "151515151515",
                "161616161616",
                101,
                50);

            response.ShouldNotBeNull();
        }
    }
}