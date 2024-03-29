using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Official.RequestHandling;

public interface IWeChatOfficialClientRequestHandlingService
{
    Task<GetAccessTokenByCodeResult> GetAccessTokenByCodeAsync(string code, [CanBeNull] string appId);

    Task<GetJsSdkConfigParametersResult> GetJsSdkConfigParametersAsync(string jsUrl, [CanBeNull] string appId);
}