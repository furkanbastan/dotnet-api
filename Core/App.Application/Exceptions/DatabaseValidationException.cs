namespace App.Application.Exceptions;

public class DatabaseValidationException : Exception
{
    public DatabaseValidationException()
    {
    }

    public DatabaseValidationException(string? message) : base(message)
    {
    }

    public DatabaseValidationException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
