using System.Net;

namespace PivoGo.Domain.Exceptions;

public class BadRequestException(string message, params string[] errorDetails) : ExceptionBase(message, errorDetails)
{
    public override HttpStatusCode ResponseStatusCode => HttpStatusCode.BadRequest;
}