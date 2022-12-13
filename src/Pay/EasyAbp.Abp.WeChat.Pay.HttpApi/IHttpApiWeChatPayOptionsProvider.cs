using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Pay.HttpApi;

public interface IHttpApiWeChatPayOptionsProvider
{
    Task<IWeChatPayOptions> GetAsync([CanBeNull] string appId);
}