# Abp.WeChat

[![NuGet](https://img.shields.io/nuget/v/EasyAbp.Abp.WeChat.Common.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.WeChat.Common)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.Abp.WeChat.Common.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.WeChat.Common)

专门为 ABP vNext 封装的微信模块，包括微信公众号和微信支付相关接口。

## 一、简要介绍

**EasyAbp.Abp.WeChat** 库是针对于微信公众号与微信支付 API 进行了二次封装的模块，与 ABP vNext 框架深度集成。开发人员如果是基于 ABP vNext  框架开发项目，集成本模块以后，可以快速实现同微信开放平台的对接。

## 二、使用方式

**微信公众号模块**：[点击查看说明](./doc/WeChatOfficial.md)

**微信支付模块：**[点击查看说明](./doc/WeChatPay.md)

## 三、API 支持情况

### 3.1 微信支付

#### 3.1.1 JS API 支付

| 功能             | 是否支持                                                     |
| ---------------- | ------------------------------------------------------------ |
| 统一下单         | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 查询订单         | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 关闭订单         | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 申请退款         | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 查询退款         | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 下载对账单       | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 下载资金账单     | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 支付结果通知     | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 交易保障         | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 退款结果通知     | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 拉取订单评价数据 | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |

### 3.2 微信公众号

#### 3.2.1 自定义菜单

| 功能               | 是否支持                                                     |
| ------------------ | ------------------------------------------------------------ |
| 创建自定义菜单     | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 查询自定义菜单     | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 删除自定义菜单     | ![Support](https://img.shields.io/badge/-支持-brightgreen.svg) |
| 自定义菜单事件推送 | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 获取自定义菜单配置 | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |

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
| 设置所属行业       | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 获取设置的行业信息 | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 获得模板 Id        | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 获取模板列表       | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 删除模板           | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
| 事件推送           | ![NotSupport](https://img.shields.io/badge/-%E4%B8%8D%E6%94%AF%E6%8C%81-red.svg) |
