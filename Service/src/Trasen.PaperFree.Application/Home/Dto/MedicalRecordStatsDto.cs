using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trasen.PaperFree.Application.Home.Dto
{
    public record MedicalRecordStatsDto
    {
        public List<Submit> submits=new List<Submit>();
        public List<Archive> archives=new List<Archive>();

    }
    /// <summary>
    /// 提交人数 按状态、月份分组
    /// </summary>
    public record Submit {
        public string SubmitMonth { get; set; }
        public int SubmitNumber { get; set; }
    }
    /// <summary>
    /// 归档人数 按状态、月份分组
    /// </summary>
    public record Archive {
    public string ArchiveMonth {  get; set; }
    public int ArchiveNumber {  get; set; }
    }
}
