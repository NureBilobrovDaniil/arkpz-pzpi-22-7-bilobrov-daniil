using MediatR;
using PivoGo.Application.CQRS.Dtos.Commands;

namespace PivoGo.Application.CQRS.Commands.Auth.Logout;

public record LogoutCommand(string Token) : IRequest<AuthResponseDto>;
