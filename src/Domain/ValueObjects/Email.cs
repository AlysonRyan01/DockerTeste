using System.Text.RegularExpressions;
using Domain.Exceptions;

namespace Domain.ValueObjects;

public sealed record Email
{
    public string Address { get; }

    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ValueObjectException<Email>(nameof(address), "Email não pode ser vazio.");

        if (!EmailRegex.IsMatch(address))
            throw new ValueObjectException<Email>(nameof(address), "Formato de email inválido.");

        Address = address.Trim().ToLower();
    }

    public override string ToString() => Address;
    
    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase
    );
};