using Models;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DocumentManagement.Messaging;
using DAL.Interfaces;

namespace DocumentManagement
{
    // pokrece neku konkretnu aktivnost
    public class ActivityService
    {
        public List<Document> Documents { get; set; }
        public Activity Activity { get; set; }
        public IDocumentRepository DocumentRepository { get; }

        private readonly DMBus _bus;

        public ActivityService(DMBus bus, IDocumentRepository documentRepository)
        {
            _bus = bus;
            DocumentRepository = documentRepository;
        }

        //dobija se aktivnost sa definisanim input i output dok
        public void SaveActivity(ActivityDTO dto)
        {
            Activity = Mapper.Map<Activity>(dto);
            _bus.Start(Activity);
        }

        //vraca objekat koji sadrzi:
        // 1. sva primljena dokumenta (received)
        // 2. dokumenta po zahtevu (requested)
        // 3. koja dokumenta treba da se kreiraju, odnosno posalju
        public OperationDTO LoadOperation()
        {
            List<DocumentDTO> received = GetReceivedDocuments();
            List<DocumentDTO> requested = RequestDocument();
            List<Document> output = Activity.OutputDocuments.ToList();
            List<DocumentDTO> outputDto = Mapper.Map<List<DocumentDTO>>(output);
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

            //posalji
            foreach(DocumentDTO dto in operation.OutputDocuments)
            {
                _bus.SendDocument(Mapper.Map<Document>(dto));
            }
        }

        public DocumentDTO GetDocumentById(int id)
        {
            return Mapper.Map<DocumentDTO>(Test.dokumenta.Single(x => x.Id == id));
        }

        //public void SendDocument(Document doc)
        //{
        //    _bus.SendDocument(doc);
        //}

        public List<DocumentDTO> RequestDocument()
        {
            List<DocumentDTO> result = new List<DocumentDTO>();

            List<Document> req = Activity.InputDocuments.Where(x => x.InputOperation == InputOperations.Request).ToList();
            foreach (Document d in req)
            {
                // posalji request i stavi rezultat u operation
                DocumentsResponse response = _bus.SendRequest(d);
                if (response != null)
                {
                    result.AddRange(Mapper.Map<List<DocumentDTO>>(response.Documents));
                    // dodaj u bazu
                    // čisto da postoji kopija i evidencija o pročitanim dokumentima
                    Test.dokumenta.AddRange(response.Documents);
                }
            }
            return result;
        }

        public List<DocumentDTO> GetReceivedDocuments()
        {
            List<DocumentDTO> result = Mapper.Map<List<DocumentDTO>>(Test.dokumenta);
            return result;
        }
    }
}
