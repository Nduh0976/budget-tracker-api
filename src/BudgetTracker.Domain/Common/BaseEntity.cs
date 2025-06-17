namespace BudgetTracker.Domain.Common;

public class BaseEntity
{
    protected BaseEntity() { }

    protected BaseEntity(Guid tenantId)
    {
        TenantId = tenantId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; protected set; } = Guid.NewGuid();

    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; protected set; }

    public string CreatedBy { get; protected set; } = string.Empty;

    public string? UpdatedBy { get; protected set; }

    public bool IsDeleted { get; protected set; }

    public DateTime? DeletedAt { get; protected set; }

    public string? DeletedBy { get; protected set; }

    public Guid TenantId { get; protected set; }

    public void MarkAsDeleted(string deletedBy)
    {
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        DeletedBy = deletedBy;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsUpdated(string updatedBy)
    {
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }
}