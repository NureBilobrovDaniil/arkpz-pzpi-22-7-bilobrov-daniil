using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PivoGo.Application.Interfaces.Repositories;
using PivoGo.Domain.UserAggregate;
using PivoGo.Infrastructure.Database;

namespace PivoGo.Infrastructure.Repositories;

public class AuthRepository(UserManager<User> userManager, PivoGoContext context) : IAuthRepository
{
    public async Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await userManager.FindByEmailAsync(email);
    }

    public async Task<IdentityResult> AddLoginAsync(User user, UserLoginInfo loginInfo, CancellationToken cancellationToken)
    {
        return await userManager.AddLoginAsync(user, loginInfo);
    }

    public async Task<IList<UserLoginInfo>> GetLoginsAsync(User user, CancellationToken cancellationToken)
    {
        return await userManager.GetLoginsAsync(user);
    }

    public async Task<IdentityResult> RemoveTokenAsync(string token, CancellationToken cancellationToken)
    {
        var userToken = await context.UserTokens.FirstOrDefaultAsync(z => z.Value == token, cancellationToken);

        if (userToken == null)
        {
            return IdentityResult.Failed(new IdentityError { Description = "Token not found." });
        }

        context.UserTokens.Remove(userToken);

        await context.SaveChangesAsync(cancellationToken);

        return IdentityResult.Success;
    }
}
