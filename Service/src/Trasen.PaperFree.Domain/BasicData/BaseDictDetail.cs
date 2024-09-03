using System.ComponentModel.DataAnnotations.Schema;

namespace Trasen.PaperFree.Domain.BasicData
{
    /// <summary>
    /// 字典明细
    /// </summary>
    [Table("BASE_DICT_DETAIL")]
    public class BaseDictDetail
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        public string CODE { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 国标码
        /// </summary>
        public string? GB_CODE { get; set; }

        /// <summary>
        /// 分类编码
        /// </summary>
        public string CATALOG_CODE { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string? CATALOG_NAME { get; set; }

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
        /// 是否默认（1是0否）
        /// </summary>
        public string? IS_DEFAULT { get; set; }

        /// <summary>
        /// 所属标准
        /// </summary>
        public string? STANDARD { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string? CREATE_USER { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string UPDATE_USER { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CREATE_DATE { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UPDATE_DATE { get; set; }

        /// <summary>
        /// 是否删除（Y是N否）
        /// </summary>
        public string IS_DELETE { get; set; }

        /// <summary>
        /// 电子病历关联明细ID
        /// </summary>
        public int EMR_DETAIL_ID { get; set; }
    }
}