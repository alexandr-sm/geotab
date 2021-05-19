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
    public class PersonService
    {
        public HttpClient Client { get; }

        public PersonService(HttpClient client)
        {
            client.BaseAddress = new Uri("https://www.names.privserv.com/api/");
            Client = client;
        }

        public async Task<Person> GetRandomPersonAsync(IDictionary<string, string> parameters = null)
        {
            string url = "".AddQueryString(parameters);
            var person = await Client.GetFromJsonAsync<Person>(url);
            return person;
        }

        public async Task<(string firstname, string lastname)> GetNamesAsync(IDictionary<string, string> parameters = null)
        {
            var person = await GetRandomPersonAsync(parameters);
            return (person.Name, person.Surname);
        }

        public async Task<(string firstname, string lastname)> GetCanadaNamesAsync(IDictionary<string, string> parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, string>();
            parameters.Add("region", "Canada");
            var person = await GetRandomPersonAsync(parameters);
            return (person.Name, person.Surname);
        }

    }
}
