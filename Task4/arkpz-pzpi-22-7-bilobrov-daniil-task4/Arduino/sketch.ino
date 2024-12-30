#include <WiFi.h>
#include <PubSubClient.h>

// WiFi настройки
const char* ssid = "Wokwi-GUEST";
const char* password = "";

// MQTT настройки
const char* mqtt_server = "broker.hivemq.com";  // HiveMQ публичный MQTT брокер
const int mqtt_port = 1883;  // Порт для незащищенного MQTT (без SSL)
const char* mqtt_user = "";  
const char* mqtt_password = "";

WiFiClient espClient;
PubSubClient client(espClient);

bool orderSent = false;  // По умолчанию - данные не отправлены

// Функция для подключения к WiFi
void setup_wifi() {
  delay(10);
  Serial.println();
  Serial.print("Подключение к ");
  Serial.println(ssid);

  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(".");
  }

  Serial.println("");
  Serial.println("WiFi подключен!");
  Serial.print("IP address: ");
  Serial.println(WiFi.localIP());
}

// Функция для подключения к MQTT брокеру
void reconnect() {
  while (!client.connected()) {
    Serial.print("Подключение к MQTT...");
    if (client.connect("ESP32Client", mqtt_user, mqtt_password)) {
      Serial.println("подключено!");
    } else {
      Serial.print("ошибка, rc=");
      Serial.print(client.state());
      Serial.println(" попытка подключения через 5 секунд");
      delay(5000);
    }
  }
}

// Генерация случайного времени отправки и прибытия
String generateRandomTime() {
  int hour = random(0, 24);
  int minute = random(0, 60);
  int second = random(0, 60);
  char timeBuffer[9];
  snprintf(timeBuffer, sizeof(timeBuffer), "%02d:%02d:%02d", hour, minute, second);
  return String(timeBuffer);
}

// Отправка данных заказа
void sendOrder() {
  if (orderSent) {
    Serial.println("Данные заказа уже были отправлены. Останавливаем отправку.");
    return;  // Если данные уже отправлены, прекращаем отправку
  }

  // Генерация данных заказа
  String orderNumber = String(random(1000, 9999));  // Номер заказа
  String route = "Маршрут: точка A -> точка B";  // Расчёт маршрута
  String dispatchTime = generateRandomTime();  // Время отправки
  String arrivalTime = generateRandomTime();  // Время прибытия
  String recipientName = "Иван Иванов";  // Имя получателя

  // Создание JSON-формата
  String jsonBody = "{"
                     "\"orderNumber\": \"" + orderNumber + "\"," +
                     "\"route\": \"" + route + "\"," +
                     "\"dispatchTime\": \"" + dispatchTime + "\"," +
                     "\"arrivalTime\": \"" + arrivalTime + "\"," +
                     "\"recipientName\": \"" + recipientName + "\"" +
                     "}";

  Serial.println("Отправка данных заказа...");
  Serial.println(jsonBody);

  // Публикация данных на тему "orders/data"
  if (client.publish("orders/data", jsonBody.c_str())) {
    Serial.println("Данные заказа успешно отправлены!");
    orderSent = true;  // Помечаем, что данные были отправлены
    stopProgram();  // Остановка программы после успешной отправки
  } else {
    Serial.println("Ошибка отправки данных заказа!");
  }
}

// Функция для остановки программы
void stopProgram() {
  Serial.println("Программа остановлена.");
  while (true) {
    delay(1000);  // Бесконечно останавливаем программу
  }
}

void setup() {
  Serial.begin(115200);
  setup_wifi();
  client.setServer(mqtt_server, mqtt_port);
}

void loop() {
  if (!client.connected()) {
    reconnect();
  }
  client.loop();
  // Отправка данных заказа только один раз
  if (!orderSent) {
    static unsigned long lastMsg = 0;
    unsigned long now = millis();
    if (now - lastMsg > 10000) {  // 10 секунд
      lastMsg = now;
      sendOrder();
    }
  }
}