using Microsoft.Extensions.Configuration;
using System.Reflection;
using Trasen.PaperFree.Domain.Shared.Appsettings;
using Trasen.PaperFree.Domain.Shared.Config;
using Trasen.PaperFree.Infrastructure.SeedWork.Redis;

namespace Trasen.PaperFree.Infrastructure.SeedWork
{
    public static class ModuleInitializerExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static IServiceCollection RunModuleInitializers(this IServiceCollection services,
         IEnumerable<Assembly> assemblies)
        {
            foreach (var asm in assemblies)
            {
                Type[] types = asm.GetTypes();
                var moduleInitializerTypes = types.Where(t => !t.IsAbstract && typeof(IModuleInitializer).IsAssignableFrom(t));
                foreach (var implType in moduleInitializerTypes)
                {
                    var initializer = (IModuleInitializer?)Activator.CreateInstance(implType);
                    if (initializer == null)
                    {
                        throw new ArgumentNullException($"Cannot create{implType}");
                    }
                    initializer.Initialize(services);
                }
            }
            return services;
        }
    }

    public static class CachingRedisModuleInitializer
    {
        public static void RunCachingRedisModuleInitializer(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var redisprovider = serviceProvider.GetRequiredService<IRedisProvider>();
            var redisSetting = Appsetting.Instance.GetSection("RedisConfig").Get<RedisSetting>();

            string? redisconnect = $"{redisSetting.Host}:{redisSetting.Port},password={redisSetting.Password}";
            //如果以后扩展成多租户redis 可以添加多个连接默认情况下只读取单个
            redisprovider?.InitializeAsync(new List<(string ConnectionName, string ConnectionString)>()
            {
                (redisprovider.DefaultConnectionName, redisconnect)
            });
        }
    }
}