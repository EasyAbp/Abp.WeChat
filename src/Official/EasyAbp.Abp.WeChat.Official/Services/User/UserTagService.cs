using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;
using EasyAbp.Abp.WeChat.Official.Services.CustomMenu.Request;
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

        public Task<CreateUserTagResponse> CreateAsync(string name)
        {
            return WeChatOfficialApiRequester.RequestAsync<CreateUserTagResponse>(CreateApiUrl,
                HttpMethod.Post,
                new CreateUserTagRequest(name)
            );
        }

        public Task<GetAllUserTagListResponse> GetCreatedTagsAsync()
        {
            return WeChatOfficialApiRequester.RequestAsync<GetAllUserTagListResponse>(GetCreatedTagsApiUrl,
                HttpMethod.Get
            );
        }
    }
}