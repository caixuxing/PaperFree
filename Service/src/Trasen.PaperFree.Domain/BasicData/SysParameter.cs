using System.ComponentModel.DataAnnotations.Schema;

namespace Trasen.PaperFree.Domain.BasicData
{
    [Table("SYS_PARAMETER")]
    public class SysParameter
    {
        public string ID { get; set; }
        public string CODE { get; set; }
        public string VALUE { get; set; }
        public string DATATYPE { get; set; }
        public int READ_TYPE { get; set; }
        public string IS_LOCAL { get; set; }
        public string DESCRIPTION { get; set; }
        public string GROUP_ID { get; set; }
        public DateTime CREATE_DATE { get; set; }
        public string CREATE_USER { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public string UPDATE_USER { get; set; }
        public string IS_DELETE { get; set; }
    }
}