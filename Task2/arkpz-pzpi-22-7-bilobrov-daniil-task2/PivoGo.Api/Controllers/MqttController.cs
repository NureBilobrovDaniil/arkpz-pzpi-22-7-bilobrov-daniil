using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MQTTnet;
using PivoGo.Application.Interfaces.Repositories;
using PivoGo.Domain.OrderAggregate;

namespace PivoGo.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MqttController : ControllerBase
    {
        private readonly MqttService _mqttService;
        private readonly IOrderRepository _orderRepository;

        public MqttController(MqttService mqttService, IOrderRepository orderRepository)
        {
            _mqttService = mqttService;
            _orderRepository = orderRepository;
        }

        // Получение последнего полученного сообщения
        [HttpGet("last-message")]
        public IActionResult GetLastMessage()
        {
            if (string.IsNullOrEmpty(_mqttService.LastMessage))
            {
                return NotFound("Нет полученных сообщений.");
            }

            return Ok(_mqttService.LastMessage);
        }

        // Получение и сохранение заказа из последнего сообщения
        [HttpPost("process-message")]
        public async Task<IActionResult> ProcessMessageAsync(CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(_mqttService.LastMessage))
            {
                return BadRequest("Нет полученного сообщения для обработки.");
            }

            try
            {
                // Десериализация последнего сообщения в объект Order
                var orderData = JsonConvert.DeserializeObject<Order>(_mqttService.LastMessage);

                if (orderData == null)
                {
                    return BadRequest("Ошибка десериализации данных.");
                }

                // Сохранение заказа в базу данных
                await _orderRepository.CreateAsync(orderData, cancellationToken);

                return Ok("Данные заказа успешно сохранены.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Произошла ошибка при обработке сообщения: {ex.Message}");
            }
        }
    }
}
