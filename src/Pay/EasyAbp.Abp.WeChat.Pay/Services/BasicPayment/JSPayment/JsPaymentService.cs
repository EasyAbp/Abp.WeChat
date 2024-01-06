using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment.Models;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;
using EasyAbp.Abp.WeChat.Pay.Services.ParametersModel;
using Volo.Abp.DependencyInjection;
using CreateOrderRequest = EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment.Models.CreateOrderRequest;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment;

public class JsPaymentService : WeChatPayServiceBase
{
    public const string CreateOrderUrl = "https://api.mch.weixin.qq.com/v3/pay/transactions/jsapi";
    public const string QueryOrderByWechatNumberUrl = "https://api.mch.weixin.qq.com/v3/pay/transactions/{transaction_id}";
    public const string QueryOrderByOutTradeNumberUrl = "https://api.mch.weixin.qq.com/v3/pay/transactions/out-trade-no/{out_trade_no}";
    public const string CloseOrderUrl = "https://api.mch.weixin.qq.com/v3/pay/transactions/out-trade-no/{out_trade_no}/close";
    public const string RefundUrl = "https://api.mch.weixin.qq.com/v3/refund/domestic/refunds";
    public const string QueryRefundOrderUrl = "https://api.mch.weixin.qq.com/v3/refund/domestic/refunds/{out_refund_no}";
    public const string GetTransactionBillUrl = "https://api.mch.weixin.qq.com/v3/bill/tradebill";

    public JsPaymentService(AbpWeChatPayOptions options,
        IAbpLazyServiceProvider lazyServiceProvider) : base(options,
        lazyServiceProvider)
    {
    }

    public Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request)
    {
        return ApiRequester.RequestAsync<CreateOrderResponse>(HttpMethod.Post, CreateOrderUrl, request);
    }

    public Task<QueryOrderResponse> QueryOrderByWechatNumberAsync(QueryOrderByWechatNumberRequest request)
    {
        var requestUrl = QueryOrderByWechatNumberUrl.Replace("{transaction_id}", request.TransactionId);
        return ApiRequester.RequestAsync<QueryOrderResponse>(HttpMethod.Get, requestUrl, request);
    }

    public Task<QueryOrderResponse> QueryOrderByOutTradeNumberAsync(QueryOrderByOutTradeNumberRequest request)
    {
        var requestUrl = QueryOrderByOutTradeNumberUrl.Replace("{out_trade_no}", request.OutTradeNo);
        return ApiRequester.RequestAsync<QueryOrderResponse>(HttpMethod.Get, requestUrl, request);
    }

    public Task<WeChatPayCommonErrorResponse> CloseOrderAsync(CloseOrderRequest request)
    {
        var requestUrl = CloseOrderUrl.Replace("{out_trade_no}", request.OutTradeNo);
        return ApiRequester.RequestAsync<WeChatPayCommonErrorResponse>(HttpMethod.Post, requestUrl, request);
    }

    public Task<RefundOrderResponse> RefundAsync(RefundOrderRequest orderRequest)
    {
        return ApiRequester.RequestAsync<RefundOrderResponse>(HttpMethod.Post, RefundUrl, orderRequest);
    }

    public Task<RefundOrderResponse> QueryRefundOrderAsync(QueryRefundOrderRequest request)
    {
        var requestUrl = QueryRefundOrderUrl.Replace("{out_refund_no}", request.OutRefundNo);
        return ApiRequester.RequestAsync<RefundOrderResponse>(HttpMethod.Get, requestUrl);
    }

    public Task<GetTransactionBillResponse> GetTransactionBillAsync(GetTransactionBillRequest request)
    {
        return ApiRequester.RequestAsync<GetTransactionBillResponse>(HttpMethod.Get, GetTransactionBillUrl, request);
    }
}