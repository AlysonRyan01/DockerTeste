namespace Application.Exceptions;

public class CommandException<T> : BaseCommandException
{
    public T Type { get; }

    public CommandException(T type, string message) : base(message)
    {
        Type = type;
    }
}

public abstract class BaseCommandException : Exception
{
    protected BaseCommandException(string message) : base(message) { }
}