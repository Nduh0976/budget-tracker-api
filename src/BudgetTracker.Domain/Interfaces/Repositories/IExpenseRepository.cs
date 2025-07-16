using BudgetTracker.Domain.Entities;

namespace BudgetTracker.Domain.Interfaces.Repositories;

public interface IExpenseRepository
{
    Task<IEnumerable<Expense>> GetByBudgetIdAsync(Guid budgetId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Expense>> GetByCategoryIdAsync(Guid categoryId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Expense>> GetByDateRangeAsync(Guid budgetId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);

    Task<(IEnumerable<Expense> Items, int TotalCount)> GetPagedAsync(Guid budgetId, int page, int pageSize, string? searchTerm = null, CancellationToken cancellationToken = default);
}
