using NUnit.Framework;
using JokeGenerator.Services;
using JokeGenerator.Models;
using System;
using System.IO;
using JokeGenerator.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace JokeGeneratorTest
{
    public class UserInputValidatorTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("not-existing-category")]
        public void NotExistingCategoryAsyncTest(string category)
        {
            var validationOutcome = UserInputValidator.JokeCategoryValidate(category);
            Assert.That(validationOutcome, Is.EqualTo(ValidationOutcome.ValidationInvalidInputGeneric));
        }

        [Test]
        [TestCaseSource(nameof(GetCategories))]
        public void ExistingCategoryAsyncTest(string category)
        {
            var validationOutcome = UserInputValidator.JokeCategoryValidate(category);
            Assert.That(validationOutcome, Is.EqualTo(ValidationOutcome.ValidationSuccess));
        }

        [TestCaseSource(nameof(GetBadInputForRange))]
        public void RangeValidateBadScenarioTest(string inputValue)
        {
            var validationOutcome = UserInputValidator.RangeValidate(inputValue);
            Assert.That(validationOutcome, !Is.EqualTo(ValidationOutcome.ValidationSuccess));
        }

        [TestCaseSource(nameof(GetGoodInputForRange))]
        public void RangeValidateGoodScenarioTest(string inputValue)
        {
            var validationOutcome = UserInputValidator.RangeValidate(inputValue);
            Assert.That(validationOutcome, Is.EqualTo(ValidationOutcome.ValidationSuccess));
        }

        #region TestCaseSources
        static IEnumerable<string> GetCategories()
        {
            ChuckNorrisService _chuckNorrisService = new();
            return _chuckNorrisService.GetCategoriesAsync().Result;
        }

        static IEnumerable<string> GetBadInputForRange()
        {
            return new string[] { "-1", "abs", "90", "", "*^*&", "44", "-90", "er567" };
        }

        static IEnumerable<string> GetGoodInputForRange()
        {
            return Enumerable.Range(1, 9).Select(i => i.ToString());
        }
        #endregion
    }
}