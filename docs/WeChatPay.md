## 零、支持情况

### 0.0 如何直接调用微信支付 V3 的接口?

由于精力有限，在项目初期没有完成所有微信支付 V3 接口的封装，这个时候你可以自己继承 `WeChatPayServiceBase` 基类，并直接使用基类提供的 `ApiRequester` 属性发起微信支付 V3 接口的请求。

所有的证书管理和签名验证都已经实现，你只需要自己编写对应的请求体对象和响应对象的类型定义。

你可以参考以下代码:

```csharp
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

    public Task<CreateCouponBatchResponse> CreateCouponBatchAsync(CreateCouponBatchRequest request)
    {
        return ApiRequester.RequestAsync<CreateCouponBatchResponse>(HttpMethod.Post, CreateCouponBatchUrl, request, MchId);
    }
}
```

### 0.1 基础支付

| 服务        | 支持情况                                                     | 备注                                                         |
| ----------- | ------------------------------------------------------------ | ------------------------------------------------------------ |
| JSAPI 支付  | ![Support](https://img.shields.io/badge/-支持-bright_green.svg) |                                                              |
| APP 支付    | ![Support](https://img.shields.io/badge/-支持-bright_green.svg) |                                                              |
| H5 支付     | ![Support](https://img.shields.io/badge/-支持-bright_green.svg) |                                                              |
| Native 支付 | ![Support](https://img.shields.io/badge/-支持-bright_green.svg) |                                                              |
| 小程序支付  | ![Support](https://img.shields.io/badge/-支持-bright_green.svg) |                                                              |
| 合单支付    | ![Support](https://img.shields.io/badge/-未实现-red.svg)     |                                                              |
| 付款码支付  | ![Support](https://img.shields.io/badge/-未实现-red.svg)     | 付款码支付仍然使用的是 微信支付 V2 版本的 API。<br />目前不考虑再支持此支付方式，请考虑使用 Native 支付替代。 |

### 0.2 经营能力

TODO

### 0.3 行业方案

TODO

### 0.4 营销工具

TODO

### 0.5 资金应用

TODO

### 0.6 风险合规

TODO

### 0.7 其他能力

TODO

## 一、基本模块配置

### 1.1 模块的引用

添加 **EasyAbp.Abp.WeChat.Pay** 模块的 NuGet 包或者项目引用到 **Domain** 层，并在对应的模块上面添加 `[DependsOn]` 特性标签。

```csharp
[DependsOn(typeof(AbpWeChatPayModule))]
public class XXXDomainModule : AbpModule
{
}
```

添加 **EasyAbp.Abp.WeChat.Pay.HttpApi** 模块的 NuGet 包或者项目引用到 **Http.Api** 层，并在对应的模块上面添加 `[DependsOn]` 特性标签。

```csharp
[DependsOn(typeof(AbpWeChatPayHttpApiModule))]
public class XXXHttpApiModule : AbpModule
{
}
```

### 1.2 模块的配置

本模块的默认配置参数使用 ABP Setting 设施管理，在 Setting 的值未提供时，由 `AbpWeChatPayOptions` 进行补充。如果您的应用只使用单个微信支付商户，只需在启动模块的 `ConfigureService()` 方法中进行配置即可：

```csharp
public override void ConfigureServices (ServiceConfigurationContext context) 
{
    Configure<AbpWeChatPayOptions> (op => 
    {
        // 默认商户 Id
        op.MchId = "000000000000000";
        // 微信支付的 API V3 密钥信息，会在后续进行 签名/加密/解密 时被使用。
        op.ApiV3Key = "****************************";
        // 支付结果回调地址，用于接收支付结果通知。
        // 如果安装了本模块提供的 HttpApi 模块，则默认是 域名 + /wechat-pay/notify 路由。
        op.NotifyUrl = "https://xxx.xxxx.com/wechat-pay/notify";
      
        // 如果需要支持退款操作，则以下配置必须
        
        // 退款结果回调地址，用于接收退款结果通知。
        // 如果安装了本模块提供的 HttpApi 模块，则默认是 域名 + /wechat-pay/refund-notify 路由。
        op.RefundNotifyUrl = "https://xxx.xxxx.com/wechat-pay/refund-notify";
        // 存放微信支付API证书的BLOB容器名，参考：https://docs.abp.io/en/abp/latest/Blob-Storing
        op.CertificateBlobContainerName = "MyBlobContainer";
        // 存放微信支付API证书的BLOB名称
        op.CertificateBlobName = "WeChatPayCert";
        // 微信支付API证书秘钥，默认为商户名
        op.CertificateSecret = "000000000000000";
        // 微信支付API证书公钥Id
        op.PublicKeyId = "PUB_KEY_ID_xxxxx";
        // 微信支付API证书公钥 要删除头、尾、换行符
        op.PublicKey = "MIIBIjANBgkqhkiG9w0BAQEFA";
    });
}
```

完整的 Setting 项清单：https://github.com/EasyAbp/Abp.WeChat/blob/master/src/Pay/EasyAbp.Abp.WeChat.Pay/Settings/AbpWeChatPaySettingDefinitionProvider.cs

> 注意，如您在 appsettings.json 中通过 Setting 设置 `ApiV3Key` 或 `CertificateSecret`，须自行加密后填入，参考：https://docs.abp.io/en/abp/latest/String-Encryption

## 二、提供的回调接口

### 2.1 支付回调接口

支付通知接口的默认路由是 `/wechat-pay/notify`。当开发人员调用了统一下单接口之后，微信会将支付结果通过异步回调的方式请求 **支付通知接口** 进行通知。本路由可以修改相关 Setting 配置，或在模块启动时使用 `Configure<AbpWeChatPayOptions>()` 方法，对 `NotifyUrl` 参数进行配置，您可参考上文“模块的配置”章节了解如何修改配置。

用户如果需要对支付结果进行处理，只需要实现一个或多个 `IWeChatPayEventHandler` 处理器即可。当框架接受到微信通知时，会触发开发人员编写的处理器，并将微信结果传递给这些处理器。

```csharp
  public class PaidWeChatPayEventHandler : WeChatPayPaidEventHandlerBase
  {
      public Task<WeChatRequestHandlingResult> HandleAsync(WeChatPayEventModel<WeChatPayPaidEventModel> model)
      {
          Console.WriteLine("支付成功。");
          return Task.FromResult(new WeChatRequestHandlingResult(true));
      }
  }
```

编写完成之后，则需要开发人员手动注入这些处理器。

```csharp
public class XXXDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<IWeChatPayEventHandler, WeChatPaymentHandler>();
    }
}
```

如果在处理过程当中出现了异常，那么你可以在返回 `WeChatRequestHandlingResult` 对象时，设置 `success` 参数为 `false`，并且可以填写对应的失败原因。

WeChatPay 模块默认提供了参数校验处理器，各个处理器的调用顺序是按照 **注入顺序** 来的，目前暂时不支持处理器自定义排序。

### 2.2 退款回调接口

退款通知接口的默认路由是 `/wechat-pay/refund-notify`。当开发人员发起了退款操作之后，微信会将退款结果通过异步回调的方式请求 **退款通知接口** 进行通知。本路由可以修改相关 Setting 配置，或在模块启动时使用 `Configure<AbpWeChatPayOptions>()` 方法，对 `RefundNotifyUrl` 参数进行配置，您可参考上文“模块的配置”章节了解如何修改配置。

用户如果需要对退款通知进行处理，只需要实现一个或多个 `IWeChatPayEventHandler` 处理器即可。当框架接受到微信通知时，会触发开发人员编写的处理器，并将微信结果传递给这些处理器。

```csharp
  public class RefundWeChatPayEventHandler : WeChatPayRefundEventHandlerBase
  {
      public Task<WeChatRequestHandlingResult> HandleAsync(WeChatPayEventModel<WeChatPayRefundEventModel> model)
      {
          Console.WriteLine("退款成功。");
          return Task.FromResult(new WeChatRequestHandlingResult(true));
      }
  }
```

编写完成之后，则需要开发人员手动注册这些处理器。

```csharp
public class XXXDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<IWeChatPayEventHandler, XXXAAAHandler>();
    }
}
```

## 三、服务的使用

服务的所有使用方法你都可以参考集成测试项目下的内容 [https://github.com/EasyAbp/Abp.WeChat/blob/master/tests/EasyAbp.Abp.WeChat.Pay.Tests/Services](https://github.com/EasyAbp/Abp.WeChat/blob/master/tests/EasyAbp.Abp.WeChat.Pay.Tests/Services)。

### 3.1 发起支付请求

```csharp
[Fact]
public async Task CreateOrderAsync_Test()
{
    // Arrange
    var service = await _weChatPayServiceFactory.CreateAsync<JsPaymentService>();
    var request = new CreateOrderRequest
    {
        MchId = service.MchId,
        OutTradeNo = RandomStringHelper.GetRandomString(),
        NotifyUrl = AbpWeChatPayTestConsts.NotifyUrl,
        AppId = AbpWeChatPayTestConsts.AppId, // 请替换为你的 AppId
        Description = "Image形象店-深圳腾大-QQ公仔",
        Amount = new CreateOrderAmountModel
        {
            Total = 1,
            Currency = "CNY"
        },
        Payer = new CreateOrderRequest.CreateOrderPayerModel
        {
            OpenId = AbpWeChatPayTestConsts.OpenId // 请替换为测试用户的 OpenId，具体 Id 可以在微信公众号平台-用户管理进行查看。
        }
    };

    // Act
    var response = await service.CreateOrderAsync(request);

    // Assert
    response.ShouldNotBeNull();
    response.PrepayId.ShouldNotBeNullOrEmpty();
}
```

### 3.2 发起退款请求

```csharp
[Fact]
public async Task RefundAsync_Test()
{
    // Arrange
    var service = await _weChatPayServiceFactory.CreateAsync<JsPaymentService>();
    var request = new RefundOrderRequest
    {
        OutRefundNo = RandomStringHelper.GetRandomString(),
        OutTradeNo = "kel9xerwcjib2zs8eixyazuis3qsmo",
        NotifyUrl = AbpWeChatPayTestConsts.RefundNotifyUrl,
        Amount = new RefundOrderRequest.AmountInfo
        {
            Refund = 1,
            Total = 1,
            Currency = "CNY"
        }
    };
        
    // Act
    var response = await service.RefundAsync(request);

    // Assert
    response.ShouldNotBeNull();
    response.RefundId.ShouldNotBeNullOrEmpty();
}
```

## 四、多商户

在您调用服务，或处理微信请求的事件通知回调时，若提供的 `mchId` 与 Setting 中的默认值可能不同，则您需要手动实现 `IAbpWeChatPayOptionsProvider`，若使用 EasyAbp 封装的[支付服务模块](https://github.com/EasyAbp/PaymentService)，则您无需再手动实现。

本模块提供的用于处理支付结果和退款结果的 HTTP API 回调接口，也支持多商户和多租户，您需要使用这些替代路由：
  * `/wechat-pay/notify`
  * `/wechat-pay/notify/tenant-id/{tenantId}`
  * `/wechat-pay/notify/mch-id/{mchId}`
  * `/wechat-pay/notify/tenant-id/{tenantId}/mch-id/{mchId}`
  * `/wechat-pay/refund-notify`
  * `/wechat-pay/refund-notify/tenant-id/{tenantId}`
  * `/wechat-pay/refund-notify/mch-id/{mchId}`
  * `/wechat-pay/refund-notify/tenant-id/{tenantId}/mch-id/{mchId}`
