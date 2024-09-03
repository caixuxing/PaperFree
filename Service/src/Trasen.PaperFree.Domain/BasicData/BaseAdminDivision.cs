using System.ComponentModel.DataAnnotations.Schema;

namespace Trasen.PaperFree.Domain.BasicData
{
    /// <summary>
    /// 行政区划信息
    /// </summary>
    [Table("BASE_ADMIN_DIVISION")]
    public class BaseAdminDivision
    {
        public string ID { get; set; }

        /// <summary>
        /// 地区编码
        /// </summary>
        public string AREA_CODE { get; set; }

        /// <summary>
        /// 地区名称
        /// </summary>
        public string AREA_NAME { get; set; }

        /// <summary>
        /// 地区简称
        /// </summary>
        public string? AREA_SHORTNAME { get; set; }

        /// <summary>
        /// 行政级别
        /// </summary>
        public string? ADMIN_LEVEL { get; set; }

        /// <summary>
        /// 上级地区编码
        /// </summary>
        public string PARENT_AREA_CODE { get; set; }

        /// <summary>
        /// 是否删除 Y删除 N不删除
        /// </summary>
        public string IS_DELETE { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CREATE_USER { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string? UPDATE_USER { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CREATE_DATE { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UPDATE_DATE { get; set; }
    }
}