using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;
using EasyAbp.Abp.WeChat.Official.Services.User.Request;
using EasyAbp.Abp.WeChat.Official.Services.User.Response;

namespace EasyAbp.Abp.WeChat.Official.Services.User
{
    /// <summary>
    /// 用户标签管理服务。
    /// </summary>
    public class UserTagService : CommonService
    {
        protected const string CreateApiUrl = "https://api.weixin.qq.com/cgi-bin/tags/create?";
        protected const string UpdateApiUrl = "https://api.weixin.qq.com/cgi-bin/tags/update?";
        protected const string DeleteApiUrl = "https://api.weixin.qq.com/cgi-bin/tags/delete?";
        protected const string GetCreatedTagsApiUrl = "https://api.weixin.qq.com/cgi-bin/tags/get?";
        protected const string GetUsersByTagApiUrl = "https://api.weixin.qq.com/cgi-bin/user/tag/get?";
        protected const string BatchTaggingApiUrl = "https://api.weixin.qq.com/cgi-bin/tags/members/batchtagging?";
        protected const string BatchUnTaggingApiUrl = "https://api.weixin.qq.com/cgi-bin/tags/members/batchuntagging?";
        protected const string GetTagsByUserApiUrl = "https://api.weixin.qq.com/cgi-bin/tags/getidlist?";

        /// <summary>
        /// 创建新的用户标签。
        /// </summary>
        /// <param name="name">标签名称。</param>
        public virtual Task<CreateUserTagResponse> CreateAsync(string name)
        {
            return WeChatOfficialApiRequester.RequestAsync<CreateUserTagResponse>(CreateApiUrl,
                HttpMethod.Post,
                new CreateUserTagRequest(name)
            );
        }

        /// <summary>
        /// 获取当前公众号的所有用户标签列表。
        /// </summary>
        public virtual Task<GetAllUserTagListResponse> GetCreatedTagsAsync()
        {
            return WeChatOfficialApiRequester.RequestAsync<GetAllUserTagListResponse>(GetCreatedTagsApiUrl,
                HttpMethod.Get
            );
        }

        /// <summary>
        /// 根据标签 ID，删除指定的标签。
        /// </summary>
        /// <param name="tagId">标签的唯一 ID。</param>
        public virtual Task<OfficialCommonResponse> DeleteAsync(long tagId)
        {
            return WeChatOfficialApiRequester.RequestAsync<OfficialCommonResponse>(DeleteApiUrl,
                HttpMethod.Post,
                new DeleteUserTagRequest(tagId)
            );
        }

        /// <summary>
        /// 根据标签 ID，更新指定的标签名称。
        /// </summary>
        /// <param name="tagId">标签的唯一 ID。</param>
        /// <param name="name">新的标签名称。</param>
        public virtual Task<OfficialCommonResponse> UpdateAsync(long tagId, string name)
        {
            return WeChatOfficialApiRequester.RequestAsync<OfficialCommonResponse>(UpdateApiUrl,
                HttpMethod.Post,
                new UpdateUserTagRequest(tagId, name)
            );
        }

        /// <summary>
        /// 批量为用户打上标签。
        /// </summary>
        /// <param name="tagId">需要打上的标签 ID。</param>
        /// <param name="openIds">需要打标签的用户 OPEN ID 集合。</param>
        public virtual Task<OfficialCommonResponse> BatchTaggingAsync(long tagId, List<string> openIds)
        {
            return WeChatOfficialApiRequester.RequestAsync<OfficialCommonResponse>(BatchTaggingApiUrl,
                HttpMethod.Post,
                new BatchTagOperationRequest(tagId, openIds)
            );
        }

        /// <summary>
        /// 批量为用户取消标签。
        /// </summary>
        /// <param name="tagId">需要取消的标签 ID。</param>
        /// <param name="openIds">需要取消标签的用户 OPEN ID 集合。</param>
        public virtual Task<OfficialCommonResponse> BatchUnTaggingAsync(long tagId, List<string> openIds)
        {
            return WeChatOfficialApiRequester.RequestAsync<OfficialCommonResponse>(BatchUnTaggingApiUrl,
                HttpMethod.Post,
                new BatchTagOperationRequest(tagId, openIds)
            );
        }

        /// <summary>
        /// 获取用户身上的标签列表。
        /// </summary>
        /// <param name="openId">需要查询用户的 OPENID。</param>
        public virtual Task<GetTagsByUserResponse> GetTagsByUserAsync(string openId)
        {
            return WeChatOfficialApiRequester.RequestAsync<GetTagsByUserResponse>(GetTagsByUserApiUrl,
                HttpMethod.Post,
                new GetTagsByUserRequest(openId)
            );
        }

        /// <summary>
        /// 获取标签下粉丝列表。
        /// </summary>
        /// <param name="tagId">标签的唯一 ID。</param>
        /// <param name="firstOpenId">起始粉丝的 OPENID，将会从指定的 OPENID 往后拉取粉丝数据。</param>
        public virtual Task<GetUsersByTagResponse> GetUsersByTagAsync(long tagId, string firstOpenId = null)
        {
            return WeChatOfficialApiRequester.RequestAsync<GetUsersByTagResponse>(GetUsersByTagApiUrl,
                HttpMethod.Post,
                new GetUsersByTagRequest(tagId, firstOpenId)
            );
        }
    }
}