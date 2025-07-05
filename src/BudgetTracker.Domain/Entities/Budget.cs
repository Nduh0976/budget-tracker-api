using BudgetTracker.Domain.Common;
using BudgetTracker.Domain.Enums;
using BudgetTracker.Domain.Exceptions;
using BudgetTracker.Domain.ValueObjects;

namespace BudgetTracker.Domain.Entities;

public class Budget : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public Money Amount { get; private set; } = null!;

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public BudgetStatus Status { get; private set; }

    public Guid UserId { get; private set; }

    // Navigation properties
    public virtual User User { get; private set; } = null!;

    public virtual ICollection<Category> Categories { get; private set; } = new List<Category>();

    public virtual ICollection<Expense> Expenses { get; private set; } = new List<Expense>();

    private Budget() { } // EF Core

    public Budget(string name, string? description, Money amount, DateTime startDate, DateTime endDate, Guid userId, Guid tenantId)
        : base(tenantId)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Budget name is required", nameof(name));
        }

        if (amount.Amount <= 0)
        {
            throw new InvalidBudgetAmountException();
        }

        if (startDate >= endDate)
        {
            throw new InvalidDateRangeException();
        }

        Name = name;
        Description = description;
        Amount = amount;
        StartDate = startDate;
        EndDate = endDate;
        UserId = userId;
        Status = BudgetStatus.Active;
    }

    public Money GetTotalSpent()
    {
        if (!Expenses.Any())
        {
            return Money.Zero(Amount.Currency);
        }

        var total = Money.Zero(Amount.Currency);
        
        foreach (var expense in Expenses.Where(e => !e.IsDeleted))
        {
            total = total.Add(expense.Amount);
        }

        return total;
    }

    public Money GetRemainingAmount()
    {
        return Amount.Subtract(GetTotalSpent());
    }

    public bool IsExceeded()
    {
        return GetTotalSpent().IsGreaterThan(Amount);
    }

    public void UpdateStatus()
    {
        if (IsExceeded())
        {
            Status = BudgetStatus.Exceeded;
        }
        else if (DateTime.UtcNow > EndDate)
        {
            Status = BudgetStatus.Completed;
        }
        else
        {
            Status = BudgetStatus.Active;
        }
    }

    public void Update(string name, string? description, Money amount, DateTime startDate, DateTime endDate, string updatedBy)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Budget name is required", nameof(name));
        }

        if (amount.Amount <= 0)
        {
            throw new InvalidBudgetAmountException();
        }

        if (startDate >= endDate)
        {
            throw new InvalidDateRangeException();
        }

        Name = name;
        Description = description;
        Amount = amount;
        StartDate = startDate;
        EndDate = endDate;

        MarkAsUpdated(updatedBy);
        UpdateStatus();
    }
}
