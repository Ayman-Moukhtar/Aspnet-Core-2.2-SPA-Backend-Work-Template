using System;

namespace WorkTemplate.Croscutting.Contracts.CustomExceptions
{
    public class UserErrorException : Exception
    {
        public UserErrorException(string message) : base(message)
        {
        }
    }
}
