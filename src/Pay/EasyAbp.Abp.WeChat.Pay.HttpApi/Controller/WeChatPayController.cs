using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Common;
using EasyAbp.Abp.WeChat.Pay.RequestHandling;
using EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.Abp.WeChat.Pay.Controller
{
    [RemoteService(Name = AbpWeChatRemoteServiceConsts.RemoteServiceName)]
    [Area(AbpWeChatRemoteServiceConsts.ModuleName)]
    [ControllerName("WeChatPay")]
    [Route("/wechat-pay")]
    public class WeChatPayController : AbpControllerBase
    {
        private readonly IWeChatPayEventRequestHandlingService _eventRequestHandlingService;
        private readonly IWeChatPayClientRequestHandlingService _clientRequestHandlingService;

        public WeChatPayController(
            IWeChatPayEventRequestHandlingService eventRequestHandlingService,
            IWeChatPayClientRequestHandlingService clientRequestHandlingService)
        {
            _eventRequestHandlingService = eventRequestHandlingService;
            _clientRequestHandlingService = clientRequestHandlingService;
        }

        /// <summary>
        /// 微信支付模块提供的支付成功通知接口，开发人员需要实现 <see cref="IWeChatPayEventHandler"/> 处理器来处理回调请求。
        /// </summary>
        [HttpPost]
        [Route("notify")]
        public virtual async Task<IActionResult> NotifyAsync([CanBeNull] [FromQuery] string tenantId,
            [CanBeNull] [FromQuery] string mchId)
        {
            using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId!));

            var requestBody = await GetPostDataAsync();
            var result = await _eventRequestHandlingService.PaidNotifyAsync(new PaidNotifyInput
            {
                MchId = mchId,
                RequestBodyString = requestBody,
                RequestBody = JsonSerializer.Deserialize<PaymentNotifyCallbackRequest>(requestBody),
                HttpHeader = new NotifyHttpHeaderModel(Request.Headers["Wechatpay-Serial"],
                    Request.Headers["Wechatpay-TimeStamp"],
                    Request.Headers["Wechatpay-Nonce"],
                    Request.Headers["Wechatpay-Signature"])
            });

            return !result.Success ? NotifyFailure(result.FailureReason) : NotifySuccess();
        }

        /// <summary>
        /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
        /// 见 <see cref="NotifyAsync"/>
        /// </summary>
        [HttpPost]
        [Route("notify/tenant-id/{tenantId}")]
        public virtual Task<IActionResult> Notify2Async([CanBeNull] string tenantId, [CanBeNull] string mchId)
        {
            return NotifyAsync(tenantId, mchId);
        }

        /// <summary>
        /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
        /// 见 <see cref="NotifyAsync"/>
        /// </summary>
        [HttpPost]
        [Route("notify/mch-id/{mchId}")]
        public virtual Task<IActionResult> Notify3Async([CanBeNull] string tenantId, [CanBeNull] string mchId)
        {
            return NotifyAsync(tenantId, mchId);
        }

        /// <summary>
        /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
        /// 见 <see cref="NotifyAsync"/>
        /// </summary>
        [HttpPost]
        [Route("notify/tenant-id/{tenantId}/mch-id/{mchId}")]
        public virtual Task<IActionResult> Notify4Async([CanBeNull] string tenantId, [CanBeNull] string mchId)
        {
            return NotifyAsync(tenantId, mchId);
        }

        /// <summary>
        /// 微信支付模块提供的退款回调接口，开发人员需要实现 <see cref="IWeChatPayEventHandler"/> 处理器来处理回调请求。
        /// </summary>
        [HttpPost]
        [Route("refund-notify")]
        public virtual async Task<IActionResult> RefundNotifyAsync([CanBeNull] string tenantId, [CanBeNull] string mchId)
        {
            using var changeTenant = CurrentTenant.Change(tenantId.IsNullOrWhiteSpace() ? null : Guid.Parse(tenantId!));
            var requestBody = await GetPostDataAsync();

            var result = await _eventRequestHandlingService.RefundNotifyAsync(new RefundNotifyInput
            {
                MchId = mchId,
                Xml = await GetPostDataAsync()
            });
            
            return !result.Success ? NotifyFailure(result.FailureReason) : NotifySuccess();
        }

        /// <summary>
        /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
        /// 见 <see cref="RefundNotifyAsync"/>
        /// </summary>
        [HttpPost]
        [Route("refund-notify/tenant-id/{tenantId}")]
        public virtual Task<IActionResult> RefundNotify2Async([CanBeNull] string tenantId, [CanBeNull] string mchId)
        {
            return RefundNotifyAsync(tenantId, mchId);
        }

        /// <summary>
        /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
        /// 见 <see cref="RefundNotifyAsync"/>
        /// </summary>
        [HttpPost]
        [Route("refund-notify/mch-id/{mchId}")]
        public virtual Task<IActionResult> RefundNotify3Async([CanBeNull] string tenantId, [CanBeNull] string mchId)
        {
            return RefundNotifyAsync(tenantId, mchId);
        }

        /// <summary>
        /// 本方法是为了避免多 Route 导致 ABP ApiDescription 报 Warning。
        /// 见 <see cref="RefundNotifyAsync"/>
        /// </summary>
        [HttpPost]
        [Route("refund-notify/tenant-id/{tenantId}/mch-id/{mchId}")]
        public virtual Task<IActionResult> RefundNotify4Async([CanBeNull] string tenantId, [CanBeNull] string mchId)
        {
            return RefundNotifyAsync(tenantId, mchId);
        }

        /// <summary>
        /// 根据统一下单接口返回的预支付 Id 生成支付签名。
        /// </summary>
        /// <param name="appId">AppId</param>
        /// <param name="prepayId">预支付 Id。</param>
        /// <param name="mchId">商户 Id</param>
        [HttpGet]
        [Route("js-sdk-config-parameters")]
        public virtual async Task<IActionResult> GetJsSdkWeChatPayParametersAsync(string mchId,
            [FromQuery] string appId, string prepayId)
        {
            var result = await _clientRequestHandlingService.GetJsSdkWeChatPayParametersAsync(
                new GetJsSdkWeChatPayParametersInput
                {
                    MchId = mchId,
                    AppId = appId,
                    PrepayId = prepayId
                });

            return new JsonResult(new
            {
                nonceStr = result.NonceStr,
                timeStamp = result.TimeStamp,
                package = result.Package,
                signType = result.SignType,
                paySign = result.PaySign
            });
        }

        #region > Utilities methods <

        private IActionResult NotifySuccess()
        {
            return Ok(new PaymentNotifyCallbackResponse
            {
                Code = "SUCCESS"
            });
        }

        private IActionResult NotifyFailure(string message)
        {
            return BadRequest(new PaymentNotifyCallbackResponse
            {
                Code = "FAIL",
                Message = message
            });
        }

        protected virtual async Task<string> GetPostDataAsync()
        {
            Request.EnableBuffering();

            using var streamReader = new StreamReader(Request.Body);

            var postData = await streamReader.ReadToEndAsync();

            Request.Body.Position = 0;

            return postData;
        }

        #endregion
    }
}