namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.CaseManagement
{
    public class ModifyCaseShelfManagementCmd : IRequest<bool>
    {
        /// <summary>
        /// 仓库编号
        /// </summary>
        public string WarehouseNumber { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WarehouseName { get; set; }

        /// <summary>
        /// 病架号
        /// </summary>
        public string ShelfNo { get; set; }

        /// <summary>
        /// 存储号段
        /// </summary>
        public string StorageNumberSegment { get; set; }

        /// <summary>
        /// 行数
        /// </summary>
        public string LineNumber { get; set; }

        /// <summary>
        /// 列数
        /// </summary>
        public string NumberOlumns { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrgCode { get; set; }
        /// <summary>
        /// 院区编码
        /// </summary>
        public string HospCode { get; set; }
        /// <summary>
        /// 辖区编码
        /// </summary>
        public string InputCode { set; get; }
        public string Id { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ModifyCaseShelfManagementCmd SetId(string id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// 验证规则
        /// </summary>
        public class ModifyCaseShelfManagementValidate : AbstractValidator<ModifyCaseShelfManagementCmd>
        {
            /// <summary>
            /// 构造函数
            /// </summary>
            public ModifyCaseShelfManagementValidate()
            {
                RuleFor(x => x.WarehouseName).NotEmpty().WithMessage("仓库名称不能为空！")
                 .MaximumLength(20).WithMessage("仓库名称长度不能超过20个字符");
                RuleFor(x => x.WarehouseNumber).NotEmpty().WithMessage("仓库编号不能为空！")
                .MaximumLength(20).WithMessage("仓库编号长度不能超过20个字符");
                RuleFor(x => x.StorageNumberSegment).NotEmpty().WithMessage("存储号段不能为空！")
                .MaximumLength(20).WithMessage("存储号段长度不能超过20个字符");
                RuleFor(x => x.ShelfNo).NotEmpty().WithMessage("病架号不能为空！")
               .MaximumLength(20).WithMessage("病架号长度不能超过10个字符");
                RuleFor(x => x.LineNumber).NotEmpty().WithMessage("存储行不能为空！")
                .MaximumLength(10).WithMessage("存储行长度不能超过10个字符");
                RuleFor(x => x.NumberOlumns).NotEmpty().WithMessage("存储列不能为空！")
                .MaximumLength(10).WithMessage("存储列长度不能超过10个字符");
                RuleFor(x => x.Status).NotEmpty().WithMessage("使用状态不能为空！")
                .MaximumLength(10).WithMessage("使用状态长度不能超过10个字符");
            }
        }
    }
}