using EasyAbp.Abp.WeChat.Official.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Official.Services.CustomMenu
{
    /// <summary>
    /// 个性化菜单服务，管理微信公众号下个性化菜单的相关接口。
    /// </summary>
    public class PersonalizedMenuWeService : OfficialAbpWeChatServiceBase
    {
        public PersonalizedMenuWeService(AbpWeChatOfficialOptions options, IAbpLazyServiceProvider lazyServiceProvider) : base(options, lazyServiceProvider)
        {
        }
    }
}