using MediatR;
using PivoGo.Application.CQRS.Dtos.Commands;

namespace PivoGo.Application.CQRS.Commands.Auth.Register;

public record RegisterCommand(string UserName, string Email, string Password) : IRequest<AuthResponseDto>;
