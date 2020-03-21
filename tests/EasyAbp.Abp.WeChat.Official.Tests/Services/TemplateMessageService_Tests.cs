using System.Drawing;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage;

namespace EasyAbp.Abp.WeChat.Official.Tests.Services
{
    public class TemplateMessageService_Tests : AbpWeChatOfficialTestBase
    {
        private readonly TemplateMessageService _templateMessageService;

        private const string OpenId = "on7qq1HZmDVgYTmzz8r3tayh-wqw";
        private const string TemplateId = "KbYdb1K23gXbaXfO1NCZ2xw4iHPO5zHqcpp5nnXx8Xs";

        public TemplateMessageService_Tests()
        {
            _templateMessageService = GetRequiredService<TemplateMessageService>();
        }

        [Fact]
        public async Task Should_Send_A_Template_Message()
        {
            var templateMessage = new TemplateMessage(new TemplateMessageItem("起点科技您好",Color.Blue), new TemplateMessageItem("请尽快处理，谢谢合作.",Color.Black))
                .AddKeywords("keyword1","北京路店设备")
                .AddKeywords("keyword2","断电 1 分钟")
                .AddKeywords("keyword3","2016-02-02 01:223:36");
            
            var result = await _templateMessageService.SendMessageAsync(OpenId,TemplateId,"https://www.baidu.com",templateMessage);
            result.MessageId.ShouldBeGreaterThan(0);
        }
    }
}