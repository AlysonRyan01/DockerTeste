namespace Domain.Exceptions;

public sealed class ValueObjectException<T> : BaseValueObjectException
{
    public string Prop { get; }
    
    public ValueObjectException(string prop, string message)
        : base(message)
    {
        Prop = prop;
    }
}

public abstract class BaseValueObjectException : Exception
{
    protected BaseValueObjectException(string message)
        : base(message)
    {
    }
}
