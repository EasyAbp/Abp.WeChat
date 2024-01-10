namespace EasyAbp.Abp.WeChat.Pay.RequestHandling.Dtos;

public class NotifyHttpHeaderModel
{
    public string SerialNumber { get; set; }

    public string Timestamp { get; set; }

    public string Nonce { get; set; }

    public string Signature { get; set; }

    public NotifyHttpHeaderModel(string serialNumber, string timestamp, string nonce, string signature)
    {
        SerialNumber = serialNumber;
        Timestamp = timestamp;
        Nonce = nonce;
        Signature = signature;
    }
}