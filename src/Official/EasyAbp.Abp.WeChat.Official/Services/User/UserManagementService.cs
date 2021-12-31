using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Services.User.Request;
using EasyAbp.Abp.WeChat.Official.Services.User.Response;

namespace EasyAbp.Abp.WeChat.Official.Services.User
{
    public class UserManagementService : CommonService
    {
        protected const string GetOfficialUserListUrl = "https://api.weixin.qq.com/cgi-bin/user/get?";

        /// <summary>
        /// 获取用户列表，公众号可通过本接口来获取帐号的关注者列表。<br/>
        /// 一次拉取调用最多拉取 10000 个关注者的 OpenID，可以通过多次拉取的方式来满足需求。
        /// </summary>
        /// <param name="firstOpenId">第一个拉取的OPENID，不填默认从头开始拉取。</param>
        public Task<OfficialUserListResponse> GetOfficialUserListAsync(string firstOpenId = null)
        {
            return WeChatOfficialApiRequester.RequestAsync<OfficialUserListResponse>(GetOfficialUserListUrl,
                HttpMethod.Get,
                new GetOfficialUserListRequest(firstOpenId));
        }
    }
}