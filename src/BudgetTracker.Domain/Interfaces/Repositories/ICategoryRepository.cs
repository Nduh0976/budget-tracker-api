using BudgetTracker.Domain.Entities;

namespace BudgetTracker.Domain.Interfaces.Repositories;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<IEnumerable<Category>> GetByBudgetIdAsync(Guid budgetId, CancellationToken cancellationToken = default);

    Task<Category?> GetByIdWithExpensesAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> IsCategoryNameUniqueAsync(string name, Guid budgetId, CancellationToken cancellationToken = default);
}
