using BudgetTracker.Domain.Common;

namespace BudgetTracker.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; private set; } = string.Empty;

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public DateTime? LastLoginAt { get; private set; }

    public bool IsActive { get; private set; } = true;


    // TODO: Implement Navigation property for budget

    private User() { } // EF Core

    public User(string email, string firstName, string lastName, string passwordHash, Guid tenantId)
        : base(tenantId)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email is required", nameof(email));
        }

        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("First name is required", nameof(firstName));
        }
        
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Last name is required", nameof(lastName));
        }

        Email = email.ToLowerInvariant();
        FirstName = firstName;
        LastName = lastName;
        PasswordHash = passwordHash;
    }

    public string FullName => $"{FirstName} {LastName}";

    public void UpdateProfile(string firstName, string lastName, string updatedBy)
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("First name is required", nameof(firstName));
        }
            
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Last name is required", nameof(lastName));
        }

        FirstName = firstName;
        LastName = lastName;
        MarkAsUpdated(updatedBy);
    }

    public void UpdateLastLogin()
    {
        LastLoginAt = DateTime.UtcNow;
    }

    public void Deactivate(string updatedBy)
    {
        IsActive = false;
        MarkAsUpdated(updatedBy);
    }
}