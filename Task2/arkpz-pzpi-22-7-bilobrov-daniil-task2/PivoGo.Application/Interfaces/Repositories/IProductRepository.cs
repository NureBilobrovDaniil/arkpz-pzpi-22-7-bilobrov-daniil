using PivoGo.Domain.ProductAggregate;

namespace PivoGo.Application.Interfaces.Repositories;

public interface IProductRepository
{
    /// <summary>
    /// Получить все продукты.
    /// </summary>
    Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Создать новый продукт.
    /// </summary>
    Task CreateAsync(Product product, CancellationToken cancellationToken);

    /// <summary>
    /// Обновить существующий продукт.
    /// </summary>
    Task UpdateAsync(Product product, CancellationToken cancellationToken);

    /// <summary>
    /// Удалить продукт.
    /// </summary>
    Task DeleteAsync(Product product, CancellationToken cancellationToken);

    /// <summary>
    /// Получить продукт по идентификатору.
    /// </summary>
    Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}
