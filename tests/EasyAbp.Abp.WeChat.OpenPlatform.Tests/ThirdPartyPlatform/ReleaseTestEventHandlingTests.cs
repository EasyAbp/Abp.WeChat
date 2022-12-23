using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Models;
using EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.RequestHandling;
using Shouldly;
using Volo.Abp.Data;
using Xunit;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Tests.ThirdPartyPlatform;

public class ReleaseTestEventHandlingTests : AbpWeChatOpenPlatformTestBase
{
    protected static readonly string OfficialAuthorizerAppId = ReleaseTestConsts.OfficialAppIds.First();
    protected static readonly string MiniProgramAuthorizerAppId = ReleaseTestConsts.MiniProgramsAppIds.First();

    [Fact]
    public async Task Should_Handle_Release_Test_Official_User_Message()
    {
        var handler = GetRequiredService<ReleaseTestWeChatThirdPartyPlatformAppEventHandler>();

        var eventModel = new WeChatAppEventModel();
        eventModel.SetProperty("ToUserName", "toUser");
        eventModel.SetProperty("FromUserName", "fromUser");
        eventModel.SetProperty("CreateTime", "1482048670");
        eventModel.SetProperty("MsgType", "text");
        eventModel.SetProperty("Content", "TESTCOMPONENT_MSG_TYPE_TEXT");

        var result =
            await handler.HandleAsync(AbpWeChatOpenPlatformTestsConsts.AppId, OfficialAuthorizerAppId, eventModel);

        result.Success.ShouldBeTrue();
        result.SpecifiedResponseContent.ShouldBe("TESTCOMPONENT_MSG_TYPE_TEXT_callback");
    }

    [Fact]
    public async Task Should_Handle_Release_Test_Official_Service_Center_Message()
    {
        var handler = GetRequiredService<ReleaseTestWeChatThirdPartyPlatformAppEventHandler>();

        var eventModel = new WeChatAppEventModel();
        eventModel.SetProperty("ToUserName", "toUser");
        eventModel.SetProperty("FromUserName", "fromUser");
        eventModel.SetProperty("CreateTime", "1482048670");
        eventModel.SetProperty("MsgType", "text");
        eventModel.SetProperty("Content", "QUERY_AUTH_CODE:12345");

        var result =
            await handler.HandleAsync(AbpWeChatOpenPlatformTestsConsts.AppId, OfficialAuthorizerAppId, eventModel);

        result.Success.ShouldBeFalse();
        result.FailureReason.ShouldBe("全网发布检测（公众号客服消息）处理失败。");
    }

    [Fact]
    public async Task Should_Handle_Release_Test_MiniProgram_Customer_Message()
    {
        var handler = GetRequiredService<ReleaseTestWeChatThirdPartyPlatformAppEventHandler>();

        var eventModel = new WeChatAppEventModel();
        eventModel.SetProperty("ToUserName", "toUser");
        eventModel.SetProperty("FromUserName", "fromUser");
        eventModel.SetProperty("CreateTime", "1482048670");
        eventModel.SetProperty("MsgType", "text");
        eventModel.SetProperty("Content", "QUERY_AUTH_CODE:12345");
        eventModel.SetProperty("MsgId", "1234567890123456");

        var result =
            await handler.HandleAsync(AbpWeChatOpenPlatformTestsConsts.AppId, MiniProgramAuthorizerAppId, eventModel);

        result.Success.ShouldBeFalse();
        result.FailureReason.ShouldBe("全网发布检测（小程序客服消息）处理失败。");
    }
}