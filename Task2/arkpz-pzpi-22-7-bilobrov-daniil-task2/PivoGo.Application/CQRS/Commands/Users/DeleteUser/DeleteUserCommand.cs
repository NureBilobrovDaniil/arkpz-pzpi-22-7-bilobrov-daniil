using MediatR;

namespace PivoGo.Application.CQRS.Commands.Users.DeleteUser;

// Команда для удаления пользователя
public record DeleteUserCommand(Guid UserId) : IRequest;