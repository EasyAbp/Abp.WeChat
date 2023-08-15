using System.Security.Cryptography;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common.Extensions;
using EasyAbp.Abp.WeChat.Common.Infrastructure;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Signature;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.RequestHandling;

public class WeChatPayClientRequestHandlingService : IWeChatPayClientRequestHandlingService, ITransientDependency
{
    private readonly IAbpWeChatPayOptionsProvider _optionsProvider;
    private readonly ISignatureGenerator _signatureGenerator;

    public WeChatPayClientRequestHandlingService(
        IAbpWeChatPayOptionsProvider optionsProvider,
        ISignatureGenerator signatureGenerator)
    {
        _optionsProvider = optionsProvider;
        _signatureGenerator = signatureGenerator;
    }

    public virtual async Task<GetJsSdkWeChatPayParametersResult> GetJsSdkWeChatPayParametersAsync(
        GetJsSdkWeChatPayParametersInput input)
    {
        if (string.IsNullOrEmpty(input.PrepayId))
        {
            return new GetJsSdkWeChatPayParametersResult("请传入有效的预支付订单 Id。");
        }

        var options = await _optionsProvider.GetAsync(input.MchId);

        var nonceStr = RandomStringHelper.GetRandomString();
        var timeStamp = DateTimeHelper.GetNowTimeStamp();
        var package = $"prepay_id={input.PrepayId}";

        const string signType = "MD5";

        var @params = new WeChatParameters();
        @params.AddParameter("appId", input.AppId);
        @params.AddParameter("nonceStr", nonceStr);
        @params.AddParameter("timeStamp", timeStamp);
        @params.AddParameter("package", package);
        @params.AddParameter("signType", signType);

        var paySign = _signatureGenerator.Generate(@params, MD5.Create(), options.ApiV3Key);

        return new GetJsSdkWeChatPayParametersResult(nonceStr, timeStamp, package, signType, paySign);
    }
}