using StackExchange.Redis;

namespace Trasen.PaperFree.Infrastructure.SeedWork.Redis
{
    /// <summary>
    /// redis初始化的方法
    /// </summary>
    public interface IRedisProvider
    {
        public string DefaultConnectionName { get; }

        /// <summary>
        /// 获取带租户前缀的key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<string> RenameKeyAsync(string key);

        /// <summary>
        /// 多个连接根据租户建立客户端
        /// </summary>
        /// <param name="redisConnections"></param>
        /// <returns></returns>
        public Task InitializeAsync(IEnumerable<(string ConnectionName, string ConnectionString)> redisConnections);

        /// <summary>
        /// 获取默认的database
        /// </summary>
        /// <returns></returns>
        public Task<IDatabase> GetGetRedisClientAsync();
    }
}