namespace Domain.Entities;

public class Payment
{
    public string Id { get; private set; } = null!;
    public string CustomerId { get; private set; } = null!;
    public decimal Amount { get; private set; }
    public string Status { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? PaidAt { get; private set; }
    
    protected Payment() { }

    private Payment(
        string id,
        string customerId,
        decimal amount,
        string status, 
        DateTime createdAt, 
        DateTime? paidAt = null)
    {
        Id = id;
        CustomerId = customerId;
        Amount = amount;
        Status = status;
        CreatedAt = createdAt;
        PaidAt = paidAt;
    }

    public static Payment Create(
        string customerId, 
        decimal amount, 
        string status, 
        DateTime createdAt,
        DateTime? paidAt = null)
    {
        if (string.IsNullOrWhiteSpace(customerId))
            throw new ArgumentException("CustomerId não pode ser vazio.");

        if (amount <= 0)
            throw new ArgumentException("Amount deve ser maior que zero.");

        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Status não pode ser vazio.");

        var validStatuses = new[] { "PENDING", "RECEIVED", "OVERDUE", "CANCELLED" };
        if (!validStatuses.Contains(status.ToUpper()))
            throw new ArgumentException($"Status inválido. Valores válidos: {string.Join(", ", validStatuses)}");

        if (createdAt == default)
            throw new ArgumentException("CreatedAt inválido.");

        if (paidAt.HasValue && paidAt.Value < createdAt)
            throw new ArgumentException("PaidAt não pode ser anterior a CreatedAt.");
        
        var id = Guid.NewGuid().ToString();

        return new Payment(id, customerId, amount, status.ToUpper(), createdAt, paidAt);
    }
    
    public void MarkAsPaid()
    {
        Status = "RECEIVED";
        PaidAt = DateTime.Now;
    }
    
    public void MarkAsOverdue()
    {
        Status = "OVERDUE";
    }

    public void Cancel()
    {
        Status = "CANCELLED";
    }
}
