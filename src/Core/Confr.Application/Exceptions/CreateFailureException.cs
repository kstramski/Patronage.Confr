using System;

namespace Confr.Application.Exceptions
{
    public class CreateFailureException : Exception
    {
        public CreateFailureException(string message)
            : base(message)
        {
        }

        public CreateFailureException(string name, object key, string message)
            : base($"Creation of entity \"{name}\" ({key}) failed. {message}")
        {
        }
    }
}
