namespace Zony.Abp.WeiXin.Official.HttpApi.Models
{
    public class RedirectUrlRequest
    {
        public string Code { get; set; }

        public int State { get; set; }
    }
}