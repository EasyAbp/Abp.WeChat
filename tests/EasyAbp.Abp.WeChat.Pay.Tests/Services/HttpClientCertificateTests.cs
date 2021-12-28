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
    public class HttpClientCertificateTests : AbpWeChatPayTestBase
    {
        private readonly IBlobContainer _blobContainer;

        public HttpClientCertificateTests()
        {
            _blobContainer = GetRequiredService<IBlobContainer>();
        }

        [Fact]
        public async Task Should_Return_Certificate_Bytes()
        {
            // Arrange & Act
            var options = await GetRequiredService<IWeChatPayOptionsResolver>().ResolveAsync();
            if (string.IsNullOrEmpty(options.CertificateBlobName)) throw new NullReferenceException();

            var blobContainer = options.CertificateBlobContainerName.IsNullOrEmpty()
                ? GetRequiredService<IBlobContainer>()
                : GetRequiredService<IBlobContainerFactory>().Create(options.CertificateBlobContainerName);
                
            var certificateBytes = AsyncHelper.RunSync(() => blobContainer.GetAllBytesOrNullAsync(options.CertificateBlobName));
            if (certificateBytes == null) throw new FileNotFoundException("指定的证书路径无效，请重新指定有效的证书文件路径。");

            // Assert
            certificateBytes.Length.ShouldBe(2);
        }
    }
}