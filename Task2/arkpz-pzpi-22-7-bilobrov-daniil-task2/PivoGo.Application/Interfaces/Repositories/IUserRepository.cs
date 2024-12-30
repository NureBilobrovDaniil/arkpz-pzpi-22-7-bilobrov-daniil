using PivoGo.Domain.UserAggregate;

namespace PivoGo.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Получить пользователя по идентификатору.
        /// </summary>
        Task<User> GetUserAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Получить всех пользователей.
        /// </summary>
        Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Удалить пользователя.
        /// </summary>
        Task DeleteAsync(User user, CancellationToken cancellationToken);
    }
}
