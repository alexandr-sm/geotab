using JokeGenerator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using JokeGenerator.Helpers;

namespace JokeGenerator
{
    public class EntryPoint
    {
        
        private readonly IChuckNorrisService _chuckNorrisService;
        private readonly IPersonService _personService;
        private readonly IPrinter _printer;
        private readonly ILogger<EntryPoint> _logger;

        public EntryPoint(IChuckNorrisService chuckNorrisService, IPersonService personService, IPrinter printer, ILogger<EntryPoint> logger)
        {
            _chuckNorrisService = chuckNorrisService;
            _printer = printer;
            _personService = personService;
            _logger = logger;
        }

        public async Task Run(String[] args)
        {
            char key;
            (string first, string last)? names;
            string category;
            IAsyncEnumerable<string> jokes;
            try
            {
                while (true)
                {
                    names = null;
                    category = null;
                    jokes = null;

                    _printer.Print(UIPrompts.ToGetCategoriesList);
                    _printer.Print(UIPrompts.ToGetRandomJokes);
                    key = GetEnteredKey(Console.ReadKey());
                    if (key == 'c')
                    {
                        var categories = await _chuckNorrisService.GetCategoriesAsync();
                        _printer.Print(categories);
                    }
                    if (key == 'r')
                    {
                        names = await GetRandomNames();

                        category = await GetJokeCategory();

                        jokes = GetJokes(names: names, categoryOfJokes: category);
                        
                        await foreach (var joke in jokes)
                        {
                            _printer.Print(joke);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
            //}
        }

        private IAsyncEnumerable<string> GetJokes((string first, string last)? names, string categoryOfJokes = null)
        {
            _printer.Print(UIPrompts.ToGetJokesNumber);
            string numberOfJokesStr = Console.ReadLine();
            var validationResult = UserInputValidator.RangeValidate(numberOfJokesStr);
            while (validationResult != ValidationOutcome.ValidationSuccess)
            {
                _printer.PrintLine(validationResult.Description);
                _printer.Print(UIPrompts.ToGetJokesNumber);
                numberOfJokesStr = Console.ReadLine();
                validationResult = UserInputValidator.RangeValidate(numberOfJokesStr);
            }

            int.TryParse(numberOfJokesStr, out int numberOfJokes);
            return _chuckNorrisService.GetRandomJokesAsync(names: names, numberOfJokes: numberOfJokes, categoryOfJokes: categoryOfJokes);
        }

        private async Task<string> GetJokeCategory()
        {
            _printer.PrintLine(UIPrompts.ToGetIfCategoryNeed);
            var key = GetEnteredKey(Console.ReadKey());
            var validationResult = UserInputValidator.YesNoValidate(key);
            while (validationResult != ValidationOutcome.ValidationSuccess)
            {
                _printer.PrintLine(validationResult.Description);
                _printer.Print(UIPrompts.ToGetIfCategoryNeed);
                key = GetEnteredKey(Console.ReadKey());
                validationResult = UserInputValidator.YesNoValidate(key);
            }

            if (key == 'n')
            {
                return String.Empty;
            }

            _printer.PrintLine(UIPrompts.ToGetCategory);
            var category = Console.ReadLine();
            validationResult = UserInputValidator.JokeCategoryValidate(category);
            while (validationResult != ValidationOutcome.ValidationSuccess)
            {
                _printer.Print(validationResult.Description);
                _printer.Print(UIPrompts.AvailableCategories);
                var categories = await _chuckNorrisService.GetCategoriesAsync();
                _printer.Print(categories);
                _printer.Print(UIPrompts.ToGetCategory);
                category = Console.ReadLine();
                validationResult = UserInputValidator.JokeCategoryValidate(category);
            }

            return category;

        }

        private async Task<(string fistname, string lastname)?> GetRandomNames()
        {
            _printer.PrintLine(UIPrompts.ToGetRandomName);
            var key = GetEnteredKey(Console.ReadKey());
            var validationResult = UserInputValidator.YesNoValidate(key);
            while (validationResult != ValidationOutcome.ValidationSuccess)
            {
                _printer.PrintLine(validationResult.Description);
                _printer.Print(UIPrompts.ToGetRandomName);
                key = GetEnteredKey(Console.ReadKey());
                validationResult = UserInputValidator.YesNoValidate(key);
            }
            if (key == 'y')
            {
                (string first, string last)? names = await _personService.GetCanadaNamesAsync();
                if (names != null)
                {
                    _printer.PrintLine($"Generated name: {names.Value.first?.Trim()} {names.Value.last?.Trim()}");
                    return names;
                }
            }
            return null;
        }

        private static char GetEnteredKey(ConsoleKeyInfo consoleKeyInfo)
        {
            char key;
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.C:
                    key = 'c';
                    break;
                case ConsoleKey.D0:
                    key = '0';
                    break;
                case ConsoleKey.D1:
                    key = '1';
                    break;
                case ConsoleKey.D3:
                    key = '3';
                    break;
                case ConsoleKey.D4:
                    key = '4';
                    break;
                case ConsoleKey.D5:
                    key = '5';
                    break;
                case ConsoleKey.D6:
                    key = '6';
                    break;
                case ConsoleKey.D7:
                    key = '7';
                    break;
                case ConsoleKey.D8:
                    key = '8';
                    break;
                case ConsoleKey.D9:
                    key = '9';
                    break;
                case ConsoleKey.R:
                    key = 'r';
                    break;
                case ConsoleKey.Y:
                    key = 'y';
                    break;
                case ConsoleKey.N:
                    key = 'n';
                    break;
                default:
                    key = Char.MinValue;
                    break;
            }
            return key;
        }
    }
}
