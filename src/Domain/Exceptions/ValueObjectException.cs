namespace Domain.Exceptions;

public sealed class ValueObjectException<T> : BaseValueObjectException
{
    public string Prop { get; }
    
    public ValueObjectException(string prop, string? message = null)
        : base(message ?? $"Erro ao criar o Value Object do tipo {typeof(T).Name}.")
    {
        Prop = prop;
    }
}

public abstract class BaseValueObjectException : Exception
{
    protected BaseValueObjectException(string prop, string? message = null) { }
}