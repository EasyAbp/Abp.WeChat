using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve
{
    public interface IWeChatMiniProgramOptionsResolver
    {
        Task<IWeChatMiniProgramOptions> ResolveAsync();
    }
}