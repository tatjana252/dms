using Consumer;
using MassTransit;
using MassTransit.RabbitMqTransport;
using MassTransit.Util;
using MassTransitExtensions;
using RabbitMQ.Client;
using System;
using Models;

namespace DocumentManagement.Messaging
{
    // ova klasa treba da definise osnovne metode koje su bitne pri radu za messaging bus-om
    //podesavanja slanja, redova, osluskivaca
    //vracanje klijenata koji su zadruzeni za slanje commandi i objavljivanje dogadjaja
    //konkretna podesavanja vezana za konkretni messaging bus
    public class RabbitMqBus
    {
        private IBusControl bus;
        private IRabbitMqHost host;

        public RabbitMqBus()
        {
            SetUpBus();
        }

        public void SetUpBus()
        {
            bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                //setting up sending
                sbc.Send<Document>(x => x.UseRoutingKeyFormatter(e => e.Message.Type));
                sbc.Message<Document>(x => x.SetEntityName("document"));
                sbc.Publish<Document>(x => { x.ExchangeType = ExchangeType.Direct; });
            });
        }

        public void AddConsumer(Document input)
        {
            //dodajemo osluskivac koji dobija dokumenta sa tim routing key
            //routing key je tip dokumenta
            host.AddDocumentEndpoint<ReceiveDocument>(input.Type);
        }

        public DocumentsResponse Request(Document input)
        {

            try
            {
                var address = new Uri("rabbitmq://localhost/" + input.Type + "Request");
                var requestTimeout = TimeSpan.FromSeconds(10);
                IRequestClient<Document, DocumentsResponse> requestClient = 
                    new MessageRequestClient<Document, DocumentsResponse>(bus, address, requestTimeout);
                return requestClient.Request(input).Result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Publish(Document document)
        {
            bus.Publish(document);
        }

        public void AddRequestConsumer(Document input)
        {
            host.ConnectReceiveEndpoint($"{input.Type}Request", e =>
            {
                e.BindMessageExchanges = false;
                e.Consumer<DocumentRequestHandler>();
            });
        }

        public void StartBus()
        {
            TaskUtil.Await(() => bus.StartAsync());
        }

        public void StopBus()
        {
            TaskUtil.Await(() => bus.StopAsync());
        }

        ~RabbitMqBus()
        {
            StopBus();
        }
    }
}

