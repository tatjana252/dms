using Models;
using MassTransit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Consumer
{
    public class DocumentRequestHandler : IConsumer<Document>
    {
        public Task Consume(ConsumeContext<Document> context)
        {
            //vratiti
            List<Document> allDocuments = new List<Document>() {
            new Document { Id = 121, Type = "c", File = new FileWrapper{
            Name ="request121.txt", File = File.ReadAllBytes(Path.GetFullPath(@"file\request121.txt")) } } ,
            new Document { Id = 122, Type = "c", File = new FileWrapper{
            Name="request122.txt", File = File.ReadAllBytes(Path.GetFullPath(@"file\request122.txt")) } },
            new Document { Id = 123, Type = "c", File = new FileWrapper{
            Name="request123.txt", File = File.ReadAllBytes(Path.GetFullPath(@"file\request123.txt")) }} };
            context.Respond(new DocumentsResponse { Documents = allDocuments });
            return Task.CompletedTask;
        }
    }
}
