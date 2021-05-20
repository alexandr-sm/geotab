using System;

namespace JokeGenerator.Exceptions
{
     public class PersonServiceException : Exception
    {

        public PersonServiceException() : base()
        {
        }

        public PersonServiceException(string message) : base(message)
        {
        }

        public PersonServiceException(string message, Exception innerException) :
            base(message, innerException)
        {
        }

        public static string PERSON_GET_ERROR => "Can't get a person info at this moment.";
    }
}
