namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto;

/// <summary>
/// 个人参数响应模型
/// </summary>
public record FindPersonalParameterListByPageDto
{
    /// <summary>
    ///
    /// </summary>
    public string ID { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string ORG_CODE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string HOSP_CODE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string MEMBER_CODE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string MEMBER_NAME { get; set; }

    /// <summary>
    /// 测试i洗脑子
    /// </summary>
    public string CODE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string VALUE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string CREATE_USER { get; set; }

    /// <summary>
    ///
    /// </summary>
    public DateTime? CREATE_DATE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? UPDATE_USER { get; set; }

    /// <summary>
    ///
    /// </summary>
    public DateTime? UPDATE_DATE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string IS_DELETE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string PY_CODE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string WB_CODE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string GROUP_ID { get; set; }

    /// <summary>
    /// 残疾人士
    /// </summary>
    public string GROUP_NAME { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string PARAM_TYPE { get; set; }

    /// <summary>
    /// 胜多负少发
    /// </summary>
    public string DESCRIPTION { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string KEYWORDS { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string CATALOG_CODE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public Array? CODELIST { get; set; }

    /// <summary>
    ///
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    ///
    /// </summary>
    public int PageSize { get; set; }
}