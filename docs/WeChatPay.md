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
        // 微信支付的 API 密钥信息，会在后续进行签名时被使用。
        // 注意，本值是密文，如您在 appsettings.json 或 Configure<AbpWeChatPayOptions> 中设置本值，须自行根据加密后填入，参考：https://docs.abp.io/en/abp/latest/String-Encryption
        // 同样是密文的配置项还有：CertificateSecret
        op.ApiKey = "****************************";
        // 支付结果回调地址，用于接收支付结果通知。
        // 如果安装了本模块提供的 HttpApi 模块，则默认是 域名 + /wechat-pay/notify 路由。
        op.NotifyUrl = "https://xxx.xxxx.com/wechat-pay/notify";
        // 退款结果回调地址，用于接收退款结果通知。
        // 如果安装了本模块提供的 HttpApi 模块，则默认是 域名 + /wechat-pay/refund-notify 路由。
        op.RefundNotifyUrl = "https://xxx.xxxx.com/wechat-pay/refund-notify";
    });
}
```

完整的 Setting 项清单：https://github.com/EasyAbp/Abp.WeChat/blob/master/src/Pay/EasyAbp.Abp.WeChat.Pay/Settings/AbpWeChatPaySettingDefinitionProvider.cs

## 二、提供的回调接口

### 2.1 支付回调接口

支付通知接口的默认路由是 `/wechat-pay/notify`。当开发人员调用了统一下单接口之后，微信会将支付结果通过异步回调的方式请求 **支付通知接口** 进行通知。本路由可以修改相关 Setting 配置，或在模块启动时使用 `Configure<AbpWeChatPayOptions>()` 方法，对 `NotifyUrl` 参数进行配置，您可参考上文“模块的配置”章节了解如何修改配置。

用户如果需要对支付结果进行处理，只需要实现一个或多个 `IWeChatPayEventHandler` 处理器即可。当框架接受到微信通知时，会触发开发人员编写的处理器，并将微信结果传递给这些处理器。

```csharp
public class WeChatPaymentHandler : IWeChatPayEventHandler
{
    // 定义当前的处理的事件类型为：支付成功事件
    public WeChatHandlerType Type => WeChatHandlerType.Paid;
  
    public async Task<WeChatRequestHandlingResult> HandleAsync(WeChatPayEventModel model)
    {
        Console.WriteLine("我知道支付成功了");
        return new WeChatRequestHandlingResult(true);
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

其中 `XmlDocument` 对象内部的参数含义，可以参考微信支付 **[开发文档](https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_7&index=8)**。

WeChatPay 模块默认提供了参数校验处理器，各个处理器的调用顺序是按照 **注入顺序** 来的，目前暂时不支持处理器自定义排序。

### 2.2 退款回调接口

退款通知接口的默认路由是 `/wechat-pay/refund-notify`。当开发人员发起了退款操作之后，微信会将退款结果通过异步回调的方式请求 **退款通知接口** 进行通知。本路由可以修改相关 Setting 配置，或在模块启动时使用 `Configure<AbpWeChatPayOptions>()` 方法，对 `RefundNotifyUrl` 参数进行配置，您可参考上文“模块的配置”章节了解如何修改配置。

用户如果需要对退款通知进行处理，只需要实现一个或多个 `IWeChatPayEventHandler` 处理器即可。当框架接受到微信通知时，会触发开发人员编写的处理器，并将微信结果传递给这些处理器。

```csharp
public class XXXAAAHandler : IWeChatPayEventHandler
{
    // 定义当前处理器的类型为退款。
    public WeChatHandlerType Type => WeChatHandlerType.Refund;
  
    public Task HandleAsync(XmlDocument xmlDocument)
    {
        Console.WriteLine("接受到了数据");
        return Task.CompletedTask;
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

其中 `XmlDocument` 对象内部的参数含义，可以参考微信支付 **[开发文档](https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_16&index=10)**。

## 三、服务的使用

### 3.1 发起支付请求

```csharp
[Fact]
public async Task UnifiedOrder_Test()
{
    var mchId = null; // 目标商户的 mchid，如果为空则取 Setting 中的默认值
    var _payService = await WeChatPayServiceFactory.CreateAsync<OrdinaryMerchantPayWeService>(mchId);

    var result = await _payService.UnifiedOrderAsync(
        "你的 AppId",
        "你的商户 Id",
        "订单的描述信息",
        "订单号", // 订单号需要你自己生成，长度不超过 32 位。
        101, // 支付金额，单位是分。
        TradeType.JsApi); // 交易类型。
    
    result.ShouldNotBeNull();
}
```

### 3.2 发起退款请求

```csharp
[Fact]
public async Task Ref()
{
    var mchId = null; // 目标商户的 mchid，如果为空则取 Setting 中的默认值
    var _payService = await WeChatPayServiceFactory.CreateAsync<OrdinaryMerchantPayWeService>(mchId);

    var response = await _payService.RefundAsync(
        "你的 AppId",
        "你的商户 Id",
        "订单号",
        "退款订单号", // 退款订单号需要你自己生成，长度不超过 32 位。
        101, // 原始订单金额，单位是分，必须准确。
        50); // 退款订单金额，单位是分。
    
    response.ShouldNotBeNull();
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
