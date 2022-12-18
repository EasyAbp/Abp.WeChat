using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Services.User;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Official.Tests.Services;

public class BlackListServiceTests : AbpWeChatOfficialTestBase
{
    [Fact]
    public async Task Should_Pull_Black_User_And_UnBlack_User()
    {
        var blackListService = await WeChatServiceFactory.CreateAsync<BlackListWeService>();

        var response = await blackListService.BatchBlackListAsync(new List<string>
        {
            "on7qq1H94tJeuwdC61iRsb6IQiAU"
        });

        response.ErrorCode.ShouldBe(0);
        response.ErrorMessage.ShouldBe("ok");

        // Get the black user list.
        var blackList = await blackListService.GetBlackListAsync();
        blackList.ErrorCode.ShouldBe(0);
        blackList.Count.ShouldBe(1);
        blackList.Total.ShouldBe(1);
        blackList.Data.OpenIds.ShouldContain("on7qq1H94tJeuwdC61iRsb6IQiAU");

        // UnBlack the user.
        var unBlackResponse = await blackListService.BatchUnBlackListAsync(new List<string>
        {
            "on7qq1H94tJeuwdC61iRsb6IQiAU"
        });

        unBlackResponse.ErrorCode.ShouldBe(0);
        unBlackResponse.ErrorMessage.ShouldBe("ok");
    }
}