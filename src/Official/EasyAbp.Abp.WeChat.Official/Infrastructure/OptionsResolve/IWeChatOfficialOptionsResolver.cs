using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve
{
    public interface IWeChatOfficialOptionsResolver
    {
        [NotNull]
        IWeChatOfficialOptions Resolve();
    }
}