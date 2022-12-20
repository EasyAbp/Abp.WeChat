using System.Net.Http;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;

public interface IOpenPlatformRequest
{
    StringContent ToStringContent();
}