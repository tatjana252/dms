using Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using DAL.Context;
using DAL.Interfaces;
using System.Linq;

namespace Consumers
{
    public class DocCreatedEventHandler : IConsumer<DocumentCreatedEvent>
    {
        private readonly IDocumentRepository _db;

        public DocCreatedEventHandler(IDocumentRepository db)
        {
            _db = db;
        }

        public DocCreatedEventHandler()
        {

        }

        public Task Consume(ConsumeContext<DocumentCreatedEvent> context)
        {
            DocumentCreatedEvent dce = context.Message;
            dce.CorrelatedDocs = new Dictionary<int, string> { { 12, "Narudzbenica" } };
            foreach (KeyValuePair<int, string> i in dce.CorrelatedDocs)
            {
                context.Publish(new UpdateDocumentCommand
                {
                    OnlyStatus = true,
                    Id = i.Key,
                    Type = i.Value,
                    //ovde bi u repozitorijumu trebalo proveriti koji status da se postavi
                    State = STATE.CHANGED
                }, typeof(UpdateDocumentCommand));
            }
            

            
            return Task.CompletedTask;
        }
    }
}
