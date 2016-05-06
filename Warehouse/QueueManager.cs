using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.MessagePatterns;

namespace DataWarehouse.Queing
{
  public class QueueManager
  {
    IEnumerable<Task> Subscriptions = default(IEnumerable<Task>);

    public void Subscribe(string queueName, Action<string> callback)
    {      
      this.Subscriptions = this.Subscriptions ?? Enumerable.Empty<Task>();
      this.Subscriptions = this.Subscriptions.Concat(new[] {
        Task.Factory.StartNew(delegate {
          var factory = new ConnectionFactory()
          {
            VirtualHost = "/",
            HostName = "127.0.0.1",
            Port = AmqpTcpEndpoint.UseDefaultPort
          };
          factory.UserName = "guest";
          factory.Password = "guest";

          using (var connection = factory.CreateConnection())
          {
            using (var channel = connection.CreateModel())
            {
              var subscription = new Subscription(channel, queueName, false);
              while(true)
              {
                var eventArgs = subscription.Next();
                var content = eventArgs == null ? string.Empty : Encoding.UTF8.GetString(eventArgs.Body);
                subscription.Ack(eventArgs);

                callback(content);
              }
            }
          }
        }
      )});
    }

    public static void Store(string body)
    {
      var information = JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<object>>>(body);
    }
  }
}