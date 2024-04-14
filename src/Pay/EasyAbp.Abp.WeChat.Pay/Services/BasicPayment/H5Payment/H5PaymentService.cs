using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.H5Payment;

public class H5PaymentService : BasicPaymentService
{
    public const string CreateOrderUrl = "https://api.mch.weixin.qq.com/v3/pay/transactions/h5";

    public H5PaymentService(AbpWeChatPayOptions options,
        IAbpLazyServiceProvider lazyServiceProvider) : base(options,
        lazyServiceProvider)
    {
    }

    public virtual Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request)
    {
        return ApiRequester.RequestAsync<CreateOrderResponse>(HttpMethod.Post, CreateOrderUrl, request);
    }
}