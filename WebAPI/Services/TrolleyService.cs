using Newtonsoft.Json;
using WebAPI.Entity;
using WebAPI.Util;

namespace WebAPI.Services
{
    public class TrolleyService : ITrolleyService
    {
        private const string UrlPath = "api/resource/trolleyCalculator";

        private readonly IHttpClientWrapper _httpClientWrapper;


        public TrolleyService(IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
        }

        public double CalculateTrolley(TrolleyInput input)
        {
            var url = UrlConstructor.ConstructUri(UrlPath);
             return _httpClientWrapper.Post<double>(url, JsonConvert.SerializeObject(input)).Result;
        }
    }
}
