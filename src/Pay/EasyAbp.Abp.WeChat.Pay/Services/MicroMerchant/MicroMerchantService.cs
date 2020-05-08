using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using JetBrains.Annotations;
using Volo.Abp;
using EasyAbp.Abp.WeChat.Pay.Extensions;
using EasyAbp.Abp.WeChat.Pay.Models;

namespace EasyAbp.Abp.WeChat.Pay.Services.MicroMerchant
{
    public class MicroMerchantService : WeChatPayService
    {
        private const string SubmitUrl = "https://api.mch.weixin.qq.com/applyment/micro/submit";
        private const string GetStateUrl = "https://api.mch.weixin.qq.com/applyment/micro/getstate";
        private const string GetCertificateUrl = "https://api.mch.weixin.qq.com/risk/getcertficates";
        private const string UploadMediaUrl = "https://api.mch.weixin.qq.com/secapi/mch/uploadmedia";

        private const long MaxMediaFileSize = 1024 * 1024 * 2;

        /// <summary>
        /// 根据商户号获取平台证书。
        /// </summary>
        /// <param name="mchId">微信支付分配的商户号。</param>
        public async Task<XmlDocument> GetCertificateAsync(string mchId)
        {
            var options = await GetAbpWeChatPayOptions();

            var request = new WeChatPayParameters();
            request.AddParameter("mch_id", mchId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());
            request.AddParameter("sign_type", "HMAC-SHA256");

            request.AddParameter("sign",
                SignatureGenerator.Generate(request, new HMACSHA256(Encoding.UTF8.GetBytes(options.ApiKey)), options.ApiKey));

            return await RequestAndGetReturnValueAsync(GetCertificateUrl, request);
        }

        /// <summary>
        /// 图片上传功能，用于上传证件照片等数据。
        /// </summary>
        /// <param name="mchId">服务商商户号或渠道号。</param>
        /// <param name="imagePath">图片的文件路径。</param>
        /// <returns>图片关联的 Id。</returns>
        public async Task<string> UploadMediaAsync(string mchId, string imagePath)
        {
            if (new FileInfo(imagePath).Length > MaxMediaFileSize)
            {
                throw new ArgumentOutOfRangeException(nameof(imagePath), "指定的图像文件大小超过 2 M。");
            }

            var bytes = File.ReadAllBytes(imagePath);
            var mediaHash = MD5.Create().ComputeHash(bytes).Select(b => b.ToString("X2")).JoinAsString("");

            // 构建并计算签名值。
            var parameters = new WeChatPayParameters();
            parameters.AddParameter("mch_id", mchId);
            parameters.AddParameter("media_hash", mediaHash);

            var options = await GetAbpWeChatPayOptions();

            var sign = SignatureGenerator.Generate(parameters, MD5.Create(), options.ApiKey);

            // 构建表单请求。
            var fileContent = new ByteArrayContent(bytes);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

            var form = new MultipartFormDataContent
            {
                {new StringContent(mchId), "\"mch_id\""},
                {new ByteArrayContent(bytes), "\"media\"", $"\"{HttpUtility.UrlEncode(Path.GetFileName(imagePath))}\""},
                {new StringContent(mediaHash), "\"media_hash\""},
                {new StringContent(sign), "\"sign\""}
            };

            // 处理 Boundary 蛋疼的引号问题，参考 https://developers.de/blogs/damir_dobric/archive/2013/09/10/problems-with-webapi-multipart-content-upload-and-boundary-quot-quotes.aspx 。
            var boundaryValue = form.Headers.ContentType.Parameters.Single(p => p.Name == "boundary");
            boundaryValue.Value = boundaryValue.Value.Replace("\"", String.Empty);

            using var client = HttpClientFactory.CreateClient("WeChatPay");
            using var resp = await client.PostAsync(UploadMediaUrl, form);

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(await resp.Content.ReadAsStringAsync());
            return xmlDocument.SelectSingleNode("/xml/media_id")?.InnerText;
        }

        /// <summary>
        /// 使用申请入驻接口提交你的小微商户资料，申请后一般 5 分钟左右可以查询到具体的申请结果。
        /// </summary>
        /// <param name="version">接口版本号，固定版本号为 3.0。</param>
        /// <param name="certSn">平台证书序列号，通过 <see cref="GetCertificateAsync"/> 接口获取。</param>
        /// <param name="mchId">服务商的商户号。</param>
        /// <param name="businessCode">业务申请编号，服务商自定义的商户唯一编号。每个编号对应一个申请单，每个申请单审核通过后会生成一个微信支付商户号。</param>
        /// <param name="idCardCopy">身份证人像面照片，请使用 <see cref="UploadMediaAsync"/> 接口预先上传图片，传递其 media_id 。</param>
        /// <param name="idCardNational">身份证国徽面照片，请使用 <see cref="UploadMediaAsync"/> 接口预先上传图片，传递其 media_id。</param>
        /// <param name="idCardName">身份证姓名，请填写小微商户本人身份证上的姓名，使用 <see cref="WeChatPayToolUtility"/> 提供的加密方法进行加密。</param>
        /// <param name="idCardNumber">身份证号码，15 位数字 或  17 位数字 + 1 位数字 | X ，使用 <see cref="WeChatPayToolUtility"/> 提供的加密方法进行加密。</param>
        /// <param name="idCardValidTime">身份证有效期限，格式应该和 ["1970-01-01","长期"] 一致，结束时间需要大于开始时间，需要和上传的身份证内容一致。</param>
        /// <param name="accountName">开户名称，必须与身份证姓名一致，使用 <see cref="WeChatPayToolUtility"/> 提供的加密方法进行加密。</param>
        /// <param name="accountBank">开户银行，请参考 <see cref="AccountBanks"/> 类型中定义的银行列表。</param>
        /// <param name="bankAddressCode">开户银行省市编码，至少精确到市，请参考 https://pay.weixin.qq.com/wiki/doc/api/xiaowei.php?chapter=22_1 提供的对照表。</param>
        /// <param name="bankName">
        /// 开户银行全称（含支行），17 家直连银行无需填写(在 <see cref="AccountBanks"/> 定义的)，其他银行请务必填写。<br/>
        /// 需填写银行全称，如 "深圳农村商业银行 XXX 支行"，详细信息请参考 https://pay.weixin.qq.com/wiki/doc/api/xiaowei.php?chapter=22_1。</param>
        /// <param name="accountNumber">银行账号，数字，长度遵循系统支持的对私卡号长度要求，使用 <see cref="WeChatPayToolUtility"/> 提供的加密方法进行加密。</param>
        /// <param name="storeName">
        /// 门店名称，最长 50 个中文字符。<br/>
        /// 门店场所：填写门店名称<br/>
        /// 流动经营 / 便民服务：填写经营 / 服务名称<br/>
        /// 线上商品 / 服务交易：填写线上店铺名称<br/>
        /// </param>
        /// <param name="storeAddressCode">
        /// 门店省市编码，至少精确到市，参考 https://pay.weixin.qq.com/wiki/doc/api/xiaowei.php?chapter=22_1 提供的对照表。<br/>
        /// 门店场所：填写门店省市编码<br/>
        /// 流动经营 / 便民服务：填写经营 / 服务所在地省市编码<br/>
        /// 线上商品 / 服务交易：填写卖家所在地省市编码<br/>
        /// </param>
        /// <param name="storeStreet">
        /// 门店街道名称，最长 500 个中文字符（无需填写省市信息）。<br/>
        /// 门店场所：填写店铺详细地址，具体区 / 县及街道门牌号或大厦楼层<br/>
        /// 流动经营 / 便民服务：填写 “无 "<br/>
        /// 线上商品 / 服务交易：填写电商平台名称<br/>
        /// </param>
        /// <param name="storeLongitude">门店经度。</param>
        /// <param name="storeLatitude">门店纬度。</param>
        /// <param name="storeEntrancePic">
        /// 门店门口照片，请使用 <see cref="UploadMediaAsync"/> 接口预先上传图片，传递其 media_id 。<br/>
        /// 门店场所：提交门店门口照片，要求招牌清晰可见<br/>
        /// 流动经营 / 便民服务：提交经营 / 服务现场照片<br/>
        /// 线上商品 / 服务交易：提交店铺首页截图<br/>
        /// </param>
        /// <param name="indoorPic">
        /// 店内环境照片，请使用 <see cref="UploadMediaAsync"/> 接口预先上传图片，传递其 media_id 。<br/>
        /// 门店场所：提交店内环境照片<br/>
        /// 流动经营 / 便民服务：可提交另一张经营 / 服务现场照片<br/>
        /// 线上商品 / 服务交易：提交店铺管理后台截图<br/>
        /// </param>
        /// <param name="addressCertification">经营场地证明，门面租赁合同扫描件或经营场地证明（需与身份证同名），请使用 <see cref="UploadMediaAsync"/> 接口预先上传图片，传递其 media_id 。</param>
        /// <param name="merchantShortName">商户简称，UTF-8 格式，中文占 3 个字节，即最多 16 个汉字长度。将在支付完成页向买家展示，需与商家的实际经营场景相符。</param>
        /// <param name="servicePhone">客服电话，UTF-8 格式，中文占 3 个字节，即最多 16 个汉字长度。在交易记录中向买家展示，请确保电话畅通以便平台回拨确认。</param>
        /// <param name="productDesc">
        /// 售卖商品 / 提供服务描述，请填写以下描述之一：<br/>
        /// 餐饮、线下零售、居民生活服务、休闲娱乐、交通出行、其他。
        /// </param>
        /// <param name="rate">费率，由服务商指定，具体有效费率枚举值，可以参考 https://pay.weixin.qq.com/wiki/doc/api/xiaowei.php?chapter=22_1。</param>
        /// <param name="businessAdditionDesc">补充说明，可填写需要额外说明的文字。</param>
        /// <param name="businessAdditionPics">补充材料，最多可上传 5 张照片，请填写已预先上传图片生成好的 MediaID，例如 ["123","456"]。</param>
        /// <param name="contact">
        /// 超级管理员姓名，和身份证姓名一致。超级管理员需在开户后进行签约，并可接收日常重要管理信息和进行资金操作，请确定其为商户法定代表人或负责人。<br/>
        /// 使用 <see cref="WeChatPayToolUtility"/> 提供的加密方法进行加密。
        /// </param>
        /// <param name="contactPhone">手机号码，11 位数字，手机号码，使用 <see cref="WeChatPayToolUtility"/> 提供的加密方法进行加密。</param>
        /// <param name="contactEmail">联系邮箱，需要带 @，遵循邮箱格式校验，使用 <see cref="WeChatPayToolUtility"/> 提供的加密方法进行加密。</param>
        /// <returns></returns>
        public async Task<XmlDocument> SubmitAsync(string version, string certSn, string mchId, string businessCode,
            string idCardCopy, string idCardNational, string idCardName, string idCardNumber, string idCardValidTime,
            string accountName, string accountBank, string bankAddressCode, [CanBeNull] string bankName, string accountNumber,
            string storeName, string storeAddressCode, string storeStreet, [CanBeNull] string storeLongitude, [CanBeNull] string storeLatitude,
            string storeEntrancePic, string indoorPic, [CanBeNull] string addressCertification, [CanBeNull] string merchantShortName,
            string servicePhone, string productDesc, string rate, [CanBeNull] string businessAdditionDesc, [CanBeNull] string businessAdditionPics,
            string contact, string contactPhone, [CanBeNull] string contactEmail)
        {
            var request = new WeChatPayParameters();

            request.AddParameter("version", version);
            request.AddParameter("cert_sn", certSn);
            request.AddParameter("mch_id", mchId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());
            request.AddParameter("sign_type", "HMAC-SHA256");
            request.AddParameter("business_code", businessCode);
            request.AddParameter("id_card_copy", idCardCopy);
            request.AddParameter("id_card_national", idCardNational);
            request.AddParameter("id_card_name", idCardName);
            request.AddParameter("id_card_number", idCardNumber);
            request.AddParameter("id_card_valid_time", idCardValidTime);
            request.AddParameter("account_name", accountName);
            request.AddParameter("account_bank", accountBank);
            request.AddParameter("bank_address_code", bankAddressCode);
            request.AddParameter("bank_name", bankName);
            request.AddParameter("account_number", accountNumber);
            request.AddParameter("store_name", storeName);
            request.AddParameter("store_address_code", storeAddressCode);
            request.AddParameter("store_street", storeStreet);
            request.AddParameter("store_longitude", storeLongitude);
            request.AddParameter("store_latitude", storeLatitude);
            request.AddParameter("store_entrance_pic", storeEntrancePic);
            request.AddParameter("indoor_pic", indoorPic);
            request.AddParameter("address_certification", addressCertification);
            request.AddParameter("merchant_shortname", merchantShortName);
            request.AddParameter("service_phone", servicePhone);
            request.AddParameter("product_desc", productDesc);
            request.AddParameter("rate", rate);
            request.AddParameter("business_addition_desc", businessAdditionDesc);
            request.AddParameter("business_addition_pics", businessAdditionPics);
            request.AddParameter("contact", contact);
            request.AddParameter("contact_phone", contactPhone);
            request.AddParameter("contact_email", contactEmail);

            var options = await GetAbpWeChatPayOptions();

            request.AddParameter("sign", SignatureGenerator.Generate(request, new HMACSHA256(Encoding.UTF8.GetBytes(options.ApiKey)), options.ApiKey));
            return await RequestAndGetReturnValueAsync(SubmitUrl, request);
        }

        /// <summary>
        /// 使用申请入驻接口提交小微商户资料后，一般 5 分钟左右可以通过该查询接口查询具体的申请结果。
        /// </summary>
        /// <param name="version">接口版本号，默认值 1.0。</param>
        /// <param name="mchId">服务商的商户号。</param>
        /// <param name="applyentId">商户申请单号，微信支付分配的申请单号。</param>
        /// <param name="businessCode">业务申请编号，服务商自定义的商户唯一编号，当 <paramref name="applyentId"/>  已填写时，此字段无效。</param>
        /// <returns></returns>
        public async Task<XmlDocument> GetStateAsync(string version, string mchId, string applyentId, string businessCode)
        {
            var request = new WeChatPayParameters();
            request.AddParameter("version", version);
            request.AddParameter("mch_id", mchId);
            request.AddParameter("nonce_str", RandomHelper.GetRandom());
            request.AddParameter("sign_type", "HMAC-SHA256");
            request.AddParameter("applyment_id", applyentId);
            request.AddParameter("business_code", businessCode);

            var options = await GetAbpWeChatPayOptions();
            request.AddParameter("sign", SignatureGenerator.Generate(request, new HMACSHA256(Encoding.UTF8.GetBytes(options.ApiKey)), options.ApiKey));

            return await RequestAndGetReturnValueAsync(GetStateUrl, request);
        }
    }
}