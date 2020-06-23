using System.Xml;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve
{
    public class WeChatPayHandlerContext
    {
        public XmlDocument WeChatRequestXmlData { get; set; }

        public bool IsSuccess { get; set; } = true;

        public string FailedResponse { get; set; } = null;
    }
}