# Abp.WeChat

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FAbp.WeChat%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.Abp.WeChat.Common.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.WeChat.Common)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.Abp.WeChat.Common.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.Abp.WeChat.Common)
[![Discord online](https://badgen.net/discord/online-members/xyg8TrRa27?label=Discord)](https://discord.gg/xyg8TrRa27)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/Abp.WeChat?style=social)](https://www.github.com/EasyAbp/Abp.WeChat)

针对 ABP vNext 框架，封装的微信 SDK 模块，包含对微信小程序、微信公众号、企业微信、微信小商店、微信智能对话、微信开放平台（包括第三方平台）、微信支付。

> 推荐你使用 EasyAbp 封装的[微信管理模块](https://github.com/EasyAbp/WeChatManagement)，它依赖本模块做了二次封装，提供应用级的各项功能。

## 一、简要介绍

**EasyAbp.Abp.WeChat** 库是针对于微信应用与微信支付 API 进行了二次封装的模块，与 ABP vNext 框架深度集成。开发人员如果是基于 ABP vNext 框架开发项目，集成本模块以后，可以快速实现同微信的对接。

## 二、API 支持情况

| 功能             | 支持情况                                                      | 文档                                    |
| ---------------- | ------------------------------------------------------------- | --------------------------------------- |
| 微信支付模块     | ![Support](https://img.shields.io/badge/-部分支持-orange.svg) | [访问文档](/docs/WeChatPay.md)          |
| 微信公众号模块   | ![Support](https://img.shields.io/badge/-部分支持-orange.svg) | [访问文档](/docs/WeChatOfficial.md)     |
| 微信小程序模块   | ![Support](https://img.shields.io/badge/-部分支持-orange.svg) | [访问文档](/docs/WeChatMiniProgram.md)  |
| 微信开放平台模块 | ![Support](https://img.shields.io/badge/-部分支持-orange.svg) | [访问文档](/docs/WeChatOpenPlatform.md) |
| 企业微信模块     | ![Support](https://img.shields.io/badge/-不支持-red.svg)      |                                         |
| 微信小商店模块   | ![Support](https://img.shields.io/badge/-不支持-red.svg)      |                                         |
| 微信智能对话模块 | ![Support](https://img.shields.io/badge/-不支持-red.svg)      |                                         |

> 关于多实例争抢 AccessToken 或类似票券的问题，请参考：https://github.com/EasyAbp/Abp.WeChat/issues/74
