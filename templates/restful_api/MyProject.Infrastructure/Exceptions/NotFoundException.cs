using System;

namespace MyProject.Infrastructure.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public NotFoundException(object param, Type type)
            : base($"Could not find entity of type {type.Name} by {param}")
        {
        }
    }
}
