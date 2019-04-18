using MassTransit;
using MassTransit.RabbitMqTransport;
using RabbitMQ.Client;
using System;

namespace MassTransitExtensions
{
    public static class MassTransitHostExstensions
    {
        public static void AddDocumentEndpoint<TConsumer>(this IRabbitMqHost host, string epName) where TConsumer : class, IConsumer, new()
        {
            host.ConnectReceiveEndpoint(epName, e =>
            {
                e.BindMessageExchanges = false;
                e.Consumer<TConsumer>();

                //document is queue name
                e.Bind("document", s =>
                {
                    s.RoutingKey = epName;
                    s.ExchangeType = ExchangeType.Direct;
                });
                e.Durable = true;
                e.AutoDelete = true;
                

            });
        }
    }
}
