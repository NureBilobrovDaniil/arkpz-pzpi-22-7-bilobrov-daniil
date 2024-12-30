using PivoGo.Application.CQRS.Dtos.Queries;
using MediatR;

namespace PivoGo.Application.CQRS.Queries.User
{
   public record GetUserInfoQuery(Guid UserId) : IRequest<GetUserInfoQueryDto>;

}
