using Trasen.PaperFree.Domain.Shared.Appsettings;

namespace Trasen.PaperFree.Application.SystemBasicInfo.Common
{
    /// <summary>
    ///
    /// </summary>
    internal static class HttpTools
    {
        /// <summary>
        /// Post 请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClientFactory"></param>
        /// <param name="methodName"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public static async Task<T?> PostAsync<T>(this IHttpClientFactory httpClientFactory, string methodName, object obj)
        {
            var basePlatformSetting = Appsetting.Instance.GetSection("TRASEN_BASE_PLATFORM").Get<TrasenBasePlatformSetting>();
            var client = httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(10);
            StringContent strcontent = new StringContent(
                JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{basePlatformSetting.domainName}{methodName}", strcontent);
            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ResultJson<T>>(result);
            if (data == null) throw new BusinessException(MessageType.Error, $"平台接口请求超时或异常", $"{basePlatformSetting.domainName}{methodName}{result}");
            return data.Data;
        }
    }
}