using System.Net.Http;
using System.Threading.Tasks;
using EasyAbp.Abp.WeChat.Pay.Options;
using EasyAbp.Abp.WeChat.Pay.Services.MarketingTools.VoucherService.ParametersModel;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.WeChat.Pay.Services.MarketingTools.VoucherService;

/// <summary>
/// 代金券服务。
/// </summary>
public class VoucherWeService : WeChatPayServiceBase
{
    public const string CreateCouponBatchUrl = "https://api.mch.weixin.qq.com/v3/marketing/favor/coupon-stocks";

    public VoucherWeService(AbpWeChatPayOptions options,
        IAbpLazyServiceProvider lazyServiceProvider) : base(options,
        lazyServiceProvider)
    {
    }

    public virtual Task<CreateCouponBatchResponse> CreateCouponBatchAsync(CreateCouponBatchRequest request)
    {
        return ApiRequester.RequestAsync<CreateCouponBatchResponse>(
            HttpMethod.Post, CreateCouponBatchUrl, request, MchId);
    }
}