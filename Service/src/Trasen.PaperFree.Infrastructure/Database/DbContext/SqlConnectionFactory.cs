using Oracle.ManagedDataAccess.Client;
using Trasen.PaperFree.Application.Data;

namespace Trasen.PaperFree.Infrastructure.Database.DbContext
{
    /// <summary>
    ///
    /// </summary>
    public class SqlConnectionFactory : ISqlConnectionFactory, IDisposable
    {
        private readonly string _connectionString;
        private OracleConnection _connection;
        private readonly DbConnectionOption _dbConnection;
        private readonly SlaveRoundRobin _slaveRoundRobin;

        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        /// <param name="slaveRoundRobin"></param>
        public SqlConnectionFactory(IOptions<DbConnectionOption> options, SlaveRoundRobin slaveRoundRobin)
        {
            _dbConnection = options.Value;
            _connectionString = slaveRoundRobin.GetNext();
        }

        public OracleConnection GetOpenConnection()
        {
            if (this._connection == null || this._connection.State != ConnectionState.Open)
            {
                //this._connection = new SqlConnection(_connectionString);
                this._connection = new OracleConnection(_connectionString);
                this._connection.Open();
            }

            return this._connection;
        }

        public void Dispose()
        {
            if (this._connection != null && this._connection.State == ConnectionState.Open)
            {
                this._connection.Dispose();
            }
        }
    }
}