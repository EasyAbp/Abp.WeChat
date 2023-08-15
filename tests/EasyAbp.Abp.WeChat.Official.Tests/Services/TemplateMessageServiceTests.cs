using System.Drawing;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using EasyAbp.Abp.WeChat.Official.Services.TemplateMessage;

namespace EasyAbp.Abp.WeChat.Official.Tests.Services
{
    public class TemplateMessageServiceTests : AbpWeChatOfficialTestBase
    {
        private const string OpenId = "on7qq1HZmDVgYTmzz8r3tayh-wqw";
        private const string TemplateId = "KbYdb1K23gXbaXfO1NCZ2xw4iHPO5zHqcpp5nnXx8Xs";

        [Fact]
        public async Task Should_Send_A_Template_Message()
        {
            var templateMessage = new TemplateMessage(new TemplateMessageItem("起点科技您好", Color.Blue),
                    new TemplateMessageItem("请尽快处理，谢谢合作.", Color.Black))
                .AddKeywords("keyword1", "北京路店设备")
                .AddKeywords("keyword2", "断电 1 分钟")
                .AddKeywords("keyword3", "2016-02-02 01:223:36");

            var templateMessageService = await WeChatServiceFactory.CreateAsync<TemplateMessageWeService>();

            var result =
                await templateMessageService.SendMessageAsync(OpenId, TemplateId, "https://www.baidu.com",
                    templateMessage);
            result.MessageId.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task Should_Set_Industry()
        {
            var templateMessageService = await WeChatServiceFactory.CreateAsync<TemplateMessageWeService>();

            var response = await templateMessageService.SetIndustryAsync("1", "4");

            response.ErrorCode.ShouldBe(0);
        }

        [Fact]
        public async Task Should_Get_Current_Industry()
        {
            var templateMessageService = await WeChatServiceFactory.CreateAsync<TemplateMessageWeService>();

            var response = await templateMessageService.GetIndustryAsync();

            response.ErrorCode.ShouldBe(0);
            response.PrimaryIndustry.FirstClass.ShouldBe("IT科技");
            response.SecondaryIndustry.FirstClass.ShouldBe("IT科技");
            response.PrimaryIndustry.SecondClass.ShouldBe("互联网|电子商务");
            response.SecondaryIndustry.SecondClass.ShouldBe("电子技术");
        }

        [Fact]
        public async Task Should_Create_A_Template_And_Return_TemplateId()
        {
            var templateMessageService = await WeChatServiceFactory.CreateAsync<TemplateMessageWeService>();

            var response = await templateMessageService.CreateTemplateAsync("OPENTM206482867");

            response.ErrorCode.ShouldBe(0);
            response.TemplateId.ShouldNotBeNullOrEmpty();
        }

        [Fact]
        public async Task Should_Return_All_Template()
        {
            var templateMessageService = await WeChatServiceFactory.CreateAsync<TemplateMessageWeService>();

            var response = await templateMessageService.GetAllPrivateTemplateAsync();

            response.ErrorCode.ShouldBe(0);
        }

        [Fact]
        public async Task Should_Delete_Template_By_Id()
        {
            var templateMessageService = await WeChatServiceFactory.CreateAsync<TemplateMessageWeService>();

            var createdTemplateResponse = await templateMessageService.CreateTemplateAsync("OPENTM206482867");
            var deletedTemplateResponse =
                await templateMessageService.DeleteTemplateAsync(createdTemplateResponse.TemplateId);

            deletedTemplateResponse.ErrorCode.ShouldNotBe(0);
        }

        [Fact]
        public async Task Should_Parse_Colors()
        {
            const string serializedContent = """
{"value":"test","color":"#00ffff"}
""";
            var item = new TemplateMessageItem("test", Color.Aqua);

            Newtonsoft.Json.JsonConvert.SerializeObject(item).ShouldBe(serializedContent);
            System.Text.Json.JsonSerializer.Serialize(item).ShouldBe(serializedContent);
        }
    }
}