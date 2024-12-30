using AutoMapper;
using MediatR;
using PivoGo.Application.Interfaces.Repositories;
using PivoGo.Domain.ProductAggregate;

namespace PivoGo.Application.CQRS.Commands.Products.UpdateProduct;

public class UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<UpdateProductCommand>
{
    public async Task Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        // Получаем продукт по ID
        var product = await productRepository.GetByIdAsync(command.ProductId, cancellationToken)
                      ?? throw new Exception("Product not found");

        // Используем AutoMapper для обновления данных
        mapper.Map(command, product);

        // Обновляем дату последнего изменения
        product.UpdatedAt = DateTime.UtcNow;

        // Сохраняем изменения в репозитории
        await productRepository.UpdateAsync(product, cancellationToken);
    }
}
