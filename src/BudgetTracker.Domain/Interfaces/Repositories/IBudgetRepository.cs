using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.Enums;

namespace BudgetTracker.Domain.Interfaces.Repositories;

public interface IBudgetRepository : IBaseRepository<Budget>
{
    Task<IEnumerable<Budget>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<IEnumerable<Budget>> GetByStatusAsync(BudgetStatus status, Guid tenantId, CancellationToken cancellationToken = default);

    Task<Budget?> GetWithDetailsAsync(Guid id, CancellationToken cancellationToken = default);

    Task<(IEnumerable<Budget> Items, int TotalCount)> GetPagedAsync(int page, int pageSize, string? searchTerm, BudgetStatus? status, Guid tenantId, CancellationToken cancellationToken = default);
}
