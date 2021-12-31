using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Services.User;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Official.Tests.Services;

public class UserManagementServiceTests : AbpWeChatOfficialTestBase
{
    private readonly UserManagementService _userManagementService;

    public UserManagementServiceTests()
    {
        _userManagementService = GetRequiredService<UserManagementService>();
    }

    [Fact]
    public async Task Should_Return_All_User_OpenIds()
    {
        var openIdsResponse = await _userManagementService.GetOfficialUserListAsync();
        openIdsResponse.ShouldNotBeNull();
        openIdsResponse.Count.ShouldBeGreaterThan(0);
        openIdsResponse.NextOpenId.ShouldNotBeNullOrEmpty();
    }

    [Fact]
    public async Task Should_Update_User_Remark_And_Return_Ok_Message()
    {
        var response = await _userManagementService.UpdateUserRemarkAsync("on7qq1HZmDVgYTmzz8r3tayh-wqw", "RealZony");
        
        response.ErrorMessage.ShouldBe("ok");
        response.ErrorCode.ShouldBe(0);
    }
}