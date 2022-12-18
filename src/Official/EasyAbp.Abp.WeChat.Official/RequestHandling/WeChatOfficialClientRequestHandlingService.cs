using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;
using EasyAbp.Abp.WeChat.Common.Extensions;
using EasyAbp.Abp.WeChat.Common.Infrastructure;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Options;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Signature;
using EasyAbp.Abp.WeChat.Official.JsTickets;
using EasyAbp.Abp.WeChat.Official.Options;
using EasyAbp.Abp.WeChat.Official.Services.Login;
using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Official.RequestHandling;

public class WeChatOfficialClientRequestHandlingService : IWeChatOfficialClientRequestHandlingService,
    ITransientDependency
{
    private readonly IJsTicketProvider _jsTicketProvider;
    private readonly ISignatureGenerator _signatureGenerator;
    private readonly IAbpWeChatServiceFactory _serviceFactory;
    private readonly IAbpWeChatOptionsProvider<AbpWeChatOfficialOptions> _optionsProvider;

    public WeChatOfficialClientRequestHandlingService(
        IJsTicketProvider jsTicketProvider,
        ISignatureGenerator signatureGenerator,
        IAbpWeChatServiceFactory serviceFactory,
        IAbpWeChatOptionsProvider<AbpWeChatOfficialOptions> optionsProvider)
    {
        _jsTicketProvider = jsTicketProvider;
        _signatureGenerator = signatureGenerator;
        _serviceFactory = serviceFactory;
        _optionsProvider = optionsProvider;
    }


    public virtual async Task<GetAccessTokenByCodeResult> GetAccessTokenByCodeAsync(string code, string appId)
    {
        var loginService = await _serviceFactory.CreateAsync<LoginWeService>(appId);

        var response = await loginService.Code2AccessTokenAsync(code);

        return response.ErrorCode == 0
            ? new GetAccessTokenByCodeResult(
                response.AccessToken, response.Scope, response.ExpiresIn, response.OpenId, response.RefreshToken)
            : new GetAccessTokenByCodeResult(response.ErrorCode, response.ErrorMessage);
    }

    public virtual async Task<GetJsSdkConfigParametersResult> GetJsSdkConfigParametersAsync(string jsUrl, string appId)
    {
        var options = await _optionsProvider.GetAsync(appId);

        var nonceStr = RandomStringHelper.GetRandomString();
        var timeStamp = DateTimeHelper.GetNowTimeStamp();
        var ticket = await _jsTicketProvider.GetTicketAsync(options.AppId, options.AppSecret);

        var @params = new WeChatParameters();
        @params.AddParameter("noncestr", nonceStr);
        @params.AddParameter("jsapi_ticket", ticket);
        @params.AddParameter("url", HttpUtility.UrlDecode(jsUrl));
        @params.AddParameter("timestamp", timeStamp);

        var signStr = _signatureGenerator.Generate(@params, SHA1.Create()).ToLower();

        return new GetJsSdkConfigParametersResult(appId, nonceStr, timeStamp, signStr, ticket);
    }
}