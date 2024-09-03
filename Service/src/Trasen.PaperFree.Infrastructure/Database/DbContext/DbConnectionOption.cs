namespace Trasen.PaperFree.Infrastructure.Database.DbContext;

public class DbConnectionOption
{
    public string MasterConnection { get; set; }
    public IList<string> SlaveConnections { get; set; }

    public string BasicDataConnection { get; set; }
}