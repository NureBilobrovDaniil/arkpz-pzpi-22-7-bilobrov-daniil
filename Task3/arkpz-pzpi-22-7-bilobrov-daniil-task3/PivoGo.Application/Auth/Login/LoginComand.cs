using MediatR;
using PivoGo.Application.CQRS.Dtos.Commands;

namespace PivoGo.Application.CQRS.Commands.Auth.Login;

public record LoginCommand(string Email, string Password) : IRequest<AuthResponseDto>;
