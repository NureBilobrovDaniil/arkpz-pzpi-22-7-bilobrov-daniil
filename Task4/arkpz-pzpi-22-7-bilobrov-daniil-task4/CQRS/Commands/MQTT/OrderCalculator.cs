using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PivoGo.Application.CQRS.Commands.MQTT
{
    public class OrderCalculator
    {
        public static string ProcessDeliveryData(string orderNumber, string route, string dispatchTime, string arrivalTime, string recipientName)
        {
            // Обработка данных доставки
            return $"Order Number: {orderNumber}\n" +
                   $"Route: {route}\n" +
                   $"Dispatch Time: {dispatchTime}\n" +
                   $"Arrival Time: {arrivalTime}\n" +
                   $"Recipient Name: {recipientName}";
        }
    }
}
