using System;
using System.Threading.Tasks;

namespace WebAPI.Util
{
    public interface IHttpClientWrapper
    {
        Task<T> Get<T>(Uri url);
        Task<T> Post<T>(Uri url, string jsonData);
    }
}