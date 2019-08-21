using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Zony.Abp.WeiXin.Official.HttpApi.Models;
using Zony.Abp.WeiXin.Official.Infrastructure;

namespace Zony.Abp.WeiXin.Official.HttpApi.Controllers
{
    [RemoteService]
    [ControllerName("WeiXin")]
    [Route("/weixin")]
    public class WeiXinController : AbpController
    {
        private readonly AbpWeiXinOfficialOptions _officialOptions;
        private readonly SignatureChecker _signatureChecker;

        public WeiXinController(SignatureChecker signatureChecker, IOptions<AbpWeiXinOfficialOptions> officialOptions)
        {
            _signatureChecker = signatureChecker;
            _officialOptions = officialOptions.Value;
        }

        [HttpGet]
        [Route("verify")]
        public virtual Task<string> Verify(VerifyRequestDto input)
        {
            if (_signatureChecker.Validate(_officialOptions.Token, input.Timestamp, input.Nonce, input.Signature))
            {
                return Task.FromResult(input.EchoStr);
            }

            return Task.FromResult("非法参数。");
        }
    }
}