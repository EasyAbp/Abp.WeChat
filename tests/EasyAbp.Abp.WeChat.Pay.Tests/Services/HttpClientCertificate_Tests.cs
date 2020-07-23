using System;
using System.IO;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve;
using Shouldly;
using Volo.Abp.BlobStoring;
using Volo.Abp.Threading;
using Xunit;

namespace EasyAbp.Abp.WeChat.Pay.Tests.Services
{
    public class HttpClientCertificate_Tests : AbpWeChatPayTestBase
    {
        private readonly IBlobContainer _blobContainer;

        public HttpClientCertificate_Tests()
        {
            _blobContainer = GetRequiredService<IBlobContainer>();
        }

        [Fact]
        public async Task Should_Return_Certificate_Bytes()
        {
            var options = await GetRequiredService<IWeChatPayOptionsResolver>().ResolveAsync();
            if (string.IsNullOrEmpty(options.CertificateName)) throw new NullReferenceException();

            var certificateBytes = await GetRequiredService<IBlobContainer>().GetAllBytesOrNullAsync(options.CertificateName);
            if (certificateBytes == null) throw new FileNotFoundException("指定的证书路径无效，请重新指定有效的证书文件路径。");

            certificateBytes.Length.ShouldBe(2);
        }
    }
}