using NUnit.Framework;
using JokeGenerator.Services;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using JokeGenerator.Exceptions;

namespace JokeGeneratorTest
{
    public class ChuckNorrisServiceTests
    {
        private ChuckNorrisService _chuckNorrisService;

        [SetUp]
        public void Setup()
        {
            _chuckNorrisService = new(new System.Net.Http.HttpClient(), new MemoryCache(new MemoryCacheOptions()));
        }

        [Test]
        public void GetCategoriesAsyncTest()
        {
            var categories = _chuckNorrisService.GetCategoriesAsync().Result;
            categories.ToList().ForEach(c => Assert.Greater(c.Length, 0));
        }

        [Test]
        public void GetRandomJokeAsyncTest()
        {
            var joke = _chuckNorrisService.GetRandomJokeAsync().Result;
            Assert.IsInstanceOf(typeof(string), joke);
            Assert.Greater(joke.Length, 0);
        }

        [Test]
        public void GetRandomJokeAsyncExceptionTest()
        {
            var parameters = new Dictionary<string, string>() { { "category", "not-existing-category" } };
            var ex = Assert.ThrowsAsync<ChuckNorrisServiceException>(async () => { await _chuckNorrisService.GetRandomJokeAsync(parameters); });
            Assert.That(ex.Message, Is.EqualTo(ChuckNorrisServiceException.JOKES_GET_ERROR));
        }

        [TestCaseSource(nameof(GetNameAndJokesNumber))]
        public void GetRandomJokesAsyncTestAsync((string firstname, string lastname, int numberOfJokes) v)
        {
            var randomJokes = _chuckNorrisService.GetRandomJokesAsync(names: (v.firstname, v.lastname), numberOfJokes: v.numberOfJokes, categoryOfJokes: null).ToListAsync().Result;

            foreach (var joke in randomJokes)
            {
                Assert.That(joke, Contains.Substring($"{v.firstname} {v.lastname}"));
            }

            Assert.AreEqual(v.numberOfJokes, randomJokes.Count);
        }

        #region TestCaseSources
        static IEnumerable<(string firstname, string lastname, int numberOfJokes)> GetNameAndJokesNumber()
        {
            return new (string firstname, string lastname, int numberOfJokes)[]
            {
                ("aaa", "bbb", 9),
                ("aaa", "bbb", 5)
            };

        }

        #endregion

    }
}