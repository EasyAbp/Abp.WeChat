using System.Linq;
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

        // Clean up
        await _userTagService.DeleteAsync(userTag.Tag.Id);
    }

    [Fact]
    public async Task Should_Return_OneTags()
    {
        await _userTagService.CreateAsync("TestTag");
        var allTags = await _userTagService.GetCreatedTagsAsync();

        allTags.ErrorCode.ShouldBe(0);
        allTags.Tags.Count.ShouldBeGreaterThan(0);

        // Clean up
        foreach (var tag in allTags.Tags)
        {
            await _userTagService.DeleteAsync(tag.Id);
        }
    }

    [Fact]
    public async Task Should_Delete_UserTag()
    {
        var createdTag = await _userTagService.CreateAsync("TestTag");
        var response = await _userTagService.DeleteAsync(createdTag.Tag.Id);

        response.ErrorCode.ShouldBe(0);
        response.ErrorMessage.ShouldBe("ok");
    }
}