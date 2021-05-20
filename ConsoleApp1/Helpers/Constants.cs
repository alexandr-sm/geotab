using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JokeGenerator.Helpers
{
    public static class CacheKeys
    {
        public static string JokeCategories => "_JokeCategories";
    }

    public static class QueryStringParameters
    {
        public static string JokeCategory => "category";
    }

    public static class UIPrompts
    {
        public static string ToGetCategoriesList => "Press c to get categories";
        public static string ToGetRandomJokes => "Press r to get random jokes";
    }
    
}
