using System.Collections.Generic;
using System.Text.Json.Serialization;
using EasyAbp.Abp.WeChat.OpenPlatform.Shared.Models;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.OpenPlatform.ThirdPartyPlatform.Services.Response;

public class QueryAuthResponse : OpenPlatformCommonResponse
{
    [JsonPropertyName("authorization_info")]
    [JsonProperty("authorization_info")]
    public QueryAuthResponseAuthorizationInfo AuthorizationInfo { get; set; }
}

public class QueryAuthResponseAuthorizationInfo
{
    [JsonPropertyName("authorizer_appid")]
    [JsonProperty("authorizer_appid")]
    public string AuthorizerAppId { get; set; }

    [JsonPropertyName("authorizer_access_token")]
    [JsonProperty("authorizer_access_token")]
    public string AuthorizerAccessToken { get; set; }

    [JsonPropertyName("expires_in")]
    [JsonProperty("expires_in")]
    public int ExpiresIn { get; set; }

    [JsonPropertyName("authorizer_refresh_token")]
    [JsonProperty("authorizer_refresh_token")]
    public string AuthorizerRefreshToken { get; set; }

    [JsonPropertyName("func_info")]
    [JsonProperty("func_info")]
    public List<QueryAuthResponseFuncInfoItem> FuncInfo { get; set; }
}

public class QueryAuthResponseFuncInfoItem
{
    [JsonPropertyName("funcscope_category")]
    [JsonProperty("funcscope_category")]
    public QueryAuthResponseFuncScopeCategory FuncScopeCategory { get; set; }
}

public class QueryAuthResponseFuncScopeCategory
{
    [JsonPropertyName("id")]
    [JsonProperty("id")]
    public int Id { get; set; }
}