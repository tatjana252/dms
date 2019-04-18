using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UpravljanjeDokumentacijomWebApp.Models
{
    public class DocumentViewModel
    {
        //Type of document
        public string Type { get; set; }
        //CorrelationID - from SagaStateMachineInstance
        public Guid CorrelationId { get; set; }
        public int? Id { get; set; }
        public FormFileWrapper Document { get; set; } = null;

        public bool Selected { get; set; }

        public string IdName { get; set; }
        public InputOperationsViewModel? InputOperationViewModel { get; set; }
        public OutputOperationsViewModel? OutputOperationViewModel { get; set; }
    }

    public class FormFileWrapper
    {
        public IFormFile File { get; set; } = null;
        public string Name { get; set; }
    }
}
