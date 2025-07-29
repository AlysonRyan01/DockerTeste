using Domain.Exceptions;

namespace Domain.ValueObjects;

public sealed record PasswordHash : ValueObject
{
    public string Value { get; }

    public PasswordHash(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ValueObjectException<PasswordHash>("PasswordHash", "Hash da senha não pode ser vazio.");

        Value = value;
    }

    public override string ToString() => Value;
};