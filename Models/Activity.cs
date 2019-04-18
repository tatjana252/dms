using System;
using System.Collections.Generic;
using System.Text;
using Automatonymous;


namespace Models
{
    public static class Test
    {
        public static List<Document> dokumenta = new List<Document>();
    }


    public class Activity
    {
        public string Status { get; set; }
        public Guid CorrelationId { get; set; }
        public IEnumerable<Document> InputDocuments { get; set; }
        public IEnumerable<Document> OutputDocuments { get; set; }
    }

    public enum InputOperations
    {
        Receive, Request,
    }

    public enum OutputOperations
    {
        Create, Send
    }
}
