using Models;
using MassTransit;
using System;
using System.IO;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace Consumers
{
    public class ReceiveDocument : IConsumer<Document>
    {

        private readonly IDocumentRepository _db;

        public ReceiveDocument(IDocumentRepository db)
        {
            _db = db;
        }

        public ReceiveDocument()
        {

        }

        public Task Consume(ConsumeContext<Document> context)
        {
            //sacuvati u bazi
            Document doc = context.Message;
            doc.InputOperation = InputOperations.Receive;
            _db.Insert(doc);
           // File.WriteAllBytes(@"C:\Users\hp\Desktop\Foo.txt", context.Message.File?.File);
            return Task.CompletedTask;
        }
    }
}
