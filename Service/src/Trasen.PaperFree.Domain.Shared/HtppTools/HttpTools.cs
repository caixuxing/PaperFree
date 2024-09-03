using Newtonsoft.Json;
using System.Text;
using Trasen.PaperFree.Domain.Shared.CustomException;
using Trasen.PaperFree.Domain.Shared.Response;

namespace Trasen.PaperFree.Domain.Shared.HtppTools
{
    /// <summary>
    ///
    /// </summary>
    public static class HttpTools
    {
        /// <summary>
        /// Post 请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpClientFactory"></param>
        /// <param name="InterfaceUrl">接口地址</param>
        /// <param name="obj">参数</param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public static async Task<T?> PostAsync<T>(this IHttpClientFactory httpClientFactory, string InterfaceUrl, object obj)
        {
            var client = httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(10);
            StringContent strcontent = new StringContent(
                JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{InterfaceUrl}", strcontent);
            var result = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<ResultJson<T>>(result);
            if (data == null) throw new BusinessException(MessageType.Error, $"平台接口请求超时或异常", $"{InterfaceUrl}{result}");
            return data.Data;
        }
    }
}