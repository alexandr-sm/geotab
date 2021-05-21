using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using JokeGenerator.Models;
using JokeGenerator.Helpers;
using JokeGenerator.Exceptions;
using Microsoft.Extensions.Caching.Memory;

namespace JokeGenerator.Services
{


    public class ChuckNorrisService : IChuckNorrisService
    {
        public HttpClient Client { get; }
        private readonly IMemoryCache _memoryCache;
        const string BaseUrl = "https://api.chucknorris.io";
        const string CategoryUrl = "jokes/categories";
        const string RandomJokeUrl = "jokes/random";

        public ChuckNorrisService(HttpClient client, IMemoryCache memoryCache)
        {
            client.BaseAddress = new Uri(BaseUrl);
            Client = client;
            _memoryCache = memoryCache;
        }

        public ChuckNorrisService() : this (new HttpClient(), new MemoryCache(new MemoryCacheOptions()))
        {
        }

        public async Task<IEnumerable<string>> GetCategoriesAsync()
        {
            try
            {
                var categories = await _memoryCache.GetOrCreateAsync<IEnumerable<string>>(CacheKeys.JokeCategories, async entry =>
                {
                    entry.SlidingExpiration = TimeSpan.FromHours(1);
                    return await Client.GetFromJsonAsync<IEnumerable<string>>(CategoryUrl);
                });
                
                return categories;
            }
            catch (Exception ex)
            {
                throw new ChuckNorrisServiceException(ChuckNorrisServiceException.CATEGORY_GET_ERROR, ex);
            }
        }

        public async Task<String> GetRandomJokeAsync(IDictionary<string, string> parameters = null)
        {
            try
            {
                string url = RandomJokeUrl.AddQueryString(parameters);
                var joke = await Client.GetFromJsonAsync<Joke>(url);
                return joke.Value;
            }
            catch (Exception ex)
            {
                throw new ChuckNorrisServiceException(ChuckNorrisServiceException.JOKES_GET_ERROR, ex);
            }
        }


        public async IAsyncEnumerable<string> GetRandomJokesAsync(int numberOfJokes = 1, string categoryOfJokes = null)
        {
            Dictionary<string, string> category = 
                String.IsNullOrEmpty(categoryOfJokes.Trim()) ? null 
                : new() { { QueryStringParameters.JokeCategory, categoryOfJokes.Trim() } };
            foreach (int n in Enumerable.Range(1, numberOfJokes))
            {
                yield return await GetRandomJokeAsync(category);
            }
        }

        public async IAsyncEnumerable<string> GetRandomJokesAsync((string first, string last)? names, int numberOfJokes = 1, string categoryOfJokes = null)
        {
            var newname = names.HasValue ? $"{names.Value.first.Trim()} {names.Value.last.Trim()}" : String.Empty;
            Dictionary<string, string> category = 
                String.IsNullOrEmpty(categoryOfJokes) ? null 
                : new() { { QueryStringParameters.JokeCategory, categoryOfJokes.Trim() } };

            foreach (int n in Enumerable.Range(1, numberOfJokes))
            {
                var joke = await GetRandomJokeAsync(category);
                joke = String.IsNullOrEmpty(newname) ? joke : joke.Replace("Chuck Norris", newname);
                yield return joke;
            }
        }
    }
}
