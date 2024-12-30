using MediatR;

namespace PivoGo.Application.CQRS.Commands.Products.DeleteProduct;

// Команда для удаления продукта
public record DeleteProductCommand(Guid ProductId) : IRequest;
