namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.Models
{
    public interface IHasBinaryData
    {
        byte[] BinaryData { get; set; }
    }
}