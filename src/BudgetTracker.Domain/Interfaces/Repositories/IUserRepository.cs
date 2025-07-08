using BudgetTracker.Domain.Entities;

namespace BudgetTracker.Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);

    Task<bool> EmailExistsAsync(string email, Guid tenantId, CancellationToken cancellationToken = default);

    Task<IEnumerable<User>> GetActiveUsersAsync(Guid tenantId, CancellationToken cancellationToken = default);
}
