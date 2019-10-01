using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Zony.Abp.WeChat.Official.Services.CustomMenu;

namespace Zony.Abp.WeChat.Official.Tests.Services
{
    public class CustomMenuService_Tests : AbpWeChatOfficialTestBase
    {
        private readonly CustomMenuService _customMenuService;

        public CustomMenuService_Tests()
        {
            _customMenuService = GetRequiredService<CustomMenuService>();
        }

        [Fact]
        public async Task Should_Create_A_CustomMenu()
        {
            var newCustomMenu = new List<CustomButton>();
            newCustomMenu.Add(new CustomButton("今日歌曲",CustomButtonType.Click,"V1001_TODAY_MUSIC")
            {
                SubButtons = new List<CustomButton>
                {
                    new CustomButton("赞一下我们",CustomButtonType.Click,"V1001_GOOD")
                }
            });

            await _customMenuService.CreateCustomMenuAsync(newCustomMenu);
        }
    }
}