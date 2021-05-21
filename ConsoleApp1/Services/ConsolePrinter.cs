using System;
using System.Collections.Generic;
using System.Linq;

namespace JokeGenerator.Services
{
    public class ConsolePrinter : IPrinter
    {
        public void Print(String value) => Console.WriteLine(value);
        public void PrintLine(String value) => Console.WriteLine($"{Environment.NewLine}{value}");
        public void Print(IEnumerable<String> value)
        {
            Console.WriteLine(); 
            value.ToList().ForEach(v => Console.WriteLine(v));
        }
    }
}
