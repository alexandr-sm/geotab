﻿using JokeGenerator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JokeGenerator
{
    public class EntryPoint
    {
        static string[] results = new string[50];
        static char key;
        static (string first, string last)? names;
        //static ConsolePrinter printer = new ConsolePrinter();
        static string category;

        private readonly ChuckNorrisService _chuckNorrisService;
        private readonly PersonService _personService;
        private readonly IPrinter _printer;

        public EntryPoint(ChuckNorrisService chuckNorrisService, PersonService personService, IPrinter printer)
        {
            _chuckNorrisService = chuckNorrisService;
            _printer = printer;
            _personService = personService;
        }

        public async Task Run(String[] args)
        {
            //_printer.Print("Press ? to get instructions.");
            //if (Console.ReadLine() == "?")
            //{
            while (true)
            {

                _printer.Print("Press c to get categories");
                _printer.Print("Press r to get random jokes");
                GetEnteredKey(Console.ReadKey());
                if (key == 'c')
                {
                    getCategories();
                    var categories = await _chuckNorrisService.GetCategoriesAsync();
                    _printer.Print(categories.Select(c => c.Name));
                    //PrintResults();
                }
                if (key == 'r')
                {
                    _printer.Print("\nWant to use a random name? y/n");
                    GetEnteredKey(Console.ReadKey());
                    if (key == 'y')
                    {
                        names = await _personService.GetCanadaNamesAsync();
                    }
                        //GetNames();
                    _printer.Print("\nWant to specify a category? y/n");
                    GetEnteredKey(Console.ReadKey());
                    if (key == 'y')
                    {
                        //_printer.Print("\nHow many jokes do you want? (1-9)");
                        //int n = Int32.Parse(Console.ReadLine());
                        _printer.Print("\nEnter a category;");
                        category = Console.ReadLine();
                        //var joke = _chuckNorrisService.GetRandomJokesAsync();
                        //GetRandomJokes(Console.ReadLine(), n);
                        //_printer.Print(results);
                        ////PrintResults();
                    }
                    else
                    {
                        category = String.Empty;
                        //_printer.Print("\nHow many jokes do you want? (1-9)");
                        //int n = Int32.Parse(Console.ReadLine());
                        //var joke = _chuckNorrisService.GetRandomJokesAsync();
                        //GetRandomJokes(null, n);
                        //_printer.Print(results);
                        ////PrintResults();
                    }
                    _printer.Print("\nHow many jokes do you want? (1-9)");
                    int n = Int32.Parse(Console.ReadLine());
                    var jokes = _chuckNorrisService.GetRandomJokesAsync(names: names, numberOfJokes: n, categoryOfJokes: category);
                    await foreach (var joke in jokes)
                    {
                        _printer.Print(joke);
                    }
                }
                names = null;
            }
            //}
        }

        //private static void PrintResults()
        //{
        //    _printer.Print("[" + string.Join(",", results) + "]");
        //}

        private static void GetEnteredKey(ConsoleKeyInfo consoleKeyInfo)
        {
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
            }
        }



        private static void GetRandomJokes(string category, int number)
        {
            new JsonFeed("https://api.chucknorris.io", number);
            results = JsonFeed.GetRandomJokes(names?.Item1, names?.Item2, category);
        }

        private static void getCategories()
        {
            new JsonFeed("https://api.chucknorris.io", 0);
            results = JsonFeed.GetCategories();
        }

        private static void GetNames()
        {
            new JsonFeed("https://www.names.privserv.com/api/", 0);
            dynamic result = JsonFeed.Getnames();
            names = Tuple.Create(result.name.ToString(), result.surname.ToString());
        }

    }
}