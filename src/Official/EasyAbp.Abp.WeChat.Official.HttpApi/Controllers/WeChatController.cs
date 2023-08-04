using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.Common.RequestHandling.Dtos;
using EasyAbp.Abp.WeChat.Official.RequestHandling;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
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

        [Obsolete("请使用统一的Notify接口")]
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
        [Obsolete("请使用统一的Notify接口")]
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
        [Obsolete("请使用统一的Notify接口")]
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
        [Obsolete("请使用统一的Notify接口")]
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

        /// <summary>
        /// 微信应用事件通知接口，开发人员需要实现 <see cref="IWeChatOfficialAppEventHandler"/> 处理器来处理回调请求。
        /// </summary>
        [HttpPost]
        [Route("notify")]
        public virtual async Task<ActionResult> NotifyAsync([CanBeNull] string tenantId, [CanBeNull] string appId)
        {
            using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId!));

            var model = await CreateRequestModelAsync();

            if (model is null)
            {
                return BadRequest();
            }

            var result = await _eventRequestHandlingService.NotifyAsync(model, appId);

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
        /// 见 <see cref="NotifyAsync"/>
        /// </summary>
        [HttpGet]
        [Route("notify")]
        public virtual Task<ActionResult> NotifyGetAsync([CanBeNull] string tenantId, [NotNull] string appId)
        {
            return NotifyAsync(tenantId, appId);
        }

        /// <summary>
        /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
        /// 见 <see cref="NotifyAsync"/>
        /// </summary>
        [HttpPost]
        [Route("notify/tenant-id/{tenantId}")]
        public virtual Task<ActionResult> Notify2Async([CanBeNull] string tenantId, [NotNull] string appId)
        {
            return NotifyAsync(tenantId, appId);
        }

        /// <summary>
        /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
        /// 见 <see cref="NotifyAsync"/>
        /// </summary>
        [HttpGet]
        [Route("notify/tenant-id/{tenantId}")]
        public virtual Task<ActionResult> Notify2GetAsync([CanBeNull] string tenantId, [NotNull] string appId)
        {
            return Notify2Async(tenantId, appId);
        }

        /// <summary>
        /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
        /// 见 <see cref="NotifyAsync"/>
        /// </summary>
        [HttpPost]
        [Route("notify/app-id/{appId}")]
        public virtual Task<ActionResult> Notify3Async([CanBeNull] string tenantId, [NotNull] string appId)
        {
            return NotifyAsync(tenantId, appId);
        }

        /// <summary>
        /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
        /// 见 <see cref="NotifyAsync"/>
        /// </summary>
        [HttpGet]
        [Route("notify/app-id/{appId}")]
        public virtual Task<ActionResult> Notify3GetAsync([CanBeNull] string tenantId, [NotNull] string appId)
        {
            return Notify3Async(tenantId, appId);
        }

        /// <summary>
        /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
        /// 见 <see cref="NotifyAsync"/>
        /// </summary>
        [HttpPost]
        [Route("notify/tenant-id/{tenantId}/app-id/{appId}")]
        public virtual Task<ActionResult> Notify4Async([CanBeNull] string tenantId, [NotNull] string appId)
        {
            return NotifyAsync(tenantId, appId);
        }

        /// <summary>
        /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
        /// 见 <see cref="NotifyAsync"/>
        /// </summary>
        [HttpGet]
        [Route("notify/tenant-id/{tenantId}/app-id/{appId}")]
        public virtual Task<ActionResult> Notify4GetAsync([CanBeNull] string tenantId, [NotNull] string appId)
        {
            return Notify4Async(tenantId, appId);
        }

        [ItemCanBeNull]
        protected virtual async Task<WeChatOfficialEventRequestModel> CreateRequestModelAsync()
        {
            var echostr = Request.Query["echostr"].FirstOrDefault();

            if (!echostr.IsNullOrWhiteSpace() && Request.Method != "GET")
            {
                return null;
            }

            Request.EnableBuffering();

            using var streamReader = new StreamReader(Request.Body);

            var postData = await streamReader.ReadToEndAsync();

            Request.Body.Position = 0;

            if (!postData.IsNullOrWhiteSpace() && Request.Method != "POST")
            {
                return null;
            }

            return new WeChatOfficialEventRequestModel
            {
                PostData = postData,
                MsgSignature = Request.Query["msg_signature"].FirstOrDefault() ??
                               Request.Query["signature"].FirstOrDefault(),
                Timestamp = Request.Query["timestamp"].FirstOrDefault(),
                Nonce = Request.Query["nonce"].FirstOrDefault(),
                EchoStr = echostr
            };
        }
    }
}