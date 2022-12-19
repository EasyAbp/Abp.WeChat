using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling;

public interface IWeChatPayClientRequestHandlingService
{
    Task<GetJsSdkWeChatPayParametersResult> GetJsSdkWeChatPayParametersAsync(GetJsSdkWeChatPayParametersInput input);
}