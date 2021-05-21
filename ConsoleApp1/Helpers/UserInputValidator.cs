using System;
using System.Linq;
using JokeGenerator.Services;

namespace JokeGenerator.Helpers
{
    public static class UserInputValidator
    {
        private const int JokesMinNumber = 1;
        private const int JokesMaxNumber = 9;

        /// <summary>
        /// Check if inputValue is 'y' or 'n'
        /// </summary>
        /// <param name="inputValue">char to be validated</param>
        /// <returns>one of ValidationOutcome options</returns>
        public static ValidationOutcome YesNoValidate(char inputValue)
        {
            return new char[] { 'y', 'n' }.Contains(inputValue) ? ValidationOutcome.ValidationSuccess 
                : ValidationOutcome.ValidationInvalidInputGeneric;
        }

        /// <summary>
        /// Check if input parameter is in category collection
        /// </summary>
        /// <param name="inputCategoryName"></param>
        /// <returns></returns>
        public static ValidationOutcome JokeCategoryValidate(string inputCategoryName)
        {
            var _categoryList = new ChuckNorrisService().GetCategoriesAsync().Result;
            return _categoryList.Contains(inputCategoryName) ? ValidationOutcome.ValidationSuccess 
                : ValidationOutcome.ValidationInvalidInputGeneric;
        }

        /// <summary>
        /// Check if input parameter is int and also in range between 1 and 9
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        public static ValidationOutcome RangeValidate(string inputValue)
        {
            if (!int.TryParse(inputValue, out int jokeNumbers))
            {
                return ValidationOutcome.ValidationInvalidInputGeneric;
            }

            return jokeNumbers <= JokesMaxNumber && jokeNumbers >= JokesMinNumber ? ValidationOutcome.ValidationSuccess 
                : ValidationOutcome.ValidationOutOfRange;
        }
    }
}
