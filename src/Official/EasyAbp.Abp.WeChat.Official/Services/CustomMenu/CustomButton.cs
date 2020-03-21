using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

// ReSharper disable StringLiteralTypo

namespace EasyAbp.Abp.WeChat.Official.Services.CustomMenu
{
    public class CustomButton
    {
        protected CustomButton()
        {
            
        }

        public CustomButton(string name,string type,string key = null)
        {
            if (type == CustomButtonType.Click && string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"当按钮的类型为 {nameof(CustomButtonType.Click)} 时，参数 {nameof(key)} 不能够为空。");
            }
            
            Name = name;
            Type = type;
            Key = key;
        }
        
        [Required]
        [JsonProperty("type")]
        public string Type { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [Required]
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
        
        [JsonProperty("media_id")]
        public string MediaId { get; set; }

        [JsonProperty("appid")]
        public string AppId { get; set; }

        [JsonProperty("pagepath")]
        public string PagePath { get; set; }
        
        [JsonProperty("sub_button")]
        public IList<CustomButton> SubButtons { get; set; }
    }
}