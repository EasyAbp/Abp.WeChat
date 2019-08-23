namespace Zony.Abp.WeiXin.Official
{
    public class AbpWeiXinOfficialOptions
    {
        public string Token { get; set; }

        public string AppId { get; set; }

        public string AppSecret { get; set; }

        public string EncodingAesKey { get; set; }

        public string OAuthRedirectUrl { get; set; }
    }
}