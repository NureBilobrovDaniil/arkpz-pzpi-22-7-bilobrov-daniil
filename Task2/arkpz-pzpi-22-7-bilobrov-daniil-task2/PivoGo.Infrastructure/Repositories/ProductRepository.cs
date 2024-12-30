using Microsoft.EntityFrameworkCore;
using PivoGo.Application.Interfaces.Repositories;
using PivoGo.Domain.ProductAggregate;
using PivoGo.Infrastructure.Database;

namespace PivoGo.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly PivoGoContext _context;

        public ProductRepository(PivoGoContext context)
        {
            _context = context;
        }

        // Получить все продукты
        public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Products.ToListAsync(cancellationToken);
        }

        // Создать новый продукт
        public async Task CreateAsync(Product product, CancellationToken cancellationToken = default)
        {
            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        // Получить продукт по его ID
        public async Task<Product> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id, cancellationToken);
        }

        // Обновить существующий продукт
        public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
        }

        // Удалить продукт
        public async Task DeleteAsync(Product product, CancellationToken cancellationToken = default)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
