using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.MiniProgram.Services.ACode;
using EasyAbp.Abp.WeChat.MiniProgram.Services.SubscribeMessage;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.MiniProgram.Tests.Services
{
    public class SubscribeMessageService_Tests : AbpWeChatMiniProgramTestBase
    {
        private const string OpenId = "on7qq1HZmDVgYTmzz8r3tayh-wqw";
        private const string TemplateId = "mguHAOQ5opP5MrAQGzg8C0FilbCgnUu8RmHQLLTW-v0";
        
        private readonly SubscribeMessageService _subscribeMessageService;

        public SubscribeMessageService_Tests()
        {
            _subscribeMessageService = GetRequiredService<SubscribeMessageService>();
        }

        [Fact]
        public async Task Should_Get_Unlimited_ACode()
        {
            var result = await _subscribeMessageService.SendAsync(OpenId, TemplateId, null, new SubscribeMessageData
            {
                {"任务标题", new SubscribeMessageDataItem {Value = "标题1"}},
                {"任务类别", new SubscribeMessageDataItem {Value = "类别1"}},
                {"任务内容", new SubscribeMessageDataItem {Value = "内容1"}}
            });
            
            result.ShouldNotBeNull();
            result.ErrorMessage.ShouldBeNull();
            result.ErrorCode.ShouldBe(0);
        }
    }
}