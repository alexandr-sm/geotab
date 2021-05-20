using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JokeGenerator.Helpers
{
    public class ValidationOutcome
    {
        public int Code { get; set; }
        public string Description { get; set; }
    }

    public static class UserInputValidator
    {
        public static ValidationOutcome ValidationSuccess => new() { Code = 0, Description = "Ok." };
        public static ValidationOutcome ValidationInvalidInputGeneric => new() { Code = 1, Description = "Invalid input, try again." };

        public static ValidationOutcome YesNoValidate(char inputValue)
        {
            return new char[] { 'y', 'n' }.Contains(inputValue) ? ValidationSuccess : ValidationInvalidInputGeneric;
        }

        public static ValidationOutcome JokeCategoryValidate(string inputCategoryName)
        {
            return new();
        }

        public static ValidationOutcome RangeValidate(int inputValue, int minValue, int maxValue)
        {
            return new();
        }
    }
}
