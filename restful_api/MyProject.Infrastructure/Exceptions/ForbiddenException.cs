using System;

namespace MyProject.Infrastructure.Exceptions
{
    /// <summary>
    ///     Thrown when trying to access protected resource not in access scope
    /// </summary>
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message = "Not authorized") : base(message)
        {
        }

        public ForbiddenException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
