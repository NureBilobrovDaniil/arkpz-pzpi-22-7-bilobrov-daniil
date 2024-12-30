using MediatR;
using PivoGo.Application.Interfaces;
using PivoGo.Application.Interfaces.Repositories;
using System;

namespace PivoGo.Application.CQRS.Commands.Users.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            // Поиск пользователя по ID с изменённым методом
            var user = await _userRepository.GetUserAsync(command.UserId, cancellationToken);

            if (user == null)
            {
                // Логирование и выбрасывание исключения, если пользователь не найден
                throw new Exception($"User with ID {command.UserId} not found");
            }

            // Удаление пользователя
            await _userRepository.DeleteAsync(user, cancellationToken);
        }
    }
}
