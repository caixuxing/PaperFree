using StackExchange.Redis;
using System.Collections.Concurrent;

namespace Trasen.PaperFree.Infrastructure.SeedWork.Redis
{
    public class DefaultRedisProvider : IRedisProvider
    {
        private static ConcurrentDictionary<string, ConnectionMultiplexer> RedisClients;
        private static ConcurrentDictionary<string, string> RedisNameAndConnectString;

        public string DefaultConnectionName
        {
            get { return "default"; }
        }

        public async Task<string> RenameKeyAsync(string key)
        {
            return await Task.FromResult(key);
        }

        public async Task InitializeAsync(IEnumerable<(string ConnectionName, string ConnectionString)> redisConnections)
        {
            if (!redisConnections.Any())
            {
                throw new ArgumentNullException("连接字符串不能为空");
            }

            RedisClients = new ConcurrentDictionary<string, ConnectionMultiplexer>(Environment.ProcessorCount * 2, 919);
            RedisNameAndConnectString = new ConcurrentDictionary<string, string>(Environment.ProcessorCount * 2, 919);
            foreach (var item in redisConnections)
            {
                RedisNameAndConnectString.TryAdd(item.ConnectionName, item.ConnectionString);
                RedisClients.TryAdd(item.ConnectionString, await ConnectionMultiplexer.ConnectAsync(item.ConnectionString));
            }
        }

        public async Task<IDatabase> GetGetRedisClientAsync()
        {
            return await GetDefaultRedisClientAsync();
        }

        #region 私有方法

        /// <summary>
        /// 默认连接
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private Task<IDatabase> GetDefaultRedisClientAsync()
        {
            if (!RedisNameAndConnectString.TryGetValue(DefaultConnectionName, out var connectName))
            {
                throw new ArgumentException("redis实例为空");
            }

            if (RedisClients.TryGetValue(connectName, out var connect))
            {
                return Task.FromResult(connect.GetDatabase());
            }
            else
            {
                throw new ArgumentException("redis实例为空");
            }
        }

        private async Task<IDatabase> GetRedisClientAsync(string connectname)
        {
            if (!RedisNameAndConnectString.TryGetValue(connectname, out var connectlink))
            {
                throw new ArgumentException("redis实例为空");
            }
            if (RedisClients.TryGetValue(connectlink, out var connect))
            {
                return connect.GetDatabase();
            }
            else
            {
                return await GetDefaultRedisClientAsync();
            }
        }

        #endregion 私有方法
    }
}