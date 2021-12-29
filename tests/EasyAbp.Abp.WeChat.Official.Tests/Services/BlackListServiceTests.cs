using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Services.User;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Official.Tests.Services;

public class BlackListServiceTests : AbpWeChatOfficialTestBase
{
    private readonly BlackListService _blackListService;

    public BlackListServiceTests()
    {
        _blackListService = GetRequiredService<BlackListService>();
    }

    [Fact]
    public async Task Should_Pull_Black_User_And_UnBlack_User()
    {
        var response = await _blackListService.BatchBlackListAsync(new List<string>
        {
            "on7qq1H94tJeuwdC61iRsb6IQiAU"
        });

        response.ErrorCode.ShouldBe(0);
        response.ErrorMessage.ShouldBe("ok");

        // Get the black user list.
        var blackList = await _blackListService.GetBlackListAsync();
        blackList.ErrorCode.ShouldBe(0);
        blackList.Count.ShouldBe(1);
        blackList.Total.ShouldBe(1);
        blackList.Data.OpenIds.ShouldContain("on7qq1H94tJeuwdC61iRsb6IQiAU");

        // UnBlack the user.
        var unBlackResponse = await _blackListService.BatchUnBlackListAsync(new List<string>
        {
            "on7qq1H94tJeuwdC61iRsb6IQiAU"
        });

        unBlackResponse.ErrorCode.ShouldBe(0);
        unBlackResponse.ErrorMessage.ShouldBe("ok");
    }
}