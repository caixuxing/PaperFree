using StackExchange.Redis;
using Trasen.PaperFree.Domain.Shared.Jsons;

namespace Trasen.PaperFree.Infrastructure.SeedWork.Redis
{
    public static class RedisExtension
    {
        /// <summary>
        /// 转redisValue
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static RedisValue ToRedisValue(this object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            if (value is string)
            {
                return value.ToString();
            }

            if (value is int)
            {
                return value.ToString();
            }
            else
            {
                return value.ToJsonString();
            }
        }

        /// <summary>
        /// 转redisValues
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static RedisValue[] ToRedisValues<T>(this T[] values)
        {
            return values.Select(n => ToRedisValue(n)).ToArray();
        }

        /// <summary>
        /// keys转Rediskey[]
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public static RedisKey[] ToRedisKeys(this string[] keys)
        {
            return keys.Select(x => new RedisKey(x)).ToArray();
        }

        /// <summary>
        /// RedisValue[] 转 string[]
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string[] ToValues(RedisValue[] values)
        {
            return values.Select(n => (string)n).ToArray();
        }

        /// <summary>
        /// 转泛型数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <returns></returns>
        public static T[] ToValues<T>(this IEnumerable<string> values)
        {
            return values.Select(n => ToObjectValue<T>(n)).ToArray();
        }

        /// <summary>
        /// 字符串转泛型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T ToObjectValue<T>(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default;
            }

            return value.FromJsonString<T>();
        }
    }
}