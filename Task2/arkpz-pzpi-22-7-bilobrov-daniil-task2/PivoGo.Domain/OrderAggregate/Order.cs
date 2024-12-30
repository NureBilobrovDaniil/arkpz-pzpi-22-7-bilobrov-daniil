using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PivoGo.Domain.OrderAggregate
{
    public class Order
    {
        public Guid OrderId { get; private set; } // Уникальный идентификатор заказа
        public string OrderNumber { get; private set; } // Номер заказа
        public string Route { get; private set; } // Маршрут доставки
        public TimeSpan DispatchTime { get; private set; } // Время отправки
        public TimeSpan ArrivalTime { get; private set; } // Время прибытия
        public string RecipientName { get; private set; } // Имя получателя
    }
}
