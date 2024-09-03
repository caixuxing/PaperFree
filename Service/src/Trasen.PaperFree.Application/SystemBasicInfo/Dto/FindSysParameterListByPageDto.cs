namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto;

/// <summary>
/// 系统参数响应模型
/// </summary>
public record FindSysParameterListByPageDto
{
    public string ID { get; set; }

    public string CODE { get; set; }

    public string VALUE { get; set; }

    public string DATATYPE { get; set; }

    public string? READ_TYPE { get; set; }

    public string? IS_LOCAL { get; set; }

    public string DESCRIPTION { get; set; }

    public string GROUP_ID { get; set; }

    public string GROUP_NAME { get; set; }
    public DateTime CREATE_DATE { get; set; }

    public string CREATE_USER { get; set; }
    public DateTime? UPDATE_DATE { get; set; }
    public string? UPDATE_USER { get; set; }

    public string IS_DELETE { get; set; }

    public Array? CODELIST { get; set; }

    public Array? IDLIST { get; set; }

    public string CATALOG_CODE { get; set; }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }
}