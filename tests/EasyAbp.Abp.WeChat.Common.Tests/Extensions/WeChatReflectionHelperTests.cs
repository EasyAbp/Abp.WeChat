using EasyAbp.Abp.WeChat.Common.Extensions;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.WeChat.Common.Tests.Extensions;

public class WeChatReflectionHelperTests : AbpWeChatCommonTestBase<AbpWeChatCommonModule>
{
    [Fact]
    public void Should_Convert_To_Query_String()
    {
        // Arrange
        var obj = new
        {
            Name = "test",
            Age = 18
        };

        // Act
        var queryString = WeChatReflectionHelper.ConvertToQueryString(obj);

        // Assert
        queryString.ShouldBe("Name=test&Age=18");
    }
    
    public class JsonPropertyTestClass
    {
        [JsonProperty("test")]
        public string Name { get; set; }
        
        public int Age { get; set; }
    }
    
    [Fact]
    public void Should_Convert_To_Query_String_With_Json_Property_Name()
    {
        // Arrange & Act
        var queryString = WeChatReflectionHelper.ConvertToQueryString(new JsonPropertyTestClass
        {
            Name = "admin",
            Age = 18
        });
        
        // Assert
        queryString.ShouldBe("test=admin&Age=18");
    }
}