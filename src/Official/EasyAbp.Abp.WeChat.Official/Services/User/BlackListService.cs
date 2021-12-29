using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;
using EasyAbp.Abp.WeChat.Official.Services.User.Request;
using EasyAbp.Abp.WeChat.Official.Services.User.Response;

namespace EasyAbp.Abp.WeChat.Official.Services.User
{
    /// <summary>
    /// 黑名单管理服务，管理公众号的黑名单。
    /// </summary>
    public class BlackListService : CommonService
    {
        protected const string GetBlackListUrl = "https://api.weixin.qq.com/cgi-bin/tags/members/getblacklist?";
        protected const string BatchBlackListUrl = "https://api.weixin.qq.com/cgi-bin/tags/members/batchblacklist?";
        protected const string BatchUnBlackListUrl = "https://api.weixin.qq.com/cgi-bin/tags/members/batchunblacklist?";

        /// <summary>
        /// 获取公众号的黑名单列表，接口每次最多拉取 10000 个黑名单用户。<br/>
        /// 当列表数量较多的时候，可以采用分批拉取的方式。
        /// </summary>
        /// <param name="beginOpenId">起始 OPENID，如果传递则从该 OPENID 往后拉取。默认则从头开始拉取。</param>
        public Task GetBlackListAsync(string beginOpenId = null)
        {
            return WeChatOfficialApiRequester.RequestAsync<GetBlackListResponse>(GetBlackListUrl,
                HttpMethod.Post,
                new GetBlackListRequest(beginOpenId));
        }

        /// <summary>
        /// 拉黑指定用户，每次最多拉黑 20 个用户。
        /// </summary>
        /// <param name="openIds">需要拉黑的用户 OPENID。</param>
        public Task BatchBlackListAsync(List<string> openIds)
        {
            return WeChatOfficialApiRequester.RequestAsync<OfficialCommonResponse>(BatchBlackListUrl,
                HttpMethod.Post,
                new BatchBlackListRequest(openIds));
        }

        /// <summary>
        /// 取消拉黑指定用户，每次最多取消拉黑 20 个用户。
        /// </summary>
        /// <param name="openIds">需要取消拉黑的用户 OPENID。</param>
        public Task BatchUnBlackListAsync(List<string> openIds)
        {
            return WeChatOfficialApiRequester.RequestAsync<OfficialCommonResponse>(BatchUnBlackListUrl,
                HttpMethod.Post,
                new BatchUnBlackListRequest(openIds));
        }
    }
}