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
            var result = await ordinaryMerchantPayService.UnifiedOrderAsync("wxe32e0204e9db0b1c", "1540561391",
                "测试支付", null, DateTime.Now.ToString("yyyyMMddHHmmss"), 101, TradeType.JsApi, null);

            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task OrderRefund_Test()
        {
            var ordinaryMerchantPayService = await WeChatPayServiceFactory.CreateAsync<OrdinaryMerchantPayWeService>();

            var response = await ordinaryMerchantPayService.RefundAsync("wxe32e0204e9db0b1c",
                "1540561391",
                "151515151515",
                "161616161616",
                101,
                50);

            response.ShouldNotBeNull();
        }
    }
}