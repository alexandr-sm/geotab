using NUnit.Framework;
using JokeGenerator.Services;
using JokeGenerator.Models;
using System.Linq;
using System;
using System.IO;
//using System.Runtime.Caching;
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
        public void GetNamesAsyncTest()
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
    }
}