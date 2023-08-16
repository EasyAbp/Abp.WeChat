using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.Pay.Security.PlatformCertificate;

public interface IPlatformCertificateManager
{
    Task<PlatformCertificateCacheItem> GetPlatformCertificateAsync(string serialNo);
}