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

微信模块的配置参数都存放在 `AbpWeChatPayOptions` 内部，开发人员只需要在启动模块的 `ConfigureService()` 方法中进行配置即可，下面是最小启动配置。

```csharp
[DependsOn (typeof (AbpWeChatPayHttpApiModule))]
public class XXXHttpApiModule : AbpModule 
{
    public override void ConfigureServices (ServiceConfigurationContext context) 
    {
        Configure<AbpWeChatPayOptions> (op => 
        {
            // 发起微信支付请求的产品 App Id。如果是公众号需要发起支付请求则是公众号的 AppId，小程序则是小程序的 AppId。
            op.AppId = "000000000000000";
            // 微信支付的 API 密钥信息，会在后续进行签名时被使用。
            op.ApiKey = "000000000000000000000000000";
            // 支付回调地址，用于接收支付结果通知。
            // 如果使用了 HttpApi 模块的 Controller，则默认是 域名 + /WeChatPay/Notify 路由。
            op.NotifyUrl = "http://xxx.xxxx.com/WeChatPay/Notify";
        });
    }
}
```

进行上述配置以后，你的项目就集成了微信支付功能，如果你需要启用沙箱模式，可以设置 `AbpWeChatPayOptions.IsSandBox` 为 `true` 。

其他配置参数，可以参考 `AbpWeChatPayOptions` 类型的定义，上面针对各个配置参数都有详细的注释说明。

#### 1.2.1 配置提供器

我们参考 `ITenantResolver` 的方式，将微信支付相关的配置参数抽象到各个 Provider 当中。默认的 Provider 实现是基于 `IOptions<AbpWeChatPayOptions>`，他会从上述的配置项中读取 AppId 等参数。

如果你的系统是根据租户分隔，那么只需要自己实现 `IWeChatPayOptionResolveContributor` 接口，在内部处理逻辑即可。

## 二、提供的回调接口

### 2.1 支付回调接口

支付通知接口的默认路由是 `/WeChatPay/Notify`，当开发人员调用了统一下单接口之后，微信会将支付结果通过异步回调的方式请求 **支付通知接口**，该参数可以通过注入 `AbpWeChatPayOptions.NotifyUrl` 进行读取或设置。

> 开发人员也可以自己编写回调接口，只需要在配置的时候，参数传递自己的回调接口 URL 即可。

用户如果需要对支付结果进行处理，只需要实现一个或多个 `IWeChatPayHandler` 处理器即可。当框架接受到微信通知时，会触发开发人员编写的处理器，并将微信结果传递给这些处理器。

```csharp
public class WeChatPaymentHandler : IWeChatPayHandler
{
    public Task HandleAsync(WeChatPayHandlerContext context)
    {
        Console.WriteLine("接受到了数据");
        return Task.CompletedTask;
    }
}
```

编写完成之后，则需要开发人员手动注入这些处理器。

```csharp
public class XXXDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<IWeChatPayHandler, WeChatPaymentHandler>();
    }
}
```

如果在处理过程当中出现了异常，那么你可以设置 `WeChatPayHandlerContext` 当中的 `IsSuccess` 参数为 `false`，并且可以填写对应的失败原因。

其中 `XmlDocument` 对象内部的参数含义，可以参考微信支付 **[开发文档](https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_7&index=8)**。

WeChatPay 模块默认提供了参数校验处理器，各个处理器的调用顺序是按照 **注入顺序** 来的，目前暂时不支持处理器自定义排序。

### 2.2 退款回调接口

当开发人员发起了退款操作之后，微信会将退款通知，通过异步回调的方式请求 **退款通知接口**，具体的接口地址可以在模块启动时使用 `Configure<AbpWeChatPayOptions>()` 方法，对 `RefundNotifyUrl` 参数进行配置。

>开发人员也可以自己编写回调接口，只需要在配置的时候，参数传递自己的回调接口 URL 即可。

用户如果需要对退款通知进行处理，只需要实现一个或多个 `IWeChatPayRefundHandler` 处理器即可。当框架接受到微信通知时，会触发开发人员编写的处理器，并将微信结果传递给这些处理器。

```csharp
public class XXXAAAHandler : IWeChatPayRefundHandler
{
    public Task HandleAsync(XmlDocument xmlDocument)
    {
        Console.WriteLine("接受到了数据");
        return Task.CompletedTask;
    }
}
```

编写完成之后，则需要开发人员手动注入这些处理器。

```csharp
public class XXXDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddSingleton<IWeChatPayRefundHandler, XXXAAAHandler>();
    }
}
```

其中 `XmlDocument` 对象内部的参数含义，可以参考微信支付 **[开发文档](https://pay.weixin.qq.com/wiki/doc/api/jsapi.php?chapter=9_16&index=10)**。

## 三、服务的使用

针对微信支付服务，目前模块所有接口都封装到 `PayService` 实现内部，开发人员只需要注入 `PayService` 服务即可使用下面的接口方法。

### 3.1 发起支付请求

```csharp
[Fact]
public async Task UnifiedOrder_Test()
{
    var result = await _payService.UnifiedOrderAsync("你的 AppId",
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
    var response = await _payService.RefundAsync("你的 AppId",
        "你的商户 Id",
        "订单号",
        "退款订单号", // 退款订单号需要你自己生成，长度不超过 32 位。
        101, // 原始订单金额，单位是分，必须准确。
        50); // 退款订单金额，单位是分。
    
    response.ShouldNotBeNull();
}
```
