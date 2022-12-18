using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling;

public interface IWeChatPayClientRequestHandlingService
{
    Task<GetJsSdkWeChatPayParametersResult> GetJsSdkWeChatPayParametersAsync(
        string mchId, [NotNull] string appId, [NotNull] string prepayId);
}