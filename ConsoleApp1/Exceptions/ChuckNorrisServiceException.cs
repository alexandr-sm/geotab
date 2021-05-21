using System;

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

        public static string CATEGORY_GET_ERROR => "Can't get categories at this moment.";
        public static string JOKES_GET_ERROR => "Can't get jokes at this moment.";
    }
}
