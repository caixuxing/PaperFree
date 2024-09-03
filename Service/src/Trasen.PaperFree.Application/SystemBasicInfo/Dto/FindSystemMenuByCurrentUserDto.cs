namespace Trasen.PaperFree.Application.SystemBasicInfo.Dto;

/// <summary>
/// 系统菜单
/// </summary>
public class FindSystemMenuByCurrentUserDto
{
    public string SYSTEM_CODE { get; set; }

    public string MENU_CODE { get; set; }

    public string MENU_NAME { get; set; }

    public string SUPER_CODE { get; set; }

    public string SERIAL_NUMBER { get; set; }

    public int MENU_LEVEL { get; set; }

    public string MENU_DESC { get; set; }

    public string DISPLAY_ICO { get; set; }

    public string NAME_SPACE { get; set; }

    public string WINDOWS_NAME { get; set; }

    public string ASSEMBLY_NAME { get; set; }

    public string WINDOWS_STATUS { get; set; }

    public string ICO_NUMBER { get; set; }

    public string WINDOWS_OPENTYPE { get; set; }

    public string TOOLBAR_FLAG { get; set; }

    public string WINDOWS_TYPE { get; set; }

    public string WINDOWS_PARAMETER { get; set; }

    public string URL_ADDRESS { get; set; }

    public string MENU_SHORT { get; set; }

    public string CLIENT_TYPE { get; set; }
}