using Oracle.ManagedDataAccess.Client;

namespace Trasen.PaperFree.Application.Data
{
    /// <summary>
    ///
    /// </summary>
    public interface ISqlConnectionFactory
    {
        OracleConnection GetOpenConnection();
    }
}