using System.Collections.Generic;

namespace JokeGenerator.Services
{
    public interface IPrinter
    {
        void Print(IEnumerable<string> value);
        void Print(string value);
        void PrintLine(string value);
    }
}