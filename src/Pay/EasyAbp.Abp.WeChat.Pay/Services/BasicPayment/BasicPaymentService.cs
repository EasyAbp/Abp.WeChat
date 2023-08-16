using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment;

public abstract class BasicPaymentService : WeChatPayServiceBase, IBasicPaymentService
{
    public string CreateOrderUrl { get; protected set; } = string.Empty;

    public string QueryOrderUrl { get; protected set; } = string.Empty;

    public string CloseOrderUrl { get; protected set; } = string.Empty;

    public string RefundOrderUrl { get; protected set; } = string.Empty;

    public string QueryRefundOrderUrl { get; protected set; } = string.Empty;

    public BasicPaymentService(AbpWeChatPayOptions options,
        IAbpLazyServiceProvider lazyServiceProvider) : base(options,
        lazyServiceProvider)
    {
    }

    public virtual Task<TResponse> CreateOrderAsync<TRequest, TResponse>(TRequest request)
    {
        return ApiRequester.RequestAsync<TResponse>(HttpMethod.Post, CreateOrderUrl, request);
    }
}