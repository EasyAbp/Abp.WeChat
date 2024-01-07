using System.Text;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Extensions;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.Pay.Security;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling;

public class WeChatPayClientRequestHandlingService : IWeChatPayClientRequestHandlingService, ITransientDependency
{
    private readonly ICertificatesManager _certificatesManager;
    private readonly IAbpWeChatPayOptionsProvider _optionsProvider;

    public WeChatPayClientRequestHandlingService(ICertificatesManager certificatesManager,
        IAbpWeChatPayOptionsProvider optionsProvider)
    {
        _certificatesManager = certificatesManager;
        _optionsProvider = optionsProvider;
    }

    public virtual async Task<GetJsSdkWeChatPayParametersResult> GetJsSdkWeChatPayParametersAsync(
        GetJsSdkWeChatPayParametersInput input)
    {
        if (string.IsNullOrEmpty(input.PrepayId))
        {
            return new GetJsSdkWeChatPayParametersResult("请传入有效的预支付订单 Id。");
        }

        var options = await _optionsProvider.GetAsync(input.MchId);

        const string signType = "RSA";
        var nonceStr = RandomStringHelper.GetRandomString();
        var timeStamp = DateTimeHelper.GetNowTimeStamp();
        var package = $"prepay_id={input.PrepayId}";

        var waitSignString = new StringBuilder();
        waitSignString.Append(input.AppId).Append('\n')
            .Append(timeStamp).Append('\n')
            .Append(nonceStr).Append('\n')
            .Append("prepay_id=").Append(input.PrepayId).Append('\n');
        var certificate = await _certificatesManager.GetCertificateAsync(options.MchId);
        var paySign = _certificatesManager.GetSignature(waitSignString.ToString(), certificate);

        return new GetJsSdkWeChatPayParametersResult(nonceStr, timeStamp, package, signType, paySign);
    }
}