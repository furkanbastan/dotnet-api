using System.Runtime.Serialization;

namespace App.Domain.Exceptions;

public class ValidationExceptions : Exception
{
    public ValidationExceptions()
    {
    }

    public ValidationExceptions(string? message) : base(message)
    {
    }

    public ValidationExceptions(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
