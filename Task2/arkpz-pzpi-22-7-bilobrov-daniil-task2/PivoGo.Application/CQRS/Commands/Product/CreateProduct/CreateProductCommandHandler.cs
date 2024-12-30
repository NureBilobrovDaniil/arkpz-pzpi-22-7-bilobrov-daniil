using AutoMapper;
using MediatR;
using PivoGo.Application.CQRS.Commands.Products.CreateProduct;
using PivoGo.Application.Interfaces.Repositories;
using PivoGo.Domain.ProductAggregate;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PivoGo.Application.CQRS.Commands.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Преобразуем команду в сущность Product с помощью AutoMapper
            var product = _mapper.Map<Product>(command);

            // Добавляем дату создания для нового продукта
            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;

            // Сохраняем продукт в репозитории
            await _repository.CreateAsync(product, cancellationToken);
        }
    }
}
