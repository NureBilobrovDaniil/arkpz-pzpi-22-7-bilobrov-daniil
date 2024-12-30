using MediatR;

namespace PivoGo.Application.CQRS.Commands.Products.UpdateProduct;

public record UpdateProductCommand
(
    Guid ProductId,
    string Name,
    string Description,
    decimal Price,
    int StockQuantity
) : IRequest;
    