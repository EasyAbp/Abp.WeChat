using System.Threading.Tasks;
using System.Xml;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.RequestHandling;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Pay.Tests.Infrastructure;

public class WeChatPayEventXmlDecrypterTests : AbpWeChatPayTestBase
{
    protected readonly IAbpWeChatPayOptionsProvider OptionsProvider;
    protected readonly IWeChatPayEventXmlDecrypter Decrypter;

    public WeChatPayEventXmlDecrypterTests()
    {
        OptionsProvider = GetRequiredService<IAbpWeChatPayOptionsProvider>();
        Decrypter = GetRequiredService<IWeChatPayEventXmlDecrypter>();
    }

    [Fact]
    public async Task Should_Decrypt_Xml()
    {
        var options = await OptionsProvider.GetAsync(null);

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

        var (decryptingResult, decryptedXmlDocument) = await Decrypter.TryDecryptAsync(xml, options);

        decryptingResult.ShouldBeTrue();

        var node = decryptedXmlDocument.SelectSingleNode("/root/refund_recv_accout");

        node.ShouldNotBeNull();
        node.InnerText.ShouldBe("支付用户零钱");
    }
}