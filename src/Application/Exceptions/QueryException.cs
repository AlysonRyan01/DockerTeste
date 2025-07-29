namespace Application.Exceptions;

public class QueryException<T> : BaseQueryException
{
    public T Type { get; }
    
    public QueryException(T type, string message) : base(message)
    {
        Type = type;
    }
}

public abstract class BaseQueryException : Exception
{
    protected BaseQueryException(string message) : base(message) { }
}