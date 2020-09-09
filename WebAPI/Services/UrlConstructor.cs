using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.WebUtilities;
using UrlCombineLib;

namespace WebAPI.Services
{
    public static class UrlConstructor
    {
        public static Uri ConstructUri(string path)
        {
            var url = UrlCombine.Combine(Constants.UrlResourceBase, path);
            var tokenParam = new Dictionary<string, string>
            {
                {Constants.UrlParamToken, Constants.Token}
            };
            var finalUrl = new Uri(QueryHelpers.AddQueryString(url, tokenParam));
            return finalUrl;
        }
    }
}
