namespace Trasen.PaperFree.Domain.BasicData.Repository
{
    public interface IBasOrgMemberRepo
    {
        IQueryable<BaseOrgMember> QueryAll();
    }
}