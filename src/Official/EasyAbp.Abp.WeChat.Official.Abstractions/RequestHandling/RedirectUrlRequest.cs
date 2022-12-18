namespace EasyAbp.Abp.WeChat.OpenPlatform.RequestHandling
{
    public class RedirectUrlRequest
    {
        public string Code { get; set; }

        public int State { get; set; }
    }
}