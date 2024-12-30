using MediatR;
using Microsoft.AspNetCore.Mvc;
using PivoGo.Application.CQRS.Commands.Auth.Login;
using PivoGo.Application.CQRS.Commands.Auth.Logout;
using PivoGo.Application.CQRS.Commands.Auth.Register;
using PivoGo.Application.CQRS.Commands.Auth;
using EmailService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using PivoGo.Application.CQRS.Dtos.Commands;
using PivoGo.Domain.UserAggregate;

namespace PivoGo.Api.Controllers;

[Route("api/auth")]
public class AuthController(IMediator mediator, UserManager<User> userManager, IEmailSender emailSender) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(new { Message = "User registered successfully." });
        }

        return BadRequest(result.ErrorMessage);
    }

    [HttpPost("login-with-google")]
    public async Task<IActionResult> LoginWithGoogle([FromBody] LoginWithGoogleCommand command,
    CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(new { result.Token, Message = "Login information added successfully." });
        }

        return BadRequest(result.ErrorMessage);
    }

    [HttpPost("login-with-github")]
    public async Task<IActionResult> LoginWithGitHub([FromQuery] LoginWithGitHubCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(new { result.Token, Message = "GitHub login successful." });
        }

        return BadRequest(result.ErrorMessage);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(new { result.Token, Message = "User logged in successfully." });
        }

        return StatusCode(403, result.ErrorMessage);
    }

    [HttpPut("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command, cancellationToken);

        if (result.IsSuccess)
        {
            return Ok(new { Message = "Logout successful." });
        }

        return BadRequest(result.ErrorMessage);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPassword)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var user = await userManager.FindByEmailAsync(forgotPassword.Email!);
        if (user is null)
            return BadRequest("User not found");

        var token = userManager.GeneratePasswordResetTokenAsync(user);
        var param = new Dictionary<string, string?>
            {
                { "token", await token },
                { "email", forgotPassword.Email! }
            };

        var callback = QueryHelpers.AddQueryString(forgotPassword.ClientUri!, param);

        var message = new Message([user.Email], "Reset Password", callback);

        await emailSender.SendEmail(message);

        return Ok();
    }

    [HttpPost("reset-user-password")]
    public async Task<IActionResult> ResetUserPassword([FromBody] ResetPasswordDto resetPassword)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var user = await userManager.FindByEmailAsync(resetPassword.Email!);
        if (user is null)
            return BadRequest("User not found");

        var result = await userManager.ResetPasswordAsync(user, resetPassword.Token!, resetPassword.Password!);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);

            return BadRequest(new { Errors = errors });
        }

        return Ok();
    }
}