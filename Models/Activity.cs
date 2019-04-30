using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        public Guid Id { get; set; }
        [NotMapped]
        public IEnumerable<DocumentInfo> InputDocuments { get; set; }
        [NotMapped]
        public IEnumerable<DocumentInfo> OutputDocuments { get; set; }
        public DateTime DateCreated { get; set; }

    }

    public class DocumentInfo
    {
        public string Type { get; set; }
        public int Id { get; set; }
        public InputOperations? InputOperation { get; set; }
        public OutputOperations? OutputOperation { get; set; }

        public FileWrapper TemplateDocument { get; set; }
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
