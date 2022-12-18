using EasyAbp.Abp.WeChat.Common.Infrastructure.Services;
using EasyAbp.Abp.WeChat.MiniProgram.ApiRequests;
using EasyAbp.Abp.WeChat.MiniProgram.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.MiniProgram.Services;

public abstract class MiniProgramAbpWeChatServiceBase :
    AbpWeChatServiceBase<AbpWeChatMiniProgramOptions, IWeChatMiniProgramApiRequester>
{
    protected MiniProgramAbpWeChatServiceBase(AbpWeChatMiniProgramOptions options, IAbpLazyServiceProvider lazyServiceProvider) : base(options, lazyServiceProvider)
    {
    }
}