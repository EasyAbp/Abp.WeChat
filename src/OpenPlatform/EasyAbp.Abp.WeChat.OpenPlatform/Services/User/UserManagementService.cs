using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models;
using EasyAbp.Abp.WeChat.OpenPlatform.Services.User.Request;
using EasyAbp.Abp.WeChat.OpenPlatform.Services.User.Response;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Services.User;

public class UserManagementService : CommonService
{
    protected const string GetOfficialUserListUrl = "https://api.weixin.qq.com/cgi-bin/user/get?";
    protected const string UpdateUserRemarkUrl = "https://api.weixin.qq.com/cgi-bin/user/info/updateremark?";
    protected const string GetUserUnionInfoUrl = "https://api.weixin.qq.com/cgi-bin/user/info?";
    protected const string BatchGetUserUnionInfoUrl = "https://api.weixin.qq.com/cgi-bin/user/info/batchget?";

    /// <summary>
    /// 获取用户列表，公众号可通过本接口来获取帐号的关注者列表。<br/>
    /// 一次拉取调用最多拉取 10000 个关注者的 OpenID，可以通过多次拉取的方式来满足需求。
    /// </summary>
    /// <param name="firstOpenId">第一个拉取的OPENID，不填默认从头开始拉取。</param>
    public virtual Task<OpenPlatformUserListResponse> GetOfficialUserListAsync(string firstOpenId = null)
    {
        return WeChatOpenPlatformApiRequester.RequestAsync<OpenPlatformUserListResponse>(GetOfficialUserListUrl,
            HttpMethod.Get,
            new GetOpenPlatformUserListRequest(firstOpenId));
    }

    /// <summary>
    /// 设置指定用户的备注名，本接口仅开放给微信认证的服务号。
    /// </summary>
    /// <param name="openId">微信公众号的用户唯一标识。</param>
    /// <param name="remark">新的备注名，长度必须小于 30 字符。</param>
    public virtual Task<OpenPlatformCommonResponse> UpdateUserRemarkAsync(string openId, string remark)
    {
        return WeChatOpenPlatformApiRequester.RequestAsync<OpenPlatformCommonResponse>(UpdateUserRemarkUrl,
            HttpMethod.Post,
            new UpdateUserRemarkRequest(openId, remark));
    }

    /// <summary>
    /// 获取用户基本信息，公众号可通过本接口来获取帐号的关注者基本信息。
    /// </summary>
    /// <param name="openId">普通用户的标识，对当前公众号唯一。</param>
    /// <param name="language">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语。</param>
    public virtual Task<UnionUserInfoResponse> GetUserUnionInfoAsync(string openId, string language = null)
    {
        return WeChatOpenPlatformApiRequester.RequestAsync<UnionUserInfoResponse>(GetUserUnionInfoUrl,
            HttpMethod.Get,
            new GetUserUnionInfoRequest(openId, language));
    }

    /// <summary>
    /// 批量获取用户基本信息，公众号可通过本接口来批量获取用户基本信息。
    /// </summary>
    /// <param name="userIds">需要查询的用户 OPENID 列表，最多支持 100 个。</param>
    public virtual Task<BatchUnionUserInfoResponse> BatchGetUserUnionInfoAsync(List<GetUserUnionInfoRequest> userIds)
    {
        return WeChatOpenPlatformApiRequester.RequestAsync<BatchUnionUserInfoResponse>(BatchGetUserUnionInfoUrl,
            HttpMethod.Post,
            new BatchGetUserUnionInfoRequest(userIds));
    }
}