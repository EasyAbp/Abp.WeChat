using System.Reflection;
using System.Text;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Common.Extensions;

public static class WeChatReflectionHelper
{
    // TODO: This method has performance issues because it uses reflection; it can be optimized using expression trees.
    public static string ConvertToQueryString(object obj)
    {
        var sb = new StringBuilder();
        var properties = obj.GetType().GetProperties();
        foreach (var propertyInfo in properties)
        {
            var value = propertyInfo.GetValue(obj);
            if (value == null)
            {
                continue;
            }

            var ignoreAttribute = propertyInfo.GetCustomAttribute<JsonIgnoreAttribute>();
            if(ignoreAttribute != null) continue;

            var jsonPropertyAttribute = propertyInfo.GetCustomAttribute<JsonPropertyAttribute>();
            var name = jsonPropertyAttribute?.PropertyName ?? propertyInfo.Name;
            var type = propertyInfo.PropertyType;
            if (type.IsPrimitive || type == typeof(string))
            {
                sb.Append($"{name}={value}&");
            }
        }

        return sb.ToString().TrimEnd('&');
    }
}