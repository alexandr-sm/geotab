using JokeGenerator.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JokeGenerator.Services
{
    public interface IPersonService
    {
        HttpClient Client { get; }

        Task<(string firstname, string lastname)?> GetCanadaNamesAsync(IDictionary<string, string> parameters = null);
        Task<(string firstname, string lastname)> GetNamesAsync(IDictionary<string, string> parameters = null);
        Task<Person> GetRandomPersonAsync(IDictionary<string, string> parameters = null);
    }
}