using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.JSPayment.Models;
using EasyAbp.Abp.WeChat.Pay.Services.BasicPayment.Models;
using EasyAbp.Abp.WeChat.Pay.Services.ParametersModel;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Services.BasicPayment;

public class BasicPaymentService : WeChatPayServiceBase
{
    public const string QueryOrderByWechatNumberUrl =
        "https://api.mch.weixin.qq.com/v3/pay/transactions/id/{transaction_id}";

    public const string QueryOrderByOutTradeNumberUrl =
        "https://api.mch.weixin.qq.com/v3/pay/transactions/out-trade-no/{out_trade_no}";

    public const string CloseOrderUrl =
        "https://api.mch.weixin.qq.com/v3/pay/transactions/out-trade-no/{out_trade_no}/close";

    public const string RefundUrl = "https://api.mch.weixin.qq.com/v3/refund/domestic/refunds";

    public const string QueryRefundOrderUrl =
        "https://api.mch.weixin.qq.com/v3/refund/domestic/refunds/{out_refund_no}";

    public const string GetTransactionBillUrl = "https://api.mch.weixin.qq.com/v3/bill/tradebill";
    public const string GetFundFlowBillUrl = "https://api.mch.weixin.qq.com/v3/bill/fundflowbill";

    public BasicPaymentService(AbpWeChatPayOptions options,
        IAbpLazyServiceProvider lazyServiceProvider) : base(options,
        lazyServiceProvider)
    {
    }

    public virtual Task<QueryOrderResponse> QueryOrderByWechatNumberAsync(QueryOrderByWechatNumberRequest request)
    {
        var requestUrl = QueryOrderByWechatNumberUrl.Replace("{transaction_id}", request.TransactionId);
        return ApiRequester.RequestAsync<QueryOrderResponse>(HttpMethod.Get, requestUrl, request);
    }

    public virtual Task<QueryOrderResponse> QueryOrderByOutTradeNumberAsync(QueryOrderByOutTradeNumberRequest request)
    {
        var requestUrl = QueryOrderByOutTradeNumberUrl.Replace("{out_trade_no}", request.OutTradeNo);
        return ApiRequester.RequestAsync<QueryOrderResponse>(HttpMethod.Get, requestUrl, request);
    }

    public virtual Task<CloseOrderResponse> CloseOrderAsync(CloseOrderRequest request)
    {
        var requestUrl = CloseOrderUrl.Replace("{out_trade_no}", request.OutTradeNo);
        return ApiRequester.RequestAsync<CloseOrderResponse>(HttpMethod.Post, requestUrl, request);
    }

    public virtual Task<RefundOrderResponse> RefundAsync(RefundOrderRequest orderRequest)
    {
        return ApiRequester.RequestAsync<RefundOrderResponse>(HttpMethod.Post, RefundUrl, orderRequest);
    }

    public virtual Task<RefundOrderResponse> QueryRefundOrderAsync(QueryRefundOrderRequest request)
    {
        var requestUrl = QueryRefundOrderUrl.Replace("{out_refund_no}", request.OutRefundNo);
        return ApiRequester.RequestAsync<RefundOrderResponse>(HttpMethod.Get, requestUrl);
    }

    public virtual Task<GetBillResponse> GetTransactionBillAsync(GetTransactionBillRequest request)
    {
        return ApiRequester.RequestAsync<GetBillResponse>(HttpMethod.Get, GetTransactionBillUrl, request);
    }

    public virtual Task<GetBillResponse> GetFundFlowBillAsync(GetFundFlowBillRequest request)
    {
        return ApiRequester.RequestAsync<GetBillResponse>(HttpMethod.Get, GetFundFlowBillUrl, request);
    }

    public virtual async Task<Stream> DownloadBillFileAsync(string billDownloadUrl)
    {
        return await (await ApiRequester.RequestRawAsync(HttpMethod.Get, billDownloadUrl)).Content.ReadAsStreamAsync();
    }
}