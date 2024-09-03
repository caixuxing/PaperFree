using System.ComponentModel.DataAnnotations;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.ArchiverMeumCmd
{
    public record 批量ModifyArchiverMeumCmd(List<ModifyArchiverMeumCmd> cmd) : IRequest<bool>;

    public record ModifyArchiverMeumCmd : IRequest<bool>
    {
        /// <summary>
        /// 目录名称
        /// </summary>
        [Required]
        public string MenuName { get;  set; }
        /// <summary>
        /// 上级目录ID
        /// </summary>
        public string? ParentId { get;  set; }
        /// <summary>
        /// 权限等级 根据权限设置医护所能操作的目录
        /// </summary>
        [Required]
        public string Permission { get; set; }
        /// <summary>
        /// 目录类型 目录类型（1、目录，2、节点）
        /// </summary>
        public string MeumType { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int Orderby { get; set; }
        /// <summary>
        /// 保密等级
        /// </summary>
        [Required]
        public string SecretLevel { get; set; }
        /// <summary>
        /// 是否高拍
        /// </summary>
        [Required]
        public WhetherType IsHighShots { get; set; }

        /// <summary>
        /// 是否签名
        /// </summary>
        [Required]
        public WhetherType IsSignature { get; set; }
        /// <summary>
        /// 是否适用所有机构 是否适用所有机构（0、否；1、是）
        /// </summary>
        [Required]
        public WhetherType IsAllorg { get; set; }
        /// <summary>
        /// 机构编码
        /// </summary>
        [Required]
        public string OrgCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        [Required]
        public string HospCode { get; set; }
        public string Id { get; private set; }
        public ModifyArchiverMeumCmd SetId(string id)
        {
            Id = id;
            return this;
        }
        /// <summary>
        /// 验证规则
        /// </summary>
        public class ModifyArchiverMeumValidate : AbstractValidator<ModifyArchiverMeumCmd>
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            public ModifyArchiverMeumValidate()
            {
                RuleFor(x => x.ParentId).MaximumLength(50).WithMessage("目录编码字段长度不能超过50个字符");
                RuleFor(x => x.MenuName).NotEmpty().WithMessage("目录名称值不能为空！")
                    .MaximumLength(20).WithMessage("目录名称字段长度不能超过20个字符");
                RuleFor(x => x.Permission).NotEmpty().WithMessage("权限等级值不能为空！")
                    .MaximumLength(20).WithMessage("权限等级字段长度不能超过20个字符");
                RuleFor(x => x.Orderby).NotEmpty().WithMessage("排序不能为空！");
                RuleFor(x => x.OrgCode).MaximumLength(50).WithMessage("机构编码长度不能超过").NotEmpty().WithMessage("机构编码不能为空");
                RuleFor(x => x.HospCode).MaximumLength(50).WithMessage("院区编码长度不能超过").NotEmpty().WithMessage("院区编码不能为空");
                //RuleFor(x => x.SecretLevel).NotEmpty().WithMessage("是否保密等级值不能为空！")
                //     .MaximumLength(4).WithMessage("否使保密等级字段长度不能超过4个字符");
                //RuleFor(x => x.IsSignature).MaximumLength(4).WithMessage("是否签名长度不能超过4个字符");//.NotEmpty().WithMessage("是否签名值不能为空！")

                //RuleFor(x => x.IsHighShots).NotEmpty().WithMessage("是否高拍值不能为空！")
                //     .MaximumLength(4).WithMessage("是否高拍长度不能超过4个字符");
                //RuleFor(x => x.IsAllorg).NotEmpty().WithMessage("是否适用所有机构值不能为空！")
                //     .MaximumLength(4).WithMessage("是否适用所有机构字段长度不能超过4个字符");
            }
        }
    }
}