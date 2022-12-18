using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.MiniProgram.Services.SubscribeMessage;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.MiniProgram.Tests.Services
{
    public class SubscribeMessageServiceTests : AbpWeChatMiniProgramTestBase
    {
        private const string OpenId = "on7qq1HZmDVgYTmzz8r3tayh-wqw";
        private const string TemplateId = "mguHAOQ5opP5MrAQGzg8C0FilbCgnUu8RmHQLLTW-v0";

        [Fact]
        public async Task Should_Send_Subscribe_Message()
        {
            var subscribeMessageService = await WeChatServiceFactory.CreateAsync<SubscribeMessageWeService>();

            var result = await subscribeMessageService.SendAsync(OpenId, TemplateId, null, new SubscribeMessageData
            {
                { "thing1", new SubscribeMessageDataItem { Value = "标题1" } },
                { "thing2", new SubscribeMessageDataItem { Value = "类别1" } },
                { "thing3", new SubscribeMessageDataItem { Value = "内容1" } }
            });

            result.ShouldNotBeNull();
            result.ErrorMessage.ShouldBeNull();
            result.ErrorCode.ShouldBe(0);
        }
    }
}