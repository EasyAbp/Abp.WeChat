using System;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Shouldly;
using Xunit;
using EasyAbp.Abp.WeChat.Pay.Extensions;
using EasyAbp.Abp.WeChat.Pay.Services.MicroMerchant;

namespace EasyAbp.Abp.WeChat.Pay.Tests.Services
{
    public class MicroMerchantServiceTests : AbpWeChatPayTestBase
    {
        private readonly MicroMerchantService _service;

        public MicroMerchantServiceTests()
        {
            _service = GetRequiredService<MicroMerchantService>();
        }

        [Fact]
        public async Task Should_Return_Certificate_Serial_Number()
        {
            // Arrange & Act
            var result = await _service.GetCertificateAsync(AbpWeChatPayTestConsts.MchId);

            // Assert
            result.ShouldNotBeNull();
            var serialNumber = JObject.Parse(result.SelectSingleNode("/xml/certificates")?.InnerText)?.SelectToken("$.data[0].serial_no")?.Value<string>();
            serialNumber.ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_Upload_Image_Success_And_Return_MediaId()
        {
            // Arrange
            var picPath = @"C:\Users\EasyAbp\Desktop\Back.jpg";

            // Act
            var result = await _service.UploadMediaAsync(AbpWeChatPayTestConsts.MchId, picPath);

            // Assert
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_Return_Success()
        {
            // Arrange
            var serialNumber = "";
            var businessCode = DateTime.Now.ToString("yyyyMMddHHmmss");
            var frontend = "";
            var backend = "";
            var pemPath = @"";
            var homePicId = "";
            var backMagmentPicId = "";

            var certificate = JObject.Parse((await _service.GetCertificateAsync(AbpWeChatPayTestConsts.MchId)).SelectSingleNode("/xml/certificates")?.InnerText);
            var key = WeChatPayToolUtility.GetCertificate("",
                certificate.SelectToken("$.data[0].encrypt_certificate.associated_data").Value<string>(),
                certificate.SelectToken("$.data[0].encrypt_certificate.nonce").Value<string>(),
                certificate.SelectToken("$.data[0].encrypt_certificate.ciphertext").Value<string>());

            // Act
            var result = await _service.SubmitAsync("3.0", serialNumber, AbpWeChatPayTestConsts.MchId, businessCode, frontend, backend,
                "张三".Encrypt(key), "".Encrypt(key), "[\"2010-08-06\",\"2020-08-06\"]", "张三".Encrypt(key),
                AccountBanks.CITIC, "510100", null, "".Encrypt(key), "张三的店", "510100",
                "张三的店", null, null, homePicId, backMagmentPicId, null, "张三的店", "18888888888",
                "其他", "0.6%", null, null, "张三".Encrypt(key), "18888888888".Encrypt(key), null);

            // Assert
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task Should_Return_Success_And_ApplymentId()
        {
            // Arrange & Act
            var result = await _service.GetStateAsync("1.0", AbpWeChatPayTestConsts.MchId, "1230000109", null);

            // Assert
            result.ShouldNotBeNull();
            result.SelectSingleNode("/xml/return_code")?.InnerText.ShouldBe("SUCCESS");
            result.SelectSingleNode("/xml/result_code")?.InnerText.ShouldBe("SUCCESS");
            result.SelectSingleNode("/xml/applyment_id")?.InnerText.ShouldNotBeNull();
        }
    }
}