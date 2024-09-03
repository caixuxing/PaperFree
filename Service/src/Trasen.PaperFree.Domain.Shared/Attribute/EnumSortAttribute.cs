namespace Trasen.PaperFree.Domain.Shared.Attribute;

[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
public class EnumSortAttribute : System.Attribute
{
    public int Sort { get; set; }

    public EnumSortAttribute(int value)
    {
        Sort = value;
    }
}