using System.Threading.Tasks;

namespace EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve
{
    public interface IWeChatPayOptionResolveContributor
    {
        string Name { get; }

        Task ResolveAsync(WeChatPayOptionsResolverContext context);
    }
}