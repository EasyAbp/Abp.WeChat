namespace EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve
{
    public interface IWeChatOfficialOptionsResolveContributor
    {
        string Name { get; }

        void Resolve(WeChatOfficialResolveContext context);
    }
}