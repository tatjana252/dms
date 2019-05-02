using Consumers;
using MassTransit;
using MassTransit.RabbitMqTransport;
using MassTransit.Util;
using MassTransitExtensions;
using RabbitMQ.Client;
using System;
using Models;
using DAL.Repositories;
using DAL.Interfaces;

namespace DocumentManagement.Messaging
{
    // ova klasa treba da definise osnovne metode koje su bitne pri radu za messaging bus-om
    //podesavanja slanja, redova, osluskivaca
    //vracanje klijenata koji su zadruzeni za slanje commandi i objavljivanje dogadjaja
    //konkretna podesavanja vezana za konkretni messaging bus
    public class RabbitMqBus : IBus
    {
        private IBusControl bus;
        private IRabbitMqHost host;
        private IDocumentRepository documentRepository;

        public RabbitMqBus(IDocumentRepository documentRepository)
        {
            SetUpBus();
            this.documentRepository = documentRepository;
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
                sbc.Send<Document>(x => { x.UseRoutingKeyFormatter(e => e.Message.Type); });
                sbc.Message<Document>(x => x.SetEntityName("document"));
                sbc.Publish<Document>(x => { x.ExchangeType = ExchangeType.Direct;});


                sbc.Message<DocumentCreatedEvent>(x => x.SetEntityName("document_created"));
                sbc.Publish<DocumentCreatedEvent>(x => x.ExchangeType = ExchangeType.Direct);


                sbc.Message<UpdateDocumentCommand>(x => x.SetEntityName("updatedoc"));
                sbc.Publish<UpdateDocumentCommand>(x => x.ExchangeType = ExchangeType.Direct);
                sbc.Send<UpdateDocumentCommand>(x => x.UseRoutingKeyFormatter(e => e.Message.Type));
            });
        }

        public void AddConsumer(DocumentInfo input)
        {
            //dodajemo osluskivac koji dobija dokumenta sa tim routing key
            //routing key je tip dokumenta
            host.AddDocumentEndpoint<ReceiveDocument>(input.Type, documentRepository);
        }

        public DocumentsResponse? Request(DocumentInfo input)
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
        public void AddUpdateConsumer(DocumentInfo i)
        {
            host.ConnectReceiveEndpoint(i.Type, e =>
            {
                e.BindMessageExchanges = false;
                e.Consumer(() => new UpdateDocument(documentRepository));

                //document is queue name
                e.Bind("updatedoc", s =>
                {
                    s.RoutingKey = i.Type;
                    s.ExchangeType = ExchangeType.Direct;
                });
                e.Durable = true;
                e.AutoDelete = true;


            });
        }

        public void AddBinderConsumer(DocumentInfo i)
        {
            host.ConnectReceiveEndpoint("created_" + i.Type.ToLower(), e =>
              {
                  e.BindMessageExchanges = false;
                  e.Consumer(() => new DocCreatedEventHandler(documentRepository));
                  e.Bind("document_created", s =>
                  {
                      s.RoutingKey = i.Type;
                      s.ExchangeType = ExchangeType.Direct;
                  });
              });
        }

        public void Publish(object document)
        {
            bus.Publish(document);

        }

        public void PublishDocumentCreated(Document document)
        {
            DocumentCreatedEvent evt = new DocumentCreatedEvent
            {
                Id = document.Id,
                Type = document.Type,
                CorrelatedDocs = document.CorrelatedDocs
            };
            bus.Publish(evt, typeof(DocumentCreatedEvent), c => { c.SetRoutingKey(document.Type);
                
            });
        }

        public void AddRequestConsumer(DocumentInfo input)
        {
            host.ConnectReceiveEndpoint($"{input.Type.Replace(" ", "")}Request", e =>
            {
                e.BindMessageExchanges = false;
                if (documentRepository == null)
                {
                    e.Consumer<DocumentRequestHandler>();
                }
                else
                {

                    e.Consumer(() => { return new DocumentRequestHandler(documentRepository); });
                }

                e.AutoDelete = false;
                e.Durable = true;
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

