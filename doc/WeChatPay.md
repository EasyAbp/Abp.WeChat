## 一、基本模块配置

### 1.1 模块的引用

添加 **Zony.Abp.WeChat.Pay** 模块的 NuGet 包或者项目引用到 **Domain** 层，并在对应的模块上面添加 `[DependsOn]` 特性标签。

```csharp
[DependsOn(typeof(AbpWeChatPayModule))]
public class XXXDomainModule : AbpModule
{

}
```

添加 **Zony.Abp.WeChat.Pay.HttpApi** 模块的 NuGet 包或者项目引用到 **Http.Api** 层，并在对应的模块上面添加 `[DependsOn]` 特性标签。

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

## 二、默认启用的接口

### 2.1 预支付签名接口

### 2.2 支付通知接口

支付通知接口的默认路由是 `/WeChatPay/Notify`，当开发人员调用了统一下单接口之后，微信会将支付结果通过异步回调的方式请求 **支付通知接口** 。

> 开发人员也可以自己编写回调接口，只需要在调用统一下单接口时，传递自己的回调接口 URL 即可。

用户如果需要对支付结果进行处理，只需要实现一个或多个 `IWeChatPayHandler` 处理器即可。当框架接受到微信通知时，会触发开发人员编写的处理器，并将微信结果传递给这些处理器。

```csharp
public class WeChatPaymentHandler : IWeChatPayHandler
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
        context.Services.AddSingleton<IWeChatPayHandler, WeChatPaymentHandler>();
    }
}
```

## 三、服务的使用

// TODO。