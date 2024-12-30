using Microsoft.AspNetCore.Identity;

namespace PivoGo.Domain.UserAggregate;

public class User : IdentityUser<Guid>
{
    public string? Photo { get; set; }
    public bool IsBanned { get; set; } = false;
}
