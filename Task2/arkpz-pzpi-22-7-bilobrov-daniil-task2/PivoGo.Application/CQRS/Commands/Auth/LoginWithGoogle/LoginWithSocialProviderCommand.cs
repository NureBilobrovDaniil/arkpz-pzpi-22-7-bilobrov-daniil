using MediatR;
using PivoGo.Application.CQRS.Dtos.Commands;

namespace PivoGo.Application.CQRS.Commands.Auth.Login
{
    public record LoginWithGoogleCommand(
        string UserId,
        string Token
    ) : IRequest<AuthResponseDto>;
}
