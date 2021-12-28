using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

// ReSharper disable StringLiteralTypo

namespace EasyAbp.Abp.WeChat.Official.Services.CustomMenu
{
    /// <summary>
    /// 自定义菜单的模型定义。
    /// </summary>
    public class CustomButton
    {
        /// <summary>
        /// 构造一个新的 <see cref="CustomButton"/> 对象。
        /// </summary>
        protected CustomButton()
        {
        }

        /// <summary>
        /// 构造一个新的 <see cref="CustomButton"/> 对象。
        /// </summary>
        /// <param name="name">菜单标题，不超过 16 个字节，子菜单不超过 60 个字节。</param>
        /// <param name="type">菜单的响应动作类型，具体定义可以参考 <see cref="CustomButtonType"/> 常量。</param>
        /// <param name="key">
        /// 菜单的 KEY 值，用于消息接口推送，不超过 128 字节。<br/>
        /// Click 等点击类型必须。
        /// </param>
        public CustomButton(string name, string type, string key = null)
        {
            if (type == CustomButtonType.Click && string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"当按钮的类型为 {nameof(CustomButtonType.Click)} 时，参数 {nameof(key)} 不能够为空。");
            }

            Name = name;
            Type = type;
            Key = key;
        }

        /// <summary>
        /// 菜单的响应动作类型，具体定义可以参考 <see cref="CustomButtonType"/> 常量。
        /// </summary>
        [Required]
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// 菜单标题，不超过 16 个字节，子菜单不超过 60 个字节。
        /// </summary>
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 菜单的 KEY 值，用于消息接口推送，不超过 128 字节。<br/>
        /// Click 等点击类型必须。
        /// </summary>
        [Required]
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// 网页链接，用户点击菜单可打开链接，不超过 1024 字节。<br/>
        /// 类型为: <see cref="CustomButtonType.View"/> 或 <see cref="CustomButtonType.MiniProgram"/> 时必填。
        /// </summary>
        /// <remarks>
        /// 当 <see cref="Type"/> 类型为 <see cref="CustomButtonType.MiniProgram"/> 时，不支持小程序的老版本客户端将打开本 URL。
        /// </remarks>
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// 调用新增永久素材接口返回的合法 media_id。<br/>
        /// 类型为: <see cref="CustomButtonType.SendMediaMessage"/> 或 <see cref="CustomButtonType.RedirectGraphicMessage"/> 时必填。
        /// </summary>
        [JsonProperty("media_id")]
        public string MediaId { get; set; }

        /// <summary>
        /// 小程序的 App ID（仅认证公众号可配置）。<br/>
        /// 类型为: <see cref="CustomButtonType.MiniProgram"/> 时必填。
        /// </summary>
        [JsonProperty("appid")]
        public string AppId { get; set; }

        /// <summary>
        /// 小程序的页面路径。<br/>
        /// 类型为: <see cref="CustomButtonType.MiniProgram"/> 时必填。
        /// </summary>
        [JsonProperty("pagepath")]
        public string PagePath { get; set; }

        /// <summary>
        /// 发布后获得的合法 article_id。<br/>
        /// 类型为: <see cref="CustomButtonType.Article"/> 时必填。
        /// </summary>
        [JsonProperty("sub_button")]
        public IList<CustomButton> SubButtons { get; set; }
    }
}