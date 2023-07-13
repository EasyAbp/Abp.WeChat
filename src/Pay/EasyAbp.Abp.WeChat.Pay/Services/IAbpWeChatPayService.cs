namespace EasyAbp.Abp.WeChat.Pay.Services;

/// <summary>
/// 请使用 <see cref="IAbpWeChatPayServiceFactory"/> 创建本实例。
/// </summary>
public interface IAbpWeChatPayService
{
    string MchId { get; }
}