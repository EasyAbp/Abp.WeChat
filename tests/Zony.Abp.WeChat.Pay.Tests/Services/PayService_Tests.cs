using System;
using System.Threading.Tasks;
using Xunit;
using Zony.Abp.WeChat.Pay.Services.Pay;

namespace Zony.Abp.WeChat.Pay.Tests.Services
{
    public class PayService_Tests : AbpWeChatPayTestBase
    {
        private readonly PayService _payService;

        public PayService_Tests()
        {
            _payService = GetRequiredService<PayService>();
        }

        [Fact]
        public async Task UnifiedOrder_Test()
        {
            await _payService.UnifiedOrderAsync("wxe32e0204e9db0b1c",
                "1540561391",
                "测试支付",
                DateTime.Now.ToString("yyyyMMddHHmmss"),
                101,
                TradeType.JsApi);
        }
    }
}