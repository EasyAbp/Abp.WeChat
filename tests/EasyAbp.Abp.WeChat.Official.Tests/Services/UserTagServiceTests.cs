using System.Collections.Generic;
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

    [Fact]
    public async Task Should_Update_TestTag_To_TestTagModified()
    {
        var newTag = await _userTagService.CreateAsync("TestTag");
        var response = await _userTagService.UpdateAsync(newTag.Tag.Id, "TestTagModified");

        var modifyTag = (await _userTagService.GetCreatedTagsAsync()).Tags.FirstOrDefault(x => x.Id == newTag.Tag.Id);

        response.ErrorCode.ShouldBe(0);
        response.ErrorMessage.ShouldBe("ok");

        modifyTag.ShouldNotBeNull();
        modifyTag.Name.ShouldBe("TestTagModified");

        // Clean up
        await _userTagService.DeleteAsync(newTag.Tag.Id);
    }

    [Fact]
    public async Task Should_Tagging_One_User()
    {
        var testTag = await _userTagService.CreateAsync("TestTag");
        var response = await _userTagService.BatchTaggingAsync(testTag.Tag.Id, new List<string>
        {
            "on7qq1HZmDVgYTmzz8r3tayh-wqw"
        });

        response.ErrorCode.ShouldBe(0);

        // Query the user's tag.
        var users = await _userTagService.GetUsersByTagAsync(testTag.Tag.Id);

        users.ErrorCode.ShouldBe(0);
        users.Data.OpenIds.Count.ShouldBeGreaterThan(0);

        // Clean up
        await _userTagService.DeleteAsync(testTag.Tag.Id);
    }

    [Fact]
    public async Task Should_Tagging_One_User_And_UnTagging_User()
    {
        var testTag = await _userTagService.CreateAsync("TestTag");
        var response = await _userTagService.BatchTaggingAsync(testTag.Tag.Id, new List<string>
        {
            "on7qq1HZmDVgYTmzz8r3tayh-wqw"
        });

        response.ErrorCode.ShouldBe(0);

        // Query the user's tag.
        var users = await _userTagService.GetUsersByTagAsync(testTag.Tag.Id);

        users.ErrorCode.ShouldBe(0);
        users.Data.OpenIds.Count.ShouldBeGreaterThan(0);
        
        var userTags = await _userTagService.GetTagsByUserAsync("on7qq1HZmDVgYTmzz8r3tayh-wqw");
        userTags.ErrorCode.ShouldBe(0);
        userTags.TagIds.Count.ShouldBeGreaterThan(0);

        // UnTagging the user.
        var res1 = await _userTagService.BatchUnTaggingAsync(testTag.Tag.Id, new List<string>
        {
            "on7qq1HZmDVgYTmzz8r3tayh-wqw"
        });
        res1.ErrorCode.ShouldBe(0);
        
        // User should be untagged.
        var res2 = await _userTagService.GetTagsByUserAsync("on7qq1HZmDVgYTmzz8r3tayh-wqw");
        res2.ErrorCode.ShouldBe(0);
        res2.TagIds.Count.ShouldBe(0);
        
        // Clean up
        await _userTagService.DeleteAsync(testTag.Tag.Id);
    }

    [Fact]
    public async Task CleanUp()
    {
        // var allTags = await _userTagService.GetCreatedTagsAsync();
        //
        // // Clean up
        // foreach (var tag in allTags.Tags)
        // {
        //     await _userTagService.DeleteAsync(tag.Id);
        // }

        await Task.CompletedTask;
    }
}