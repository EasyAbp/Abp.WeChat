using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Xml;
using EasyAbp.Abp.WeChat.Common.Infrastructure;
using EasyAbp.Abp.WeChat.Common.Infrastructure.Signature;
using EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve;
using Microsoft.Extensions.Logging;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure.Handlers
{
    public class SignVerifyHandler : IWeChatPayHandler
    {
        protected readonly IWeChatPayOptionsResolver WeChatPayOptionsResolver;
        protected readonly ISignatureGenerator SignatureGenerator;
        protected readonly ILogger<SignVerifyHandler> Logger;

        public SignVerifyHandler(IWeChatPayOptionsResolver weChatPayOptionsResolver,
            ISignatureGenerator signatureGenerator,
            ILogger<SignVerifyHandler> logger)
        {
            WeChatPayOptionsResolver = weChatPayOptionsResolver;
            SignatureGenerator = signatureGenerator;
            Logger = logger;
        }

        public async Task HandleAsync(WeChatPayHandlerContext context)
        {
            var parameters = new WeChatParameters();

            var nodes = context.WeChatRequestXmlData.SelectSingleNode("/xml")?.ChildNodes;
            if (nodes == null)
            {
                return;
            }

            foreach (XmlNode node in nodes)
            {
                if (node.Name == "sign")
                {
                    continue;
                }
                
                parameters.AddParameter(node.Name, node.InnerText);
            }

            var options = await WeChatPayOptionsResolver.ResolveAsync();
            var responseSign = SignatureGenerator.Generate(parameters, MD5.Create(), options.ApiKey);

            if (responseSign != context.WeChatRequestXmlData.SelectSingleNode("/xml/sign")?.InnerText)
            {
                context.IsSuccess = false;
                context.FailedResponse = "订单签名验证没有通过";
                Logger.LogWarning("订单签名验证没有通过。");
            }
        }
    }
}