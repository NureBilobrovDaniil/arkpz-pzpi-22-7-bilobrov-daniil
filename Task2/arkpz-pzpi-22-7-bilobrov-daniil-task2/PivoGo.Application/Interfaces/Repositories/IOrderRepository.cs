using PivoGo.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PivoGo.Application.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        // Получить заказ по его ID
        Task<Order> GetOrderByIdAsync(Guid orderId);

        // Получить все заказы
        Task<IEnumerable<Order>> GetAllOrdersAsync();

        // Добавить новый заказ
        Task CreateAsync(Order order, CancellationToken cancellationToken);

        // Обновить существующий заказ
        Task UpdateOrderAsync(Order order);

        // Удалить заказ по его ID
        Task DeleteOrderAsync(Guid orderId);

    }
}
