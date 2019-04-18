using AutoMapper;
using DAL.Context;
using DAL.Repositories;
using DocumentManagement;
using DocumentManagement.Messaging;
using DTO.Models;
using Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Test
{
    class Program
    {
        static void Main()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(typeof(DocumentManagement.Automapper.Profiles.ActivityProfile).Assembly);
            });
            ActivityService activityService = new ActivityService(new DMBus(new RabbitMqBus()), new DocumentRepository(new DMSContext()));
            activityService.SaveActivity(new ActivityDTO() { OutputDocuments = new List<DocumentDTO> { new DocumentDTO { OutputOperation = OutputOperationsDTO.Send, Type = "b" }, new DocumentDTO { OutputOperation = OutputOperationsDTO.Create, Type = "c" } } });

            Document doc = new Document { OutputOperation = OutputOperations.Send, Type = "b", Id = 1 };
            doc.File.File = File.ReadAllBytes(Path.GetFullPath(@"file\mean.txt"));
            doc.File.Name = "mean.txt";
            doc.Id = 123456789;

            activityService.SaveOperation(new OperationDTO {
                OutputDocuments=new List<DocumentDTO>() {
                    new DocumentDTO{ OutputOperation = OutputOperationsDTO.Send, Type = "b", Id = 2, File = new FileWrapperDTO{ Name = "FileName2", File = doc.File.File } },
                    new DocumentDTO{ OutputOperation = OutputOperationsDTO.Send, Type = "b", Id = 3, File = new FileWrapperDTO{ Name = "FileName3", File = doc.File.File } },
                    new DocumentDTO{ OutputOperation = OutputOperationsDTO.Send, Type = "b", Id = 4, File = new FileWrapperDTO{ Name = "FileName4", File = doc.File.File } },
                    new DocumentDTO{ OutputOperation = OutputOperationsDTO.Send, Type = "e", Id = 33, File = new FileWrapperDTO{ Name = "FileName3", File = doc.File.File } },
                    new DocumentDTO{ OutputOperation = OutputOperationsDTO.Send, Type = "e", Id = 34, File = new FileWrapperDTO{ Name = "FileName4", File = doc.File.File } },
                    new DocumentDTO{ OutputOperation = OutputOperationsDTO.Create, Type = "c", Id = 5, File = new FileWrapperDTO{ Name = "FileName5", File = doc.File.File } },
                    new DocumentDTO{ OutputOperation = OutputOperationsDTO.Create, Type = "c", Id = 6, File = new FileWrapperDTO{ Name = "FileName6", File = doc.File.File } },
                }


            });



            //DMBus mb = new DMBus(new DocumentManagement.Messaging.RabbitMqBus());
            //mb.Start(new Models.Activity() { OutputDocuments = new List<Document> { new Document { OutputOperation = OutputOperations.Send, Type = "b" }, new Document { OutputOperation = OutputOperations.Create, Type = "c" } } });

            //Document doc = new Document { OutputOperation = OutputOperations.Send, Type = "b", Id = 1 };
            //doc.File.File = File.ReadAllBytes(Path.GetFullPath(@"file\mean.txt"));
            //doc.File.Name = "mean.txt";
            //mb.SendDocument(doc);
            //doc.Id = 123456789;
            //mb.SendDocument(new Document { OutputOperation = OutputOperations.Send, Type = "b", Id = 2 });
            //mb.SendDocument(new Document { OutputOperation = OutputOperations.Send, Type = "b", Id = 3 });

            //mb.SendDocument(new Document { OutputOperation = OutputOperations.Create, Type = "b", Id = 11 });
            //mb.SendDocument(new Document { OutputOperation = OutputOperations.Create, Type = "b", Id = 12 });
            //mb.SendDocument(new Document { OutputOperation = OutputOperations.Create, Type = "b", Id = 13 });
            Console.WriteLine("Kraj!");
            Console.ReadLine();
        }
    }
}
