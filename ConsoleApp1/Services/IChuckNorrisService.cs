using JokeGenerator.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JokeGenerator.Services
{
    public interface IChuckNorrisService
    {
        HttpClient Client { get; }

        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<string> GetRandomJokeAsync(IDictionary<string, string> parameters = null);
        IAsyncEnumerable<string> GetRandomJokesAsync((string first, string last)? names, int numberOfJokes = 1, string categoryOfJokes = null);
        IAsyncEnumerable<string> GetRandomJokesAsync(int numberOfJokes = 1, string categoryOfJokes = null);
    }
}