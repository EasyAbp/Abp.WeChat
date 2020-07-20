using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json.Linq;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.Common.Infrastructure.AccessToken;
using EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve;

namespace EasyAbp.Abp.WeChat.Official.Infrastructure
{
    public class DefaultAccessTokenAccessor : IAccessTokenAccessor, ISingletonDependency
    {
        private readonly IAccessTokenProvider _accessTokenProvider;
        private readonly IWeChatOfficialOptionsResolver _weChatOfficialOptionsResolver;

        public DefaultAccessTokenAccessor(
            IAccessTokenProvider accessTokenProvider,
            IWeChatOfficialOptionsResolver weChatOfficialOptionsResolver)
        {
            _accessTokenProvider = accessTokenProvider;
            _weChatOfficialOptionsResolver = weChatOfficialOptionsResolver;
        }

        public virtual async Task<string> GetAccessTokenAsync()
        {
            var options = _weChatOfficialOptionsResolver.Resolve();

            return await _accessTokenProvider.GetAccessTokenAsync(options.AppId, options.AppSecret);
        }
    }
}