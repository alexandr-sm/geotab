
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
        public static string ToGetRandomName => "Want to use a random name? y/n";
        public static string ToGetIfCategoryNeed => "Want to specify a category? y/n";
        public static string ToGetCategory => "Enter a category";
        public static string AvailableCategories => "Available categories:";
        public static string ToGetJokesNumber => "How many jokes do you want? (1-9)";
    }
    
}
