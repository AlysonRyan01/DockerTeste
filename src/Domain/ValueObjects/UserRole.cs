namespace Domain.ValueObjects;

public sealed record UserRole
{
    public string Name { get; }
    public DateTime? ValidUntil { get; private set; }

    public UserRole(string name, DateTime? validUntil = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role inválida", nameof(name));

        Name = name.ToUpper();
        ValidUntil = validUntil;
    }

    public bool IsActive() 
        => !ValidUntil.HasValue || ValidUntil.Value >= DateTime.UtcNow;

    public void ExtendValidity(DateTime newExpiry)
    {
        if (!ValidUntil.HasValue || newExpiry > ValidUntil.Value)
            ValidUntil = newExpiry;
    }
};