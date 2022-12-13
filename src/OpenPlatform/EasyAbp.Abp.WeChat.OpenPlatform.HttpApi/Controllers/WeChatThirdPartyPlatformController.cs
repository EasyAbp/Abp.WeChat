using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Encryption;
using EasyAbp.Abp.WeChat.Common.Models;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.Models.ThirdPartyPlatform;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.OptionsResolve;
using EasyAbp.Abp.WeChat.OpenPlatform.Infrastructure.ThirdPartyPlatform.OptionsResolve.Contributors;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Controllers;

[Area(AbpWeChatRemoteServiceConsts.ModuleName)]
[ControllerName("WeChatThirdPartyPlatform")]
[Route("/wechat/third-party-platform")]
public class WeChatThirdPartyPlatformController : AbpControllerBase
{
    private readonly IWeChatThirdPartyPlatformAsyncLocal _weChatThirdPartyPlatformAsyncLocal;
    private readonly IWeChatNotificationEncryptor _weChatNotificationEncryptor;
    private readonly IWeChatThirdPartyPlatformOptionsResolver _optionsResolver;

    public WeChatThirdPartyPlatformController(
        IWeChatThirdPartyPlatformAsyncLocal weChatThirdPartyPlatformAsyncLocal,
        IWeChatNotificationEncryptor weChatNotificationEncryptor,
        IWeChatThirdPartyPlatformOptionsResolver optionsResolver)
    {
        _weChatThirdPartyPlatformAsyncLocal = weChatThirdPartyPlatformAsyncLocal;
        _weChatNotificationEncryptor = weChatNotificationEncryptor;
        _optionsResolver = optionsResolver;
    }

    /// <summary>
    /// 授权事件通知接口，开发人员需要实现 <see cref="IWeChatThirdPartyPlatformAuthEventHandler"/> 处理器来处理回调请求。
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/ThirdParty/token/component_verify_ticket.html
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/Before_Develop/authorize_event.html#infotype-%E8%AF%B4%E6%98%8E
    /// </summary>
    [HttpPost]
    [Route("notify/auth")]
    [Route("notify/auth/tenant-id/{tenantId}")]
    public virtual async Task<ActionResult> NotifyAuthAsync([CanBeNull] string tenantId)
    {
        using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId));

        var handlers = LazyServiceProvider.LazyGetService<IEnumerable<IWeChatThirdPartyPlatformAuthEventHandler>>();

        Request.EnableBuffering();
        using (var streamReader = new StreamReader(HttpContext.Request.Body))
        {
            var model = await DecryptMsgAsync<AuthNotificationModel>(await streamReader.ReadToEndAsync());
            var context = new WeChatThirdPartyPlatformAuthEventHandlerContext
            {
                Model = model
            };

            // 如果你拥有不止一个第三方平台，请务必实现 IHttpApiWeChatThirdPartyOptionsProvider
            var options = await ResolveOptionsAsync(model.AppId);
            using var changeOptions = _weChatThirdPartyPlatformAsyncLocal.Change(options);

            foreach (var handler in handlers.Where(x => x.InfoType == model.InfoType))
            {
                await handler.HandleAsync(context);

                if (!context.IsSuccess)
                {
                    return BadRequest();
                }
            }

            Request.Body.Position = 0;
        }

        return Ok("success");
    }

    /// <summary>
    /// 微信应用事件通知接口，开发人员需要实现 <see cref="IWeChatThirdPartyPlatformAppEventHandler"/> 处理器来处理回调请求。
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/operation/thirdparty/prepare.html
    /// </summary>
    [HttpPost]
    [Route("notify/app/component-app-id/{componentAppId}/app-id/{appId}")]
    [Route("notify/app/tenant-id/{tenantId}/app-id/{appId}")]
    [Route("notify/app/tenant-id/{tenantId}/component-app-id/{componentAppId}/app-id/{appId}")]
    public virtual async Task<ActionResult> NotifyAppAsync(
        [CanBeNull] string tenantId, [CanBeNull] string componentAppId, [NotNull] string appId)
    {
        using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId));

        // 如果你拥有不止一个第三方平台，请务必实现 IHttpApiWeChatThirdPartyOptionsProvider
        var options = await ResolveOptionsAsync(componentAppId);

        using var changeOptions = _weChatThirdPartyPlatformAsyncLocal.Change(options);

        var handlers = LazyServiceProvider.LazyGetService<IEnumerable<IWeChatThirdPartyPlatformAppEventHandler>>();

        Request.EnableBuffering();
        using (var streamReader = new StreamReader(HttpContext.Request.Body))
        {
            var model = await DecryptMsgAsync<WeChatAppNotificationModel>(await streamReader.ReadToEndAsync());
            var context = new WeChatThirdPartyPlatformAppEventHandlerContext
            {
                ComponentAppId = componentAppId,
                AuthorizerAppId = appId,
                Model = model
            };

            foreach (var handler in handlers.Where(x => x.Event == context.Model.Event))
            {
                await handler.HandleAsync(context);

                if (!context.IsSuccess)
                {
                    return BadRequest();
                }
            }

            Request.Body.Position = 0;
        }

        return Ok("success");
    }

    protected virtual async Task<T> DecryptMsgAsync<T>(string postData) where T : ExtensibleObject, new()
    {
        var options = await _optionsResolver.ResolveAsync();

        return await _weChatNotificationEncryptor.DecryptPostDataAsync<T>(
            options.Token,
            options.EncodingAesKey,
            options.AppId,
            HttpContext.Request.Query["msg_signature"].FirstOrDefault(),
            HttpContext.Request.Query["timestamp"].FirstOrDefault(),
            HttpContext.Request.Query["nonce"].FirstOrDefault(),
            postData);
    }

    protected virtual async Task<IWeChatThirdPartyPlatformOptions> ResolveOptionsAsync(string componentAppId)
    {
        var options = await _optionsResolver.ResolveAsync();

        if (componentAppId.IsNullOrWhiteSpace() || componentAppId == options.AppId)
        {
            return options;
        }

        var provider = LazyServiceProvider.LazyGetRequiredService<IHttpApiWeChatThirdPartyPlatformOptionsProvider>();

        return await provider.GetAsync(componentAppId);
    }
}