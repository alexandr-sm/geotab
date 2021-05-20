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

namespace JokeGenerator.Services
{
    public class PersonService
    {
        const string BaseUrl = "https://www.names.privserv.com/api/";
        readonly (string Name, string Value) DefaultRegionParam = ("region", "Canada");
        public HttpClient Client { get; }
        
        public PersonService(HttpClient client)
        {
            client.BaseAddress = new Uri(BaseUrl);
            Client = client;
        }

        public async Task<Person> GetRandomPersonAsync(IDictionary<string, string> parameters = null)
        {
            try
            {
                string url = String.Empty.AddQueryString(parameters);
                var person = await Client.GetFromJsonAsync<Person>(url);
                return person;
            }
            catch (PersonServiceException ex)
            {
                throw new PersonServiceException(PersonServiceException.PERSON_GET_ERROR, ex);
            }
            
        }

        public async Task<(string firstname, string lastname)> GetNamesAsync(IDictionary<string, string> parameters = null)
        {
            var person = await GetRandomPersonAsync(parameters);
            return (person?.Name, person?.Surname);
        }

        public async Task<(string firstname, string lastname)?> GetCanadaNamesAsync(IDictionary<string, string> parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, string>();
            parameters.Add(DefaultRegionParam.Name, DefaultRegionParam.Value);
            var person = await GetRandomPersonAsync(parameters);
            return person == null ? null : (person.Name, person.Surname);
        }

    }
}
