using System.Collections.Generic;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.ThirdPartyPlatform.Response;

public class QueryAuthResponse : OpenPlatformCommonResponse
{
    [JsonProperty("authorization_info")]
    public QueryAuthResponseAuthorizationInfo AuthorizationInfo { get; set; }
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
    public List<QueryAuthResponseFuncInfoItem> FuncInfo { get; set; }
}

public class QueryAuthResponseFuncInfoItem
{
    [JsonProperty("funcscope_category")]
    public QueryAuthResponseFuncScopeCategory FuncScopeCategory { get; set; }
}

public class QueryAuthResponseFuncScopeCategory
{
    [JsonProperty("id")]
    public int Id { get; set; }
}