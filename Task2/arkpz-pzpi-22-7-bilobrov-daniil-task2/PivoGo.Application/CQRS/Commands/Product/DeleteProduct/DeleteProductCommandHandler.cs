using MediatR;
using PivoGo.Application.Interfaces.Repositories;

namespace PivoGo.Application.CQRS.Commands.Products.DeleteProduct;

public class DeleteProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<DeleteProductCommand>
{
    public async Task Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        // Поиск продукта по ID
        var product = await productRepository.GetByIdAsync(command.ProductId, cancellationToken)
            ?? throw new Exception("Product not found");

        // Удаление продукта
        await productRepository.DeleteAsync(product, cancellationToken);
    }
}
