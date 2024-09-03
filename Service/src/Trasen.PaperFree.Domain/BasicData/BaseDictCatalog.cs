using System.ComponentModel.DataAnnotations.Schema;

namespace Trasen.PaperFree.Domain.BasicData
{
    /// <summary>
    /// 字典分类
    /// </summary>
    [Table("BASE_DICT_CATALOG")]
    public class BaseDictCatalog
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 分类编码
        /// </summary>
        public string CODE { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 上级编码
        /// </summary>
        public string? PARENT_CODE { get; set; }

        /// <summary>
        /// 上级名称
        /// </summary>
        public string? PARENT_NAME { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>
        public string? WB_CODE { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        public string? PY_CODE { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string? VERSION_ID { get; set; }

        /// <summary>
        /// 所属标准
        /// </summary>
        public string? STANDARD { get; set; }

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
        public DateTime CREATE_DATE { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UPDATE_DATE { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int? ORDER_NUM { get; set; }

        /// <summary>
        /// 是否删除（Y已删除N未删除）
        /// </summary>
        public string IS_DELETE { get; set; }
    }
}