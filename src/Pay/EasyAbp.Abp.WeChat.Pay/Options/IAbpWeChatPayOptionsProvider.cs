using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.WeChat.Pay.Options;

public interface IAbpWeChatPayOptionsProvider
{
    Task<AbpWeChatPayOptions> GetAsync([CanBeNull] string mchId);
}