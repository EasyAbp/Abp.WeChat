using EasyAbp.Abp.WeChat.MiniProgram.Models;

namespace EasyAbp.Abp.WeChat.MiniProgram.Services.SubscribeMessage
{
    public class SendSubscribeMessageResponse : IMiniProgramResponse
    {
        public string ErrorMessage { get; set; }
        
        public int ErrorCode { get; set; }
    }
}