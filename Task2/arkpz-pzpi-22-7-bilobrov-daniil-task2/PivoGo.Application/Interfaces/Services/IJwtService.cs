using PivoGo.Domain.UserAggregate;

namespace PivoGo.Application.Interfaces.Services;

public interface IJwtService
{
    Task<string> GenerateJwtTokenAsync(User user);
}
