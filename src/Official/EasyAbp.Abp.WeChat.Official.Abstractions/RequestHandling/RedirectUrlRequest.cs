namespace EasyAbp.Abp.WeChat.Official.RequestHandling
{
    public class RedirectUrlRequest
    {
        public string Code { get; set; }

        public int State { get; set; }
    }
}