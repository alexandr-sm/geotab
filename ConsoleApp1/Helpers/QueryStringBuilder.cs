using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JokeGenerator.Helpers
{
    public static class QueryStringBuilder
    {
        /// <summary>
        /// Build connection string (?key1=value1&key2=value2)
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters">Dictionary with key value pairs</param>
        /// <returns>url + builded connection string</returns>
        public static string AddQueryString(this string url, IDictionary<string, string> parameters)
        {
            if (parameters == null)
            {
                return url;
            }

            var array = parameters
                .Select(p => string.Format("{0}={1}", HttpUtility.UrlEncode(p.Key), HttpUtility.UrlEncode(p.Value)))
                .ToArray();
            return $"{url}?{string.Join("&", array)}";
        }
    }
}
