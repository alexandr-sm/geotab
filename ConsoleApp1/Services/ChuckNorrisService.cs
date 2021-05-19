using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using JokeGenerator.Models;
using JokeGenerator.Helpers;

namespace JokeGenerator.Services
{
    public class ChuckNorrisService
    {
        public HttpClient Client { get; }

        public ChuckNorrisService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://api.chucknorris.io");
            Client = client;
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await Client.GetFromJsonAsync<IEnumerable<String>>("jokes/categories");
            return categories.Select(c => new Category() { Name = c });
        }

        public async Task<String> GetRandomJokeAsync(IDictionary<string, string> parameters = null)
        {
            string url = "jokes/random".AddQueryString(parameters);
            var joke = await Client.GetFromJsonAsync<Joke>(url);
            return joke.Value;
        }


        public async IAsyncEnumerable<string> GetRandomJokesAsync(int numberOfJokes = 1, string categoryOfJokes = null)
        {
            Dictionary<string, string> category = String.IsNullOrEmpty(categoryOfJokes.Trim()) ? null : new () { { "category", categoryOfJokes.Trim() } };
            foreach (int n in Enumerable.Range(1, numberOfJokes))
            {
                yield return await GetRandomJokeAsync(category);
            }
        }

        public async IAsyncEnumerable<string> GetRandomJokesAsync((string first, string last)? names, int numberOfJokes = 1, string categoryOfJokes = null)
        {
            var newname = names.HasValue ? $"{names.Value.first.Trim()} {names.Value.last.Trim()}" : String.Empty;
            Dictionary<string, string> category = String.IsNullOrEmpty(categoryOfJokes.Trim()) ? null : new() { { "category", categoryOfJokes.Trim() } };
            foreach (int n in Enumerable.Range(1, numberOfJokes))
            {
                var joke = await GetRandomJokeAsync(category);
                joke = String.IsNullOrEmpty(newname) ? joke : joke.Replace("Chuck Norris", newname);
                yield return joke;
            }
        }

        //public async IAsyncEnumerable<string> GetRandomJokeAsync(string firstname, string lastname, int numberOfJokes,  string categoryOfJokes)
        //{
        //    return Task.WhenAll(from n in Enumerable.Range(1, numberOfJokes) select GetRandomJokeAsync());
        //}

        //public static string[] GetRandomJokes(string firstname, string lastname, string category)
        //{
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri(_url);
        //    string url = "jokes/random";
        //    if (category != null)
        //    {
        //        if (url.Contains('?'))
        //            url += "&";
        //        else url += "?";
        //        url += "category=";
        //        url += category;
        //    }

        //    string joke = Task.FromResult(client.GetStringAsync(url).Result).Result;

        //    if (firstname != null && lastname != null)
        //    {
        //        int index = joke.IndexOf("Chuck Norris");
        //        string firstPart = joke.Substring(0, index);
        //        string secondPart = joke.Substring(0 + index + "Chuck Norris".Length, joke.Length - (index + "Chuck Norris".Length));
        //        joke = firstPart + " " + firstname + " " + lastname + secondPart;
        //    }

        //    return new string[] { JsonConvert.DeserializeObject<dynamic>(joke).value };
        //}

    }
}
