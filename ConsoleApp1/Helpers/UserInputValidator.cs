using System;
using System.Collections.Generic;
using System.Linq;
using JokeGenerator.Services;
using System.Text;
using System.Threading.Tasks;

namespace JokeGenerator.Helpers
{
    public static class UserInputValidator
    {
        private const int JokesMinNumber = 1;
        private const int JokesMaxNumber = 9;

        public static ValidationOutcome YesNoValidate(char inputValue)
        {
            return new char[] { 'y', 'n' }.Contains(inputValue) ? ValidationOutcome.ValidationSuccess 
                : ValidationOutcome.ValidationInvalidInputGeneric;
        }

        public static ValidationOutcome JokeCategoryValidate(string inputCategoryName)
        {
            var _categoryList = new ChuckNorrisService().GetCategoriesAsync().Result;
            return _categoryList.Contains(inputCategoryName) ? ValidationOutcome.ValidationSuccess 
                : ValidationOutcome.ValidationInvalidInputGeneric;
        }

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
