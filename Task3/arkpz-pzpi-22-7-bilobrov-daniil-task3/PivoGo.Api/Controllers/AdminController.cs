using MediatR;
using Microsoft.AspNetCore.Mvc;
using PivoGo.Application.CQRS.Commands.Users.DeleteUser;
using PivoGo.Application.CQRS.Commands.Users.UpdateUser;
using PivoGo.Application.CQRS.Queries.User;
using System.ComponentModel.DataAnnotations;

namespace PivoGo.Api.Controllers
{
    [Route("api/admin")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut("edit-profile")]
        public async Task<IActionResult> UpdateUser([FromForm, Required] UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsSuccess)
            {
                return Ok(new { result.Token, Message = "User profile updated successfully." });
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserInfo(Guid userId, CancellationToken cancellationToken)
        {
            var query = new GetUserInfoQuery(userId);
            var userInfo = await _mediator.Send(query, cancellationToken);

            return Ok(userInfo);
        }

        [HttpDelete("delete-user/{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId, CancellationToken cancellationToken)
        {
            try
            {
                var command = new DeleteUserCommand(userId);
                await _mediator.Send(command, cancellationToken);
                return Ok(new { Message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                // Возвращаем 404, если пользователь не найден
                if (ex.Message.Contains("not found"))
                {
                    return NotFound(new { Message = ex.Message });
                }

                // Возвращаем общую ошибку в случае других проблем
                return StatusCode(500, new { Message = "An error occurred while deleting the user." });
            }
        }
    }
}
