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
using System.Linq;

namespace Test
{
    class Program
    {
        static void Main()
        {
            using DMSContext con = new DMSContext();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(typeof(DocumentManagement.Automapper.Profiles.ActivityProfile).Assembly);
            });
            
           
            ActivityService activityService = new ActivityService(new DMBus(new RabbitMqBus(new DocumentRepository(con))), new DocumentRepository(con));

            ActivityDTO newActivity = new ActivityDTO();
            List<DocumentDTO> InputDocuments = new List<DocumentDTO>();
            List<DocumentDTO> OutputDocuments = new List<DocumentDTO>();
            do
            {
                DocumentDTO d = new DocumentDTO();
                Console.WriteLine("Insert document");
                Console.Write("Type: ");
                d.Type = Console.ReadLine();
                Console.Write("Operation ");
                string operation = Console.ReadLine();

                switch (operation)
                {
                    case "Send":
                        d.OutputOperation = OutputOperationsDTO.Send;
                        OutputDocuments.Add(d);
                        break;
                    case "Create":
                        d.OutputOperation = OutputOperationsDTO.Create;
                        OutputDocuments.Add(d);
                        break;
                    case "Receive":
                        d.InputOperation = InputOperationsDTO.Receive;
                        InputDocuments.Add(d);
                        break;
                    case "Request":
                        d.InputOperation = InputOperationsDTO.Request;
                        InputDocuments.Add(d);
                        break;
                    default:
                        break;
                }

            } while (Console.ReadKey().Key != ConsoleKey.Escape);
            newActivity.InputDocuments = InputDocuments;
            newActivity.OutputDocuments = OutputDocuments;
            activityService.SaveActivity(newActivity);
            Console.WriteLine("Activity created!");
            Console.WriteLine("Create new operation?");
            Console.ReadKey();
            OperationDTO opDTO = activityService.LoadOperation();
            Console.WriteLine("Received");

            opDTO.Received?.ToList().ForEach(item => {
                Console.WriteLine(item.Type + " " + item.File.Name);
            });


            Console.WriteLine("Requested");
            opDTO.Requested?.ToList().ForEach(item => {
                Console.WriteLine(item.Type + " " + item.File.Name);
                Console.Write("Id: ");
                string id = Console.ReadLine();
                item.Id = Int32.Parse(id);
            });
            Console.WriteLine("Output");
            opDTO.OutputDocuments?.ToList().ForEach(item => {
                Console.WriteLine(item.Type + " " + item.File.Name);
                Console.Write("Id: ");
                string id = Console.ReadLine();
                item.Id = int.Parse(id);
            });

            activityService.SaveOperation(opDTO);


            #region jic
            /*
             * Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(typeof(DocumentManagement.Automapper.Profiles.ActivityProfile).Assembly);
            });
            ActivityService activityService = new ActivityService(new DMBus(new RabbitMqBus()), new DocumentRepository(new DMSContext()));
            activityService.SaveActivity(new ActivityDTO() { OutputDocuments = new List<DocumentDTO> { new DocumentDTO { OutputOperation = OutputOperationsDTO.Send, Type = "b" }, new DocumentDTO { OutputOperation = OutputOperationsDTO.Create, Type = "c" } } });
            Console.WriteLine("Activity created!");
            Document doc = new Document { OutputOperation = OutputOperations.Send, Type = "b", Id = 1 };
            doc.File.File = File.ReadAllBytes(Path.GetFullPath(@"file\mean.txt"));
            doc.File.Name = "mean.txt";
            doc.Id = 123456789;
            Console.WriteLine("Operation!");
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
             * 
            */
            #endregion



            #region
            /*
             * 
             * 
             *  //DMBus mb = new DMBus(new DocumentManagement.Messaging.RabbitMqBus());
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
            */
            #endregion
            Console.WriteLine("Kraj!");
            Console.ReadLine();
            
        }
    }
}
