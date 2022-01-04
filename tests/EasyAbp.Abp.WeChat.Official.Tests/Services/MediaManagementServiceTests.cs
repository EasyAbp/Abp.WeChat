using System;
using System.IO;
using System.Threading.Tasks;
using Bogus;
using EasyAbp.Abp.WeChat.Official.Services.MediaManagement;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Official.Tests.Services;

public class MediaManagementServiceTests : AbpWeChatOfficialTestBase
{
    private readonly MediaManagementService _mediaManagementService;
    private readonly Faker _defaultFaker;

    public MediaManagementServiceTests()
    {
        _mediaManagementService = GetRequiredService<MediaManagementService>();
        _defaultFaker = new Faker();
    }

    [Fact]
    public void Should_All_Media_Type_Validation_Succeed()
    {
        MediaType.IsValidMediaFile(new MemoryStream(_defaultFaker.System.Random.Bytes(MediaType.MaxImageSize)), MediaType.Image).ShouldBeTrue();
        MediaType.IsValidMediaFile(new MemoryStream(_defaultFaker.System.Random.Bytes(MediaType.MaxVoiceSize)), MediaType.Voice).ShouldBeTrue();
        MediaType.IsValidMediaFile(new MemoryStream(_defaultFaker.System.Random.Bytes(MediaType.MaxVideoSize)), MediaType.Video).ShouldBeTrue();
        MediaType.IsValidMediaFile(new MemoryStream(_defaultFaker.System.Random.Bytes(MediaType.MaxThumbSize)), MediaType.Thumb).ShouldBeTrue();
    }

    [Fact]
    public void Should_All_Media_Type_Validation_Failed()
    {
        MediaType.IsValidMediaFile(new MemoryStream(_defaultFaker.System.Random.Bytes(MediaType.MaxImageSize + 1)), MediaType.Image).ShouldBeFalse();
        MediaType.IsValidMediaFile(new MemoryStream(_defaultFaker.System.Random.Bytes(MediaType.MaxVoiceSize + 1)), MediaType.Voice).ShouldBeFalse();
        MediaType.IsValidMediaFile(new MemoryStream(_defaultFaker.System.Random.Bytes(MediaType.MaxVideoSize + 1)), MediaType.Video).ShouldBeFalse();
        MediaType.IsValidMediaFile(new MemoryStream(_defaultFaker.System.Random.Bytes(MediaType.MaxThumbSize + 1)), MediaType.Thumb).ShouldBeFalse();
    }

    [Fact]
    public async Task Should_Upload_Temp_Media_File_Success()
    {
        var thumbBase64 =
            @"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";
        var thumbStream = new MemoryStream(Convert.FromBase64String(thumbBase64));

        var response = await _mediaManagementService.UploadTempMediaAsync(thumbStream, MediaType.Thumb, $"{Guid.NewGuid().ToString()}.jpg");
        response.ShouldNotBeNull();
        response.ErrorCode.ShouldBe(0);
        response.CreatedAt.ShouldBeGreaterThan(0);
        response.ThumbMediaId.ShouldNotBeNullOrEmpty();
        response.Type.ShouldBe(MediaType.Thumb);
    }
}