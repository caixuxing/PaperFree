using System.ComponentModel.DataAnnotations;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord
{
    /// <summary>
    /// 更新流程节点Cmd
    /// </summary>
    public class ModifyProcessNodeCmd : IRequest<bool>
    {
        /// <summary>
        /// 流程主键ID
        /// </summary>
        [Required]
        public string ProcessDesignId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        [Required]
        public string NodeName { get; set; }

        /// <summary>
        /// 节点代码值
        /// </summary>
        [Required]
        public string NodeCode { get; set; }

        /// <summary>
        /// 上级节点 上游节点（第一步默认为开始）否则上级审批节点
        /// </summary>
        [Required]
        public string UpperNodeId { get; set; }

        /// <summary>
        /// 下级节点 下游节点（最后一步结束）否则下级审批节点
        /// </summary>
        [Required]
        public string LowerNodeId { get; set; }

        /// <summary>
        /// 节点流程状态
        /// </summary>
        [Required]
        public int NodeMapWorkflowStatus { get; set; }

        /// <summary>
        /// 事件方向分支【JSON存储(通过、拒绝、驳回)】
        /// </summary>
        [Required]
        public List<EventDirectionType> EventDirectionBranch { get; set; }

        /// <summary>
        /// 是否可驳回指定节点 是否开启当前节点审批人员拥有回退到指定节点功能
        /// </summary>
        public bool? IsRejectToNode { get; set; }

        /// <summary>
        /// 排序号 顺序，值越小越靠前，否则反之
        /// </summary>
        [Required]
        public int OderNo { get; set; }

        /// <summary>
        /// 当前节点审批人集合
        /// </summary>
        [Required]
        public List<NodeApproverValueObj> CurrentNodeApprovers { get; set; }

        [Required]
        public string Id { get; private set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ModifyProcessNodeCmd SetId(string id)
        {
            Id = id;
            return this;
        }
    }

    /// <summary>
    /// 验证规则
    /// </summary>
    public class ModifyProcessNodeValidate : AbstractValidator<ModifyProcessNodeCmd>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ModifyProcessNodeValidate()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("更新主键ID不能为空！");
            RuleFor(x => x.NodeName).NotEmpty().WithMessage("节点名称不能为空！")
             .MaximumLength(20).WithMessage("节点名称长度不能超过20个字符");
            RuleFor(x => x.NodeCode).NotEmpty().WithMessage("节点编码不能为空！")
                .MaximumLength(20).WithMessage("节点编码长度不能超过20个字符");
            RuleFor(x => x.CurrentNodeApprovers).NotEmpty().WithMessage("节点审批人不能为空！");
            RuleForEach(order => order.CurrentNodeApprovers).SetValidator(new NodeApproverValueObjValidator());
            RuleFor(x => x.EventDirectionBranch).NotEmpty().WithMessage("事件方向不能为空！")
                .Must(BeValidEnumValues).WithMessage("事件方向列表中包含无效的枚举值");
        }

        private bool BeValidEnumValues(List<EventDirectionType> values)
        {
            return values.TrueForAll(x => Enum.IsDefined(typeof(EventDirectionType), x));
        }
    }
}