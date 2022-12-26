using System;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.Abp.WeChat.Official.Controllers
{
    [RemoteService(Name = AbpWeChatRemoteServiceConsts.RemoteServiceName)]
    [Area(AbpWeChatRemoteServiceConsts.ModuleName)]
    [ControllerName("WeChat")]
    [Route("/wechat")]
    public class WeChatController : AbpControllerBase
    {
        private readonly IWeChatOfficialEventRequestHandlingService _eventRequestHandlingService;
        private readonly IWeChatOfficialClientRequestHandlingService _clientRequestHandlingService;

        public WeChatController(
            IWeChatOfficialEventRequestHandlingService eventRequestHandlingService,
            IWeChatOfficialClientRequestHandlingService clientRequestHandlingService)
        {
            _eventRequestHandlingService = eventRequestHandlingService;
            _clientRequestHandlingService = clientRequestHandlingService;
        }

        [HttpGet]
        [Route("verify")]
        public virtual async Task<string> VerifyAsync(
            VerifyRequestDto input, [CanBeNull] string tenantId, [CanBeNull] string appId)
        {
            using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId!));

            var result = await _eventRequestHandlingService.VerifyAsync(input, appId);

            return result.Success ? result.Value : result.FailureReason;
        }

        /// <summary>
        /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
        /// 见 <see cref="VerifyAsync"/>
        /// </summary>
        [HttpGet]
        [Route("verify/tenant-id/{tenantId}")]
        public virtual Task<string> Verify2Async(
            VerifyRequestDto input, [CanBeNull] string tenantId, [CanBeNull] string appId)
        {
            return VerifyAsync(input, tenantId, appId);
        }

        /// <summary>
        /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
        /// 见 <see cref="VerifyAsync"/>
        /// </summary>
        [HttpGet]
        [Route("verify/app-id/{appId}")]
        public virtual Task<string> Verify3Async(
            VerifyRequestDto input, [CanBeNull] string tenantId, [CanBeNull] string appId)
        {
            return VerifyAsync(input, tenantId, appId);
        }

        /// <summary>
        /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
        /// 见 <see cref="VerifyAsync"/>
        /// </summary>
        [HttpGet]
        [Route("verify/tenant-id/{tenantId}/app-id/{appId}")]
        public virtual Task<string> Verify4Async(
            VerifyRequestDto input, [CanBeNull] string tenantId, [CanBeNull] string appId)
        {
            return VerifyAsync(input, tenantId, appId);
        }

        [HttpGet]
        [Route("redirect-url")]
        public virtual async Task<ActionResult> RedirectUrlAsync(
            RedirectUrlRequest input, [CanBeNull] string tenantId, [CanBeNull] string appId)
        {
            if (input == null || input.Code.IsNullOrWhiteSpace())
            {
                return BadRequest();
            }

            using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId!));

            var result = await _eventRequestHandlingService.GetOAuthRedirectUrlAsync(input, appId);

            if (!result.Success)
            {
                return BadRequest();
            }

            return Redirect(result.Value);
        }

        /// <summary>
        /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
        /// 见 <see cref="RedirectUrlAsync"/>
        /// </summary>
        [HttpGet]
        [Route("redirect-url/tenant-id/{tenantId}")]
        public virtual Task<ActionResult> RedirectUrl2Async(
            RedirectUrlRequest input, [CanBeNull] string tenantId, [CanBeNull] string appId)
        {
            return RedirectUrlAsync(input, tenantId, appId);
        }

        /// <summary>
        /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
        /// 见 <see cref="RedirectUrlAsync"/>
        /// </summary>
        [HttpGet]
        [Route("redirect-url/app-id/{appId}")]
        public virtual Task<ActionResult> RedirectUrl3Async(
            RedirectUrlRequest input, [CanBeNull] string tenantId, [CanBeNull] string appId)
        {
            return RedirectUrlAsync(input, tenantId, appId);
        }

        /// <summary>
        /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
        /// 见 <see cref="RedirectUrlAsync"/>
        /// </summary>
        [HttpGet]
        [Route("redirect-url/tenant-id/{tenantId}/app-id/{appId}")]
        public virtual Task<ActionResult> RedirectUrl4Async(
            RedirectUrlRequest input, [CanBeNull] string tenantId, [CanBeNull] string appId)
        {
            return RedirectUrlAsync(input, tenantId, appId);
        }

        [HttpGet]
        [Route("access-token-by-code")]
        public virtual async Task<ActionResult> GetAccessTokenByCodeAsync(
            [FromQuery] string code, [CanBeNull] string appId)
        {
            var result = await _clientRequestHandlingService.GetAccessTokenByCodeAsync(code, appId);

            return new JsonResult(new
            {
                errmsg = result.ErrorMessage,
                errcode = result.ErrorCode,
                access_token = result.AccessToken,
                scope = result.Scope,
                expires_in = result.ExpiresIn,
                openid = result.OpenId,
                refresh_token = result.RefreshToken
            });
        }

        [HttpGet]
        [Route("js-sdk-config-parameters")]
        public virtual async Task<ActionResult> GetJsSdkConfigParametersAsync(
            [FromQuery] string jsUrl, [CanBeNull] string appId)
        {
            if (string.IsNullOrEmpty(jsUrl)) throw new UserFriendlyException("需要计算的 JS URL 参数不能够为空。");

            var result = await _clientRequestHandlingService.GetJsSdkConfigParametersAsync(jsUrl, appId);

            return new JsonResult(new
            {
                appid = result.AppId,
                noncestr = result.NonceStr,
                timestamp = result.TimeStamp,
                signature = result.SignStr,
                jsapi_ticket = result.Ticket
            });
        }
    }
}