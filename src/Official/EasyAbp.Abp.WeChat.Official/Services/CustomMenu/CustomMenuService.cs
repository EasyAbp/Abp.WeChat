using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Infrastructure.Models;

namespace EasyAbp.Abp.WeChat.Official.Services.CustomMenu
{
    /// <summary>
    /// 自定义菜单服务，可以注入本服务实现对自定义菜单的管理操作。
    /// </summary>
    public class CustomMenuService : CommonService
    {
        protected const string CreateApiUrl = "https://api.weixin.qq.com/cgi-bin/menu/create?";
        protected const string DeleteApiUrl = "https://api.weixin.qq.com/cgi-bin/menu/delete?";
        protected const string GetDetailApiUrl = "https://api.weixin.qq.com/cgi-bin/get_current_selfmenu_info?";

        /// <summary>
        /// 根据指定的参数创建自定义菜单，本方法相当于全量覆盖，执行之后微信公众号的原有菜单信息将会被覆盖。
        /// </summary>
        /// <param name="customButtons">最新的自定义菜单集合。</param>
        public Task<OfficialCommonResponse> CreateCustomMenuAsync(List<CustomButton> customButtons)
        {
            return WeChatOfficialApiRequester.RequestAsync<OfficialCommonResponse>(CreateApiUrl, HttpMethod.Post,
                new CreateCustomMenuRequest
                {
                    Buttons = customButtons
                });
        }

        /// <summary>
        /// 删除当前公众号所有自定义菜单。另请注意，在个性化菜单时，调用此接口会删除默认菜单及全部个性化菜单。
        /// </summary>
        /// <returns></returns>
        public Task<OfficialCommonResponse> DeleteCustomMenuAsync()
        {
            return WeChatOfficialApiRequester.RequestAsync<OfficialCommonResponse>(DeleteApiUrl, HttpMethod.Get);
        }

        public Task GetCustomMenuDetailAsync()
        {
            throw new NotImplementedException();
        }
    }
}