using PivoGo.Application.Interfaces.Repositories;
using MQTTnet.Client.Options;
using MQTTnet.Client;
using MQTTnet;
using PivoGo.Domain.OrderAggregate;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

public class MqttService : IHostedService
{
    private readonly IMqttClient _mqttClient;
    private readonly IMqttClientOptions _mqttOptions;
    private readonly IServiceScopeFactory _serviceScopeFactory; // Инъекция зависимостей для репозитория заказов

    private readonly string _mqttBroker = "broker.hivemq.com";  // Брокер MQTT
    private readonly int _mqttPort = 1883;                      // Порт MQTT
    private readonly string _mqttTopic = "orders/data";               // Тема для получения данных о заказах

    public string LastMessage { get; private set; }

    public MqttService(IServiceScopeFactory serviceScopeFactory) // Конструктор с инъекцией зависимостей
    {
        _serviceScopeFactory = serviceScopeFactory;

        var factory = new MqttFactory();
        _mqttClient = factory.CreateMqttClient();

        _mqttOptions = new MqttClientOptionsBuilder()
            .WithTcpServer(_mqttBroker, _mqttPort)
            .Build();

        ConfigureHandlers();
    }

    private void ConfigureHandlers()
    {
        // Обработчик подключения к MQTT брокеру
        _mqttClient.UseConnectedHandler(async e =>
        {
            Console.WriteLine("Подключено к брокеру.");
            try
            {
                // Подписка на тему для получения данных о заказах
                await _mqttClient.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic(_mqttTopic).Build());
                Console.WriteLine($"Подписано на тему: {_mqttTopic}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка подписки: {ex.Message}");
            }
        });

        // Обработчик отключения от брокера
        _mqttClient.UseDisconnectedHandler(e =>
        {
            Console.WriteLine("Отключено от брокера.");
        });

        // Обработчик получения сообщения
        _mqttClient.UseApplicationMessageReceivedHandler(async e =>
        {
            LastMessage = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            Console.WriteLine($"Получено сообщение: {LastMessage}");

            try
            {
                // Десериализация полученного сообщения в объект Order
                var orderData = JsonConvert.DeserializeObject<Order>(LastMessage);

                if (orderData != null)
                {
                    // Сохраняем заказ в базе данных
                    using var scope = _serviceScopeFactory.CreateScope();
                    var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();

                    // Сохраняем заказ
                    await orderRepository.CreateAsync(orderData, CancellationToken.None);
                    Console.WriteLine("Данные заказа успешно сохранены в базу.");
                }
                else
                {
                    Console.WriteLine("Ошибка десериализации данных.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении данных: {ex.Message}");
            }
        });
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            // Подключение к MQTT брокеру
            await _mqttClient.ConnectAsync(_mqttOptions, cancellationToken);
            Console.WriteLine("Ожидание сообщений...");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка подключения: {ex.Message}");
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        // Отключение от MQTT брокера
        await _mqttClient.DisconnectAsync();
        Console.WriteLine("Отключение от брокера...");
    }
}