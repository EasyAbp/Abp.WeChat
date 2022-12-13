using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Official.Infrastructure.OptionsResolve;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Official.HttpApi;

public interface IHttpApiWeChatOfficialOptionsProvider
{
    Task<IWeChatOfficialOptions> GetAsync([CanBeNull] string appId);
}