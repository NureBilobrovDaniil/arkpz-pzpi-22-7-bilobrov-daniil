using PivoGo.Application.Interfaces.Repositories;
using PivoGo.Domain.UserAggregate;
using PivoGo.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace PivoGo.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PivoGoContext _context;

        public UserRepository(PivoGoContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);  // Получаем пользователя по ID
        }

        public async Task<List<User>> GetAllUsersAsync(CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);  // Получаем всех пользователей
        }

        public async Task DeleteAsync(User user, CancellationToken cancellationToken)
        {
            // Проверка на существование пользователя
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null");
            }

            // Удаляем пользователя из контекста
            _context.Users.Remove(user);

            // Сохраняем изменения в базе данных
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
