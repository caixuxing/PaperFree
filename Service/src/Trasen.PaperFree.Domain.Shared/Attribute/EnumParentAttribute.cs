namespace Trasen.PaperFree.Domain.Shared.Attributes;

[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
public class EnumParentAttribute : System.Attribute
{
    public List<int> Parent { get; set; }

    public EnumParentAttribute(params int[] values)
    {
        Parent = new List<int>(values);
    }
}