

## 一、基本模块配置

### 1.1 模块的引用

添加 **EasyAbp.Abp.WeChat.Official** 模块的 NuGet 包或者项目引用到 **Domain** 层，并在对应的模块上面添加 `[DependsOn]` 特性标签。

```csharp
[DependsOn(typeof(AbpWeChatOfficialModule))]
public class XXXDomainModule : AbpModule
{
}
```

添加 **EasyAbp.Abp.WeChat.Official.HttpApi** 模块的 NuGet 包或者项目引用到 **Http.Api** 层，并在对应的模块上面添加 `[DependsOn]` 特性标签。

```csharp
[DependsOn(typeof(AbpWeChatOfficialHttpApiModule))]
public class XXXHttpApiModule : AbpModule
{
}
```

### 1.2 模块的配置

本模块的默认配置参数使用 ABP Setting 设施管理，在 Setting 的值未提供时，由 `AbpWeChatOfficialOptions` 进行补充。如果您的应用只使用单个微信公众号，只需在启动模块的 `ConfigureService()` 方法中进行配置即可：

```csharp
public override void ConfigureServices(ServiceConfigurationContext context) 
{
    Configure<AbpWeChatOfficialOptions>(op =>
    {
        // 微信公众号分配的 AppId。
        op.AppId = "0000000000";
        // 微信公众号的唯一密钥。
        op.AppSecret = "********";
        // 微信公众号所配置的 Token 和 EncodingAesKey 值。
        op.Token = "********";
        op.EncodingAesKey = "********";
        // OAuth 授权回调，用于微信公众号网页使用授权码换取 AccessToken。
        op.OAuthRedirectUrl = "https://myapp.com";
    });
}
```

完整的 Setting 项清单：https://github.com/EasyAbp/Abp.WeChat/blob/master/src/Official/EasyAbp.Abp.WeChat.Official/Settings/AbpWeChatOfficialSettingDefinitionProvider.cs

> 注意，如您在 appsettings.json 中通过 Setting 设置 `AppSecret`, `Token` 或 `EncodingAesKey`，须自行根据加密后填入，参考：https://docs.abp.io/en/abp/latest/String-Encryption

## 二、服务的使用

您可以查看已实现的服务：https://github.com/EasyAbp/Abp.WeChat/tree/master/src/Official/EasyAbp.Abp.WeChat.Official/Services

参考以下写法使用服务：

```CSharp
var appId = null; // 目标微信应用的 appid，如果为空则取 Setting 中的默认值
var customMenuService = await WeChatServiceFactory.CreateAsync<CustomMenuWeService>(appId);
var result = await customMenuService.DeleteCustomMenuAsync();
```

注意，若 `appId` 与 Setting 中的默认值不同，则您需要手动实现 `IAbpWeChatOptionsProvider<TOptions>`，若使用 EasyAbp 封装的[微信管理模块](https://github.com/EasyAbp/WeChatManagement)，则您无需再手动实现。

## 三、多微信应用

在您调用服务，或处理微信请求的事件通知回调时，若提供的 `appId` 与 Setting 中的默认值可能不同，则您需要手动实现 `IAbpWeChatOptionsProvider<TOptions>`，若使用 EasyAbp 封装的[微信管理模块](https://github.com/EasyAbp/WeChatManagement)，则您无需再手动实现。

本模块提供的用于微信服务器通讯的 HTTP API 接口，也支持多应用和多租户，您需要使用合适的替代路由：
  * `/wechat/verify`
  * `/wechat/verify/tenant-id/{tenantId}`
  * `/wechat/verify/app-id/{appId}`
  * `/wechat/verify/tenant-id/{tenantId}/app-id/{appId}`
  * `/wechat/redirect-url`
  * `/wechat/redirect-url/tenant-id/{tenantId}`
  * `/wechat/redirect-url/app-id/{appId}`
  * `/wechat/redirect-url/tenant-id/{tenantId}/app-id/{appId}`

## 四、如何实现其他未支持接口

目前本仓库主要由 [real-zony](https://github.com/real-zony) 进行维护，部分不支持的接口可能一时半会儿无法实现。你可以参考源码，继承 `OfficialAbpWeChatServiceBase` 调用基类的 `WeChatOfficialApiRequester` 对象发起 API 请求。

所有请求都需要实现 `OfficialCommonRequest` 基类，所有响应都需要实现 `OfficialCommonResponse` 基类。