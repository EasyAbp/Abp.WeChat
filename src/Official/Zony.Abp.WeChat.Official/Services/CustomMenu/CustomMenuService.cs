using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Zony.Abp.WeChat.Official.Infrastructure.Models;

namespace Zony.Abp.WeChat.Official.Services.CustomMenu
{
    /// <summary>
    /// 自定义菜单服务，可以注入本服务实现对自定义菜单的管理操作。
    /// </summary>
    public class CustomMenuService : CommonService
    {
        protected const string TargetUrl = "https://api.weixin.qq.com/cgi-bin/menu/create?";

        public Task<OfficialCommonResponse> CreateCustomMenuAsync(List<CustomButton> customButtons)
        {
            return WeChatOfficialApiRequester.RequestAsync<OfficialCommonResponse>(TargetUrl, HttpMethod.Post,
                new CreateCustomMenuRequest
                {
                    Buttons = customButtons
                });
        }

        public Task DeleteCustomMenuAsync()
        {
            throw new NotImplementedException();
        }

        public Task GetCustomMenuDetailAsync()
        {
            throw new NotImplementedException();
        }
    }
}