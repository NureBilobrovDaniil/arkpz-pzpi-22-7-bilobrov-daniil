using MediatR;
using PivoGo.Application.CQRS.Dtos.Commands;
using Microsoft.AspNetCore.Http;

namespace PivoGo.Application.CQRS.Commands.Users.UpdateUser;


public record UpdateUserCommand
(
    Guid UserId,
    string Email,
    string UserName,
    IFormFile? Photo // Изменение типа на IFormFile
) : IRequest<AuthResponseDto>;
