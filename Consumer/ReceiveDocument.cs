using Models;
using MassTransit;
using System;
using System.IO;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace Consumer
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
            Test.dokumenta.Add(context.Message);
           // File.WriteAllBytes(@"C:\Users\hp\Desktop\Foo.txt", context.Message.File?.File);
            return Task.CompletedTask;
        }
    }
}
