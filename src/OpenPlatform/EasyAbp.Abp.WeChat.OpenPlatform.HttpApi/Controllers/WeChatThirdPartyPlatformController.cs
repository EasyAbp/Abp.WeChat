using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling;
using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling.Dtos;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.Abp.WeChat.OpenPlatform.Controllers;

[Area(AbpWeChatRemoteServiceConsts.ModuleName)]
[ControllerName("WeChatThirdPartyPlatform")]
[Route("/wechat/third-party-platform")]
public class WeChatThirdPartyPlatformController : AbpControllerBase
{
    private readonly IWeChatThirdPartyPlatformEventRequestHandlingService _requestHandlingService;

    public WeChatThirdPartyPlatformController(
        IWeChatThirdPartyPlatformEventRequestHandlingService requestHandlingService)
    {
        _requestHandlingService = requestHandlingService;
    }

    /// <summary>
    /// 授权事件通知接口，开发人员需要实现 <see cref="IWeChatThirdPartyPlatformAuthEventHandler"/> 处理器来处理回调请求。
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/ThirdParty/token/component_verify_ticket.html
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/api/Before_Develop/authorize_event.html#infotype-%E8%AF%B4%E6%98%8E
    /// </summary>
    [HttpPost]
    [Route("notify/auth")]
    public virtual async Task<ActionResult> NotifyAuthAsync(
        [CanBeNull] string tenantId, [CanBeNull] string componentAppId)
    {
        using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId!));

        var result = await _requestHandlingService.NotifyAuthAsync(new NotifyAuthInput
        {
            ComponentAppId = componentAppId,
            EventRequest = await CreateRequestModelAsync()
        });

        if (!result.Success)
        {
            return BadRequest();
        }

        return Ok("success");
    }

    /// <summary>
    /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
    /// 见 <see cref="NotifyAuthAsync"/>
    /// </summary>
    [HttpPost]
    [Route("notify/auth/component-app-id/{componentAppId}")]
    public virtual Task<ActionResult> NotifyAuth2Async([CanBeNull] string tenantId, [CanBeNull] string componentAppId)
    {
        return NotifyAuthAsync(tenantId, componentAppId);
    }

    /// <summary>
    /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
    /// 见 <see cref="NotifyAuthAsync"/>
    /// </summary>
    [HttpPost]
    [Route("notify/auth/tenant-id/{tenantId}/component-app-id/{componentAppId}")]
    public virtual Task<ActionResult> NotifyAuth3Async([CanBeNull] string tenantId, [CanBeNull] string componentAppId)
    {
        return NotifyAuthAsync(tenantId, componentAppId);
    }

    /// <summary>
    /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
    /// 见 <see cref="NotifyAuthAsync"/>
    /// </summary>
    [HttpPost]
    [Route("notify/auth/tenant-id/{tenantId}")]
    public virtual Task<ActionResult> NotifyAuth4Async([CanBeNull] string tenantId, [CanBeNull] string componentAppId)
    {
        return NotifyAuthAsync(tenantId, componentAppId);
    }

    /// <summary>
    /// 微信应用事件通知接口，开发人员需要实现 <see cref="IWeChatThirdPartyPlatformAppEventHandler"/> 处理器来处理回调请求。
    /// https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/operation/thirdparty/prepare.html
    /// </summary>
    [HttpPost]
    [Route("notify/app/app-id/{appId}")]
    public virtual async Task<ActionResult> NotifyAppAsync(
        [CanBeNull] string tenantId, [CanBeNull] string componentAppId, [NotNull] string appId)
    {
        using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId!));

        var result =
            await _requestHandlingService.NotifyAppAsync(new NotifyAppInput
            {
                ComponentAppId = componentAppId,
                AuthorizerAppId = appId,
                EventRequest = await CreateRequestModelAsync()
            });

        if (!result.Success)
        {
            return BadRequest();
        }

        var contentType = new MediaTypeHeaderValue(result.ResponseToWeChatModel switch
        {
            JsonResponseToWeChatModel => "application/json",
            XmlResponseToWeChatModel => "application/xml",
            null => "text/plain",
            _ => "text/plain"
        })
        {
            Charset = Encoding.UTF8.WebName
        };

        return new ContentResult
        {
            ContentType = contentType.ToString(),
            Content = result.ResponseToWeChatModel?.Content ?? "success",
            StatusCode = 200
        };
    }

    /// <summary>
    /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
    /// 见 <see cref="NotifyAppAsync"/>
    /// </summary>
    [HttpPost]
    [Route("notify/app/tenant-id/{tenantId}/app-id/{appId}")]
    public virtual Task<ActionResult> NotifyApp2Async(
        [CanBeNull] string tenantId, [CanBeNull] string componentAppId, [NotNull] string appId)
    {
        return NotifyAppAsync(tenantId, componentAppId, appId);
    }

    /// <summary>
    /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
    /// 见 <see cref="NotifyAppAsync"/>
    /// </summary>
    [HttpPost]
    [Route("notify/app/tenant-id/{tenantId}/component-app-id/{componentAppId}/app-id/{appId}")]
    public virtual Task<ActionResult> NotifyApp3Async(
        [CanBeNull] string tenantId, [CanBeNull] string componentAppId, [NotNull] string appId)
    {
        return NotifyAppAsync(tenantId, componentAppId, appId);
    }

    /// <summary>
    /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
    /// 见 <see cref="NotifyAppAsync"/>
    /// </summary>
    [HttpPost]
    [Route("notify/app/component-app-id/{componentAppId}/app-id/{appId}")]
    public virtual Task<ActionResult> NotifyApp4Async(
        [CanBeNull] string tenantId, [CanBeNull] string componentAppId, [NotNull] string appId)
    {
        return NotifyAppAsync(tenantId, componentAppId, appId);
    }
    
    protected virtual async Task<WeChatEventRequestModel> CreateRequestModelAsync()
    {
        Request.EnableBuffering();

        using var streamReader = new StreamReader(Request.Body);

        var postData = await streamReader.ReadToEndAsync();

        Request.Body.Position = 0;

        return new WeChatEventRequestModel
        {
            PostData = postData,
            MsgSignature = Request.Query["msg_signature"].FirstOrDefault(),
            Timestamp = Request.Query["timestamp"].FirstOrDefault(),
            Notice = Request.Query["nonce"].FirstOrDefault()
        };
    }
}