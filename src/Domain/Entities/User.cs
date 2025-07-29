using Domain.ValueObjects;

namespace Domain.Entities;

public class User : Entity
{
    public FirstName FirstName { get; private set; } = null!;
    public LastName LastName { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public PasswordHash PasswordHash { get; private set; } = null!;
    private readonly List<UserRole> _roles = new();
    public IReadOnlyCollection<UserRole> Roles => _roles.AsReadOnly();

    protected User() { }
    
    private User(
        FirstName firstName, 
        LastName lastName, 
        Email email, 
        PasswordHash passwordHash,
        IEnumerable<UserRole>? roles = null)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        if (roles != null)
            _roles.AddRange(roles);
    }

    public static User Create(
        string firstName, 
        string lastName, 
        string email,
        string passwordHash,
        IEnumerable<UserRole>? roles = null)
    {
        var user = new User(
            new FirstName(firstName), 
            new LastName(lastName), 
            new Email(email),
            new PasswordHash(passwordHash),
            roles);
        
        return user;
    }

    public void ChangeFirstName(string firstName) 
        => FirstName = new FirstName(firstName);
    
    public void ChangeLastName(string lastName) 
        => LastName = new LastName(lastName);
    
    public void ChangeEmail(string email) 
        => Email = new Email(email);
    
    public void SetPassword(
        string hash)
    {
        PasswordHash = new PasswordHash(hash);
    }
    
    public void AddOrUpdateRole(UserRole role)
    {
        var existingRole = _roles.FirstOrDefault(r => r.Name == role.Name);
        if (existingRole == null)
        {
            _roles.Add(role);
        }
        else
        {
            existingRole.ExtendValidity(role.ValidUntil ?? DateTime.MaxValue);
        }
    }

    public void RemoveRole(UserRole role)
    {
        _roles.RemoveAll(r => r.Equals(role));
    }

    public bool HasRole(UserRole role)
    {
        return _roles.Any(x => x.Name == role.Name && role.IsActive());
    }
}