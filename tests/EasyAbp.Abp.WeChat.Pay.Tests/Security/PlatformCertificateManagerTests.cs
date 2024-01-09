using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Security.PlatformCertificate;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Pay.Tests.Security;

public class PlatformCertificateManagerTests : AbpWeChatPayTestBase
{
    private readonly IPlatformCertificateManager _platformCertificateManager;

    public PlatformCertificateManagerTests()
    {
        _platformCertificateManager = GetRequiredService<IPlatformCertificateManager>();
    }

    [Fact]
    public async Task GetPlatformCertificateAsync_Test()
    {
        // Arrange & Act
        var certificate = await _platformCertificateManager.GetPlatformCertificateAsync(AbpWeChatPayTestConsts.MchId,
            AbpWeChatPayTestConsts.SerialNo);

        // Assert
        certificate.ShouldNotBeNull();
        certificate.SerialNo.ShouldNotBeNull();
    }

    [Fact(Skip = "This test is used to generate a new certificate")]
    public async Task VerifySignature_Test()
    {
        // Arrange & Act
        var certificate = await _platformCertificateManager.GetPlatformCertificateAsync(AbpWeChatPayTestConsts.MchId,
            AbpWeChatPayTestConsts.SerialNo);

        const string jsonBody = """
                                1704808380
                                aBbS9ae1bThgNcB1MBlDkTH6NPJAmukF
                                {"id":"3d7a841c-9ce9-58c9-8840-f564bea68183","create_time":"2024-01-09T21:52:26+08:00","resource_type":"encrypt-resource","event_type":"TRANSACTION.SUCCESS","summary":"支付成功","resource":{"original_type":"transaction","algorithm":"AEAD_AES_256_GCM","ciphertext":"cmGZ4vFOCRa3xdtLfchYvkQu2n4QEOeCuCWCfDVIt3dvfz6Nw4qax1E5AU7F6zPs1sakAJ9v+Uv/NQ6+MFZEJdnkgfLF4wVtyzFPYNjKpMn+XB0z4/q5iO6D60Sq1Dn/9CYdVWW4364xEoHO3x2+BOF0u9gk8Ql/smB8R3d1KnraD5hV9g+qbmNpd1ghImcnwI/uVZ0ee2jC4RW8urFJSFgA1jPfpK+93vTk7KSxwXNL9brdxctzHgXFeRZG+2D4zaWzvkTvbtX2kpOUYRf1WhS8vYNMDebgpjsfm9ZYwdGC3IswaYhPjzVu178i1k2PyMbYLmJ6F0kP+/obUj0j05BYALClL3wYCSx+2QFS/3w9afahDDrRHEb7S+fidYWFIN6gsUxhYJMXOpdtIzylGQ3EYwNZWOpmXzZx1E/LnGda64B5KcZrBHdJDSFM2DcVlnhN0QZrGMauo6+ItnP72vBYsItoeR1WlSuMtCh277n2ulKl6M4IofmyxhvpooQxttsGYxrt0jE8e7CempDIOL/lMRph5tXKR4uT0VI926teppXXSYhPrjzNtzXxkS4dDhUdb6q+tlEPpGeMuQ==","associated_data":"transaction","nonce":"o583v1AJfowW"}}

                                """;
        const string sign = "NoEJmfUs2exiKPFK79hJ7wmIKiBU5lCt472EZjhO4/lIEoy2ZBnPabee+zXepUp+wZe7Hs55FmJESb5UnkT9uy68SSnelF8ewXk6XBE+n1oMiFNAtRhVcvBbFaRJYN9tyoU8weTqvBv6mJGX5xfSTCtVCfllf1j4kUbMe0fPv5FOeOKrhRVcBfFh8BxbSmJqDxhPNeWhlh1X0fKjn3OS64ZMsAexJh+RXrTPJa1Y8UWTzCEmS/3SGlwaAGrdDQZAS07TjS6WzvG2MhC5bhn2xtDIP+FtQKXqofZZxOry0Twhd7lDZFXIjJGkLFPHIzGdsBUdwTCVXDN4Anyl6ynZQQ==";
        
        var sb = new StringBuilder();
        sb.Append("1704808380").Append('\n')
            .Append("aBbS9ae1bThgNcB1MBlDkTH6NPJAmukF").Append('\n')
            .Append(jsonBody).Append('\n');

        // Act
        var verifyResponse = certificate.VerifySignature(jsonBody,sign);
        verifyResponse.ShouldBeTrue();
    }
}