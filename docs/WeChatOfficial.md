## 一、API 支持情况

| 功能                          | 是否支持                                                     |
| ----------------------------- | ------------------------------------------------------------ |
| 自定义菜单                    | ![Support](https://img.shields.io/badge/-50%25-orange.svg)     |
| [用户管理] - 用户标签管理     | ![Support](https://img.shields.io/badge/-100%25-brightgreen.svg) |
| [用户管理] - 黑名单管理       | ![Support](https://img.shields.io/badge/-100%25-brightgreen.svg) |
| [基础消息能力] - 模板消息接口 | ![Support](https://img.shields.io/badge/-100%25-brightgreen.svg) |

## 二、基本模块配置

### 2.1 模块的引用

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

### 2.2 模块的配置

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

## 三、默认启用的接口

// TODO。

## 四、服务的使用

### 4.1 自定义菜单服务

开发人员如果需要使用自定义菜单服务，只需要注入 `CustomMenuService` 类型即可，该类型的生命周期为 **瞬时对象** 。

### 4.2 模板消息服务

开发人员如果需要使用模板消息服务，只需要注入 `TemplateMessageService` 类型即可，该类型的生命周期为 **瞬时对象** 。

### 4.3 用户管理服务

开发人员如果需要使用用户管理服务，只需要注入对应的服务即可。以下为具体的服务类型表，开发人员可自行参考文档进行注入使用。

| 类型名称         | 生命周期  | 微信官方文档                                                 |
| ---------------- | --------- | ------------------------------------------------------------ |
| `UserTagService` | Transient | [点击访问](https://developers.weixin.qq.com/doc/offiaccount/User_Management/User_Tag_Management.html) |

