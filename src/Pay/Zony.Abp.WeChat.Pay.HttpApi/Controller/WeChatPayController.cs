using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Zony.Abp.WeChat.Pay.Infrastructure;

namespace Zony.Abp.WeChat.Pay.HttpApi.Controller
{
    [RemoteService]
    [ControllerName("WeChatPay")]
    [Route("/WeChatPay")]
    public class WeChatPayController : AbpController
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WeChatPayController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [Route("Notify")]
        public virtual ActionResult Notify()
        {
            var handlers = ServiceProvider.GetServices<IWeChatPayHandler>();

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(_httpContextAccessor.HttpContext.Request.Body);

            foreach (var handler in handlers)
            {
                handler.HandleAsync(xmlDocument);
            }
            
            return Ok(BuildSuccessXml());
        }

        private string BuildSuccessXml()
        {
            return @"<xml><return_code><![CDATA[SUCCESS]]></return_code>
                      <return_msg><![CDATA[OK]]></return_msg>
                 </xml>";
        }
    }
}