# 微信小程序模块

> 推荐你使用 EasyAbp 封装的[微信管理模块](https://github.com/EasyAbp/WeChatManagement)，它依赖本模块做了二次封装，提供应用级的各项功能。

## 一、基本模块配置

### 1.1 模块的引用

添加 **EasyAbp.Abp.WeChat.MiniProgram** 模块的 NuGet 包或者项目引用到 **Domain** 层，并在对应的模块上面添加 `[DependsOn]` 特性标签。

```csharp
[DependsOn(typeof(AbpWeChatMiniProgramModule))]
public class XXXDomainModule : AbpModule
{
}
```

<!-- 添加 **EasyAbp.Abp.WeChat.MiniProgram.HttpApi** 模块的 NuGet 包或者项目引用到 **Http.Api** 层，并在对应的模块上面添加 `[DependsOn]` 特性标签。

```csharp
[DependsOn(typeof(AbpWeChatMiniProgramHttpApiModule))]
public class XXXHttpApiModule : AbpModule
{
}
``` -->

### 1.2 模块的配置

本模块的默认配置参数使用 ABP Setting 设施管理，在 Setting 的值未提供时，由 `AbpWeChatMiniProgramOptions` 进行补充。如果您的应用只使用单个微信小程序，只需在启动模块的 `ConfigureService()` 方法中进行配置即可：

```csharp
public override void ConfigureServices(ServiceConfigurationContext context) 
{
    Configure<AbpWeChatMiniProgramOptions>(op =>
    {
        // 微信小程序分配的 AppId。
        op.AppId = "0000000000";
        // 微信小程序的唯一密钥。
        // 注意，本值是密文，如您在 appsettings.json 或 Configure<AbpWeChatMiniProgramOptions> 中设置本值，须自行根据加密后填入，参考：https://docs.abp.io/en/abp/latest/String-Encryption
        // 同样是密文的配置项还有：Token, EncodingAesKey
        op.AppSecret = "********";
        // 微信小程序所配置的 Token 和 EncodingAesKey 值。
        op.Token = "********";
        op.EncodingAesKey = "********";
    });
}
```

完整的 Setting 项清单：https://github.com/EasyAbp/Abp.WeChat/blob/master/src/MiniProgram/EasyAbp.Abp.WeChat.MiniProgram/Settings/AbpWeChatMiniProgramSettingDefinitionProvider.cs

## 二、服务的使用

您可以查看已实现的服务：https://github.com/EasyAbp/Abp.WeChat/tree/master/src/MiniProgram/EasyAbp.Abp.WeChat.MiniProgram/Services

参考以下写法使用服务：

```CSharp
var appId = null; // 目标微信应用的 appid，如果为空则取 Setting 中的默认值
var aCodeService = await WeChatServiceFactory.CreateAsync<ACodeWeService>(appId);
var result = await aCodeService.GetUnlimitedACodeAsync("test");
```

注意，若 `appId` 与 Setting 中的默认值不同，则您需要手动实现 `IAbpWeChatOptionsProvider<TOptions>`，若使用 EasyAbp 封装的[微信管理模块](https://github.com/EasyAbp/WeChatManagement)，则您无需再手动实现。

## 三、多微信应用

在您调用服务，或处理微信请求的事件通知回调时，若提供的 `appId` 与 Setting 中的默认值可能不同，则您需要手动实现 `IAbpWeChatOptionsProvider<TOptions>`，若使用 EasyAbp 封装的[微信管理模块](https://github.com/EasyAbp/WeChatManagement)，则您无需再手动实现。

本模块提供的用于微信服务器通讯的 HTTP API 接口，也支持多应用和多租户，您需要使用合适的替代路由：（TODO）

## 四、如何实现其他未支持接口

目前本仓库主要由 [real-zony](https://github.com/real-zony) 进行维护，部分不支持的接口可能一时半会儿无法实现。你可以参考源码，继承 `MiniProgramAbpWeChatServiceBase` 调用基类的 `WeChatMiniProgramApiRequester` 对象发起 API 请求。

所有请求都需要实现 `MiniProgramCommonRequest` 基类，所有响应都需要实现 `MiniProgramCommonResponse` 基类。
