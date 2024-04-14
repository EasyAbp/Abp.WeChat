using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;
using Volo.Abp.DependencyInjection;
using CreateOrderResponse = EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.NativePayment.Models.CreateOrderResponse;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.NativePayment;

public class NativePaymentService : BasicPaymentService
{
    public const string CreateOrderUrl = "https://api.mch.weixin.qq.com/v3/pay/transactions/native";

    public NativePaymentService(AbpWeChatPayOptions options,
        IAbpLazyServiceProvider lazyServiceProvider) : base(options,
        lazyServiceProvider)
    {
    }

    public virtual Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request)
    {
        return ApiRequester.RequestAsync<CreateOrderResponse>(HttpMethod.Post, CreateOrderUrl, request);
    }
}