namespace EasyAbp.Abp.WeChat.Pay.Services.ErrorCodes;

public class BillErrorCode : BasicPaymentErrorCode
{
    /// <summary>
    /// 描述: 账单文件不存在。<br/>
    /// 解决方案: 请检查当前商户号是否在指定日期有交易或退款发生。
    /// </summary>
    public const string NoStatementExist = "NO_STATEMENT_EXIST";

    /// <summary>
    /// 描述: 账单文件正在生成。<br/>
    /// 解决方案: 请先检查当前商户号在指定日期内是否有成功的交易或退款，若有，则在T+1日上午8点后再重新下载。
    /// </summary>
    public const string StatementCreating = "STATEMENT_CREATING";
}