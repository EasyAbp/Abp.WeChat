using JetBrains.Annotations;

namespace Zony.Abp.WeChat.Official.Infrastructure.OptionsResolve
{
    public interface IWeChatOfficialOptionsResolver
    {
        [NotNull]
        IWeChatOfficialOptions Resolve();
    }
}