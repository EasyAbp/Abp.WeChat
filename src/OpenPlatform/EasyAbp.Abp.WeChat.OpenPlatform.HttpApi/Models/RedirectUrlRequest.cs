namespace EasyAbp.Abp.WeChat.OpenPlatform.Models;

public class RedirectUrlRequest
{
    public string Code { get; set; }

    public int State { get; set; }
}