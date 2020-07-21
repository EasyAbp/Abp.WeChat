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

添加 **EasyAbp.Abp.WeChat.MiniProgram.HttpApi** 模块的 NuGet 包或者项目引用到 **Http.Api** 层，并在对应的模块上面添加 `[DependsOn]` 特性标签。

```csharp
[DependsOn(typeof(AbpWeChatMiniProgramHttpApiModule))]
public class XXXHttpApiModule : AbpModule
{
    
}
```

### 1.2 模块的配置

微信模块的配置参数都存放在 `AbpWeChatMiniProgramOptions` 内部，开发人员只需要在启动模块的 `ConfigureService()` 方法中进行配置即可，下面是最小启动配置。

```csharp
[DependsOn(typeof(AbpWeChatMiniProgramHttpApiModule))]
public class XXXHttpApiModule : AbpModule 
{
    public override void ConfigureServices(ServiceConfigurationContext context) 
    {
        Configure<AbpWeChatMiniProgramOptions>(op =>
        {
            // 微信小程序所配置的 Token 值。
            op.Token = "0000000000";
            // 微信小程序分配的 AppId。
            op.AppId = "0000000000";
            // 微信小程序的唯一密钥。
            op.AppSecret = "0000000000";
        });
    }
}
```

进行上述配置以后，你的项目就集成了微信小程序功能。现在，你可以在任意地方注入服务类，通过服务类快捷地调用微信公众平台所提供的 API 接口服务。

## 二、默认启用的接口

// TODO。

## 三、服务的使用

### 3.1 微信登录服务

开发人员如果需要使用微信登录服务，只需要注入 `LoginService` 类型即可，该类型的生命周期为 **瞬时对象** 。

### 3.2 小程序码服务

开发人员如果需要使用小程序码服务，只需要注入 `ACodeService` 类型即可，该类型的生命周期为 **瞬时对象** 。

