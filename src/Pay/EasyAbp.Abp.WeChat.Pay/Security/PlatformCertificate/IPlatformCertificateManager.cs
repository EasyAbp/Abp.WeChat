using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Pay.Security.PlatformCertificate;

public interface IPlatformCertificateManager
{
    Task<PlatformCertificateEntity> GetPlatformCertificateAsync([NotNull] string mchId, [NotNull] string serialNo);
}