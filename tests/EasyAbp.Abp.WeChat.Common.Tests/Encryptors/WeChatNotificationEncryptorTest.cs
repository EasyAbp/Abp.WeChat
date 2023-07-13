using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Encryption;
using EasyAbp.Abp.WeChat.Common.Models;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Common.Tests.Encryptors;

public class WeChatNotificationEncryptorTest : AbpWeChatCommonTestBase<AbpWeChatCommonTestsModule>
{
    [Fact]
    public async Task Should_Encrypt_Xml()
    {
        var encryptor = GetRequiredService<IWeChatNotificationEncryptor>();

        var options = new TestAbpWeChatOptions
        {
            Token = AbpWeChatCommonTestsConsts.Token,
            AppId = AbpWeChatCommonTestsConsts.AppId,
            EncodingAesKey = AbpWeChatCommonTestsConsts.EncodingAesKey
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

        var options = new TestAbpWeChatOptions
        {
            Token = AbpWeChatCommonTestsConsts.Token,
            AppId = AbpWeChatCommonTestsConsts.AppId,
            EncodingAesKey = AbpWeChatCommonTestsConsts.EncodingAesKey
        };

        var model = await encryptor.DecryptAsync<WeChatAppEventModel>(
            options.Token,
            options.EncodingAesKey,
            options.AppId,
            AbpWeChatCommonTestsConsts.ReqMsgSig,
            AbpWeChatCommonTestsConsts.ReqTimeStamp,
            AbpWeChatCommonTestsConsts.ReqNonce,
            AbpWeChatCommonTestsConsts.ReqData);

        model.MsgType.ShouldBe("text");
        model.ToUserName.ShouldBe("wx5823bf96d3bd56c7");
    }
}