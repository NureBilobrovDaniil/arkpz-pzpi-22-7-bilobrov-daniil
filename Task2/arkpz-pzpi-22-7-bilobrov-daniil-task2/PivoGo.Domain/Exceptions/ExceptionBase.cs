using System.Net;

namespace PivoGo.Domain.Exceptions;

public abstract class ExceptionBase(string message, params string[] errorDetails) : Exception(message)
{
    public abstract HttpStatusCode ResponseStatusCode { get; }

    public IReadOnlyList<string> ErrorDetails { get; } = errorDetails;
}