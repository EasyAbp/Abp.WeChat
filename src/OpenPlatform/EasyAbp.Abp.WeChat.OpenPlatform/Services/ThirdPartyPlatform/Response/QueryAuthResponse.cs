using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.ThirdPartyPlatform.Response;

public class QueryAuthResponse : OpenPlatformCommonResponse
{
    [JsonProperty("pre_auth_code")]
    public string PreAuthCode { get; set; }
}

public class QueryAuthResponseAuthorizationInfo
{
    [JsonProperty("authorizer_appid")]
    public string AuthorizerAppId { get; set; }

    [JsonProperty("authorizer_access_token")]
    public string AuthorizerAccessToken { get; set; }

    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonProperty("authorizer_refresh_token")]
    public string AuthorizerRefreshToken { get; set; }

    [JsonProperty("func_info")]
    public QueryAuthResponseFuncInfo FuncInfo { get; set; }
}

public class QueryAuthResponseFuncInfo
{
    [JsonProperty("funcscope_category")]
    public QueryAuthResponseFuncscopeCategory FuncscopeCategory { get; set; }
}

public class QueryAuthResponseFuncscopeCategory
{
    [JsonProperty("id")]
    public int Id { get; set; }
}