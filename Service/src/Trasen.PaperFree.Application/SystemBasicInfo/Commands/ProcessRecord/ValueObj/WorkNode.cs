using System.ComponentModel.DataAnnotations;
using Trasen.PaperFree.Domain.Shared.Enums.SystemBasicData;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Commands.ProcessRecord.ValueObj;

/// <summary>
/// 流程节点
/// </summary>
public record WorkNode
{
    /// <summary>
    /// 节点ID
    /// </summary>
    public string Id { get; set; } = string.Empty;
    /// <summary>
    /// 节点名称
    /// </summary>
    [Required]
    public string NodeName { get; set; } = string.Empty;
    /// <summary>
    /// 节点流程状态
    /// </summary>
    [Required]
    public int NodeMapWorkflowStatus { get; set; }
    /// <summary>
    /// 事件方向分支
    /// </summary>
    [Required]
    public List<EventDirectionType> EventDirectionBranch { get; set; } = new();
    /// <summary>
    /// 是否可驳回指定节点功能
    /// </summary>
    public bool? IsRejectToNode { get; set; }
    /// <summary>
    /// 当前节点审批人员
    /// </summary>
    [Required]
    public List<NodeApprover> CurrentNodeApprovers { get; set; } = new();

    /// <summary>
    /// 设置ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public WorkNode SetId(string id)
    {
        this.Id = id;
        return this;
    }
}

/// <summary>
/// 流程节点
/// </summary>
public class WorkNodeValidate : AbstractValidator<WorkNode>
{
    /// <summary>
    ///验证规则
    /// </summary>
    public WorkNodeValidate()
    {
        RuleFor(x => x.NodeName).NotEmpty().WithMessage("节点名称不能为空！")
            .MaximumLength(20).WithMessage("节点名称长度不能超过20个字符");
        RuleFor(x => x.CurrentNodeApprovers).NotEmpty().WithMessage("节点审批人不能为空！");
        RuleForEach(order => order.CurrentNodeApprovers).SetValidator(new NodeApproverValidator());
        RuleFor(x => x.EventDirectionBranch).NotEmpty().WithMessage("审批结果不能为空！")
            .Must(BeValidEnumValues).WithMessage("审批结果列表中包含无效的枚举值");
    }

    private bool BeValidEnumValues(List<EventDirectionType> values)
    {
        return values.TrueForAll(x => Enum.IsDefined(typeof(EventDirectionType), x));
    }
}