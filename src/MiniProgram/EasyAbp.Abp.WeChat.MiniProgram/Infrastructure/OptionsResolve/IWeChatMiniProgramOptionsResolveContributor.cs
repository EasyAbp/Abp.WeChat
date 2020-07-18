using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve
{
    public interface IWeChatMiniProgramOptionsResolveContributor
    {
        string Name { get; }

        Task ResolveAsync(WeChatMiniProgramOptionsResolveContext context);
    }
}