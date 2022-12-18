using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.Official.ApiRequests;
using EasyAbp.Abp.WeChat.Official.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Official.Services;

public abstract class OfficialAbpWeChatServiceBase :
    AbpWeChatServiceBase<AbpWeChatOfficialOptions, IWeChatOfficialApiRequester>
{
    protected OfficialAbpWeChatServiceBase(AbpWeChatOfficialOptions options, IAbpLazyServiceProvider lazyServiceProvider) : base(options, lazyServiceProvider)
    {
    }
}