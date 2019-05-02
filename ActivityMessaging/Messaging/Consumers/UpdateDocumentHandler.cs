using Models;
using MassTransit;
using System;
using System.IO;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace Consumers
{
    public class UpdateDocument : IConsumer<UpdateDocumentCommand>
    {

        private readonly IDocumentRepository _db;

        public UpdateDocument(IDocumentRepository db)
        {
            _db = db;
        }

        public UpdateDocument()
        {

        }

        public Task Consume(ConsumeContext<UpdateDocumentCommand> context)
        {
            //azuriranje dokumenta
            //statusa 
            //ili celog dokumenta
            if (context.Message.OnlyStatus)
            {
                _db.UpdateState(context.Message);
            }else
            _db.Update(context.Message);
            return Task.CompletedTask;
        }
    }
}
