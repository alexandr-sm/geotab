using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using JokeGenerator.Models;
using JokeGenerator.Helpers;
using JokeGenerator.Exceptions;

namespace JokeGenerator.Services
{
    public class PersonService : IPersonService
    {
        const string BaseUrl = "https://www.names.privserv.com/api/";
        readonly (string Name, string Value) DefaultRegionParam = ("region", "Canada");
        public HttpClient Client { get; }

        /// <summary>
        /// Setup HttpClient and Web API base url
        /// </summary>
        /// <param name="client"></param>
        public PersonService(HttpClient client)
        {
            client.BaseAddress = new Uri(BaseUrl);
            Client = client;
        }

        /// <summary>
        /// Get random generated Person data from Web API
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get person's first and last name by extracting them from random generated Person data
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<(string firstname, string lastname)> GetNamesAsync(IDictionary<string, string> parameters = null)
        {
            var person = await GetRandomPersonAsync(parameters);
            return (person?.Name, person?.Surname);
        }

        /// <summary>
        /// Get person's first and last name by extracting them from random generated Person data for Canada region
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<(string firstname, string lastname)?> GetCanadaNamesAsync(IDictionary<string, string> parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, string>();
            parameters.Add(DefaultRegionParam.Name, DefaultRegionParam.Value);
            var person = await GetRandomPersonAsync(parameters);
            return person == null ? null : (person.Name, person.Surname);
        }

    }
}
