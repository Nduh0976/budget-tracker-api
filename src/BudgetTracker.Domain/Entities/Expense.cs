using BudgetTracker.Domain.Common;
using BudgetTracker.Domain.ValueObjects;

namespace BudgetTracker.Domain.Entities;

public class Expense : BaseEntity
{
    public string Description { get; private set; } = string.Empty;

    public Money Amount { get; private set; } = null!;

    public DateTime ExpenseDate { get; private set; }

    public string? Notes { get; private set; }

    public Guid BudgetId { get; private set; }

    public Guid CategoryId { get; private set; }

    // Navigation properties
    public virtual Budget Budget { get; private set; } = null!;

    public virtual Category Category { get; private set; } = null!;

    private Expense() { } // EF Core

    public Expense(string description, Money amount, DateTime expenseDate,string? notes, Guid budgetId, Guid categoryId, Guid tenantId)
        : base(tenantId)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Expense description is required", nameof(description));
        }

        if (amount.Amount <= 0)
        {
            throw new ArgumentException("Expense amount must be greater than zero", nameof(amount));
        }

        Description = description;
        Amount = amount;
        ExpenseDate = expenseDate;
        Notes = notes;
        BudgetId = budgetId;
        CategoryId = categoryId;
    }

    public void Update(string description, Money amount, DateTime expenseDate, string? notes, Guid categoryId, string updatedBy)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            throw new ArgumentException("Expense description is required", nameof(description));
        }

        if (amount.Amount <= 0)
        {
            throw new ArgumentException("Expense amount must be greater than zero", nameof(amount));
        }

        Description = description;
        Amount = amount;
        ExpenseDate = expenseDate;
        Notes = notes;
        CategoryId = categoryId;
        MarkAsUpdated(updatedBy);
    }
}
