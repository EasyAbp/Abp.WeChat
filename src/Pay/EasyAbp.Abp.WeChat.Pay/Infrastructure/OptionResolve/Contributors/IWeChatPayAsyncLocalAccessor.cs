namespace EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve.Contributors
{
    public interface IWeChatPayAsyncLocalAccessor
    {
        IWeChatPayOptions Current { get; set; }
    }
}