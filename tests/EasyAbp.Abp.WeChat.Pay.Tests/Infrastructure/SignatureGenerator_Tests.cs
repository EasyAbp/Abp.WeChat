using System.Security.Cryptography;
using Shouldly;
using Xunit;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Signature;
using EasyAbp.Abp.WeChat.Pay.Models;

namespace EasyAbp.Abp.WeChat.Pay.Tests.Infrastructure
{
    public class SignatureGenerator_Tests : AbpWeChatPayTestBase
    {
        private readonly ISignatureGenerator _signatureGenerator;

        public SignatureGenerator_Tests()
        {
            _signatureGenerator = GetRequiredService<ISignatureGenerator>();
        }

        [Fact]
        public void Generate_Test()
        {
            AbpWeChatPayOptions.ApiKey = "192006250b4c09247ec02edce69f6a2d";

            var newParam = new WeChatPayParameters();
            newParam.AddParameter("appid", "wxd930ea5d5a258f4f");
            newParam.AddParameter("mch_id", "10000100");
            newParam.AddParameter("device_info", "1000");
            newParam.AddParameter("body", "test");
            newParam.AddParameter("nonce_str", "ibuaiVcKdpRxkhJA");

            var signStr = _signatureGenerator.Generate(newParam, MD5.Create(), AbpWeChatPayOptions.ApiKey);

            signStr.ShouldBe("9A0A8659F005D6984697E2CA0A9CF3B7");
        }
    }
}