using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve
{
    public interface IWeChatPayOptionsResolveContributor
    {
        string Name { get; }

        Task ResolveAsync(WeChatPayOptionsResolverContext context);
    }
}