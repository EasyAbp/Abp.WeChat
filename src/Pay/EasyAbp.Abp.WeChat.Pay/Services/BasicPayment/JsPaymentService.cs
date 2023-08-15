using EasyAbp.Abp.WeChat.Pay.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment;

public class JsPaymentService : BasicPaymentService
{
    public JsPaymentService(AbpWeChatPayOptions options,
        IAbpLazyServiceProvider lazyServiceProvider) : base(options,
        lazyServiceProvider)
    {
        CreateOrderUrl = "https://api.mch.weixin.qq.com/v3/pay/transactions/jsapi";
    }
}