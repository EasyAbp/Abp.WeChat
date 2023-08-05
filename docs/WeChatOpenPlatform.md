# 微信开放平台模块

我们将 **微信第三方平台** 在 **微信开放平台模块** 中实现。

对于第三方平台相关功能，本模块封装了 ComponentVerifyTicket, ComponentAccessToken, AuthorizerAccessToken, AuthorizerRefreshToken 等票券的获取和暂存。提供了微信服务器相关的 HTTP API。实现了常规微信应用授权第三方平台后，后者（开发者无感地）接管前者的微信功能调用的能力。

> 推荐你使用 EasyAbp 封装的[微信管理模块](https://github.com/EasyAbp/WeChatManagement)，它依赖本模块做了二次封装，提供应用级的各项功能。

## 一、、基本模块配置

### 1.1 模块的引用

添加 **EasyAbp.Abp.WeChat.OpenPlatform** 模块的 NuGet 包或者项目引用到 **Domain** 层，并在对应的模块上面添加 `[DependsOn]` 特性标签。

```csharp
[DependsOn(typeof(AbpWeChatOpenPlatformModule))]
public class XXXDomainModule : AbpModule
{
}
```

添加 **EasyAbp.Abp.WeChat.OpenPlatform.HttpApi** 模块的 NuGet 包或者项目引用到 **Http.Api** 层，并在对应的模块上面添加 `[DependsOn]` 特性标签。

```csharp
[DependsOn(typeof(AbpWeChatOpenPlatformHttpApiModule))]
public class XXXHttpApiModule : AbpModule
{
}
```

### 1.2 模块的配置（微信第三方平台）

本模块的默认配置参数使用 ABP Setting 设施管理，在 Setting 的值未提供时，由 `AbpWeChatThirdPartyPlatformOptions` 进行补充。如果您的应用只使用单个微信第三方平台，只需在启动模块的 `ConfigureService()` 方法中进行配置即可：

```csharp
public override void ConfigureServices(ServiceConfigurationContext context) 
{
    Configure<AbpWeChatThirdPartyPlatformOptions>(op =>
    {
        // 微信第三方平台分配的 AppId。
        op.AppId = "0000000000";
        // 微信第三方平台的唯一密钥。
        op.AppSecret = "********";
        // 微信第三方平台所配置的 Token 和 EncodingAesKey 值。
        op.Token = "********";
        op.EncodingAesKey = "********";
    });
}
```

完整的 Setting 项清单：https://github.com/EasyAbp/Abp.WeChat/blob/master/src/OpenPlatform/EasyAbp.Abp.WeChat.OpenPlatform/ThirdPartyPlatform/Settings/AbpWeChatThirdPartyPlatformSettingDefinitionProvider.cs

> 注意，如您在 appsettings.json 中通过 Setting 设置 `AppSecret`, `Token` 或 `EncodingAesKey`，须自行加密后填入，参考：https://docs.abp.io/en/abp/latest/String-Encryption

## 二、微信第三方平台的使用

在安装了第三方平台模块后，如果一个小程序/公众号应用配置的 AppSecret 为 null，则会被认为已对第三方平台授权，后者代为管理。

默认情况下，对于以上情况，将从 Settings 中取配置的第三方平台信息（如果 Settings 没有配置，则从 Options 中取值，作为默认值），然后以此平台的身份代替应用调用所有接口。

如果是多平台场景，请注入 `ICurrentWeChatThirdPartyPlatform` 改变当前 ComponentAppId：
```CSharp
var aCodeService = await WeChatServiceFactory.CreateAsync<ACodeWeService>(authorizerAppId);

using (currentWeChatThirdPartyPlatform.Change(componentAppId))
{
    // 代客户生成小程序码
    await aCodeService.GetUnlimitedACodeAsync("test");
}
```

除此之外，还需要实现 `IAbpWeChatOptionsProvider<AbpWeChatThirdPartyPlatformOptions>`，以帮助模块获取到第三方平台的机密配置信息，从而支持多平台场景。你也可以使用[微信管理模块](https://github.com/EasyAbp/WeChatManagement)，它已经做好了实现。

微信第三方平台的机制较为复杂，可参阅：https://developers.weixin.qq.com/doc/oplatform/Third-party_Platforms/2.0/getting_started/terminology_introduce.html

> 推荐你使用 EasyAbp 封装的[微信管理模块](https://github.com/EasyAbp/WeChatManagement)，它依赖本模块做了二次封装，提供应用级的各项功能。

## 三、多微信应用

在您调用服务，或处理微信请求的事件通知回调时，若提供的 `appId` 与 Setting 中的默认值可能不同，则您需要手动实现 `IAbpWeChatOptionsProvider<TOptions>`，若使用 EasyAbp 封装的[微信管理模块](https://github.com/EasyAbp/WeChatManagement)，则您无需再手动实现。

本模块提供的用于微信服务器通讯的 HTTP API 接口，也支持多应用和多租户，您需要使用合适的替代路由：
  * `/wechat/third-party-platform/notify/auth`
  * `/wechat/third-party-platform/notify/auth/tenant-id/{tenantId}`
  * `/wechat/third-party-platform/notify/auth/component-app-id/{componentAppId}`
  * `/wechat/third-party-platform/notify/auth/tenant-id/{tenantId}/component-app-id/{componentAppId}`
  * `/wechat/third-party-platform/notify/app/app-id/{appId}`
  * `/wechat/third-party-platform/notify/app/tenant-id/{tenantId}/app-id/{appId}`
  * `/wechat/third-party-platform/notify/app/component-app-id/{componentAppId}/app-id/{appId}`
  * `/wechat/third-party-platform/notify/app/tenant-id/{tenantId}/component-app-id/{componentAppId}/app-id/{appId}`

## 四、微信服务器事件处理

本模块提供了 HTTP API 接口接收微信服务器事件推送，如果您需要处理，请实现处理器类：

```csharp
// 第三方平台代接收的微信应用事件
public class MyWeChatThirdPartyPlatformAppEventHandler :
    WeChatThirdPartyPlatformAppEventHandlerBase<MyWeChatThirdPartyPlatformAppEventHandler>,
    ITransientDependency
{
    public override string MsgType => "event"; // 对应微信文档的msgType值
    public override int Priority => 0; // Priority大的处理器优先执行

    public override async Task<AppEventHandlingResult> HandleAsync(
        string componentAppId, string authorizerAppId, WeChatAppEventModel model)
    {
        // 在这里处理您的业务
        return new AppEventHandlingResult(true); // 处理后返回成功的结果
    }
}
```
