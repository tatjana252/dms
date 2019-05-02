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
    public class DocumentRequestHandler : IConsumer<Document>
    {
        private readonly IDocumentRepository _db;

        public DocumentRequestHandler(IDocumentRepository db)
        {
            _db = db;
        }

        public DocumentRequestHandler()
        {

        }

        public Task Consume(ConsumeContext<Document> context)
        {
            //vratiti

            var docs = _db.SearchFor(doc => doc.Type == context.Message.Type);

            DocumentsResponse response = new DocumentsResponse
            {
                Documents = docs.ToList()
            };

            context.Respond(response);
            return Task.CompletedTask;
        }
    }
}
