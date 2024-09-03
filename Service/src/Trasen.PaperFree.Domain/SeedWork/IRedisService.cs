using StackExchange.Redis;

namespace Trasen.PaperFree.Domain.SeedWork
{
    /// <summary>
    /// Redis 对外接口
    /// </summary>
    public interface IRedisService
    {
        #region Key

        /// <summary>
        /// 根据指定模式删除
        /// </summary>
        /// <param name="redisClient"></param>
        /// <param name="patterns">模式（正则表达式）</param>
        /// <returns></returns>
        Task<long> DelByPatternAsync(params string[] patterns);

        /// <summary>
        /// 根据key前缀获取key 的数量
        /// </summary>
        /// <param name="keyPatterns"></param>
        /// <returns></returns>
        Task<long> KeysCount(string keyPatterns);

        /// <summary>
        /// 删除缓存
        /// 如果多个key必须在同一个db中
        /// </summary>
        /// <param name="keys"></param>
        /// <returns>是否成功</returns>
        Task<long> DelAsync(string[] keys);

        Task<long> DelAsync(string keys);

        /// <summary>
        /// 检查给定 key 是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns>是否成功</returns>
        Task<bool> ExistsAsync(string key);

        /// <summary>
        /// 为给定 key 设置过期时间
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="seconds">过期秒数</param>
        /// <returns>是否成功</returns>
        Task<bool> ExpireAsync(string key, int seconds);

        /// <summary>
        /// 为给定 key 设置过期时间
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="expire">过期时间</param>
        /// <returns>是否成功</returns>
        Task<bool> ExpireAsync(string key, TimeSpan expire);

        /// <summary>
        /// 为给定 key 设置过期时间，指定时间过期
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="expire">指定时间</param>
        /// <returns>是否成功</returns>
        Task<bool> ExpireAtAsync(string key, DateTime expire);

        /// <summary>
        /// 该返回给定 key 锁储存的值所使用的内部表示(representation)
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> ObjectEncodingAsync(string key);

        /// <summary>
        /// 该返回给定 key 引用所储存的值的次数。此命令主要用于除错
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long?> ObjectRefCountAsync(string key);

        /// <summary>
        /// 返回给定 key 自储存以来的空转时间(idle， 没有被读取也没有被写入)，以秒为单位
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long?> ObjectIdleTimeAsync(string key);

        /// <summary>
        /// 移除 key 的过期时间，key 将持久保持
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<bool> PersistAsync(string key);

        /// <summary>
        /// 以秒为单位，返回给定 key 的剩余生存时间
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> TtlAsync(string key);

        /// <summary>
        /// 以毫秒为单位返回 key 的剩余的过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> PTtlAsync(string key);

        /// <summary>
        /// 修改 key 的名称,newKey存在时覆盖
        /// </summary>
        /// <param name="key">旧名称</param>
        /// <param name="newKey">新名称</param>
        /// <returns></returns>
        Task<bool> RenameAsync(string key, string newKey);

        /// <summary>
        /// 修改 key 的名称，newKey存在时，返回失败
        /// </summary>
        /// <param name="key">旧名称，</param>
        /// <param name="newKey">新名称，</param>
        /// <returns></returns>
        Task<bool> RenameNxAsync(string key, string newKey);

        #endregion Key

        #region Kv

        /// <summary>
        /// 如果 key 已经存在并且是一个字符串， APPEND 命令将指定的 value 追加到该 key 原来值（value）的末尾
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">字符串</param>
        /// <returns>追加指定值之后， key 中字符串的长度</returns>
        Task<long> AppendAsync(string key, string value);

        /// <summary>
        /// 获取指定 key 的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetAsync(string key);

        /// <summary>
        /// 获取指定 key 的值
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// 返回 key 中字符串值的子字符
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="end">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        Task<string> GetRangeAsync(string key, long start, long end);

        /// <summary>
        /// 将给定 key 的值设为 value ，并返回 key 的旧值(old value)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">值</param>
        /// <returns></returns>
        Task<string> GetSetAsync(string key, object value);

        /// <summary>
        /// 将给定 key 的值设为 value ，并返回 key 的旧值(old value)  超时时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTimeSpan"></param>
        /// <returns></returns>
        Task<string> GetSetAsync(string key, object value, TimeSpan expireTimeSpan);

        /// <summary>
        /// 将给定 key 的值设为 value ，并返回 key 的旧值(old value)
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="value">值</param>
        /// <returns></returns>
        Task<T> GetSetAsync<T>(string key, object value);

        Task<T> GetSetAsync<T>(string key, object value, TimeSpan expireTimeSpan);

        /// <summary>
        /// 获取多个指定 key 的值(数组)，必须在同一个db中
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<string[]> MGetAsync(string[] keys);

        /// <summary>
        /// 获取多个指定 key 的值(数组)，必须在同一个db中
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<T[]> MGetAsync<T>(string[] keys);

        /// <summary>
        /// 设置指定 key 的值，所有写入参数object都支持string | byte[] | 数值 | 对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">值</param>
        /// <param name="expireSeconds">过期(秒单位)</param>
        /// <param name="exists">Nx, Xx</param>
        /// <param name="isToTenant"></param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, object value, int expireSeconds = -1,
            When exists = When.Always);

        /// <summary>
        /// 设置指定 key 的值，所有写入参数object都支持string | byte[] | 数值 | 对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">值</param>
        /// <param name="expire">过期时间</param>
        /// <param name="exists">Nx, Xx</param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, object value, TimeSpan expire, When exists = When.Always);

        /// <summary>
        /// 返回 key 所储存的字符串值的长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> StrLenAsync(string key);

        /// <summary>
        /// 只有在 key 不存在时设置 key 的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task<bool> SetNxAsync(string key, object value);

        #endregion Kv

        #region Hash操作

        /// <summary>
        /// 删除一个或多个哈希表字段
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        Task<long> HDelAsync(string key, params string[] fields);

        /// <summary>
        /// 查看哈希表 key 中，指定的字段是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        Task<bool> HExistsAsync(string key, string field);

        /// <summary>
        /// 获取存储在哈希表中指定字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        Task<string> HGetAsync(string key, string field);

        /// <summary>
        /// 获取存储在哈希表中指定字段的值
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="field">字段</param>
        /// <returns></returns>
        Task<T> HGetAsync<T>(string key, string field);

        /// <summary>
        /// 获取在哈希表中指定 key 的所有字段和值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<Dictionary<string, string>> HGetAllAsync(string key);

        /// <summary>
        /// 获取在哈希表中指定 key 的所有字段和值
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<Dictionary<string, T>> HGetAllAsync<T>(string key);

        /// <summary>
        /// 获取所有哈希表中的字段
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<IEnumerable<string>> HKeysAsync(string key);

        /// <summary>
        /// 获取哈希表中字段的数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> HLenAsync(string key);

        /// <summary>
        /// 获取存储在哈希表中多个字段的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fields">字段</param>
        /// <returns></returns>
        Task<string[]> HMGetAsync(string key, string[] fields);

        /// <summary>
        /// 获取存储在哈希表中多个字段的值
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="fields">一个或多个字段</param>
        /// <returns></returns>
        Task<T[]> HMGetAsync<T>(string key, string[] fields);

        /// <summary>
        /// 同时将多个 field-value (域-值)对设置到哈希表 key 中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fieldValues">一个或多个 field value 值</param>
        /// <returns></returns>
        Task HMSetAsync(string key, (string Field, object Value)[] fieldValues);

        /// <summary>
        /// 同时将多个 field-value (域-值)对设置到哈希表 key 中
        /// </summary>
        /// <param name="key"></param>
        /// <param name="fieldValues"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        Task HMSetAsync(string key, Dictionary<string, string> fieldValues, TimeSpan? expireTime = null);

        /// <summary>
        /// 将哈希表 key 中的字段 field 的值设为 value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field">字段</param>
        /// <param name="value">值</param>
        /// <param name="expireTime"></param>
        /// <returns>如果字段是哈希表中的一个新建字段，并且值设置成功，返回true。如果哈希表中域字段已经存在且旧值已被新值覆盖</returns>
        Task<bool> HSetAsync(string key, string field, object value, TimeSpan? expireTime = null);

        /// <summary>
        /// 迭代哈希表中的键值对
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cursor">位置</param>
        /// <param name="pattern">模式</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        IAsyncEnumerable<(string field, string value)> HScanAsync(string key, int cursor,
            string pattern = null, int? count = null);

        /// <summary>
        /// 迭代哈希表中的键值对
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="cursor">位置</param>
        /// <param name="pattern">模式</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        IAsyncEnumerable<(string field, T value)> HScanAsync<T>(string key, int cursor,
            string pattern = null, int? count = null);

        #endregion Hash操作

        #region List 操作

        /// <summary>
        /// 获取列表指定范围内的元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        Task<string[]> LRangeAsync(string key, long start, long stop);

        /// <summary>
        /// 获取列表指定范围内的元素
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        Task<T[]> LRangeAsync<T>(string key, long start, long stop);

        /// <summary>
        /// 获取列表长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> LLenAsync(string key);

        /// <summary>
        /// 通过索引获取列表中的元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index">索引</param>
        /// <returns></returns>
        Task<string> LIndexAsync(string key, long index);

        /// <summary>
        /// 通过索引获取列表中的元素
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="index">索引</param>
        /// <returns></returns>
        Task<T> LIndexAsync<T>(string key, long index);

        /// <summary>
        /// 根据参数 count 的值，移除列表中与参数 value 相等的元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count">移除的数量，大于0时从表头删除数量count，小于0时从表尾删除数量-count，等于0移除所有</param>
        /// <param name="value">元素</param>
        /// <returns></returns>
        Task<long> LRemAsync(string key, long count, object value);

        /// <summary>
        /// 通过索引设置列表元素的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index">索引</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        Task LSetAsync(string key, long index, object value);

        /// <summary>
        /// 对一个列表进行修剪，让列表只保留指定区间内的元素，不在指定区间之内的元素都将被删除
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        Task LTrimAsync(string key, long start, long stop);

        /// <summary>
        /// 移出并获取列表的第一个元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> LPopAsync(string key);

        /// <summary>
        /// 移出并获取列表的第一个元素
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> LPopAsync<T>(string key);

        /// <summary>
        /// 移除并获取列表最后一个元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> RPopAsync(string key);

        /// <summary>
        /// 移除并获取列表最后一个元素
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> RPopAsync<T>(string key);

        /// <summary>
        /// 在列表中添加一个或多个值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">一个或多个值</param>
        /// <returns>执行 RPUSH 命令后，列表的长度</returns>
        Task<long> RPushAsync<T>(string key, params T[] value);

        /// <summary>
        /// 为已存在的列表添加值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">一个或多个值</param>
        /// <returns>执行 RPUSHX 命令后，列表的长度</returns>
        Task<long> RPushXAsync(string key, object value);

        /// <summary>
        /// 将一个或多个值插入到列表头部
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">一个或多个值</param>
        /// <returns>执行 LPUSH 命令后，列表的长度</returns>
        Task<long> LPushAsync<T>(string key, T[] value);

        /// <summary>
        /// 将一个值插入到已存在的列表头部
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">值</param>
        /// <returns>执行 LPUSHX 命令后，列表的长度。</returns>
        Task<long> LPushXAsync(string key, object value);

        #endregion List 操作

        #region Set操作

        /// <summary>
        /// 向集合添加一个或多个成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="members">一个或多个成员</param>
        /// <returns></returns>
        Task<long> SAddAsync<T>(string key, T[] members, TimeSpan? expireTime = null);

        /// <summary>
        /// 获取集合的成员数
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> SCardAsync(string key);

        /// <summary>
        /// 返回给定所有集合的差集
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<string[]> SDiffAsync(string[] keys);

        /// <summary>
        /// 返回给定所有集合的差集
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<T[]> SDiffAsync<T>(string[] keys);

        /// <summary>
        /// 返回给定所有集合的交集
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<string[]> SInterAsync(string[] keys);

        /// <summary>
        /// 返回给定所有集合的交集
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<T[]> SInterAsync<T>(string[] keys);

        /// <summary>
        /// 判断 member 元素是否是集合 key 的成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member">成员</param>
        /// <returns></returns>
        Task<bool> SIsMemberAsync(string key, object member);

        /// <summary>
        /// 返回集合中的所有成员
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string[]> SMembersAsync(string key);

        /// <summary>
        /// 返回集合中的所有成员
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T[]> SMembersAsync<T>(string key);

        /// <summary>
        /// 移除并返回集合中的一个随机元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> SPopAsync(string key);

        /// <summary>
        /// 移除并返回集合中的一个随机元素
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> SPopAsync<T>(string key);

        /// <summary>
        /// 返回集合中的一个随机元素
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> SRandMemberAsync(string key);

        /// <summary>
        /// 返回集合中的一个随机元素
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> SRandMemberAsync<T>(string key);

        /// <summary>
        /// 返回集合中一个或多个随机数的元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count">返回个数</param>
        /// <returns></returns>
        Task<string[]> SRandMembersAsync(string key, int count = 1);

        /// <summary>
        /// 返回集合中一个或多个随机数的元素
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="count">返回个数</param>
        /// <returns></returns>
        Task<T[]> SRandMembersAsync<T>(string key, int count = 1);

        /// <summary>
        /// 移除集合中一个或多个成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="members">一个或多个成员</param>
        /// <returns></returns>
        Task<long> SRemAsync<T>(string key, T[] members);

        /// <summary>
        /// 返回所有给定集合的并集
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<string[]> SUnionAsync(string[] keys);

        /// <summary>
        /// 返回所有给定集合的并集
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        Task<T[]> SUnionAsync<T>(string[] keys);

        /// <summary>
        /// 迭代集合中的元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cursor">位置</param>
        /// <param name="pattern">模式</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        IAsyncEnumerable<string> SScanAsync(string key, int cursor, string pattern = null,
            int? count = null);

        /// <summary>
        /// 迭代集合中的元素
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="cursor">位置</param>
        /// <param name="pattern">模式</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        IAsyncEnumerable<T> SScanAsync<T>(string key, int cursor, string pattern = null,
            int? count = null);

        #endregion Set操作

        #region Sorted Set 操作

        /// <summary>
        /// 向有序集合添加一个或多个成员，或者更新已存在成员的分数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="scoreMembers">一个或多个成员分数</param>
        /// <returns></returns>
        Task<long> ZAddAsync(string key, (decimal score, object member)[] scoreMembers);

        /// <summary>
        /// 向有序集合添加一个或多个成员，或者更新已存在成员的分数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="scoreMembers">一个或多个成员分数</param>
        /// <returns></returns>
        Task<long> ZAddAsync(string key, (double Score, object Member)[] scoreMembers);

        /// <summary>
        /// 获取有序集合的成员数量
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> ZCardAsync(string key);

        /// <summary>
        /// 计算在有序集合中指定区间分数的成员数量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min">分数最小值 decimal.MinValue 1</param>
        /// <param name="max">分数最大值 decimal.MaxValue 10</param>
        /// <returns></returns>
        Task<long> ZCountAsync(string key, decimal min, decimal max);

        /// <summary>
        /// 计算在有序集合中指定区间分数的成员数量
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min">分数最小值 -inf (1 1</param>
        /// <param name="max">分数最大值 +inf (10 10</param>
        /// <returns></returns>
        Task<long> ZCountAsync(string key, string min, string max);

        /// <summary>
        /// 有序集合中对指定成员的分数加上增量 increment
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member">成员</param>
        /// <param name="increment">增量值(默认=1)</param>
        /// <returns></returns>
        Task<decimal> ZIncrByAsync(string key, string member, decimal increment = 1);

        /// <summary>
        /// 通过索引区间返回有序集合成指定区间内的成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        Task<string[]> ZRangeAsync(string key, long start, long stop);

        /// <summary>
        /// 通过索引区间返回有序集合成指定区间内的成员
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        Task<T[]> ZRangeAsync<T>(string key, long start, long stop);

        /// <summary>
        /// 通过索引区间返回有序集合成指定区间内的成员和分数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        Task<(string member, double score)[]> ZRangeWithScoresAsync(string key, long start,
            long stop);

        /// <summary>
        /// 通过索引区间返回有序集合成指定区间内的成员和分数
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        Task<(T member, decimal score)[]> ZRangeWithScoresAsync<T>(string key, long start,
            long stop);

        /// <summary>
        /// 通过分数返回有序集合指定区间内的成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min">分数最小值 decimal.MinValue 1</param>
        /// <param name="max">分数最大值 decimal.MaxValue 10</param>
        /// <param name="limit">返回多少成员</param>
        /// <param name="offset">返回条件偏移位置</param>
        /// <returns></returns>
        Task<string[]> ZRangeByScoreAsync(string key, decimal min, decimal max, long? limit = null,
            long offset = 0);

        /// <summary>
        /// 通过分数返回有序集合指定区间内的成员
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="min">分数最小值 decimal.MinValue 1</param>
        /// <param name="max">分数最大值 decimal.MaxValue 10</param>
        /// <param name="limit">返回多少成员</param>
        /// <param name="offset">返回条件偏移位置</param>
        /// <returns></returns>
        Task<T[]> ZRangeByScoreAsync<T>(string key, decimal min, decimal max, long? limit = null,
            long offset = 0);

        /// <summary>
        /// 通过分数返回有序集合指定区间内的成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min">分数最小值 -inf (1 1</param>
        /// <param name="max">分数最大值 +inf (10 10</param>
        /// <param name="limit">返回多少成员</param>
        /// <param name="offset">返回条件偏移位置</param>
        /// <returns></returns>
        Task<string[]> ZRangeByScoreAsync(string key, string min, string max, long? limit = null,
            long offset = 0);

        /// <summary>
        /// 通过分数返回有序集合指定区间内的成员
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="min">分数最小值 -inf (1 1</param>
        /// <param name="max">分数最大值 +inf (10 10</param>
        /// <param name="limit">返回多少成员</param>
        /// <param name="offset">返回条件偏移位置</param>
        /// <returns></returns>
        Task<T[]> ZRangeByScoreAsync<T>(string key, string min, string max, long? limit = null,
            long offset = 0);

        /// <summary>
        /// 通过分数返回有序集合指定区间内的成员和分数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min">分数最小值 decimal.MinValue 1</param>
        /// <param name="max">分数最大值 decimal.MaxValue 10</param>
        /// <param name="limit">返回多少成员</param>
        /// <param name="offset">返回条件偏移位置</param>
        /// <returns></returns>
        Task<(string member, decimal score)[]> ZRangeByScoreWithScoresAsync(string key, decimal min,
            decimal max, long? limit = null, long offset = 0);

        /// <summary>
        /// 通过分数返回有序集合指定区间内的成员和分数
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="min">分数最小值 decimal.MinValue 1</param>
        /// <param name="max">分数最大值 decimal.MaxValue 10</param>
        /// <param name="limit">返回多少成员</param>
        /// <param name="offset">返回条件偏移位置</param>
        /// <returns></returns>
        Task<(T member, decimal score)[]> ZRangeByScoreWithScoresAsync<T>(string key, decimal min,
            decimal max, long? limit = null, long offset = 0);

        /// <summary>
        /// 返回有序集合中指定成员的索引
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member">成员</param>
        /// <returns></returns>
        Task<long?> ZRankAsync(string key, object member);

        /// <summary>
        /// 移除有序集合中的一个或多个成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member">一个或多个成员</param>
        /// <returns></returns>
        Task<long> ZRemAsync<T>(string key, T[] member);

        /// <summary>
        /// 移除有序集合中给定的排名区间的所有成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        Task<long> ZRemRangeByRankAsync(string key, long start, long stop);

        /// <summary>
        /// 移除有序集合中给定的分数区间的所有成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min">分数最小值 decimal.MinValue 1</param>
        /// <param name="max">分数最大值 decimal.MaxValue 10</param>
        /// <returns></returns>
        Task<long> ZRemRangeByScoreAsync(string key, double min, double max);

        /// <summary>
        /// 移除有序集合中给定的分数区间的所有成员
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min">分数最小值 -inf (1 1</param>
        /// <param name="max">分数最大值 +inf (10 10</param>
        /// <returns></returns>
        Task<long> ZRemRangeByScoreAsync(string key, string min, string max);

        /// <summary>
        /// 返回有序集中指定区间内的成员，通过索引，分数从高到底
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        Task<string[]> ZRevRangeAsync(string key, long start, long stop);

        /// <summary>
        /// 返回有序集中指定区间内的成员，通过索引，分数从高到底
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        Task<T[]> ZRevRangeAsync<T>(string key, long start, long stop);

        /// <summary>
        /// 返回有序集中指定区间内的成员和分数，通过索引，分数从高到底
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        Task<(string member, double score)[]> ZRevRangeWithScoresAsync(string key, long start,
            long stop);

        /// <summary>
        /// 返回有序集中指定区间内的成员和分数，通过索引，分数从高到底
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="start">开始位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <param name="stop">结束位置，0表示第一个元素，-1表示最后一个元素</param>
        /// <returns></returns>
        Task<(T member, double score)[]> ZRevRangeWithScoresAsync<T>(string key, long start,
            long stop);

        /// <summary>
        /// 返回有序集中指定分数区间内的成员，分数从高到低排序
        /// </summary>
        /// <param name="key"></param>
        /// <param name="max">分数最大值 decimal.MaxValue 10</param>
        /// <param name="min">分数最小值 decimal.MinValue 1</param>
        /// <param name="limit">返回多少成员</param>
        /// <param name="offset">返回条件偏移位置</param>
        /// <returns></returns>
        Task<string[]> ZRevRangeByScoreAsync(string key, decimal max, decimal min,
            long? limit = null, long? offset = 0);

        /// <summary>
        /// 返回有序集中指定分数区间内的成员，分数从高到低排序
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="max">分数最大值 decimal.MaxValue 10</param>
        /// <param name="min">分数最小值 decimal.MinValue 1</param>
        /// <param name="limit">返回多少成员</param>
        /// <param name="offset">返回条件偏移位置</param>
        /// <returns></returns>
        Task<T[]> ZRevRangeByScoreAsync<T>(string key, decimal max, decimal min, long? limit = null,
            long offset = 0);

        /// <summary>
        /// 返回有序集中指定分数区间内的成员，分数从高到低排序
        /// </summary>
        /// <param name="key"></param>
        /// <param name="max">分数最大值 +inf (10 10</param>
        /// <param name="min">分数最小值 -inf (1 1</param>
        /// <param name="limit">返回多少成员</param>
        /// <param name="offset">返回条件偏移位置</param>
        /// <returns></returns>
        Task<string[]> ZRevRangeByScoreAsync(string key, string max, string min, long? limit = null,
            long? offset = 0);

        /// <summary>
        /// 返回有序集中指定分数区间内的成员，分数从高到低排序
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="max">分数最大值 +inf (10 10</param>
        /// <param name="min">分数最小值 -inf (1 1</param>
        /// <param name="limit">返回多少成员</param>
        /// <param name="offset">返回条件偏移位置</param>
        /// <returns></returns>
        Task<T[]> ZRevRangeByScoreAsync<T>(string key, string max, string min, long? limit = null,
            long offset = 0);

        /// <summary>
        /// 返回有序集中指定分数区间内的成员和分数，分数从高到低排序
        /// </summary>
        /// <param name="key"></param>
        /// <param name="max">分数最大值 decimal.MaxValue 10</param>
        /// <param name="min">分数最小值 decimal.MinValue 1</param>
        /// <param name="limit">返回多少成员</param>
        /// <param name="offset">返回条件偏移位置</param>
        /// <returns></returns>
        Task<(string member, decimal score)[]> ZRevRangeByScoreWithScoresAsync(string key,
            decimal max, decimal min, long? limit = null, long offset = 0);

        /// <summary>
        /// 返回有序集中指定分数区间内的成员和分数，分数从高到低排序
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="max">分数最大值 decimal.MaxValue 10</param>
        /// <param name="min">分数最小值 decimal.MinValue 1</param>
        /// <param name="limit">返回多少成员</param>
        /// <param name="offset">返回条件偏移位置</param>
        /// <returns></returns>
        Task<(T member, decimal score)[]> ZRevRangeByScoreWithScoresAsync<T>(string key,
            decimal max, decimal min, long? limit = null, long offset = 0);

        /// <summary>
        /// 返回有序集合中指定成员的排名，有序集成员按分数值递减(从大到小)排序
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member">成员</param>
        /// <returns></returns>
        Task<long?> ZRevRankAsync(string key, object member);

        /// <summary>
        /// 返回有序集中，成员的分数值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member">成员</param>
        /// <returns></returns>
        Task<decimal?> ZScoreAsync(string key, object member);

        /// <summary>
        /// 迭代有序集合中的元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cursor">位置</param>
        /// <param name="pattern">模式</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        IAsyncEnumerable<(string member, decimal score)> ZScanAsync(string key, int cursor,
            string pattern = null, int? count = null);

        /// <summary>
        /// 迭代有序集合中的元素
        /// </summary>
        /// <typeparam name="T">byte[] 或其他类型</typeparam>
        /// <param name="key"></param>
        /// <param name="cursor">位置</param>
        /// <param name="pattern">模式</param>
        /// <param name="count">数量</param>
        /// <returns></returns>
        IAsyncEnumerable<(T member, decimal score)> ZScanAsync<T>(string key, int cursor,
            string pattern = null, int? count = null);

        /// <summary>
        /// 当有序集合的所有成员都具有相同的分值时，有序集合的元素会根据成员的字典序来进行排序，这个命令可以返回给定的有序集合键 key 中，值介于 min 和 max 之间的成员。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min">'(' 表示包含在范围，'[' 表示不包含在范围，'+' 正无穷大，'-' 负无限。 ZRANGEBYLEX zset - + ，命令将返回有序集合中的所有元素</param>
        /// <param name="max">'(' 表示包含在范围，'[' 表示不包含在范围，'+' 正无穷大，'-' 负无限。 ZRANGEBYLEX zset - + ，命令将返回有序集合中的所有元素</param>
        /// <returns></returns>
        Task<long> ZRemRangeByLexAsync(string key, string min, string max);

        /// <summary>
        /// 当有序集合的所有成员都具有相同的分值时，有序集合的元素会根据成员的字典序来进行排序，这个命令可以返回给定的有序集合键 key 中，值介于 min 和 max 之间的成员。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min">'(' 表示包含在范围，'[' 表示不包含在范围，'+' 正无穷大，'-' 负无限。 ZRANGEBYLEX zset - + ，命令将返回有序集合中的所有元素</param>
        /// <param name="max">'(' 表示包含在范围，'[' 表示不包含在范围，'+' 正无穷大，'-' 负无限。 ZRANGEBYLEX zset - + ，命令将返回有序集合中的所有元素</param>
        /// <returns></returns>
        Task<long> ZLexCountAsync(string key, string min, string max);

        #endregion Sorted Set 操作

        #region Geo操作

        /// <summary>
        /// 将指定的地理空间位置（纬度、经度、成员）添加到指定的key中。这些数据将会存储到sorted set这样的目的是为了方便使用GEORADIUS或者GEORADIUSBYMEMBER命令对数据进行半径查询等操作。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="member">成员</param>
        /// <returns>是否成功</returns>
        Task<bool> GeoAddAsync(string key, double longitude, double latitude, object member);

        /// <summary>
        /// 将指定的地理空间位置（纬度、经度、成员）添加到指定的key中。这些数据将会存储到sorted set这样的目的是为了方便使用GEORADIUS或者GEORADIUSBYMEMBER命令对数据进行半径查询等操作。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="values">批量添加的值</param>
        /// <returns>添加到sorted set元素的数目，但不包括已更新score的元素。</returns>
        Task<long> GeoAddAsync(string key,
            (double longitude, double latitude, object member)[] values);

        /// <summary>
        /// 返回两个给定位置之间的距离。如果两个位置之间的其中一个不存在， 那么命令返回空值。GEODIST 命令在计算距离时会假设地球为完美的球形， 在极限情况下， 这一假设最大会造成 0.5% 的误差。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member1">成员1</param>
        /// <param name="member2">成员2</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺；</param>
        /// <returns>计算出的距离会以双精度浮点数的形式被返回。 如果给定的位置元素不存在， 那么命令返回空值。</returns>
        Task<decimal?> GeoDistAsync(string key, object member1, object member2,
            GeoUnit unit = GeoUnit.Meters);

        /// <summary>
        /// 返回一个或多个位置元素的 Geohash 表示。通常使用表示位置的元素使用不同的技术，使用Geohash位置52点整数编码。由于编码和解码过程中所使用的初始最小和最大坐标不同，编码的编码也不同于标准。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="members">多个查询的成员</param>
        /// <returns>一个数组， 数组的每个项都是一个 geohash 。 命令返回的 geohash 的位置与用户给定的位置元素的位置一一对应。</returns>
        Task<string[]> GeoHashAsync(string key, object[] members);

        /// <summary>
        /// 从key里返回所有给定位置元素的位置（经度和纬度）。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="members">多个查询的成员</param>
        /// <returns>GEOPOS 命令返回一个数组， 数组中的每个项都由两个元素组成： 第一个元素为给定位置元素的经度， 而第二个元素则为给定位置元素的纬度。当给定的位置元素不存在时， 对应的数组项为空值。</returns>
        Task<(double longitude, double latitude)?[]> GeoPosAsync(string key, object[] members);

        /// <summary>
        /// 以给定的经纬度为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="radius">距离</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺；</param>
        /// <param name="count">虽然用户可以使用 COUNT 选项去获取前 N 个匹配元素， 但是因为命令在内部可能会需要对所有被匹配的元素进行处理， 所以在对一个非常大的区域进行搜索时， 即使只使用 COUNT 选项去获取少量元素， 命令的执行速度也可能会非常慢。 但是从另一方面来说， 使用 COUNT 选项去减少需要返回的元素数量， 对于减少带宽来说仍然是非常有用的。</param>
        /// <param name="sorting">排序</param>
        /// <returns></returns>
        Task<string[]> GeoRadiusAsync(string key, decimal longitude, decimal latitude,
            decimal radius, GeoUnit unit = GeoUnit.Meters, long? count = null, Order? sorting = null);

        /// <summary>
        /// 以给定的经纬度为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="radius">距离</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺；</param>
        /// <param name="count">虽然用户可以使用 COUNT 选项去获取前 N 个匹配元素， 但是因为命令在内部可能会需要对所有被匹配的元素进行处理， 所以在对一个非常大的区域进行搜索时， 即使只使用 COUNT 选项去获取少量元素， 命令的执行速度也可能会非常慢。 但是从另一方面来说， 使用 COUNT 选项去减少需要返回的元素数量， 对于减少带宽来说仍然是非常有用的。</param>
        /// <param name="sorting">排序</param>
        /// <returns></returns>
        Task<T[]> GeoRadiusAsync<T>(string key, decimal longitude, decimal latitude, decimal radius,
            GeoUnit unit = GeoUnit.Meters, long? count = null, Order? sorting = null);

        /// <summary>
        /// 以给定的经纬度为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素（包含距离）。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="radius">距离</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺；</param>
        /// <param name="count">虽然用户可以使用 COUNT 选项去获取前 N 个匹配元素， 但是因为命令在内部可能会需要对所有被匹配的元素进行处理， 所以在对一个非常大的区域进行搜索时， 即使只使用 COUNT 选项去获取少量元素， 命令的执行速度也可能会非常慢。 但是从另一方面来说， 使用 COUNT 选项去减少需要返回的元素数量， 对于减少带宽来说仍然是非常有用的。</param>
        /// <param name="sorting">排序</param>
        /// <returns></returns>
        Task<(string member, decimal dist)[]> GeoRadiusWithDistAsync(string key, decimal longitude,
            decimal latitude, decimal radius, GeoUnit unit = GeoUnit.Meters, long? count = null, Order? sorting = null);

        /// <summary>
        /// 以给定的经纬度为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素（包含距离）。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="radius">距离</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺；</param>
        /// <param name="count">虽然用户可以使用 COUNT 选项去获取前 N 个匹配元素， 但是因为命令在内部可能会需要对所有被匹配的元素进行处理， 所以在对一个非常大的区域进行搜索时， 即使只使用 COUNT 选项去获取少量元素， 命令的执行速度也可能会非常慢。 但是从另一方面来说， 使用 COUNT 选项去减少需要返回的元素数量， 对于减少带宽来说仍然是非常有用的。</param>
        /// <param name="sorting">排序</param>
        /// <returns></returns>
        Task<(T member, decimal dist)[]> GeoRadiusWithDistAsync<T>(string key, decimal longitude,
            decimal latitude, decimal radius, GeoUnit unit = GeoUnit.Meters, long? count = null, Order? sorting = null);

        /// <summary>
        /// 以给定的经纬度为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素（包含距离、经度、纬度）。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="radius">距离</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺；</param>
        /// <param name="count">虽然用户可以使用 COUNT 选项去获取前 N 个匹配元素， 但是因为命令在内部可能会需要对所有被匹配的元素进行处理， 所以在对一个非常大的区域进行搜索时， 即使只使用 COUNT 选项去获取少量元素， 命令的执行速度也可能会非常慢。 但是从另一方面来说， 使用 COUNT 选项去减少需要返回的元素数量， 对于减少带宽来说仍然是非常有用的。</param>
        /// <param name="sorting">排序</param>
        /// <returns></returns>
        Task<(string member, double? dist, double? longitude, double? latitude)[]>
            GeoRadiusWithDistAndCoordAsync(string key, double longitude, double latitude, double radius,
                GeoUnit unit = GeoUnit.Meters, long? count = null, Order? sorting = null);

        /// <summary>
        /// 以给定的经纬度为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素（包含距离、经度、纬度）。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        /// <param name="radius">距离</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺；</param>
        /// <param name="count">虽然用户可以使用 COUNT 选项去获取前 N 个匹配元素， 但是因为命令在内部可能会需要对所有被匹配的元素进行处理， 所以在对一个非常大的区域进行搜索时， 即使只使用 COUNT 选项去获取少量元素， 命令的执行速度也可能会非常慢。 但是从另一方面来说， 使用 COUNT 选项去减少需要返回的元素数量， 对于减少带宽来说仍然是非常有用的。</param>
        /// <param name="sorting">排序</param>
        /// <returns></returns>
        Task<(T member, double? dist, double? longitude, double? latitude)[]>
            GeoRadiusWithDistAndCoordAsync<T>(string key, double longitude, double latitude, double radius,
                GeoUnit unit = GeoUnit.Meters, long? count = null, Order? sorting = null);

        /// <summary>
        /// 以给定的成员为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member">成员</param>
        /// <param name="radius">距离</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺；</param>
        /// <param name="count">虽然用户可以使用 COUNT 选项去获取前 N 个匹配元素， 但是因为命令在内部可能会需要对所有被匹配的元素进行处理， 所以在对一个非常大的区域进行搜索时， 即使只使用 COUNT 选项去获取少量元素， 命令的执行速度也可能会非常慢。 但是从另一方面来说， 使用 COUNT 选项去减少需要返回的元素数量， 对于减少带宽来说仍然是非常有用的。</param>
        /// <param name="sorting">排序</param>
        /// <returns></returns>
        Task<string[]> GeoRadiusByMemberAsync(string key, object member, double radius,
            GeoUnit unit = GeoUnit.Meters, long? count = null, Order? sorting = null);

        /// <summary>
        /// 以给定的成员为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member">成员</param>
        /// <param name="radius">距离</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺；</param>
        /// <param name="count">虽然用户可以使用 COUNT 选项去获取前 N 个匹配元素， 但是因为命令在内部可能会需要对所有被匹配的元素进行处理， 所以在对一个非常大的区域进行搜索时， 即使只使用 COUNT 选项去获取少量元素， 命令的执行速度也可能会非常慢。 但是从另一方面来说， 使用 COUNT 选项去减少需要返回的元素数量， 对于减少带宽来说仍然是非常有用的。</param>
        /// <param name="sorting">排序</param>
        /// <returns></returns>
        Task<T[]> GeoRadiusByMemberAsync<T>(string key, object member, decimal radius,
            GeoUnit unit = GeoUnit.Meters, long? count = null, Order? sorting = null);

        /// <summary>
        /// 以给定的成员为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素（包含距离）。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member">成员</param>
        /// <param name="radius">距离</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺；</param>
        /// <param name="count">虽然用户可以使用 COUNT 选项去获取前 N 个匹配元素， 但是因为命令在内部可能会需要对所有被匹配的元素进行处理， 所以在对一个非常大的区域进行搜索时， 即使只使用 COUNT 选项去获取少量元素， 命令的执行速度也可能会非常慢。 但是从另一方面来说， 使用 COUNT 选项去减少需要返回的元素数量， 对于减少带宽来说仍然是非常有用的。</param>
        /// <param name="sorting">排序</param>
        /// <returns></returns>
        Task<(string member, double dist)[]> GeoRadiusByMemberWithDistAsync(string key,
            object member, decimal radius, GeoUnit unit = GeoUnit.Meters, long? count = null, Order? sorting = null);

        /// <summary>
        /// 以给定的成员为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素（包含距离）。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member">成员</param>
        /// <param name="radius">距离</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺；</param>
        /// <param name="count">虽然用户可以使用 COUNT 选项去获取前 N 个匹配元素， 但是因为命令在内部可能会需要对所有被匹配的元素进行处理， 所以在对一个非常大的区域进行搜索时， 即使只使用 COUNT 选项去获取少量元素， 命令的执行速度也可能会非常慢。 但是从另一方面来说， 使用 COUNT 选项去减少需要返回的元素数量， 对于减少带宽来说仍然是非常有用的。</param>
        /// <param name="sorting">排序</param>
        /// <returns></returns>
        Task<(T member, decimal dist)[]> GeoRadiusByMemberWithDistAsync<T>(string key,
            object member, decimal radius, GeoUnit unit = GeoUnit.Meters, long? count = null, Order? sorting = null);

        /// <summary>
        /// 以给定的成员为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素（包含距离、经度、纬度）。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member">成员</param>
        /// <param name="radius">距离</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺；</param>
        /// <param name="count">虽然用户可以使用 COUNT 选项去获取前 N 个匹配元素， 但是因为命令在内部可能会需要对所有被匹配的元素进行处理， 所以在对一个非常大的区域进行搜索时， 即使只使用 COUNT 选项去获取少量元素， 命令的执行速度也可能会非常慢。 但是从另一方面来说， 使用 COUNT 选项去减少需要返回的元素数量， 对于减少带宽来说仍然是非常有用的。</param>
        /// <param name="sorting">排序</param>
        /// <returns></returns>
        Task<(string member, decimal dist, decimal longitude, decimal latitude)[]>
            GeoRadiusByMemberWithDistAndCoordAsync(string key, object member, decimal radius,
                GeoUnit unit = GeoUnit.Meters, long? count = null, Order? sorting = null);

        /// <summary>
        /// 以给定的成员为中心， 返回键包含的位置元素当中， 与中心的距离不超过给定最大距离的所有位置元素（包含距离、经度、纬度）。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="member">成员</param>
        /// <param name="radius">距离</param>
        /// <param name="unit">m 表示单位为米；km 表示单位为千米；mi 表示单位为英里；ft 表示单位为英尺；</param>
        /// <param name="count">虽然用户可以使用 COUNT 选项去获取前 N 个匹配元素， 但是因为命令在内部可能会需要对所有被匹配的元素进行处理， 所以在对一个非常大的区域进行搜索时， 即使只使用 COUNT 选项去获取少量元素， 命令的执行速度也可能会非常慢。 但是从另一方面来说， 使用 COUNT 选项去减少需要返回的元素数量， 对于减少带宽来说仍然是非常有用的。</param>
        /// <param name="sorting">排序</param>
        /// <returns></returns>
        Task<(T member, decimal dist, decimal longitude, decimal latitude)[]>
            GeoRadiusByMemberWithDistAndCoordAsync<T>(string key, object member, decimal radius,
                GeoUnit unit = GeoUnit.Meters, long? count = null, Order? sorting = null);

        #endregion Geo操作

        #region 委托

        /// <summary>
        /// 获取指定Key的值
        /// 如何key不存在，则从委托中获取
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="getValue">委托</param>
        /// <param name="resetExpire">重置过期时间</param>
        /// <returns></returns>
        Task<string> GetAsync(string key, Func<Task<string>> getValue, bool resetExpire = false);

        /// <summary>
        /// 获取指定Key的值
        /// 如何key不存在，则从委托中获取
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="getValue">委托</param>
        /// <param name="seconds">过期秒数</param>
        /// <param name="resetExpire">重置过期时间</param>
        /// <returns></returns>
        Task<string> GetAsync(string key, Func<Task<string>> getValue, int seconds,
            bool resetExpire = false);

        /// <summary>
        /// 获取指定Key的值
        /// 如何key不存在，则从委托中获取
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="getValue">委托</param>
        /// <param name="expire">过期时间</param>
        /// <param name="resetExpire">重置过期时间</param>
        /// <returns></returns>
        Task<string> GetAsync(string key, Func<Task<string>> getValue, TimeSpan expire,
            bool resetExpire = false);

        /// <summary>
        /// 获取指定Key的值
        /// 如何key不存在，则从委托中获取
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="getValue">委托</param>
        /// <param name="expire">指定的过期时间</param>
        /// <param name="resetExpire">重置过期时间</param>
        /// <returns></returns>
        Task<string> GetAsync(string key, Func<Task<string>> getValue, DateTime expire,
            bool resetExpire = false);

        /// <summary>
        /// 获取指定Key的值
        /// 如何key不存在，则从委托中获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="getValue">委托</param>
        /// <param name="resetExpire">重置过期时间</param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key, Func<Task<T>> getValue, bool resetExpire = false);

        /// <summary>
        /// 获取指定Key的值
        /// 如何key不存在，则从委托中获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="getValue">委托</param>
        /// <param name="seconds">过期秒数</param>
        /// <param name="resetExpire">重置过期时间</param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key, Func<Task<T>> getValue, int seconds,
            bool resetExpire = false);

        /// <summary>
        /// 获取指定Key的值
        /// 如何key不存在，则从委托中获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="getValue">委托</param>
        /// <param name="expire">过期时间</param>
        /// <param name="resetExpire">重置过期时间</param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key, Func<Task<T>> getValue, TimeSpan expire,
            bool resetExpire = false);

        /// <summary>
        /// 获取指定Key的值
        /// 如何key不存在，则从委托中获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key</param>
        /// <param name="getValue">委托</param>
        /// <param name="expire">指定的过期时间</param>
        /// <param name="resetExpire">重置过期时间</param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key, Func<Task<T>> getValue, DateTime expire,
            bool resetExpire = false);

        #endregion 委托
    }
}