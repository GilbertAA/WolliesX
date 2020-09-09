using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace WebAPI.Middleware
{
    public class RequestResponseLogger
    {
        private readonly RequestDelegate _next;

        public RequestResponseLogger(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = await FormatRequest(context.Request);
            System.Diagnostics.Trace.TraceError(request);
            await _next(context);
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var isEmptyContentType = string.IsNullOrEmpty(request.ContentType);

            if (isEmptyContentType || (
                !isEmptyContentType &&
                request.ContentType.Contains("multipart"))
            )
            {
                return ReadAndFormatRequestMultipartBody(request);
            }

            request.EnableBuffering();

            using (var reader = new StreamReader(request.Body, leaveOpen: true))
            {
                var payload = await reader.ReadToEndAsync();
                var result = ReadAndFormatRequestBody(request, payload);

                request.Body.Position = 0;
                return result;
            }
        }

        private string ReadAndFormatRequestMultipartBody(HttpRequest request)
        {
            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {HeadersAsString(request.Headers)} Multipart body content detected";
        }

        private string ReadAndFormatRequestBody(HttpRequest request, string body)
        {
            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {HeadersAsString(request.Headers)} { body }";
        }

        private string HeadersAsString(IHeaderDictionary headerDictionary)
        {
            var filteredHeaders = headerDictionary.Where(kvp => kvp.Key != "Authorization" && kvp.Key != "Cookie");
            return JsonConvert.SerializeObject(filteredHeaders, Formatting.Indented);
        }
       
    }
    public static class ResponseRequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestRequestLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLogger>();
        }
    }
}
