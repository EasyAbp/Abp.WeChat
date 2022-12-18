using System.Threading.Tasks;
using System.Xml;
using EasyAbp.Abp.WeChat.Pay.RequestHandling;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Pay.Tests.Infrastructure;

public class WeChatPayEventRequestHandlingServiceTests : AbpWeChatPayTestBase
{
    protected readonly IWeChatPayEventXmlDecrypter Decrypter;
    protected readonly IWeChatPayEventRequestHandlingService Service;

    public WeChatPayEventRequestHandlingServiceTests()
    {
        Decrypter = GetRequiredService<IWeChatPayEventXmlDecrypter>();
        Service = GetRequiredService<IWeChatPayEventRequestHandlingService>();
    }

    [Fact]
    public async Task Should_Handle_Refund()
    {
        const string reqInfo =
            "<![CDATA[s/gwtdv9S3tvVATcM8cLYaHXDZA+eCmcMDcY3P+kjHhl8cnhvVTwLw+lQAjl5O0hWtrLTmN+Afc1apfiHQ9eZ0Ykk2BdsF34FFTlijFmVVIUIzb40lJe9cTzXuZkRkriPtyoSyuNC+Lb91ShF1M9k2hRZDS+b5Rhw1RHM51ECm/DpO58FtPu+x18zH+t4QLXLr09hVykgEb5xZfg5nGx3+dV1fX0d4ArCFNrbcPBdWG7vNu8BeLH2g6X8Bp3qI557JaDwWHVztaEl49VWG1vIlUFAmW5Dg1MJsdsiE0xfKkXyM8RltSdB4rfxPfdKK6IlUo1powA62lcKgrIcdyYl3IeGcHYEpRp76mvZULJ5Lukhy/yj1NfIVElV1oQAiwcuWl5OTqFpGS1Cv26GCtE394ZuISkIRKQTP02nnN+jm7ZIZQk6CQETWG1V5sk0BHNonkQEmR7MPrVn3OSmFydYFuQvhU/zMVxM0qjAEZdrqEm+1iL3r+pAd4q8JnkZnDpZqCc/QrUFy8xirBVmLIeLJKQDt/WF8+Kt1yiJ5pntH3bWSWchYlETvkPWtkUwyUebmkKsGZvTPhzGbJ75lqPyPWr7wnr7N13LcmSZhOUnluxSUluzUhX4i7+4S7qMDSUWE/R/KfaZ0wbH4eRZ+yy/C19GjZ+K24Dtj50JxO3Tg0fNmudxbAaCBsR8PxCuO1FF8jPEZbUnQeK38T33SiuiPKWXjH0vgLnacrd+++H00kBvv/Xs6UbFisTKgFzWwoYOAjv8csKTtnGhp8GQcfPX3/E28GO6/0mDgNQmhDRcrBANuRR4ZkkOBV53wHWi7h3F+0kufKbWZ5SZGfjFuwUUTfhcLw3H5+ernR3z8ya7AnsLFIwJiRJxX8C851tVVwMGRtwvOwvq/kc5aUa/eD2YO9u3hhQjv2tdiBrw72fuOV/xNvBjuv9Jg4DUJoQ0XKweYga3dX2KRr06lB3EfPsXSbnpnAlY59ELUkL2n8MdFOO7ifwD9TWvKqDmv2zDEqDhWmOJaKFsNOqe/8s/dmXV6+EjdFmZ6LcF46MmEYfyu3FIszkA+1rgsqJQkwSMItF]]>";

        var xml = new XmlDocument();

        xml.LoadXml(@$"<xml>
<return_code>SUCCESS</return_code>
   <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
   <mch_id><![CDATA[10000100]]></mch_id>
   <nonce_str><![CDATA[TeqClE3i0mvn3DrK]]></nonce_str>
   <req_info>{reqInfo}</req_info>
</xml>");

        var result = await Service.RefundNotifyAsync("10000100", xml);

        result.Success.ShouldBeTrue();
    }

    [Fact]
    public async Task Should_Handle_Paid()
    {
        var xml = new XmlDocument();
        xml.LoadXml(@"<xml>
  <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
  <attach><![CDATA[支付测试]]></attach>
  <bank_type><![CDATA[CFT]]></bank_type>
  <fee_type><![CDATA[CNY]]></fee_type>
  <is_subscribe><![CDATA[Y]]></is_subscribe>
  <mch_id><![CDATA[10000100]]></mch_id>
  <nonce_str><![CDATA[5d2b6c2a8db53831f7eda20af46e531c]]></nonce_str>
  <openid><![CDATA[oUpF8uMEb4qRXf22hE3X68TekukE]]></openid>
  <out_trade_no><![CDATA[1409811653]]></out_trade_no>
  <result_code><![CDATA[SUCCESS]]></result_code>
  <return_code><![CDATA[SUCCESS]]></return_code>
  <sign><![CDATA[29ADB404083AA9154EC99D650DDFDCC2]]></sign>
  <time_end><![CDATA[20140903131540]]></time_end>
  <total_fee>1</total_fee>
  <coupon_fee><![CDATA[10]]></coupon_fee>
  <coupon_count><![CDATA[1]]></coupon_count>
  <coupon_type><![CDATA[CASH]]></coupon_type>
  <coupon_id><![CDATA[10000]]></coupon_id>
  <trade_type><![CDATA[JSAPI]]></trade_type>
  <transaction_id><![CDATA[1004400740201409030005092168]]></transaction_id>
</xml>");

        var result = await Service.NotifyAsync("10000100", xml);

        result.Success.ShouldBe(true);
    }
}