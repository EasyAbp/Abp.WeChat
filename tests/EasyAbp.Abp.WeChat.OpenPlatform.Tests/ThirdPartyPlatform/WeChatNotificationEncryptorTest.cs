using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Encryption;
using EasyAbp.Abp.WeChat.Common.Models;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Options;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Tests.ThirdPartyPlatform;

public class WeChatNotificationEncryptorTest : AbpWeChatOpenPlatformTestBase
{
    [Fact]
    public async Task Should_Encrypt_Xml()
    {
        var encryptor = GetRequiredService<IWeChatNotificationEncryptor>();

        var options = new AbpWeChatThirdPartyPlatformOptions
        {
            Token = AbpWeChatOpenPlatformTestsConsts.Token,
            AppId = AbpWeChatOpenPlatformTestsConsts.AppId,
            EncodingAesKey = AbpWeChatOpenPlatformTestsConsts.EncodingAesKey
        };

        var encryptedXml = await encryptor.EncryptAsync(
            options.Token,
            options.EncodingAesKey,
            options.AppId,
            "<xml><ToUserName><![CDATA[wx5823bf96d3bd56c7]]></ToUserName>" +
            "<FromUserName><![CDATA[mycreate]]></FromUserName>" +
            "<CreateTime>1409659813</CreateTime>" +
            "<MsgType><![CDATA[text]]></MsgType>" +
            "<Content><![CDATA[hello]]></Content>" +
            "<MsgId>4561255354251345929</MsgId>" +
            "<AgentID>218</AgentID>" +
            "</xml>");

        encryptedXml.ShouldStartWith("<xml><Encrypt><![CDATA[");
    }

    [Fact]
    public async Task Should_Decrypt_Xml()
    {
        var encryptor = GetRequiredService<IWeChatNotificationEncryptor>();

        var options = new AbpWeChatThirdPartyPlatformOptions
        {
            Token = AbpWeChatOpenPlatformTestsConsts.Token,
            AppId = AbpWeChatOpenPlatformTestsConsts.AppId,
            EncodingAesKey = AbpWeChatOpenPlatformTestsConsts.EncodingAesKey
        };

        var model = await encryptor.DecryptAsync<WeChatAppEventModel>(
            options.Token,
            options.EncodingAesKey,
            options.AppId,
            AbpWeChatOpenPlatformTestsConsts.ReqMsgSig,
            AbpWeChatOpenPlatformTestsConsts.ReqTimeStamp,
            AbpWeChatOpenPlatformTestsConsts.ReqNonce,
            AbpWeChatOpenPlatformTestsConsts.ReqData);

        model.MsgType.ShouldBe("text");
        model.ToUserName.ShouldBe("wx5823bf96d3bd56c7");
    }
}