using EasyAbp.Abp.WeChat.Common.SharedCache.StackExchangeRedis.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace EasyAbp.Abp.WeChat.Common.SharedCache.StackExchangeRedis.Settings
{
    public class SharedCacheStackExchangeRedisSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(DenturePlusSettings.MySetting1));
            
            context.Add(new SettingDefinition(
                SharedCacheStackExchangeRedisSettings.RedisConfiguration,
                null,
                L("SharedCacheRedisConfiguration"),
                L("SharedCacheRedisConfigurationDescription")));
        }
        
        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<SharedCacheStackExchangeRedisResource>(name);
        }
    }
}
