using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Text;
using System.Threading.Tasks;

namespace JokeGenerator.Helpers
{
    public static class QueryStringBuilder
    {
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
