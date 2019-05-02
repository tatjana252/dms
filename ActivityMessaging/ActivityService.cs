using AutoMapper;
using DAL.Interfaces;
using DocumentManagement.Messaging;
using DTO.Models;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocumentManagement
{
    // pokrece neku konkretnu aktivnost
    public class ActivityService
    {
        public List<Document> Documents { get; set; }
        public Activity Activity { get; set; }
        public IDocumentRepository DocumentRepository { get; }

        private readonly IBus _bus;

        public ActivityService(IBus bus, IDocumentRepository documentRepository)
        {
            _bus = bus;
            DocumentRepository = documentRepository;
        }

        //dobija se aktivnost sa definisanim input i output dok
        public void SaveActivity(ActivityDTO dto)
        {
            Activity = Mapper.Map<Activity>(dto);
            Activity.InputDocuments.Where(i => i.InputOperation == InputOperations.Receive)
                .ToList().ForEach(i => _bus.AddConsumer(i));
            Activity.OutputDocuments.Where(i => i.OutputOperation == OutputOperations.Create)
                .ToList().ForEach(i =>
                {
                    _bus.AddRequestConsumer(i);
                    _bus.AddUpdateConsumer(i);
                });
            _bus.StartBus();
        }

        //vraca objekat koji sadrzi:
        // 1. sva primljena dokumenta (received)
        // 2. dokumenta po zahtevu (requested)
        // 3. koja dokumenta treba da se kreiraju, odnosno posalju
        public OperationDTO LoadOperation()
        {
            List<DocumentDTO> received = GetReceivedDocuments();
            List<DocumentDTO> requested = RequestDocument();
            List<DocumentDTO> outputDto = Mapper.Map<List<DocumentDTO>>(Activity.OutputDocuments);
            OperationDTO operation = new OperationDTO()
            {
                Received = received,
                Requested = requested,
                OutputDocuments = outputDto
            };
            return operation;
        }

        // 1. arhivira procitana requested i received dokumenta
        // 2. čuva kreirana dokumenta
        // 3. šalje event za dokumenta koja ne mora da čuva (ne postoji osluškuvač)
        public void SaveOperation(OperationDTO operation)
        {
            //arhiviranje
            //foreach(DocumentDTO doc in operation.Requested)
            //{

            //}
            //čuvanje

            // SELECTED REQUESTED AND RECEIVED DOCS
            operation.Requested?.ToList().ForEach(item => DocumentRepository.Update(Mapper.Map<Document>(item)));
            operation.Received?.ToList().ForEach(item => DocumentRepository.Update(Mapper.Map<Document>(item)));
            operation.OutputDocuments?.ToList().ForEach(item => DocumentRepository.Insert(Mapper.Map<Document>(item)));

            operation.OutputDocuments?.Where(d => d.OutputOperation == OutputOperationsDTO.Create).ToList().ForEach(item =>
            {
                _bus.PublishDocumentCreated(Mapper.Map<Document>(item));
            });

            operation.OutputDocuments?.Where(d => d.OutputOperation == OutputOperationsDTO.Update).ToList().ForEach(item =>
            {
                _bus.PublishDocumentCreated(Mapper.Map<Document>(item));
            });

            operation.OutputDocuments?.Where(d => d.OutputOperation == OutputOperationsDTO.Send).ToList().ForEach(doc =>
            {
                Console.WriteLine("Sending document " + doc.Type);
                _bus.Publish(Mapper.Map<Document>(doc));
            });
        }

        //nepotrebno
        public DocumentDTO GetDocumentById(int id)
        {
            return Mapper.Map<DocumentDTO>(Test.dokumenta.Single(x => x.Id == id));
        }

        public List<DocumentDTO> RequestDocument()
        {
            List<DocumentDTO> result = new List<DocumentDTO>();
            List<DocumentInfo> req = Activity.InputDocuments.Where(x => x.InputOperation == InputOperations.Request).ToList();

            foreach (DocumentInfo d in req)
            {
                // posalji request i stavi rezultat u operation
                DocumentsResponse? response = _bus.Request(d);
                if (response != null)
                {
                    result.AddRange(Mapper.Map<List<DocumentDTO>>(response.Documents));
                }
            }
            return result;
        }

        public List<DocumentDTO> GetReceivedDocuments()
        {
            var docs = DocumentRepository.SearchFor(d => d.InputOperation == InputOperations.Receive).ToList();
            return Mapper.Map<List<DocumentDTO>>(docs);
        }

        public void AddBinder(Binder binder)
        {
            _bus.AddBinderConsumer(new DocumentInfo { Type = binder.InititatorType });
        }

    }

    public class Binder {
        public string InititatorType { get; set; } = "";
        public Dictionary<string, STATE> Outputs { get; set; } = new Dictionary<string, STATE>();
    }
}
