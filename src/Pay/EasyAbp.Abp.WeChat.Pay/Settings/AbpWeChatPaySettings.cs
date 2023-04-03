namespace EasyAbp.Abp.WeChat.Pay.Settings;

public static class AbpWeChatPaySettings
{
    private const string GroupName = "EasyAbp.Abp.WeChat.Pay";

    public const string MchId = GroupName + ".MchId";

    /// <summary>
    /// 注意，本值是密文！
    /// </summary>
    public const string ApiKey = GroupName + ".ApiKey";

    public const string IsSandBox = GroupName + ".IsSandBox";

    public const string NotifyUrl = GroupName + ".NotifyUrl";

    public const string RefundNotifyUrl = GroupName + ".RefundNotifyUrl";

    public const string CertificateBlobContainerName = GroupName + ".CertificateBlobContainerName";

    public const string CertificateBlobName = GroupName + ".CertificateBlobName";

    /// <summary>
    /// 注意，本值是密文！
    /// </summary>
    public const string CertificateSecret = GroupName + ".CertificateSecret";
}