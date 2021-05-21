using NUnit.Framework;
using JokeGenerator.Services;
using JokeGenerator.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace JokeGeneratorTest
{
    public class PersonServiceTests
    {
        private PersonService _personService;
        private Person _person;
        [SetUp]
        public void Setup()
        {
            _personService = new(new System.Net.Http.HttpClient());
            _person = new();
        }

        [Test]
        public void GetNamesAsyncTest()
        {
            var name = _personService.GetNamesAsync().Result;
            Assert.IsInstanceOf(typeof((string firstname, string lastname)), name);
            Assert.Greater(name.firstname.Length, 0);
            Assert.Greater(name.lastname.Length, 0);
        }

        [Test]
        public void GetCanadaNamesAsyncTest()
        {
            var name = _personService.GetCanadaNamesAsync().Result;
            Assert.IsInstanceOf(typeof((string firstname, string lastname)?), name);
            Assert.Greater(name.Value.firstname.Length, 0);
            Assert.Greater(name.Value.lastname.Length, 0);
        }

        [Test]
        public void GetRandomPersonAsyncTest()
        {
            var person = _personService.GetRandomPersonAsync().Result;
            Assert.IsInstanceOf(typeof(Person), person);
            Assert.Greater(person.Name.Length, 0);
            Assert.Greater(person.Surname.Length, 0);
        }
    }
}