using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JokeGenerator.Exceptions
{
    public class ChuckNorrisServiceException : Exception
    {

        public ChuckNorrisServiceException() : base()
        {
        }


        public ChuckNorrisServiceException(string message) : base(message)
        {
        }


        public ChuckNorrisServiceException(string message, Exception innerException) :
            base(message, innerException)
        {
        }
    }
}
