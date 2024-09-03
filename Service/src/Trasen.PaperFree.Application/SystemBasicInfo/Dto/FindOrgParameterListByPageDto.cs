namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto;

/// <summary>
/// 查找机构参数响应模型
/// </summary>
public record FindOrgParameterListByPageDto
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
    public string NetIn_ORG_CODE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string HOSP_CODE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string CODE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string VALUE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public DateTime? CREATE_DATE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string CREATE_USER { get; set; }

    /// <summary>
    ///
    /// </summary>
    public DateTime? UPDATE_DATE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? UPDATE_USER { get; set; }

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
    public string LOGIN_LOAD { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string GROUP_ID { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string GROUP_NAME { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string DESCRIPTION { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string KEYWORDS { get; set; }

    /// <summary>
    ///
    /// </summary>
    public Array? IDLIST { get; set; }

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

/// <summary>
/// 机构响应模型
/// </summary>
public class FindOrgDto
{
    public string ID { get; set; }
    public string ORG_CODE { get; set; }
    public string ORG_NAME { get; set; }

    public string PARENT_CODE { get; set; }

    public string ORG_TYPE { get; set; }

    public string SHORT_NAME { get; set; }

    public string ORG_LEVEL { get; set; }

    public string PINYINCODE { get; set; }

    public string FIVECODE { get; set; }
    public string ATTRIBUTIONPROVINCE { get; set; }
    public string ATTRIBUTIONCITY { get; set; }
}