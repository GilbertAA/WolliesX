using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebAPI.Util
{
    public class HttpClientWrapper
    {
        private static System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();

        public static async Task<T> Get<T>(Uri url)
        {
            var response = httpClient.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }

            return default(T);
        }
    }
}
