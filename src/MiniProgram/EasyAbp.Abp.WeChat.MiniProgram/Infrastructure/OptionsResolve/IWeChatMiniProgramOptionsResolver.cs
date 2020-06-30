using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve
{
    public interface IWeChatMiniProgramOptionsResolver
    {
        [NotNull]
        IWeChatMiniProgramOptions Resolve();
    }
}