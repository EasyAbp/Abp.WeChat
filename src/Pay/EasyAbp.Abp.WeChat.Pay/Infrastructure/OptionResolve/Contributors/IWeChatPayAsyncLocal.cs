using System;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve.Contributors
{
    public interface IWeChatPayAsyncLocal
    {
        IWeChatPayOptions CurrentOptions { get; }

        IDisposable Change(IWeChatPayOptions weChatPayOptions);
    }
}