namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto;

/// <summary>
/// 科室响应模型
/// </summary>
public record FindOrgDepartmentDto
{
    public string ID { get; set; }

    public string ORG_CODE { get; set; }

    public string HOSP_CODE { get; set; }

    public string DEPT_ID { get; set; }

    public string KEYWORDS { get; set; }

    public string DEPT_NAME { get; set; }

    public string WB_CODE { get; set; }

    public string PY_CODE { get; set; }

    public string ENABLED { get; set; }
}