using NUnit.Framework;
using JokeGenerator;
using System;
using System.IO;


namespace JokeGeneratorTest
{
    public class Tests
    {
        private JokeGenerator.Services.PersonService _personService;
        [SetUp]
        public void Setup()
        {
            _personService = new(new System.Net.Http.HttpClient());
        }

        [Test]
        public void Test1()
        {
             var name = _personService.GetNamesAsync().Result;
            Assert.IsInstanceOf(typeof( (string firstname, string lastname) ), name);
            Assert.Greater(name.firstname.Length, 0);
            Assert.Greater(name.lastname.Length, 0);
            
            //using (var sw = new StringWriter())
            //{
            //    Console.SetOut(sw);
            //    JokeGenerator.Program.Main();

            //    var result = sw.ToString().Trim();
            //    Assert.AreEqual(Expected, result);
            //}
        }

        [Test]
        public void Test2()
        {
            //using (var sw = new StringWriter())
            //{
            //    Console.SetOut(sw);
            //    JokeGenerator.Program.Main();

            //    var result = sw.ToString().Trim();
            //    Assert.AreEqual(Expected, result);
            //}
        }
    }
}