using Microsoft.EntityFrameworkCore;
using PivoGo.Application.Interfaces.Repositories;
using PivoGo.Domain.OrderAggregate;
using PivoGo.Infrastructure.Database;

namespace PivoGo.Infrastructure.Repositories;

public class OrderRepository(PivoGoContext context) : IOrderRepository
{
    // Получить заказ по его ID
    public async Task<Order> GetOrderByIdAsync(Guid orderId)
    {
        return await context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId)
               ?? throw new KeyNotFoundException($"Order with ID {orderId} not found.");
    }

    // Получить все заказы
    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await context.Orders.ToListAsync();
    }

    // Добавить новый заказ
    public async Task CreateAsync(Order order, CancellationToken cancellationToken)
    {
        await context.Orders.AddAsync(order, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    // Обновить существующий заказ
    public async Task UpdateOrderAsync(Order order)
    {
        var existingOrder = await context.Orders.FirstOrDefaultAsync(o => o.OrderId == order.OrderId);
        if (existingOrder == null)
        {
            throw new KeyNotFoundException($"Order with ID {order.OrderId} not found.");
        }

        context.Entry(existingOrder).CurrentValues.SetValues(order);
        await context.SaveChangesAsync();
    }

    // Удалить заказ по его ID
    public async Task DeleteOrderAsync(Guid orderId)
    {
        var order = await context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
        if (order == null)
        {
            throw new KeyNotFoundException($"Order with ID {orderId} not found.");
        }

        context.Orders.Remove(order);
        await context.SaveChangesAsync();
    }
}
