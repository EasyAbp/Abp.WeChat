using System.Collections.Generic;
using System.Threading.Tasks;
using Shouldly;
using Xunit;
using EasyAbp.Abp.WeChat.Official.Services.CustomMenu;

namespace EasyAbp.Abp.WeChat.Official.Tests.Services
{
    public class CustomMenuServiceTests : AbpWeChatOfficialTestBase
    {
        [Fact]
        public async Task Should_Create_A_CustomMenu()
        {
            var newCustomMenu = new List<CustomButton>
            {
                new CustomButton("今日歌曲", CustomButtonType.Click, "V1001_TODAY_MUSIC")
                {
                    SubButtons = new List<CustomButton>
                    {
                        new CustomButton("赞一下我们", CustomButtonType.Click, "V1001_GOOD")
                    }
                }
            };

            var customMenuService = await WeChatServiceFactory.CreateAsync<CustomMenuWeService>();

            var result = await customMenuService.CreateCustomMenuAsync(newCustomMenu);

            result.ShouldNotBeNull();
            result.ErrorCode.ShouldBe(0);
            result.ErrorMessage.ShouldBe("ok");
        }

        [Fact]
        public async Task Should_Delete_All_CustomMenu()
        {
            var customMenuService = await WeChatServiceFactory.CreateAsync<CustomMenuWeService>();

            var result = await customMenuService.DeleteCustomMenuAsync();

            result.ShouldNotBeNull();
            result.ErrorCode.ShouldBe(0);
            result.ErrorMessage.ShouldBe("ok");
        }

        [Fact]
        public async Task Should_Return_All_CustomMenu()
        {
            var customMenuService = await WeChatServiceFactory.CreateAsync<CustomMenuWeService>();

            var result = await customMenuService.GetAllCustomMenuAsync();

            result.Menu.Buttons.ShouldNotBeNull();
            result.Menu.Buttons.Count.ShouldNotBe(0);
        }
    }
}