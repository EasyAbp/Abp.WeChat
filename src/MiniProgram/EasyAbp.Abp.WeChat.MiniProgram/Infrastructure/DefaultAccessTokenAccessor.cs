using System;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;
using EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure
{
    public class DefaultAccessTokenAccessor : IAccessTokenAccessor, ISingletonDependency
    {
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly IWeChatMiniProgramOptionsResolver _weChatMiniProgramOptionsResolver;

        public DefaultAccessTokenAccessor(
            IAccessTokenProvider accessTokenProvider,
            IWeChatMiniProgramOptionsResolver weChatMiniProgramOptionsResolver)
        {
            _accessTokenProvider = accessTokenProvider;
            _weChatMiniProgramOptionsResolver = weChatMiniProgramOptionsResolver;
        }

        public virtual async Task<string> GetAccessTokenAsync()
        {
            var options = await _weChatMiniProgramOptionsResolver.ResolveAsync();

            return await _accessTokenProvider.GetAccessTokenAsync(options.AppId, options.AppSecret);
        }
    }
}