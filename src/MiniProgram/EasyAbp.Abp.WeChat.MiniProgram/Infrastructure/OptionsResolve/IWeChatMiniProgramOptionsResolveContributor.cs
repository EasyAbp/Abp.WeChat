namespace EasyAbp.Abp.WeChat.MiniProgram.Infrastructure.OptionsResolve
{
    public interface IWeChatMiniProgramOptionsResolveContributor
    {
        string Name { get; }

        void Resolve(WeChatMiniProgramResolveContext context);
    }
}