using Models;
using System;

namespace Models
{
    public class Document
    {
        //Type of document
        public string Type { get; set; }
        //CorrelationID - from SagaStateMachineInstance
        public Guid CorrelationId { get; set; }
        public int? Id { get; set; }
        public FileWrapper File { get; set; } = new FileWrapper();
        public InputOperations? InputOperation { get; set; }
        public OutputOperations? OutputOperation { get; set; }
    }
}
