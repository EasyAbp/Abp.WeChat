

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

微信模块的配置参数都存放在 `AbpWeChatOfficialOptions` 内部，开发人员只需要在启动模块的 `ConfigureService()` 方法中进行配置即可，下面是最小启动配置。

```csharp
[DependsOn(typeof(AbpWeChatOfficialHttpApiModule))]
public class XXXHttpApiModule : AbpModule 
{
    public override void ConfigureServices(ServiceConfigurationContext context) 
    {
        Configure<AbpWeChatOfficialOptions>(op =>
        {
            // 微信公众号所配置的 Token 值。
            op.Token = "0000000000";
            // 微信公众号分配的 AppId。
            op.AppId = "0000000000";
            // 微信公众号的唯一密钥。
            op.AppSecret = "0000000000";
            // OAuth 授权回调，用于微信公众号网页使用授权码换取 AccessToken。
            op.OAuthRedirectUrl = "http://test.hospital.wx.zhongfeiiot.com";
        });
    }
}
```

进行上述配置以后，你的项目就集成了微信公众号功能。现在，你可以在任意地方注入服务类，通过服务类快捷地调用微信公众平台所提供的 API 接口服务。

## 二、默认启用的接口

// TODO。

## 三、服务的使用

### 3.1 自定义菜单服务

开发人员如果需要使用自定义菜单服务，只需要注入 `CustomMenuService` 类型即可，该类型的生命周期为 **瞬时对象** 。

### 3.2 模板消息服务

开发人员如果需要使用模板消息服务，只需要注入 `TemplateMessageService` 类型即可，该类型的生命周期为 **瞬时对象** 。

