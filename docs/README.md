# Abp.WeChat

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FAbp.WeChat%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.Abp.WeChat.Common.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.WeChat.Common)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.Abp.WeChat.Common.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.WeChat.Common)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/Abp.WeChat?style=social)](https://www.github.com/EasyAbp/Abp.WeChat)
[![Discord online](https://badgen.net/discord/online-members/S6QaezrCRq?label=Discord)](https://discord.gg/S6QaezrCRq)

Abp 微信 SDK 模块，包含对微信小程序、公众号、企业微信、开放平台、第三方平台等相关接口封装。

> 推荐你使用 EasyAbp 封装的[微信管理模块](https://github.com/EasyAbp/WeChatManagement)，它依赖本模块做了二次封装，提供应用级的各项功能。

## 一、简要介绍

**EasyAbp.Abp.WeChat** 库是针对于微信公众号与微信支付 API 进行了二次封装的模块，与 ABP vNext 框架深度集成。开发人员如果是基于 ABP vNext  框架开发项目，集成本模块以后，可以快速实现同微信开放平台的对接。

## 二、使用方式

**微信支付模块：**[点击查看说明](/docs/WeChatPay.md)

**微信公众号模块**：[点击查看说明](/docs/WeChatOfficial.md)

**微信小程序模块：**[点击查看说明](/docs/WeChatMiniProgram.md)

## 三、API 支持情况

### 3.1 微信支付

#### 3.1.1 JS API 支付

| 功能             | 是否支持                                                     |
| ---------------- | ------------------------------------------------------------ |
| 统一下单         | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 查询订单         | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 关闭订单         | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 申请退款         | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 查询退款         | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 下载对账单       | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 下载资金账单     | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 支付结果通知     | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 交易保障         | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 退款结果通知     | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 拉取订单评价数据 | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |

#### 3.1.2 服务商特有接口
TODO: ...

### 3.2 微信公众号

#### 3.2.1 自定义菜单

| 功能               | 是否支持                                                     |
| ------------------ | ------------------------------------------------------------ |
| 创建自定义菜单     | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 查询自定义菜单     | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 删除自定义菜单     | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 自定义菜单事件推送 | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 获取自定义菜单配置 | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |

**个性化菜单接口**

| 功能                   | 是否支持                                                     |
| ---------------------- | ------------------------------------------------------------ |
| 创建个性化菜单         | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 删除个性化菜单         | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 测试个性化菜单匹配结果 | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 查询个性化菜单         | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 删除所有菜单           | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |

#### 3.2.2 消息管理

**模板消息**

| 功能               | 是否支持                                                     |
| ------------------ | ------------------------------------------------------------ |
| 发送模板消息       | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 设置所属行业       | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 获取设置的行业信息 | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 获得模板 Id        | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 获取模板列表       | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 删除模板           | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 事件推送           | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |

#### 3.2.3 用户管理

**用户标签管理**

| 功能                   | 是否支持                                                     |
| ---------------------- | ------------------------------------------------------------ |
| 创建标签               | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 获取公众号已创建的标签 | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 编辑标签               | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 删除标签               | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 获取标签下粉丝列表     | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 批量为用户打标签       | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 批量为用户取消标签     | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |

### 3.3 微信小程序

#### 3.3.1 微信登录

| 功能             | 是否支持                                                     |
| ---------------- | ------------------------------------------------------------ |
| 登录凭证校验      | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |

#### 3.3.2 小程序码

| 功能             | 是否支持                                                     |
| ---------------- | ------------------------------------------------------------ |
|  获取小程序码 Unlimited  | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |

#### 3.3.3 订阅消息

| 功能             | 是否支持                                                     |
| ---------------- | ------------------------------------------------------------ |
|  发送订阅消息     | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |

