using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebAPI.Util
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpClientWrapper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T> Get<T>(Uri url)
        {
            using var client = _httpClientFactory.CreateClient(Constants.Token);
            var response = await client.GetAsync(url);
            if (!response.IsSuccessStatusCode) return default(T);

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);

        }
    }
}
