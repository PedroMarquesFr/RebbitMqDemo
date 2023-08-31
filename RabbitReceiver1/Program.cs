using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new Uri("amqp://guest:guest@localhost:5672");
factory.ClientProvidedName = "Rabbit Receiver1 App";

IConnection connection = factory.CreateConnection();
IModel channel = connection.CreateModel();

string exchangeName = "DemoExchange";
string routingKey = "demo-routing-key";
string queueName = "DemoQueue";


channel.ExchangeDeclare(exchangeName, ExchangeType.Direct);
channel.QueueDeclare(queueName, false, false, false, null);
channel.QueueBind(queueName, exchangeName, routingKey, null);

//prefetchSize => tamanho da mensagem, 0 eh q a gnt nao liga pro tamanho,
//prefetchCount => quantas mensagens vao ser enviadas de uma vez, 1 significa q vai ser processada uma mensagem por vez
//bool global => aplica para todas as instancias ou so pra essa, flase é q é só pra essa
channel.BasicQos(0, 1, false);