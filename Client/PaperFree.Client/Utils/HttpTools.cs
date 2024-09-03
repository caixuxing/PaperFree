using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PaperFree.Client.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PaperFree.Client.Utils
{
    public class HttpTools
    {
        private readonly IHttpClientFactory httpClientFactory;
        public HttpTools(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<TResponse> PostTokenAsync<TResponse, TRequest>(string url, TRequest content)
        {
            try
            {
                string token = Cache.Instance.Get("token");
                HttpClient client = httpClientFactory.CreateClient();
                //client.Timeout = TimeSpan.FromSeconds(5);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("Authorization", $"{token}");
                StringContent strcontent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"{ApplicationProject.domainName}/{url}", strcontent);
                var result = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(result))
                {
                    TResponse u = JsonConvert.DeserializeObject<TResponse>(result);
                    return u;
                }
               
            }
            catch (TaskCanceledException ex)
            {
                new CustomException("请求超时");

            }
            return default(TResponse);
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TResponse> GetAsync<TResponse>(string url, string id)
        {
            string token = Cache.Instance.Get("token");
            HttpClient client = httpClientFactory.CreateClient();
            //client.Timeout= TimeSpan.FromSeconds(5);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"{token}");
            var response = await client.GetAsync($"{ApplicationProject.domainName}/{url}/{id}");
            var result=await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(result))
            {
                TResponse u = JsonConvert.DeserializeObject<TResponse>(result);
                return u;
            }
            return default(TResponse);
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TResponse> GetAsync<TResponse>(string url)
        {
            string token = Cache.Instance.Get("token");
            HttpClient client = httpClientFactory.CreateClient();
            //client.Timeout = TimeSpan.FromSeconds(5);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"{token}");
            var response = await client.GetAsync($"{ApplicationProject.domainName}{url}");
            var result = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(result))
            {
                TResponse u = JsonConvert.DeserializeObject<TResponse>(result);
                return u;
            }
            return default(TResponse);
        }


        /// <summary>
        /// Put
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TResponse> PutAsync<TResponse, TRequest>(string url, string id,TRequest content)
        {
            string token = Cache.Instance.Get("token");
            HttpClient client = httpClientFactory.CreateClient();
           // client.Timeout = TimeSpan.FromSeconds(5);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("Authorization", $"{token}");
            StringContent strcontent = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{ApplicationProject.domainName}/{url}/{id}", strcontent);
            var result = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(result))
            {
                TResponse u = JsonConvert.DeserializeObject<TResponse>(result);
                return u;
            }
            return default(TResponse);
        }
    }
}
