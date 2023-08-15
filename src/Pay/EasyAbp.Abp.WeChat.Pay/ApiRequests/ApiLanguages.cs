namespace EasyAbp.Abp.WeChat.Pay.ApiRequests;

public static class ApiLanguages
{
    public const string HongKong = "zh_HK";
    public const string SimplifiedChinese = "zh_CN";
    public const string TraditionalChinese = "zh_TW";
    public const string English = "en_US";

    public static string DefaultLanguage { get; set; } = English;
}