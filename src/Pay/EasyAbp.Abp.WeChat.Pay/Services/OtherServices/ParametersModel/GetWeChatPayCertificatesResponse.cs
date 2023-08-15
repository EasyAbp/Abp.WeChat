using System;
using System.Collections.Generic;
using EasyAbp.Abp.WeChat.Pay.Services.ParametersModel;
using Newtonsoft.Json;

namespace EasyAbp.Abp.WeChat.Pay.Services.OtherServices.ParametersModel;

public class GetWeChatPayCertificatesResponse : WeChatPayCommonErrorResponse
{
    [JsonProperty("data")] public IEnumerable<CertificateObject> Data { get; set; }

    public class CertificateObject
    {
        /// <summary>
        /// 平台证书的序列号。
        /// </summary>
        [JsonProperty("serial_no")]
        public string SerialNo { get; set; }

        /// <summary>
        /// 生效时间。
        /// </summary>
        [JsonProperty("effective_time")]
        public string EffectiveTimeString { get; set; }

        [JsonIgnore] public DateTime EffectiveTime => DateTime.Parse(EffectiveTimeString);

        /// <summary>
        /// 过期时间。 
        /// </summary>
        [JsonProperty("expire_time")]
        public string ExpireTimeString { get; set; }

        public DateTime ExpireTime => DateTime.Parse(ExpireTimeString);

        /// <summary>
        /// 加密的平台证书信息。
        /// </summary>
        [JsonProperty("encrypt_certificate")]
        public EncryptCertificate EncryptCertificateData { get; set; }
    }

    public class EncryptCertificate
    {
        /// <summary>
        /// 平台证书的加密算法。
        /// </summary>
        [JsonProperty("algorithm")]
        public string Algorithm { get; set; }

        /// <summary>
        /// 平台证书的随机字符串，用于后续解密操作。
        /// </summary>
        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        /// <summary>
        /// 证书的附加数据。
        /// </summary>
        [JsonProperty("associated_data")]
        public string AssociatedData { get; set; }

        /// <summary>
        /// 加密的证书密文，使用 BASE64 编码。
        /// </summary>
        [JsonProperty("ciphertext")]
        public string Ciphertext { get; set; }
    }
}