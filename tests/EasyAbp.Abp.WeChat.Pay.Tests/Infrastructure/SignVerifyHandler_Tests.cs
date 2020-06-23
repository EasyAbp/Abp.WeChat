﻿using System.Threading.Tasks;
using System.Xml;
using EasyAbp.Abp.WeChat.Pay.Infrastructure;
using EasyAbp.Abp.WeChat.Pay.Infrastructure.OptionResolve;
using Xunit;

namespace EasyAbp.Abp.WeChat.Pay.Tests.Infrastructure
{
    public class SignVerifyHandler_Tests : AbpWeChatPayTestBase
    {
        private readonly IWeChatPayHandler _signVerifyHandler;

        public SignVerifyHandler_Tests()
        {
            _signVerifyHandler = GetRequiredService<IWeChatPayHandler>();
        }

        [Fact]
        public async Task SignVerify_Test()
        {
            var xml = new XmlDocument();
            xml.LoadXml(@"<xml>
  <appid><![CDATA[wx2421b1c4370ec43b]]></appid>
  <attach><![CDATA[支付测试]]></attach>
  <bank_type><![CDATA[CFT]]></bank_type>
  <fee_type><![CDATA[CNY]]></fee_type>
  <is_subscribe><![CDATA[Y]]></is_subscribe>
  <mch_id><![CDATA[10000100]]></mch_id>
  <nonce_str><![CDATA[5d2b6c2a8db53831f7eda20af46e531c]]></nonce_str>
  <openid><![CDATA[oUpF8uMEb4qRXf22hE3X68TekukE]]></openid>
  <out_trade_no><![CDATA[1409811653]]></out_trade_no>
  <result_code><![CDATA[SUCCESS]]></result_code>
  <return_code><![CDATA[SUCCESS]]></return_code>
  <sign><![CDATA[44DBC1D1FCFE7B6579D4771098D75DE8]]></sign>
  <time_end><![CDATA[20140903131540]]></time_end>
  <total_fee>1</total_fee>
  <coupon_fee><![CDATA[10]]></coupon_fee>
  <coupon_count><![CDATA[1]]></coupon_count>
  <coupon_type><![CDATA[CASH]]></coupon_type>
  <coupon_id><![CDATA[10000]]></coupon_id>
  <trade_type><![CDATA[JSAPI]]></trade_type>
  <transaction_id><![CDATA[1004400740201409030005092168]]></transaction_id>
</xml>");
            await _signVerifyHandler.HandleAsync(new WeChatPayHandlerContext
            {
                WeChatRequestXmlData = xml
            });
        }
    }
}