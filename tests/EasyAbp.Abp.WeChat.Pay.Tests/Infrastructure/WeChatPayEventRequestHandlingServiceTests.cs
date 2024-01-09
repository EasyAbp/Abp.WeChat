using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.RequestHandling;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Pay.Tests.Infrastructure;

public class WeChatPayEventRequestHandlingServiceTests : AbpWeChatPayTestBase
{
    protected readonly IWeChatPayEventRequestHandlingService Service;

    public WeChatPayEventRequestHandlingServiceTests()
    {
        Service = GetRequiredService<IWeChatPayEventRequestHandlingService>();
    }

    [Fact]
    public async Task Should_Handle_Refund()
    {
        const string xml = @"<xml>
<return_code>SUCCESS</return_code>
   <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
   <mch_id><![CDATA[10000100]]></mch_id>
   <nonce_str><![CDATA[TeqClE3i0mvn3DrK]]></nonce_str>
   <req_info><![CDATA[s/gwtdv9S3tvVATcM8cLYaHXDZA+eCmcMDcY3P+kjHhl8cnhvVTwLw+lQAjl5O0hWtrLTmN+Afc1apfiHQ9eZ0Ykk2BdsF34FFTlijFmVVIUIzb40lJe9cTzXuZkRkriPtyoSyuNC+Lb91ShF1M9k2hRZDS+b5Rhw1RHM51ECm/DpO58FtPu+x18zH+t4QLXLr09hVykgEb5xZfg5nGx3+dV1fX0d4ArCFNrbcPBdWG7vNu8BeLH2g6X8Bp3qI557JaDwWHVztaEl49VWG1vIlUFAmW5Dg1MJsdsiE0xfKkXyM8RltSdB4rfxPfdKK6IlUo1powA62lcKgrIcdyYl3IeGcHYEpRp76mvZULJ5Lukhy/yj1NfIVElV1oQAiwcuWl5OTqFpGS1Cv26GCtE394ZuISkIRKQTP02nnN+jm7ZIZQk6CQETWG1V5sk0BHNonkQEmR7MPrVn3OSmFydYFuQvhU/zMVxM0qjAEZdrqEm+1iL3r+pAd4q8JnkZnDpZqCc/QrUFy8xirBVmLIeLJKQDt/WF8+Kt1yiJ5pntH3bWSWchYlETvkPWtkUwyUebmkKsGZvTPhzGbJ75lqPyPWr7wnr7N13LcmSZhOUnluxSUluzUhX4i7+4S7qMDSUWE/R/KfaZ0wbH4eRZ+yy/C19GjZ+K24Dtj50JxO3Tg0fNmudxbAaCBsR8PxCuO1FF8jPEZbUnQeK38T33SiuiPKWXjH0vgLnacrd+++H00kBvv/Xs6UbFisTKgFzWwoYOAjv8csKTtnGhp8GQcfPX3/E28GO6/0mDgNQmhDRcrBANuRR4ZkkOBV53wHWi7h3F+0kufKbWZ5SZGfjFuwUUTfhcLw3H5+ernR3z8ya7AnsLFIwJiRJxX8C851tVVwMGRtwvOwvq/kc5aUa/eD2YO9u3hhQjv2tdiBrw72fuOV/xNvBjuv9Jg4DUJoQ0XKweYga3dX2KRr06lB3EfPsXSbnpnAlY59ELUkL2n8MdFOO7ifwD9TWvKqDmv2zDEqDhWmOJaKFsNOqe/8s/dmXV6+EjdFmZ6LcF46MmEYfyu3FIszkA+1rgsqJQkwSMItF]]></req_info>
</xml>";

        var result = await Service.RefundNotifyAsync(new RefundNotifyInput
        {
            MchId = "10000100",
            Xml = xml
        });

        result.Success.ShouldBeTrue();
    }

    [Fact]
    public async Task Should_Handle_Paid()
    {
        var json = """
                   {
                       "id": "EV-2018022511223320873",
                       "create_time": "2015-05-20T13:29:35+08:00",
                       "resource_type": "encrypt-resource",
                       "event_type": "TRANSACTION.SUCCESS",
                       "summary": "支付成功",
                       "resource": {
                           "original_type": "transaction",
                           "algorithm": "AEAD_AES_256_GCM",
                           "ciphertext": "",
                           "associated_data": "",
                           "nonce": ""
                       }
                   }
                   """;

        // var result = await Service.PaidNotifyAsync(JsonConvert.DeserializeObject<PaymentNotifyCallbackRequest>(json), null);

        // result.Success.ShouldBe(true);
    }
}