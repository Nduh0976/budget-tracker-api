using BudgetTracker.Domain.Common;

namespace BudgetTracker.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }
    
    public Guid BudgetId { get; private set; }


    // Navigation properties
    public virtual Budget Budget { get; private set; } = null!;

    public virtual ICollection<Expense> Expenses { get; private set; } = new List<Expense>();

    private Category() { } // EF Core

    public Category(string name, string? description, Guid budgetId, Guid tenantId)
        : base(tenantId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Category name is required", nameof(name));
        }

        Name = name;
        Description = description;
        BudgetId = budgetId;
    }
}
