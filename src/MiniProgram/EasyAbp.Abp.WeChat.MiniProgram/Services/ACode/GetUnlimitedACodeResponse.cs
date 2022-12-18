using EasyAbp.Abp.WeChat.MiniProgram.Models;

namespace EasyAbp.Abp.WeChat.MiniProgram.Services.ACode
{
    public class GetUnlimitedACodeResponse : IMiniProgramResponse, IHasBinaryData
    {
        public string ErrorMessage { get; set; }
        
        public int ErrorCode { get; set; }
        
        public byte[] BinaryData { get; set; }
    }
}