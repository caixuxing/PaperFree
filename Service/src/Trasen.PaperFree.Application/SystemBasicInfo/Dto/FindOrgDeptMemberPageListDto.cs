namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto;

public record FindOrgDeptMemberPageListDto
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
    public string DEPT_ID { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string MEMBER_ID { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string MASTER_FLAG { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string CREATE_USER { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string UPDATE_USER { get; set; }

    /// <summary>
    ///
    /// </summary>
    public DateTime? CREATE_DATE { get; set; }

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
    public string USERNAME { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string USERCODE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string MEMBER_ROLE { get; set; }

    /// <summary>
    ///
    /// </summary>
    public Array? MEMBER_ROLE_LIST { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? SITLEVELID { get; set; }

    /// <summary>
    ///
    /// </summary>
    public string? EMR_MEMBER_ID { get; set; }

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
    public int PageIndex { get; set; }

    /// <summary>
    ///
    /// </summary>
    public int PageSize { get; set; }
}