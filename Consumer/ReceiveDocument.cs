using Models;
using MassTransit;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Consumer
{
    public class ReceiveDocument : IConsumer<Document>
    {
        
        public Task Consume(ConsumeContext<Document> context)
        {
            //sacuvati u bazi
            Test.dokumenta.Add(context.Message);
           // File.WriteAllBytes(@"C:\Users\hp\Desktop\Foo.txt", context.Message.File?.File);
            return Task.CompletedTask;
        }
    }
}
