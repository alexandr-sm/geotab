using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JokeGenerator.Services
{
    public class ConsolePrinter : IPrinter
    {
        public void Print(String value) => Console.WriteLine(value);
        public void Print(IEnumerable<String> value) => value.ToList().ForEach(v => Console.WriteLine(v));
    }
}
