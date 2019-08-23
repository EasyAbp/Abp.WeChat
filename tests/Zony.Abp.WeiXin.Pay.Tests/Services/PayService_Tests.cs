using System;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Xunit;
using Zony.Abp.WeiXin.Pay.Services.Pay;

namespace Zony.Abp.WeiXin.Pay.Tests.Services
{
    public class PayService_Tests : AbpWeiXinPayTestBase
    {
        private readonly PayService _payService;

        public PayService_Tests()
        {
            _payService = GetRequiredService<PayService>();
        }

        [Fact]
        public async Task UnifiedOrder_Test()
        {
            await _payService.UnifiedOrder("wxe32e0204e9db0b1c",
                "1540561391",
                "测试支付",
                DateTime.Now.ToString("yyyyMMddHHmmss"),
                101,
                TradeType.JsApi);
        }
    }
}