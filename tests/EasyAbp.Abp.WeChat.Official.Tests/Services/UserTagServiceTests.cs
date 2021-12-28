using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Services.User;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Official.Tests.Services;

public class UserTagServiceTests : AbpWeChatOfficialTestBase
{
    private readonly UserTagService _userTagService;

    public UserTagServiceTests()
    {
        _userTagService = GetRequiredService<UserTagService>();
    }

    [Fact]
    public async Task Should_Created_UserTag()
    {
        var userTag = await _userTagService.CreateAsync("TestTag");

        userTag.ErrorCode.ShouldBe(0);
        userTag.Tag.ShouldNotBeNull();
        userTag.Tag.Name.ShouldBe("TestTag");
    }
}