using Xunit;
using Zony.Abp.WeiXin.Pay.Infrastructure;

namespace Zony.Abp.WeiXin.Pay.Tests
{
    public class SignatureGenerator_Tests : AbpWeiXinPayTestBase
    {
        private readonly ISignatureGenerator _signatureGenerator;

        public SignatureGenerator_Tests()
        {
            _signatureGenerator = GetRequiredService<ISignatureGenerator>();
        }

        [Fact]
        public void Generate_Test()
        {
            // https://api.mch.weixin.qq.com/sandboxnew/pay/unifiedorder
            
            var newParam = new WeChatPayParams();
            newParam.AddParameter("appid","");
        }
    }
}