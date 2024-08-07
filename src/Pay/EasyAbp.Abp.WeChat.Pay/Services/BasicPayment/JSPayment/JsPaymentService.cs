﻿using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;
using Volo.Abp.DependencyInjection;
using CreateOrderRequest = EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment.Models.CreateOrderRequest;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment;

public class JsPaymentService : BasicPaymentService
{
    public const string CreateOrderUrl = "https://api.mch.weixin.qq.com/v3/pay/transactions/jsapi";

    public JsPaymentService(AbpWeChatPayOptions options,
        IAbpLazyServiceProvider lazyServiceProvider) : base(options,
        lazyServiceProvider)
    {
    }

    public virtual Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request)
    {
        return ApiRequester.RequestAsync<CreateOrderResponse>(HttpMethod.Post, CreateOrderUrl, request);
    }
}